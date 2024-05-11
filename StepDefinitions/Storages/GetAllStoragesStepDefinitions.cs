using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;

namespace VismaIdella.Vips.TaskManagement.Api.SystemTests.StepDefinitions.Storages;

[Binding]
public class GetAllStoragesStepDefinitions
{
    private readonly StorageRequests _storageRequests = new();
    private RestResponse _response = new();
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private readonly JSchema _getAllStoragesResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/GetAllStoragesResponseSchema.json"));
    private string _user = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _requestingUserId = string.Empty;
    private int _limit;
    private string _name = string.Empty;
    private readonly ScenarioContext _context;
    public GetAllStoragesStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"requesting user id which will be used for getting all storages with other user types ([^""]*)")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingAllStoragesWithOtherUserTypes(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user id which will be used for getting all storages is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingAllStoragesIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for getting all storages is ([^""]*)")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingAllStoragesIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for getting all storages is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForGettingAllStoragesIs(string user)
    {
        _user = user;
    }

    [Given(@"wrong header user id which will be used for getting all storages is ([^""]*)")]
    public void GivenWrongHeaderUserIdWhichWillBeUsedForGettingAllStoragesIs(string headerUserId)
    {
        _user = headerUserId;
    }

    [Given(@"limit which will be used for getting all storages is ([^""]*)")]
    public void GivenLimitWhichWillBeUsedForGettingAllStoragesIs(int limit)
    {
        _limit = limit;
    }

    [Given(@"name which will be used for getting all storages is ([^""]*)")]
    public void GivenNameWhichWillBeUsedForGettingAllStoragesIs(string StorageName)
    {
        _name = StorageName;
    }

    [When(@"get all storages request is sent")]
    public async Task WhenGetAllStoragesRequestIsSent()
    {
        _response = await _storageRequests.GetAllStoragesAsync(_limit, _name, _requestingUserId, _requestingUserType, _user);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from get all storages from backoffice and client system user types should be equal to  ([^""]*) ([^""]*)")]
    public void ThenResponseBodyFromGetAllTasksFromBackofficeAndClientSystemUserTypesShouldBeEqualTo(string StorageName, string StorageIcon)
    {
        var content = _response.Content;
        var Storages = JObject.Parse(content!);
        var StorageListResponse = (JArray)Storages[ResponseConstants.PaginationResponse.Items]!;
        var limit = (int)Storages[ResponseConstants.PaginationResponse.Pagination]![ResponseConstants.PaginationResponse.Limit]!;
        var index = Enumerable.Range(0, limit);
        var responseSchemaValidation = Storages.IsValid(_getAllStoragesResponseSchema);
        if (_limit == limit && string.IsNullOrWhiteSpace(_name))
        {
            foreach (var num in index)
            {
                var StorageIdResponse = Storages[ResponseConstants.PaginationResponse.Items]?[num]?[ResponseConstants.StorageResponse.StorageId]?.ToString();
                StorageIdResponse.Should().NotBeNullOrWhiteSpace();
                StorageListResponse.Should().NotBeEmpty();

            }
        }

        responseSchemaValidation.Should().BeTrue();
    }

    [Then(@"response body from get all storages from employer and participant user types should be equal to ([^""]*) ([^""]*)")]
    public void ThenResponseBodyFromGetAllTasksFromEmployerAndParticipantUserTypesShouldBeEqualTo(string value, string icon)
    {
        var content = _response.Content;
        var storages = JObject.Parse(content!);
        var storagesListResponse = (JArray)storages[ResponseConstants.PaginationResponse.Items]!;
        var total = (int)storages[ResponseConstants.PaginationResponse.Pagination]![ResponseConstants.PaginationResponse.Total]!;
        var index = Enumerable.Range(0, total);

        if (!string.IsNullOrWhiteSpace(_name))
        {
            foreach (var num in index)
            {
                var nameResponse = storages[ResponseConstants.PaginationResponse.Items]?[num]?[ResponseConstants.StorageResponse.Name]?.ToString();
                var iconResponse = storages[ResponseConstants.PaginationResponse.Items]?[num]?[ResponseConstants.StorageResponse.Icon]?.ToString();
                nameResponse.Should().Be(value);
                iconResponse.Should().Be(icon);
                storagesListResponse.Should().NotBeEmpty();
            }
        }

        if (_limit == 0 && string.IsNullOrWhiteSpace(_name))
        {
            storagesListResponse.Should().HaveCount(total);
        }
        storagesListResponse.Should().HaveCount(total);
    }

    [Then(@"response body from get all storages from with incorrect user IDs should have empty list of storages")]
    public void ThenResponseBodyFromGetAllTasksFromWithIncorrectUserIDsShouldHaveEmptyListOfStorages()
    {
        var content = _response.Content;
        var Storages = JObject.Parse(content!);
        var StorageListResponse = (JArray)Storages[ResponseConstants.PaginationResponse.Items]!;
        StorageListResponse.Should().BeEmpty();
    }

    [Then(@"bad request message from get all storages request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromGetAllStoragesRequestShouldHaveTextInTheField(string message, string field)
    {
        var content = _response.Content;
        var errorResponse = JObject.Parse(content!);
        var errorField = errorResponse[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Field]?.ToString();
        var errorMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var errorStatusCode = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var expectedStatusCode = (int)HttpStatusCode.BadRequest;
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);

        errorField.Should().Be(field);
        errorField.Should().NotBeNullOrEmpty();
        errorMessage.Should().Be(errorMessage);
        errorStatusCode.Should().Be(expectedStatusCode.ToString());
        errorSchemaValidation.Should().BeTrue();
    }
}
