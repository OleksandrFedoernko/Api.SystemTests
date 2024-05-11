using System.Net;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;
using Api.SystemTests.Constants;

namespace Api.SystemTests.StepDefinitions.Users;

[Binding]
public class GetAllUsersStepDefinitions
{
    private readonly UserRequests _userRequests = new();
    private RestResponse _response = new();
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private readonly JSchema _getAllUsersResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/GetAllUsersResponseSchema.json"));
    private readonly ScenarioContext _context;
    public GetAllUsersStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [When(@"get all users request is sent")]
    public async Task WhenGetAllUsersRequestIsSent()
    {
        var headerUserId = _context.Get<string>("user_id");
        var requestingUserType = _context.Get<string>("requesting_user_type");
        var requestingUserId = _context.Get<string>("requesting_user_id");
        _response = await _userRequests.GetAllUsersAsync(requestingUserId, requestingUserType, headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"amount of users should be equal to the total value")]
    public void ThenAmountOfUsersShouldBeEqualToTheTotalValue()
    {
        var content = _response.Content;
        var users = JObject.Parse(content!);
        var usersListResponse = (JArray)users[ResponseConstants.PaginationResponse.Items]!;
        var total = (int)users[ResponseConstants.PaginationResponse.Pagination]![ResponseConstants.PaginationResponse.Total]!;
        var index = Enumerable.Range(0, total);
        var responseSchemaValidation = users.IsValid(_getAllUsersResponseSchema);
        foreach (var num in index)
        {
            var userIdResponse = users[ResponseConstants.PaginationResponse.Items]?[num]?[ResponseConstants.UserResponse.UserId]?.ToString();
            var nameResponse = users[ResponseConstants.PaginationResponse.Items]?[num]?[ResponseConstants.UserResponse.Name]?.ToString();
            usersListResponse.Should().NotBeNullOrEmpty();
            usersListResponse.Should().HaveCount(total);
            userIdResponse.Should().NotBeNullOrEmpty();
            nameResponse.Should().NotBeNullOrEmpty();
        }
        responseSchemaValidation.Should().BeTrue();
    }

    [Then(@"message from get all users should be ""([^""]*)""")]
    public void ThenMessageFromGetAllUsersShouldBe(string message)
    {
        var content = _response.Content!;
        var expectedStatusCodeResult = (int)HttpStatusCode.Forbidden;
        var errorResponseBody = JObject.Parse(content);
        var jsonMessage = errorResponseBody[ResponseConstants.ErrorResponse.Message]?.ToString();
        var jsonStatusCode = errorResponseBody[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorSchemaValidation = errorResponseBody.IsValid(_errorResponseSchema);

        jsonMessage.Should().Contain(message);
        jsonMessage.Should().StartWith(message);
        jsonMessage.Should().NotBeNullOrEmpty();
        jsonStatusCode.Should().Be(expectedStatusCodeResult.ToString());
        jsonStatusCode.Should().NotBe(null);
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"message from get all users should be ([^""]*) in the field ([^""]*)")]
    public void ThenMessageFromGetAllUsersShouldBeInTheField(string message, string field)
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

