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
public class GetTaskByIdStepDefinitions
{
    private readonly TaskRequests _taskRequests = new();
    private readonly TaskRequestModel _taskRequestModelForEmp = new();
    private readonly TaskRequestModel _taskRequestModelForPart = new();
    private RestResponse _response = new();
    private RestResponse _responseForPart = new();
    private readonly JSchema _taskResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/TaskResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _taskId = string.Empty;
    private string _empTaskId = string.Empty;
    private string _partTaskId = string.Empty;
    private string _wrongTaskId = string.Empty;
    private string _empStorageId = string.Empty;
    private string _partStorageId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private readonly ScenarioContext _context;
    public GetTaskByIdStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"storage ids which will be used for creating task before getting it are ""([^""]*)"", ""([^""]*)""")]
    public void GivenStorageIdsWhichWillBeUsedForCreatingTaskBeforeGettingItAre(string partId, string empId)
    {
        _taskRequestModelForEmp.StorageId = empId;
        _empStorageId = empId;

        _taskRequestModelForPart.StorageId = partId;
        _partStorageId = partId;
    }

    [Given(@"requesting user id which will be used for creating task before getting it is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForCreatingTaskBeforeGettingItIs(string userId)
    {
        _requestingUserId = userId;
    }

    [Given(@"requesting user type which will be used for creating task before getting is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForCreatingTaskBeforeGettingIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will used for creating task before getting it is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillUsedForCreatingTaskBeforeGettingItIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [When(@"post task for get request with first id is sent")]
    public async Task WhenPostTaskForGetRequestWithFirstIdIsSent()
    {
        _responseForPart = await _taskRequests.PostTaskAsync(_taskRequestModelForPart, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [When(@"post task for get request with second id is sent")]
    public async Task WhenPostTaskForGetRequestWithSecondIdIsSent()
    {
        _response = await _taskRequests.PostTaskAsync(_taskRequestModelForEmp, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [Then(@"I save the id of storage and task")]
    public void ThenISaveTheIdOfStorageAndTask()
    {
        var responseBody = JObject.Parse(_response.Content!);
        var responseBodyForPart = JObject.Parse(_responseForPart.Content!);
        var taskIdResponse = responseBody[ResponseConstants.TaskResponse.TaskId]?.ToString();
        var taskIdResponseForPart = responseBodyForPart[ResponseConstants.TaskResponse.TaskId]?.ToString();
        _partTaskId = taskIdResponseForPart!;
        _empTaskId = taskIdResponse!;
        _taskId = taskIdResponseForPart!;
    }

    [Given(@"id which will be used for getting task is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForGettingTaskIs(string taskId)
    {
        _ = _taskId;
    }

    [Given(@"id which will be used for getting task with participant type is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForGettingTaskWithParticipantTypeIs(string partId)
    {
        _ = _partTaskId;
    }

    [Given(@"id which will be used for getting task with employer type is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForGettingTaskWithEmployerTypeIs(string p0)
    {
        _ = _empTaskId;
    }

    [Given(@"non-existent id which will be used for getting task is ""([^""]*)""")]
    public void GivenNonExistingIdWhichWillBeUsedForGettingTaskIs(string wrongTaskId)
    {
        _wrongTaskId = wrongTaskId;
    }

    [Given(@"requesting user id which will be used for getting task with wrong data is ([^""]*)")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingTaskWithWrongDataIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user id which will be used for getting task with participant type is an end of storage_id")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingTaskWithParticipantTypeIsAnEndOfStorage_Id()
    {
        _requestingUserId = _partStorageId;
    }

    [Given(@"requesting user id which will be used for getting task with employer type is an end of storage_id")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingTaskWithEmployerTypeIsAnEndOfStorage_Id()
    {
        _requestingUserId = _empStorageId;
    }

    [Given(@"requesting user type which will be used for getting task is ([^""]*)")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingTaskIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"requesting user type which will be used for getting task with employer type is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingTaskWithEmployerTypeIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"requesting user type which will be used for getting task with participant type is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingTaskWithParticipantTypeIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }
    [Given(@"header user id which will be used for getting task is with wrong data is ([^""]*)")]
    public void GivenHeaderUserIdWhichWillBeUsedForGettingTaskIsWithWrongDataIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"header user id which will be used for getting task with employer type is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForGettingTaskWithEmployerTypeIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"header user id which will be used for getting task with participant type is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForGettingTaskWithParticipantTypeIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [When(@"get task by id is sent")]
    public async Task WhenGetTaskByIdIsSent()
    {
        _response = await _taskRequests.GetTaskByIdAsync(_taskId, _requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get task by id with employer type is sent")]
    public async Task WhenGetTaskByIdWithEmployerTypeIsSent()
    {
        _response = await _taskRequests.GetTaskByIdAsync(_empTaskId, _requestingUserId.Remove(0, 16), _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get task by id with participant type is sent")]
    public async Task WhenGetTaskByIdWithParticipantTypeIsSent()
    {
        _response = await _taskRequests.GetTaskByIdAsync(_partTaskId, _requestingUserId.Remove(0, 19), _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get task by non-existent id is sent")]
    public async Task WhenGetTaskByNonExistingIdIsSent()
    {
        _response = await _taskRequests.GetTaskByIdAsync(_wrongTaskId, _requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from get task should contain ([^""]*), ([^""]*)")]
    public void ThenResponseBodyWithParticipantTypeShouldContain(string storageId, string createdBy)
    {
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        var taskIdResponse = responseBody[ResponseConstants.TaskResponse.TaskId]?.ToString();
        var storageIdResponse = responseBody[ResponseConstants.TaskResponse.StorageId]?.ToString();
        var createdByResponse = responseBody[ResponseConstants.TaskResponse.CreatedBy]?.ToString();
        var responseSchemaValidation = responseBody.IsValid(_taskResponseSchema);

        taskIdResponse.Should().NotBeNullOrWhiteSpace();
        storageIdResponse.Should().NotBeNullOrWhiteSpace();
        createdByResponse.Should().NotBeNullOrWhiteSpace();
        storageIdResponse.Should().Be(storageId);
        createdByResponse.Should().Be(createdBy);

        responseSchemaValidation.Should().BeTrue();
    }

    [Then(@"forbidden request message from get task request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromGetTaskRequestShouldHaveText(string message)
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

    [Then(@"bad request message from get task request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromGetTaskRequestShouldHaveTextInTheField(string message, string field)
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

    [Then(@"not found request message from get task request should have text ""([^""]*)""")]
    public void ThenNotFoundRequestMessageFromGetTaskRequestShouldHaveText(string message)
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

    [Then(@"I delete created task")]
    public async Task ThenIDeleteCreatedTask()
    {
        await _taskRequests.DeleteTaskByIdAsync(_empTaskId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue, "DELETE", "");
        await _taskRequests.DeleteTaskByIdAsync(_partTaskId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue, "DELETE", "");
    }

}

