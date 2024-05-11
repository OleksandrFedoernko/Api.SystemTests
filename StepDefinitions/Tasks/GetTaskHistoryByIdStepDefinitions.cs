using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow;
using static Api.SystemTests.Constants.ResponseConstants;

namespace Api.SystemTests.StepDefinitions.Tasks;

[Binding]
public class GetTaskHistoryByIdStepDefinitions
{
    private readonly TaskRequests _taskRequests = new();
    private readonly TaskRequestModel _taskRequestModelForEmp = new();
    private readonly TaskRequestModel _taskRequestModelForPart = new();
    private readonly TaskRequestModel _taskRequestModel = new();
    private RestResponse _response = new();
    private RestResponse _responseForPart = new();
    private RestResponse _responseForAll = new();
    private string _taskId = string.Empty;
    private string _taskIdForAll = string.Empty;
    private string _empTaskId = string.Empty;
    private string _partTaskId = string.Empty;
    private string _empStorageId = string.Empty;
    private string _partStorageId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private readonly ScenarioContext _context;
    public GetTaskHistoryByIdStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"storage ids which will be used for creating task before getting task history are ""([^""]*)"", ""([^""]*)"", ""([^""]*)""")]
    public void GivenStorageIdsWhichWillBeUsedForCreatingTaskBeforeGettingTaskHistoryAre(string participantStorageId, string employerStorageId, string storageId)
    {
        _taskRequestModelForEmp.StorageId = employerStorageId;
        _taskRequestModelForPart.StorageId = participantStorageId;
        _taskRequestModel.StorageId = storageId;
        _partStorageId = participantStorageId;
        _empStorageId = employerStorageId;
    }

    [Given(@"requesting user id which will be used for creating task before getting task history is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForCreatingTaskBeforeGettingTaskHistoryIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for creating task before getting task history ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForCreatingTaskBeforeGettingTaskHistory(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"requesting user type which will be used for getting task history for participant type is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingTaskHistoryForParticipantTypeIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will used for creating task before getting task history ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillUsedForCreatingTaskBeforeGettingTaskHistory(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [When(@"post task for get task history request with first id is sent")]
    public async Task WhenPostTaskForGetTaskHistoryRequestWithFirstIdIsSent()
    {
        _responseForPart = await _taskRequests.PostTaskAsync(_taskRequestModelForPart, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [When(@"post task for get task history request with second id is sent")]
    public async Task WhenPostTaskForGetTaskHistoryRequestWithSecondIdIsSent()
    {
        _response = await _taskRequests.PostTaskAsync(_taskRequestModelForEmp, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [When(@"post task for get task history request with third id is sent")]
    public async Task WhenPostTaskForGetTaskHistoryRequestWithThirdIdIsSent()
    {
        _responseForAll = await _taskRequests.PostTaskAsync(_taskRequestModel, _requestingUserId, _requestingUserType, _headerUserId);
    }

    [Then(@"I save task ids")]
    public void ThenISaveTheTaskId()
    {
        var responseBody = JObject.Parse(_response.Content!);
        var responseBodyForPart = JObject.Parse(_responseForPart.Content!);
        var responseBodyForAll = JObject.Parse(_responseForAll.Content!);
        var taskIdResponse = responseBody[ResponseConstants.TaskResponse.TaskId]?.ToString();
        var taskIdResponseForPart = responseBodyForPart[ResponseConstants.TaskResponse.TaskId]?.ToString();
        var taskIdResponseForAll = responseBodyForAll[ResponseConstants.TaskResponse.TaskId]?.ToString();
        _partTaskId = taskIdResponseForPart!;
        _empTaskId = taskIdResponse!;
        _taskId = taskIdResponseForPart!;
        _taskIdForAll = taskIdResponseForAll!;
    }

    [Given(@"task id which will be used for getting history is an id of created task")]
    public void GivenTaskIdWhichWillBeUsedForGettingHistoryIsAnIdOfCreatedTask()
    {
        _ = _partTaskId;
        _ = _empTaskId;
        _ = _taskId;
    }

    [Given(@"task id which will be used for getting history is ""([^""]*)""")]
    public void GivenTaskIdWhichWillBeUsedForGettingHistoryIs(string taskId)
    {
        _taskId = taskId;
        _empTaskId = taskId;
        _partTaskId = _taskId;
    }

    [Given(@"requesting user id which will be used for getting task history for participant type is the ending of storage id")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingTaskHistoryForParticipantTypeIsTheEndingOfStorageId()
    {
        _requestingUserId = _partStorageId;
    }

    [Given(@"requesting user id which will be used for getting task history for employer type is the ending of storage id")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingTaskHistoryForEmployerTypeIsTheEndingOfStorageId()
    {
        _requestingUserId = _empStorageId;
    }

    [Given(@"requesting user type which will be used for getting task history is ([^""]*)")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingTaskHistoryIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"requesting user id which will be used for getting task history with wrong data is ([^""]*)")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingTaskHistoryWithWrongDataIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"header user id which will be used for getting task history is with wrong data is ([^""]*)")]
    public void GivenHeaderUserIdWhichWillBeUsedForGettingTaskHistoryIsWithWrongDataIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"requesting user type which will be used for getting task history for employer type is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingTaskHistoryForEmployerTypeIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"description which will be used for updating task is ""([^""]*)""")]
    public void GivenDescriptionWhichWillBeUsedForUpdatingTaskIs(string description)
    {
        _taskRequestModel.Description = description;
        _taskRequestModelForEmp.Description = description;
        _taskRequestModelForPart.Description = description;
    }

    [When(@"delete task for getting its history is sent")]
    public async Task WhenDeleteTaskForGettingItsHistoryIsSent()
    {
        await _taskRequests.DeleteTaskByIdAsync(_taskId, _requestingUserId, HttpHeadersValues.RequestingUserTypeValue, _headerUserId, "DELETE", "");
    }

    [When(@"get task history request is sent")]
    public async Task WhenGetTaskHistoryRequestIsSent()
    {
        _response = await _taskRequests.GetTaskHistoryByIdAsync(_taskId, _requestingUserId, _requestingUserType, _headerUserId); _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"update task for getting its history request is sent")]
    public async Task WhenUpdateTaskForGettingItsHistoryRequestIsSent()
    {
        _response = await _taskRequests.UpdateTaskAsync(_taskRequestModel, _taskId, _requestingUserId, HttpHeadersValues.RequestingUserTypeValue, _headerUserId, string.Empty);
    }

    [When(@"get task history request for participant is sent")]
    public async Task WhenGetTaskHistoryRequestForParticipantIsSent()
    {
        _response = await _taskRequests.GetTaskHistoryByIdAsync(_partTaskId, _requestingUserId.Remove(0, 19), _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get task history request for employer is sent")]
    public async Task WhenGetTaskHistoryRequestForEmployerIsSent()
    {
        _response = await _taskRequests.GetTaskHistoryByIdAsync(_empTaskId, _requestingUserId.Remove(0, 16), _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get task history request with forbidden task id is sent")]
    public async Task WhenGetTaskHistoryRequestWithForbiddenTaskIdIsSent()
    {
        _response = await _taskRequests.GetTaskHistoryByIdAsync(_taskIdForAll, _requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from get task history should contain ([^""]*), ([^""]*), ([^""]*)")]
    public void ThenResponseBodyFromGetTaskHistoryShouldContain(string testId, string testUser, string p2)
    {
        var response = _response.Content!;
        var jsonResponse = JObject.Parse(response);
        var limit = (int)jsonResponse[PaginationResponse.Pagination]![PaginationResponse.Total]!;
        var index = Enumerable.Range(0, limit);
        foreach (var item in index)
        {
            var taskIdResponse = jsonResponse[PaginationResponse.Items]?[item]?[TaskHistoryResponse.TaskId]?.ToString();
            var changesIdResponse = jsonResponse[PaginationResponse.Items]?[item]?[TaskHistoryResponse.ChangeId]?.ToString();
            var changedByUserIdResponse = jsonResponse[PaginationResponse.Items]?[item]?[TaskHistoryResponse.ChangedByUserId]?.ToString();
            var changedByUserResponse = jsonResponse[PaginationResponse.Items]?[item]?[TaskHistoryResponse.ChangedByUser]?.ToString();
            var changedByUserTypeResponse = jsonResponse[PaginationResponse.Items]?[item]?[TaskHistoryResponse.ChangedByUserType]?.ToString();
            var recordedAtResponse = jsonResponse[PaginationResponse.Items]?[item]?[TaskHistoryResponse.RecordedAt]?.ToString();

            taskIdResponse.Should().NotBeNullOrWhiteSpace();
            changesIdResponse.Should().NotBeNullOrWhiteSpace();
            changedByUserIdResponse.Should().Be(HttpHeadersValues.RequestingUserIdValue);
            changedByUserResponse.Should().Be(HttpHeadersValues.RequestingUserValue);
            changedByUserTypeResponse.Should().Be(HttpHeadersValues.RequestingUserTypeValue);
            recordedAtResponse.Should().NotBeNullOrWhiteSpace();
        }
    }

    [Then(@"changed items should have old and new values after update")]
    public void ThenChangedItemsShouldHaveOldAndNewValuesAfterUpdate()
    {
        var response = _response.Content!;
        var jsonResponse = JObject.Parse(response);
        var taskIdChangesResponse = jsonResponse[PaginationResponse.Items]?[1]?[TaskHistoryResponse.Changes]?[0]?[2]?.ToString();
        var storageIdChangesResponse = jsonResponse[PaginationResponse.Items]?[1]?[TaskHistoryResponse.Changes]?[1]?[2]?.ToString();
        taskIdChangesResponse.Should().NotBeNullOrWhiteSpace();
        storageIdChangesResponse.Should().NotBeNullOrWhiteSpace();
    }

    [Then(@"changes field should have null items after getting history after delete")]
    public void ThenChangesFieldShouldHaveNullItemsAfterGettingHistoryAfterDelete()
    {
        var response = _response.Content!;
        var jsonResponse = JObject.Parse(response);
        var taskIdChangesResponse = jsonResponse[PaginationResponse.Items]?[1]?[TaskHistoryResponse.Changes]?[0]?[2]?.ToString();
        var storageIdChangesResponse = jsonResponse[PaginationResponse.Items]?[1]?[TaskHistoryResponse.Changes]?[1]?[2]?.ToString();
        var createdByChangesResponse = jsonResponse[PaginationResponse.Items]?[1]?[TaskHistoryResponse.Changes]?[3]?[2]?.ToString();
        var priorityChangesResponse = jsonResponse[PaginationResponse.Items]?[1]?[TaskHistoryResponse.Changes]?[3]?[2]?.ToString();
        if (string.IsNullOrWhiteSpace(taskIdChangesResponse))
        {
            taskIdChangesResponse.Should().BeNullOrEmpty();
            storageIdChangesResponse.Should().BeNullOrEmpty();
            createdByChangesResponse.Should().BeNullOrEmpty();
            priorityChangesResponse.Should().BeNullOrEmpty();
        }
    }

    [Then(@"forbidden request message from get task history request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromGetTaskHistoryRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var expectedStatusCodeResult = (int)HttpStatusCode.Forbidden;
        var errorResponseBody = JObject.Parse(content);
        var jsonMessage = errorResponseBody[ErrorResponse.Message]?.ToString();
        var jsonStatusCode = errorResponseBody[ErrorResponse.Status]?.ToString();

        jsonMessage.Should().Contain(message);
        jsonMessage.Should().StartWith(message);
        jsonMessage.Should().NotBeNullOrEmpty();
        jsonStatusCode.Should().Be(expectedStatusCodeResult.ToString());
        jsonStatusCode.Should().NotBe(null);
    }

    [Then(@"bad request message from get task history request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromGetTaskHistoryRequestShouldHaveTextInTheField(string message, string field)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var errorField = errorResponse[ErrorResponse.ValidationMessages]?[0]?[ErrorResponse.Field]?.ToString();
        var errorMessage = errorResponse[ErrorResponse.Message]?.ToString();
        var errorStatusCode = errorResponse[ErrorResponse.Status]?.ToString();
        var expectedStatusCode = (int)HttpStatusCode.BadRequest;

        errorField.Should().Be(field);
        errorField.Should().NotBeNullOrEmpty();
        errorMessage.Should().Be(errorMessage);
        errorStatusCode.Should().Be(expectedStatusCode.ToString());
    }

    [Then(@"I delete unused task")]
    public async Task ThenIDeleteUnusedTask()
    {
        await _taskRequests.DeleteTaskByIdAsync(_partTaskId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue, "DELETE", "");
        await _taskRequests.DeleteTaskByIdAsync(_empTaskId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue, "DELETE", "");
    }
}

