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
public class UpdateTaskStepDefinitions
{
    private readonly TaskRequests _taskRequests = new();
    private readonly TaskRequestModel _taskRequestModel = new();
    private RestResponse _response = new();
    private readonly JSchema _taskResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/TaskResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _comment = string.Empty;
    private string _taskId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private readonly ScenarioContext _context;
    public UpdateTaskStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"requesting user id which will be used for creating task for upd is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForCreatingTaskForUpdIs(string userId)
    {
        _requestingUserId = userId;
    }

    [Given(@"header user id which will be used for creating task for upd is is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForCreatingTaskForUpdIsIs(string id)
    {
        _headerUserId = id;
    }


    [Given(@"requesting user id which will be used for updating task is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForUpdatingTaskIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user id which will be used for updating task with different params is ([^""]*)")]
    public void GivenRequestingUserIdWhichWillBeUsedForUpdatingTaskWithDifferentParamsIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for updating task is ([^""]*)")]
    public void GivenRequestingUserTypeWhichWillBeUsedForUpdatingTaskIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for updating task is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForUpdatingTaskIs(string userId)
    {
        _headerUserId = userId;
    }

    [Given(@"bad header user id which will be used for updating task is ([^""]*)")]
    public void GivenBadHeaderUserIdWhichWillBeUsedForUpdatingTaskIs(string userId)
    {
        _headerUserId = userId;
    }

    [Given(@"requesting user type which will be used for creating task for upd is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForCreatingTaskForUpdIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"storage id which will be used for creating task for upd is ""([^""]*)""")]
    public void GivenStorageIdWhichWillBeUsedForCreatingTaskForUpdIs(string id)
    {
        _taskRequestModel.StorageId = id;
    }


    [Given(@"comment which will be added to history table of the task operation is ([^""]*)")]
    public void GivenCommentWhichWillBeAddedToHistoryTableOfTheTaskOperationIs(string comment)
    {
        _comment = comment;
    }

    [Given(@"task id which will be used for updating task is ([^""]*)")]
    public void GivenTaskIdWhichWillBeUsedForUpdatingTaskIs(string taskId)
    {
        _taskId = _context.Get<string>("path_id");

    }

    [Given(@"wrong task id which will be used for updating task is ([^""]*)")]
    public void GivenWrongTaskIdWhichWillBeUsedForUpdatingTaskIs(string taskId)
    {
        _taskId = taskId;
    }


    [Given(@"storage id which will be used for updating task is ([^""]*)")]
    public void GivenStorageIdWhichWillBeUsedForUpdatingTaskIs(string storageId)
    {
        _taskRequestModel.StorageId = storageId;
    }

    [When(@"post task request for upd is sent")]
    public async Task WhenPostTaskRequestForUpdIsSent()
    {
        _response = await _taskRequests.PostTaskAsync(_taskRequestModel, _requestingUserId, _requestingUserType, _headerUserId);
    }
    [When(@"I save task id")]
    public void WhenISaveTaskId()
    {
        JObject responseBody = JObject.Parse(_response.Content!);
        _context.Add("path_id", responseBody["task_id"]?.ToString());
    }


    [Given(@"reference id which will be used for updating task is ([^""]*)")]
    public void GivenReferenceIdWhichWillBeUsedForUpdatingTaskIs(string referenceId)
    {
        _taskRequestModel.ReferenceId = referenceId;
    }

    [Given(@"correlation id which will be used for updating task is ([^""]*)")]
    public void GivenCorrelationIdWhichWillBeUsedForUpdatingTaskIs(string correlationId)
    {
        _taskRequestModel.CorrelationId = correlationId;
    }

    [Given(@"causation id which will be used for updating task is ([^""]*)")]
    public void GivenCausationIdWhichWillBeUsedForUpdatingTaskIs(string causationId)
    {
        _taskRequestModel.CausationId = causationId;
    }

    [Given(@"name of the process which will be used for updating task is ([^""]*)")]
    public void GivenNameOfTheProcessWhichWillBeUsedForUpdatingTaskIs(string origin)
    {
        _taskRequestModel.Origin = origin;
    }

    [Given(@"title which will be used for updating task is ([^""]*)")]
    public void GivenTitleWhichWillBeUsedForUpdatingTaskIs(string title)
    {
        _taskRequestModel.Title = title;
    }

    [Given(@"description which will be used updating task is ([^""]*)")]
    public void GivenDescriptionWhichWillBeUsedUpdatingTaskIs(string description)
    {
        _taskRequestModel.Description = description;
    }

    [Given(@"icon which will be used for updating task is ([^""]*)")]
    public void GivenIconWhichWillBeUsedForUpdatingTaskIs(string icon)
    {
        _taskRequestModel.Icon = icon;
    }

    [Given(@"related entity which will be used for updating task is ([^""]*)")]
    public void GivenRelatedEntityWhichWillBeUsedForUpdatingTaskIs(string relEntity)
    {
        _taskRequestModel.RelatedEntity = relEntity;
    }

    [Given(@"priority which will be used for updating task is ([^""]*)")]
    public void GivenPriorityWhichWillBeUsedForUpdatingTaskIs(int priority)
    {
        _taskRequestModel.Priority = priority;
    }

    [Given(@"percent complete which will be used for updating task is ([^""]*)")]
    public void GivenPercentCompleteWhichWillBeUsedForUpdatingTaskIs(int percentComplete)
    {
        _taskRequestModel.PercentComplete = percentComplete;
    }

    [Given(@"possible outcomes which will be used for updating task are ([^""]*), ([^""]*)")]
    public void GivenPossibleOutcomesWhichWillBeUsedForUpdatingTaskAre(string possibleOutcome1, string possibleOutcome2)
    {
        _taskRequestModel.PossibleOutcomes = new();
        _taskRequestModel.PossibleOutcomes?.Add(possibleOutcome1);
        _taskRequestModel.PossibleOutcomes?.Add(possibleOutcome2);
    }

    [Given(@"chosen outcome which will be used for updating task is ([^""]*)")]
    public void GivenChosenOutcomeWhichWillBeUsedForUpdatingTaskIs(string chosenOutCome)
    {
        _taskRequestModel.ChosenOutcome = chosenOutCome;
    }

    [Given(@"assigned to which will be used for updating task task is ([^""]*)")]
    public void GivenAssignedToWhichWillBeUsedForUpdatingTaskTaskIs(string assignedTo)
    {
        _taskRequestModel.AssignedTo = assignedTo;
    }

    [Given(@"start date time which will be used for updating task is ([^""]*)")]
    public void GivenStartDateTimeWhichWillBeUsedForUpdatingTaskIs(string startDateTime)
    {
        _taskRequestModel.StartDateTime = startDateTime;
    }

    [Given(@"due date time which will be used for updating task is ([^""]*)")]
    public void GivenDueDateTimeWhichWillBeUsedForUpdatingTaskIs(string deadline)
    {
        _taskRequestModel.DueDateTime = deadline;
    }

    [When(@"update task request is sent")]
    public async Task WhenUpdateTaskRequestIsSent()
    {
       
        _response = await _taskRequests.UpdateTaskAsync(_taskRequestModel, _taskId, _requestingUserId, _requestingUserType, _headerUserId, _comment);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from update task should be valid according to the JSON schema")]
    public void ThenResponseBodyFromUpdateTaskShouldBeValidAccordingToTheJSONSchema()
    {
        var content = _response.Content!;
        var taskResponseBody = JObject.Parse(content);

        var responseSchemaValidation = taskResponseBody.IsValid(_taskResponseSchema);
        var taskIdResponse = taskResponseBody[ResponseConstants.TaskResponse.TaskId]?.ToString();
        var storageIdResponse = taskResponseBody[ResponseConstants.TaskResponse.StorageId]?.ToString();
        var startDateResponse = taskResponseBody[ResponseConstants.TaskResponse.StartDateTime]?.ToString();
        var deadlineResponse = taskResponseBody[ResponseConstants.TaskResponse.DueDateTime]?.ToString();
        var priorityResponse = (int?)taskResponseBody[ResponseConstants.TaskResponse.Priority];
        var percentResponse = (int?)taskResponseBody[ResponseConstants.TaskResponse.PercentComplete];
        var completedTaskDate = taskResponseBody[ResponseConstants.TaskResponse.CompletedDateTime]?.ToString();

        if (_taskRequestModel.StartDateTime == null && _taskRequestModel.DueDateTime == null)
        {

            deadlineResponse.Should().BeNullOrEmpty();
        }

        if (_taskRequestModel.Priority == 0)
        {
            priorityResponse.Should().Be(50);
            percentResponse.Should().Be(0);
        }

        if (_taskRequestModel.PercentComplete == 100)
        {
            completedTaskDate.Should().NotBeNullOrEmpty();
        }
        startDateResponse.Should().NotBeNullOrEmpty();
        taskResponseBody.Should().NotBeNull();
        taskIdResponse.Should().NotBeNullOrEmpty();
        storageIdResponse.Should().NotBeNullOrEmpty();
        storageIdResponse.Should().Be(_taskRequestModel.StorageId);
        responseSchemaValidation.Should().BeTrue();
    }

    [Then(@"response body from update task should contain message ""([^""]*)""")]
    public void ThenResponseBodyFromUpdateTaskShouldContainMessage(string message)
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

    [Then(@"response body from update task should contain message ([^""]*) in the field ([^""]*)")]
    public void ThenResponseBodyFromUpdateTaskShouldContainMessageInTheField(string message, string field)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        var expectedStatusCode = (int)HttpStatusCode.BadRequest;
        var errorField = errorResponse[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Field]?.ToString();
        var errorMessage = errorResponse[ResponseConstants.ErrorResponse.Message]?.ToString();
        var errorStatusCode = errorResponse[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorSchemaValidation = errorResponse.IsValid(_errorResponseSchema);
        errorField.Should().Be(field);
        errorMessage.Should().Be(errorMessage);
        errorStatusCode.Should().Be(expectedStatusCode.ToString());
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"response body from update task should contain not found message ""([^""]*)""")]
    public void ThenResponseBodyFromUpdateTaskShouldContainNotFoundMessage(string message)
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
