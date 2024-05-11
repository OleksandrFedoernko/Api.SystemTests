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
public class GetUserByIdStepDefinitions
{
    private readonly UserRequests _userRequests = new();
    private readonly UserRequestModel _userRequestModel = new();
    private RestResponse _response = new();
    private readonly JSchema _userResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/UserResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private readonly Random _random = new();
    private string _pathUserId = string.Empty;
    private string _userId = string.Empty;
    private string _newUserId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private readonly ScenarioContext _context;
    public GetUserByIdStepDefinitions(ScenarioContext scenario)
    {
        _context = scenario;
    }

    [Given(@"requesting user id for creating user before getting it is ""([^""]*)""")]
    public void GivenRequestingUserIdForCreatingUserBeforeGettingItIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type fo creating user before getting it is ""([^""]*)""")]
    public void GivenRequestingUserTypeFoCreatingUserBeforeGettingItIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"user id from header parameter which will be used creating user before getting it is ""([^""]*)""")]
    public void GivenUserIdFromHeaderParameterWhichWillBeUsedCreatingUserBeforeGettingItIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"id which will be used for creating user before getting it is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForCreatingUserBeforeGettingItIs(string bodyUserId)
    {
        var endId = _random.Next(1, 100001);
        _userId = bodyUserId + endId.ToString();
    }

    [Given(@"name which will be used for creating user before getting it is ""([^""]*)""")]
    public void GivenNameWhichWillBeUsedForCreatingUserBeforeGettingItIs(string name)
    {
        _userRequestModel.Name = name;
    }

    [Given(@"description which will be used for creating user before getting it is ""([^""]*)""")]
    public void GivenDescriptionWhichWillBeUsedForCreatingUserBeforeGettingItIs(string description)
    {
        _userRequestModel.Description = description;
    }

    [When(@"create user for future get request is sent")]
    public async Task WhenCreateUserForFutureGetRequestIsSent()
    {
        _response = await _userRequests.PostUserAsync(_userRequestModel, _userId, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [Then(@"i save user id for getting it")]
    public void ThenISaveUserIdForGettingIt()
    {
        var responseBody = JObject.Parse(_response.Content!);
        var createdUserId = responseBody[ResponseConstants.UserResponse.UserId]?.ToString();
        _newUserId = createdUserId!;
    }

    [Given(@"id which will be used in getting user is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedInGettingUserIs(string pathUserId)
    {
        _ = _newUserId;
    }

    [Given(@"nonexisting id which will be used in getting user is ""([^""]*)""")]
    public void GivenNonExistingIdWhichWillBeUsedInGettingUserIs(string badUserId)
    {
        _pathUserId = badUserId;
    }

    [Given(@"bad requesting user id for getting user by id is ([^""]*)")]
    public void GivenBadRequestingUserIdForGettingUserByIdIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type for getting user is ([^""]*)")]
    public void GivenRequestingUserTypeForGettingUserIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"bad user id from header parameter which will be used for getting user is ([^""]*)")]
    public void GivenBadUserIdFromHeaderParameterWhichWillBeUsedForGettingUserIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [When(@"get user by id request is sent")]
    public async Task WhenGetUserByIdRequestIsSent()
    {
        _response = await _userRequests.GetUserByIdAsync(_requestingUserId, _requestingUserType, _headerUserId, _newUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get user with nonexisting id request is sent")]
    public async Task WhenGetUserWithNonExistingIdRequestIsSent()
    {
        _response = await _userRequests.GetUserByIdAsync(_requestingUserId, _requestingUserType, _headerUserId, _pathUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from get user by id equals ([^""]*), ([^""]*), ([^""]*)")]
    public void ThenResponseBodyFromGetUserByIdEquals(string userId, string name, string description)
    {
        var content = _response.Content!;
        var users = JObject.Parse(content);
        var userIdResponse = users[ResponseConstants.UserResponse.UserId]?.ToString();
        var userNameResponse = users[ResponseConstants.UserResponse.Name]?.ToString();
        var userDescriptionResponse = users[ResponseConstants.UserResponse.Description]?.ToString();
        var schemaValidation = users.IsValid(_userResponseSchema);
        userIdResponse.Should().Contain(userId);
        userNameResponse.Should().Be(name);
        userDescriptionResponse.Should().Be(description);
        userIdResponse.Should().NotBeNull();
        userNameResponse.Should().NotBeNull();
        userDescriptionResponse.Should().NotBeNull();
        schemaValidation.Should().BeTrue();
    }

    [Then(@"forbidden request message from get user by id request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromGetUserByIdRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var expectedStatusCode = (int)HttpStatusCode.Forbidden;
        var responseStatusCode = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var responseMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);
        errorResponse.Should().NotBeNullOrEmpty();
        responseStatusCode.Should().Be(expectedStatusCode.ToString());
        responseStatusCode.Should().NotBeNullOrEmpty();
        responseMessage.Should().Be(message);
        responseMessage.Should().NotBeNullOrEmpty();
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"message from bad get user by id request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenMessageFromBadGetUserByIdRequestShouldHaveTextInTheField(string message, string field)
    {
        var content = _response.Content!;
        var expectedErrorCode = (int)HttpStatusCode.BadRequest;
        var errorResponse = JObject.Parse(content);
        var statusCode = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorMessage = errorResponse[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Message]?.ToString();
        var errorField = errorResponse[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Field]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);
        statusCode.Should().NotBeNullOrWhiteSpace();
        statusCode.Should().Be(expectedErrorCode.ToString());
        errorMessage.Should().NotBeNullOrWhiteSpace();
        errorMessage.Should().Be(message);
        errorField.Should().NotBeNullOrWhiteSpace();
        errorField.Should().Be(field);
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"message from not found get user by id request should have text ""([^""]*)""")]
    public void ThenMessageFromNotFoundGetUserByIdRequestShouldHaveTextInTheField(string message)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var expectedStatusCode = (int)HttpStatusCode.NotFound;
        var responseStatusCode = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var responseMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);
        errorResponse.Should().NotBeNullOrEmpty();
        responseStatusCode.Should().Be(expectedStatusCode.ToString());
        responseStatusCode.Should().NotBeNullOrEmpty();
        responseMessage.Should().Be(message);
        responseMessage.Should().NotBeNullOrEmpty();
        errorSchemaValidation.Should().BeTrue();
    }
}
