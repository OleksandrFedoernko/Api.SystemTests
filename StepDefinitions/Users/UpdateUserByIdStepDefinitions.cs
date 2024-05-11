using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;

namespace Api.SystemTests.StepDefinitions.Users;

[Binding]
public class UpdateUserByIdStepDefinitions
{
    private readonly UserRequests _userRequests = new();
    private readonly UserRequestModel _userRequestModel = new();
    private RestResponse _response = new();
    private readonly Random _random = new Random();
    private readonly JSchema _userResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/UserResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _pathUserId = string.Empty;
    private string _newUserId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private readonly ScenarioContext _context;
    public UpdateUserByIdStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"requesting_user_type which will be created  for upd ""([^""]*)""")]
    public void GivenRequesting_User_TypeWhichWillBeCreatedForUpd(string p0)
    {
        _requestingUserType = p0;
    }

    [Given(@"user id which will be created for upd is ""([^""]*)""")]
    public void GivenUserIdWhichWillBeCreatedForUpdIs(string userid)
    {
        var endId = _random.Next(1, 10001);
        _pathUserId = userid+endId;
    }

    [Given(@"user name which will be created for upd is ""([^""]*)""")]
    public void GivenUserNameWhichWillBeCreatedForUpdIs(string name)
    {
        _userRequestModel.Name = name;
    }

    [When(@"I send POST user request")]
    public async Task WhenISendPOSTUserRequest()
    {
        _response = await _userRequests.PostUserAsync(_userRequestModel, _pathUserId, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [Then(@"I save user id")]
    public void ThenISaveUserId()
    {
        var responseBody = JObject.Parse(_response.Content!);
        var userIdResponse = responseBody["user_id"]?.ToString();
        _newUserId = userIdResponse!;
    }


    [Given(@"user id which will be used for updating user is ""([^""]*)""")]
    public void GivenUserIdWhichWillBeUsedForUpdatingUserIs(string pathUserId)
    {
        _pathUserId = pathUserId;
    }

    [Given(@"user name which will be used for updating user is ([^""]*)")]
    public void GivenUserNameWhichWillBeUsedForUpdatingUserIs(string name)
    {
        _userRequestModel.Name = name;
    }

    [Given(@"user icon which will be used for updating user is ([^""]*)")]
    public void GivenUserIconWhichWillBeUsedForUpdatingUserIs(string desc)
    {
        _userRequestModel.Description = desc;
    }

    [Given(@"requesting user id for updating user is ""([^""]*)""")]
    public void GivenRequestingUserIdForUpdatingUserIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"bad requesting user id for updating user is ([^""]*)")]
    public void GivenBadRequestingUserIdForUpdatingUserIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type for updating user is ([^""]*)")]
    public void GivenRequestingUserTypeForUpdatingUserIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for updating user is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForUpdatingUserIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"bad header user id which will be used for updating user is ([^""]*)")]
    public void GivenBadHeaderUserIdWhichWillBeUsedForUpdatingUserIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [When(@"update user request is sent")]
    public async Task WhenUpdateUserRequestIsSent()
    {
        _response = await _userRequests.UpdateUserAsync(_userRequestModel, _newUserId, _requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from update user should be ([^""]*), ([^""]*)")]
    public void ThenResponseBodyFromUpdateUserShouldBe( string name, string desc)
    {
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        var userIdResponse = responseBody[ResponseConstants.UserResponse.UserId]?.ToString();
        var nameResponse = responseBody[ResponseConstants.UserResponse.Name]?.ToString();
        var descResponse = responseBody[ResponseConstants.UserResponse.Description]?.ToString();
        var responseValidation = responseBody.IsValid(_userResponseSchema);
        responseBody.Should().NotBeEmpty();
        userIdResponse.Should().Be(userIdResponse);
        userIdResponse.Should().NotBeNullOrEmpty();
        nameResponse.Should().Be(name);
        nameResponse.Should().NotBeNullOrEmpty();

        if (string.IsNullOrWhiteSpace(_userRequestModel.Description))
        {
            descResponse.Should().BeNullOrWhiteSpace();
        }
        else
        {
            descResponse.Should().Be(desc);
            descResponse.Should().NotBeNullOrEmpty();
        }
        responseValidation.Should().BeTrue();
    }

    [Then(@"forbidden request message from update user request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromUpdateUserRequestShouldHaveText(string message)
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

    [Then(@"bad request message from update user request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromUpdateUserRequestShouldHaveTextInTheField(string message, string field)
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

    [Then(@"not found request message from update user request should have text ""([^""]*)""")]
    public void ThenNotFoundRequestMessageFromUpdateUserRequestShouldHaveText(string message)
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
