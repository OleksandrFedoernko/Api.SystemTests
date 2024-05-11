using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;

namespace Api.SystemTests.StepDefinitions.Tasks;

[Binding]
public class DeleteTaskByItsIdStepDefinitions
{
    private readonly TaskRequests _taskRequests = new();
    private readonly TaskRequestModel _taskRequestModel = new();
    private RestResponse _response = new();
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _taskId = string.Empty;
    private string _savedTaskId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private string _mode = string.Empty;
    private string _reason = string.Empty;
    private readonly ScenarioContext _context;
    public DeleteTaskByItsIdStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"requesting user id which will be used for creating task before deleting it is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForCreatingTaskBeforeDeletingItIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for creating task before deleting it is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForCreatingTaskBeforeDeletingItIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for creating task before deleting it is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForCreatingTaskBeforeDeletingItIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"storage id which will be used for creating task before deleting it is ""([^""]*)""")]
    public void GivenStorageIdWhichWillBeUsedForCreatingTaskBeforeDeletingItIs(string storageId)
    {
        _taskRequestModel.StorageId = storageId;
    }

    [When(@"post task request before deleting it is sent")]
    public async Task WhenPostTaskRequestBeforeDeletingItIsSent()
    {
        _response = await _taskRequests.PostTaskAsync(_taskRequestModel, _requestingUserId, _requestingUserType, _headerUserId);
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        _taskId = responseBody[ResponseConstants.TaskResponse.TaskId]!.ToString();
    }

    [Then(@"I save task id")]
    public async Task WhenISaveTaskId()
    {
        _response = await _taskRequests.GetTaskByIdAsync(_taskId, _requestingUserId, _requestingUserType, _headerUserId);
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        _savedTaskId = responseBody[ResponseConstants.TaskResponse.TaskId]!.ToString();
    }

    [Given(@"id which will be used for deleting task is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForDeletingTaskIs(string taskId)
    {
        _ = _savedTaskId;
    }
    [Given(@"non-existent id which will be used for deleting task is ""([^""]*)""")]
    public void GivenNonExistingIdWhichWillBeUsedForDeletingTaskIs(string taskId)
    {
        _taskId = taskId;
    }

    [Given(@"bad requesting user id for deleting task is ([^""]*)")]
    public void GivenBadRequestingUserIdForDeletingTaskIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type for deleting task is ([^""]*)")]
    public void GivenRequestingUserTypeForDeletingTaskIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"bad header user id which will be used for deleting task is ([^""]*)")]
    public void GivenBadHeaderUserIdWhichWillBeUsedForDeletingTaskIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"query reason is ""([^""]*)""")]
    public void GivenQueryReasonIs(string reason)
    {
        _reason = reason;
    }

    [Given(@"delete mode is ""([^""]*)""")]
    public void GivenDeleteModeIs(string mode)
    {
        _mode = mode;
    }

    [When(@"delete task request is sent")]
    public async Task WhenDeleteTaskRequestIsSent()
    {
        _response = await _taskRequests.DeleteTaskByIdAsync(_savedTaskId, _requestingUserId, _requestingUserType, _headerUserId, _mode, _reason);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        if (_response.StatusCode != HttpStatusCode.NoContent)
        {
            var errorResponseBody = JObject.Parse(content);
            var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
            _context.Add("error_code", errorCodeFromResponse);
        }
    }

    [When(@"delete task request with non-existent id is sent")]
    public async Task WhenDeleteTaskRequestWithNonExistingIdIsSent()
    {
        _response = await _taskRequests.DeleteTaskByIdAsync(_taskId, _requestingUserId, _requestingUserType, _headerUserId, _mode, _reason);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        if (_response.StatusCode != HttpStatusCode.NoContent)
        {
            var errorResponseBody = JObject.Parse(content);
            var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
            _context.Add("error_code", errorCodeFromResponse);
        }
    }

    [Then(@"response body from cancel task should contain deletion mode ""([^""]*)""")]
    public void ThenResponseBodyFromCancelTaskShouldContainDeletionMode(string mode)
    {
        var responseBody  = JObject.Parse(_response.Content!);
        var responseMode = responseBody["status"]?.ToString();
        responseMode.Should().Be(mode);
        responseBody.Should().NotBeNullOrEmpty();
    }


    [Then(@"forbidden request message from delete task request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromDeleteTaskRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        var expectedStatus = (int)HttpStatusCode.Forbidden;
        var statusResponse = responseBody[ResponseConstants.ErrorResponse.Status]?.ToString();
        var messageResponse = responseBody[ResponseConstants.ErrorResponse.Message]?.ToString();
        var schemaValidation = responseBody.IsValid(_errorResponseSchema);

        responseBody.Should().NotBeNullOrEmpty();
        statusResponse.Should().NotBeNullOrEmpty();
        statusResponse.Should().Be(expectedStatus.ToString());
        messageResponse.Should().NotBeNullOrEmpty();
        messageResponse.Should().Be(message);
        schemaValidation.Should().BeTrue();
    }

    [Then(@"bad request message from delete task request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromDeleteTaskRequestShouldHaveTextInTheField(string message, string field)
    {
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        var expectedStatus = (int)HttpStatusCode.BadRequest;
        var statusResponse = responseBody[ResponseConstants.ErrorResponse.Status]?.ToString();
        var validationField = responseBody[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Field]?.ToString();
        var validationMessage = responseBody[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Message]?.ToString();
        var schemaValidation = responseBody.IsValid(_errorResponseSchema);

        responseBody.Should().NotBeNullOrEmpty();
        statusResponse.Should().NotBeNullOrEmpty();
        statusResponse.Should().Be(expectedStatus.ToString());
        validationField.Should().NotBeNullOrEmpty();
        validationField.Should().Be(field);
        validationMessage.Should().NotBeNullOrEmpty();
        validationMessage.Should().Be(message);
        schemaValidation.Should().BeTrue();
    }

    [Then(@"not found message from delete task request should have message ""([^""]*)""")]
    public void ThenNotFoundMessageFromDeleteTaskRequestShouldHaveMessage(string message)
    {
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        var expectedStatus = (int)HttpStatusCode.NotFound;
        var statusResponse = responseBody[ResponseConstants.ErrorResponse.Status]?.ToString();
        var messageResponse = responseBody[ResponseConstants.ErrorResponse.Message]?.ToString();
        var schemaValidation = responseBody.IsValid(_errorResponseSchema);

        responseBody.Should().NotBeNullOrEmpty();
        statusResponse.Should().NotBeNullOrEmpty();
        statusResponse.Should().Be(expectedStatus.ToString());
        messageResponse.Should().NotBeNullOrEmpty();
        messageResponse.Should().Be(message);
        schemaValidation.Should().BeTrue();
    }

    [Then(@"I delete task which was created")]
    public async Task ThenIDeleteTaskWhichWasCreated()
    {
        await _taskRequests.DeleteTaskByIdAsync(_savedTaskId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue, _mode, _reason);
    }

}

