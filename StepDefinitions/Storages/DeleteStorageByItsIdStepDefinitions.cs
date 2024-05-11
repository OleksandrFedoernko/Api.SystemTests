using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;

namespace VismaIdella.Vips.TaskManagement.Api.SystemTests.StepDefinitions.Storages;

[Binding]
public class DeleteStorageByItsIdStepDefinitions
{
    private readonly StorageRequests _storageRequests = new();
    private readonly StorageRequestModel _storageRequestModel = new();
    private RestResponse _response = new();
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _storageId = string.Empty;
    private string _nonExistingStorageId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _userId = string.Empty;
    private readonly ScenarioContext _context;
    public DeleteStorageByItsIdStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"storage id which will be used for delete is ""([^""]*)""")]
    public void GivenStorageIdWhichWillBeUsedForDeleteIs(string id)
    {
        _storageId = id;
    }

    [Given(@"requesting user id for creating storage before delete is ""([^""]*)""")]
    public void GivenRequestingUserIdForCreatingStorageBeforeDeleteIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type for creating storage before delete is ""([^""]*)""")]
    public void GivenRequestingUserTypeForCreatingStorageBeforeDeleteIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"user id from header parameter for creating storage before delete is ""([^""]*)""")]
    public void GivenUserIdFromHeaderParameterForCreatingStorageBeforeDeleteIs(string headerUserId)
    {
        _userId = headerUserId;
    }

    [Given(@"name for storage is ""([^""]*)""")]
    public void GivenNameForStorageIs(string name)
    {
        _storageRequestModel.Name = name;
    }

    [Given(@"icon for storage ""([^""]*)""")]
    public void GivenIconForStorage(string icon)
    {
        _storageRequestModel.Icon = icon;
    }


    [When(@"post storage before delete request is sent")]
    public async Task WhenPostStorageBeforeDeleteRequestIsSent()
    {
        _response = await _storageRequests.PostStorageAsync(_storageRequestModel, _storageId, _requestingUserId, _requestingUserType, _userId);
    }

    [Given(@"id which will be used for deleting storage is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForDeletingStorageIs(string holder)
    {
        _ = _storageId;
    }

    [Given(@"nonexisting id which will be used for deleting storageis ""([^""]*)""")]
    public void GivenNonExistingIdWhichWillBeUsedForDeletingStorageIs(string storageId)
    {
        _nonExistingStorageId = storageId;
    }

    [Given(@"bad requesting user id for deleting storage is ([^""]*)")]
    public void GivenBadRequestingUserIdForDeletingStorageIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type for deleting storage is ([^""]*)")]
    public void GivenRequestingUserTypeForDeletingStorageIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"bad user id from header parameter is ([^""]*)")]
    public void GivenBadUserIdFromHeaderParameterIs(string userId)
    {
        _userId = userId;
    }

    [When(@"delete storage request is sent")]
    public async Task WhenDeleteStorageRequestIsSentAsync()
    {
        _response = await _storageRequests.DeleteStorageByIdAsync(_storageId, _requestingUserId, _requestingUserType, _userId);
        _context.Add("code", _response.StatusCode);
        if (_response.StatusCode != HttpStatusCode.NoContent)
        {
            var content = _response.Content!;
            var errorResponseBody = JObject.Parse(content);
            var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
            _context.Add("error_code", errorCodeFromResponse);
        }
    }

    [When(@"delete storage request with nonexisting id is sent")]
    public async Task WhenDeleteStorageRequestWithNonExistingIdIsSent()
    {
        _response = await _storageRequests.DeleteStorageByIdAsync(_nonExistingStorageId, _requestingUserId, _requestingUserType, _userId);
        _context.Add("code", _response.StatusCode);

        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from delete storage is empty")]
    public void ThenResponseBodyFromDeleteStorageIsEmpty()
    {
        var content = _response.Content;
        content.Should().BeNullOrEmpty();
    }

    [Then(@"forbidden message from delete storage request should have text ""([^""]*)""")]
    public void ThenForbiddenMessageFromDeleteStorageRequestShouldHaveText(string message)
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

    [Then(@"bad request message from delete storage request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromDeleteStorageRequestShouldHaveTextInTheField(string message, string field)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
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

    [Then(@"not found message from delete storage request should have text ""([^""]*)""")]
    public void ThenNotFoundMessageFromDeleteStorageRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var statusCodeFromResponse = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var messageFromResponse = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var expectedStatusCode = (int)HttpStatusCode.NotFound;
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);

        statusCodeFromResponse.Should().Be(expectedStatusCode.ToString());
        statusCodeFromResponse.Should().NotBeNullOrEmpty();
        messageFromResponse.Should().Be(message);
        messageFromResponse.Should().NotBeNullOrEmpty();
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"I delete storage which was created")]
    public async Task ThenIDeleteStoragesWhichWasCreated()
    {
        await _storageRequests.DeleteStorageByIdAsync(_storageId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue);
    }
}

