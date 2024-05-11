using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using Api.SystemTests.Requests;

namespace Api.SystemTests.StepDefinitions.Users;

[Binding]
public class CreateUserStepDefinitions
{
    private readonly UserRequests _userRequests = new();
    private readonly UserRequestModel _user = new();
    private RestResponse _response = new();
    private readonly JSchema _userResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/UserResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private readonly Random _random = new();
    private string _userId = string.Empty;
    private readonly ScenarioContext _context;


    public CreateUserStepDefinitions( ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"id which will be used for creating user is ([^""]*)")]
    public void GivenIdWhichWillBeUsedForCreatingUserIs(string userId)
    {
        var endId = _random.Next(1, 10001);
        _userId = userId + endId;
    }

    [Given(@"id which will be used for creating bad user is ([^""]*)")]
    public void GivenIdWhichWillBeUsedForCreatingBadUserIs(string userId)
    {
        _userId = userId;
    }
    [Given(@"id which will be used for creating user with same id is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForCreatingUserWithSameIdIs(string userId)
    {
        _userId = userId;
    }


    [Given(@"name which will be used for creating user is ([^""]*)")]
    public void GivenNameWhichWillBeUsedForCreatingUserIs(string userName)
    {
        _user.Name = userName;
    }

    [Given(@"description which will be used for creating user is ([^""]*)")]
    public void GivenDescriptionWhichWillBeUsedForCreatingUserIs(string description)
    {
        _user.Description = description;
    }

    [When(@"post user request is sent")]
    public async Task WhenPostUserRequestIsSentAsync()
    {
        var headerUserId = _context.Get<string>("user_id");
        var requestingUserType = _context.Get<string>("requesting_user_type");
        var requestingUserId = _context.Get<string>("requesting_user_id");
        _response = await _userRequests.PostUserAsync(_user, _userId, requestingUserId, requestingUserType, headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from post user equals ([^""]*), ([^""]*), ([^""]*)")]
    public void ThenResponseBodyFromPostUserEquals(string expectedUserId, string expectedUserName, string expectedUserDesc)
    {
        var content = _response.Content!;
        var userResponse = JObject.Parse(content);
        var userName = userResponse[ResponseConstants.UserResponse.Name]?.ToString();
        var userDescription = userResponse[ResponseConstants.UserResponse.Description]?.ToString();
        var userId = userResponse[ResponseConstants.UserResponse.UserId]?.ToString();
        var responseSchemaValidation = userResponse.IsValid(_userResponseSchema);
        using (new AssertionScope())
        {         
            userName.Should().NotBeNull();
            userDescription.Should().NotBeNull();
            userId.Should().NotBeNull();
            userName.Should().Be(expectedUserName);
            userDescription.Should().Be(expectedUserDesc);
            userId.Should().Contain(expectedUserId);
            if (userDescription == null)
            {
                userDescription.Should().BeNull();
            }
            responseSchemaValidation.Should().BeTrue();
        }
    }  

    [Then(@"forbidden request message from create user request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromCreateUserRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var expectedStatusCodeResult = (int)HttpStatusCode.Forbidden;
        var errorResponse = JObject.Parse(content);
        var errorMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var statusCode = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);

        errorMessage.Should().Contain(message);
        errorMessage.Should().StartWith(message);
        errorMessage.Should().NotBeNullOrEmpty();
        statusCode.Should().Be(expectedStatusCodeResult.ToString());
        statusCode.Should().NotBe(null);
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"bad request message from create user request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromCreateUserRequestShouldHaveTextInTheField(string message, string field)
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

}
