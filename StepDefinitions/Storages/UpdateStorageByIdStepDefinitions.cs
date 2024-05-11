using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;

namespace Api.SystemTests.StepDefinitions.Storages;

[Binding]
public class UpdateStorageByIdStepDefinitions
{
    private readonly StorageRequests _storageRequests = new();
    private readonly StorageRequestModel _storageRequestModel = new();
    private RestResponse _response = new();
    private readonly JSchema _storageResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/StorageResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private readonly Random _random = new Random();
    private string _storageId = string.Empty;
    private string _newStorageId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private readonly ScenarioContext _context;
    public UpdateStorageByIdStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"id which will be created for upd is ""([^""]*)""")]
    public void GivenIdWhichWillBeCreatedForUpdIs(string id)
    {
        var endId = _random.Next(1, 10001);
        _storageId = id+endId;
    }

    [Given(@"name which will be created for upd is ""([^""]*)""")]
    public void GivenNameWhichWillBeCreatedForUpdIs(string name)
    {
        _storageRequestModel.Name = name;
    }

    [Given(@"requesting user type which will be used for upd ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForUpd(string userType)
    {
        _requestingUserType = userType;
    }

    [When(@"I send request")]
    public async Task WhenISendRequest()
    {
        _response = await _storageRequests.PostStorageAsync(_storageRequestModel, _storageId, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [Then(@"I save storage id")]
    public void ThenISaveStorageId()
    {
        var responseBody = JObject.Parse(_response.Content!);
        var storageId = responseBody["storage_id"]?.ToString();
        _newStorageId = storageId!;
    }

    [Given(@"id which will be used for updating storage is ([^""]*)")]
    public void GivenIdWhichWillBeUsedFirUpdatingStorageIs(string storageId)
    {
        _storageId = storageId;
    }

    [Given(@"storage name which will be used as new value is ([^""]*)")]
    public void GivenStorageNameWhichWillBeUsedAsNewValueIs(string name)
    {
        _storageRequestModel.Name = name;
    }

    [Given(@"storage icon which will be used as new values is ([^""]*)")]
    public void GivenStorageIconWhichWillBeUsedAsNewValuesIs(string icon)
    {
        _storageRequestModel.Icon = icon;
    }

    [Given(@"requesting user id for updating storage is ""([^""]*)""")]
    public void GivenRequestingUserIdForUpdatingStorageIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"bad requesting user id for updating storage is ([^""]*)")]
    public void GivenBadRequestingUserIdForUpdatingStorageIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type for updating storage is ([^""]*)")]
    public void GivenRequestingUserTypeForUpdatingStorageIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"user id from header parameter which will be used for updating storage is ""([^""]*)""")]
    public void GivenUserIdFromHeaderParameterWhichWillBeUsedForUpdatingStorageIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"bad user id from header parameter which will be used for updating storage is ([^""]*)")]
    public void GivenBadUserIdFromHeaderParameterWhichWillBeUsedForUpdatingStorageIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [When(@"update storage request is sent")]
    public async Task WhenUpdateStorageRequestIsSent()
    {
        _response = await _storageRequests.UpdateStorageByIdAsync(_storageRequestModel, _newStorageId, _requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from update storage equals ([^""]*), ([^""]*)")]
    public void ThenResponseBodyFromUpdateStorageEquals( string responseName, string responseIcon)
    {
        var content = _response.Content!;
        var storageResponse = JObject.Parse(content);
        var schemaValidation = storageResponse.IsValid(_storageResponseSchema);
        var actualStorageId = storageResponse[ResponseConstants.StorageResponse.StorageId]?.ToString();
        var actualStorageName = storageResponse[ResponseConstants.StorageResponse.Name]?.ToString();
        var actualStorageIcon = storageResponse[ResponseConstants.StorageResponse.Icon]?.ToString();

        actualStorageId.Should().NotBeNullOrWhiteSpace();        

        if (_storageRequestModel.Name == null)
        {
            actualStorageName.Should().BeNullOrWhiteSpace();
        }
        else
        {
            actualStorageName.Should().Be(responseName);
        }

        if (_storageRequestModel.Icon == null)
        {
            actualStorageIcon.Should().BeNullOrWhiteSpace();
        }
        else
        {
            actualStorageIcon.Should().Be(responseIcon);
        }

        if (_storageRequestModel.Name != null && _storageRequestModel.Icon != null)
        {
            schemaValidation.Should().BeTrue();
        }
    }

    [Then(@"forbidden request message from update storage request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromUpdateStorageRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var expectedStatusCode = (int)HttpStatusCode.Forbidden;
        var actualMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var actualCodeFromResponseBody = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);
        actualMessage.Should().NotBeNullOrWhiteSpace();
        actualMessage.Should().Be(message);
        actualCodeFromResponseBody.Should().NotBeNullOrWhiteSpace();
        actualCodeFromResponseBody.Should().Be(expectedStatusCode.ToString());
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"bad request message from update storage request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromUpdateStorageRequestShouldHaveTextInTheField(string message, string field)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var expectedStatusCode = (int)HttpStatusCode.BadRequest;
        var errorField = errorResponse[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Field]?.ToString();
        var errorMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var errorStatusCode = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);
        errorField.Should().Be(field);
        errorField.Should().NotBeNullOrEmpty();
        errorMessage.Should().Be(errorMessage);
        errorStatusCode.Should().Be(expectedStatusCode.ToString());
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"not found request message from update storage request should have text ""([^""]*)""")]
    public void ThenNotFoundRequestMessageFromUpdateStorageRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var expectedStatusCode = (int)HttpStatusCode.NotFound;
        var actualMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var actualCodeFromResponseBody = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);
        actualMessage.Should().NotBeNullOrWhiteSpace();
        actualMessage.Should().Be(message);
        actualCodeFromResponseBody.Should().NotBeNullOrWhiteSpace();
        actualCodeFromResponseBody.Should().Be(expectedStatusCode.ToString());
        errorSchemaValidation.Should().BeTrue();
    }
}
