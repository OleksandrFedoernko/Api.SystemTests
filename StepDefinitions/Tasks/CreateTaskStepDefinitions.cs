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
public class CreateTaskStepDefinitions
{
    private readonly TaskRequests _taskRequests = new();
    private readonly TaskRequestModel _taskRequestModel = new();
    private RestResponse _response = new();
    private readonly JSchema _taskResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/TaskResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _headerUserId = string.Empty;
    private string _taskId = string.Empty;
    private readonly ScenarioContext _context;
    public CreateTaskStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"requesting user id which will be used for creating task is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForCreatingTaskIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"bad requesting user id which will be used for creating task is ([^""]*)")]
    public void GivenBadRequestingUserIdWhichWillBeUsedForCreatingTaskIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for creating task is ([^""]*)")]
    public void GivenRequestingUserTypeWhichWillBeUsedForCreatingTaskIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for creating task is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForCreatingTaskIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"bad header user id which will be used for creating task is ([^""]*)")]
    public void GivenBadHeaderUserIdWhichWillBeUsedForCreatingTaskIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"storage id which will be used for creating task is ([^""]*)")]
    public void GivenStorageIdWhichWillBeUsedForCreatingTaskIs(string storageId)
    {
        _taskRequestModel.StorageId = storageId;
    }

    [Given(@"reference id which will be used for creating task is ([^""]*)")]
    public void GivenReferenceIdWhichWillBeUsedForCreatingTaskIs(string referenceId)
    {
        _taskRequestModel.ReferenceId = referenceId;
    }

    [Given(@"correlation id which will be used for creating task is ([^""]*)")]
    public void GivenCorrelationIdWhichWillBeUsedForCreatingTaskIs(string correlationId)
    {
        _taskRequestModel.CorrelationId = correlationId;
    }

    [Given(@"causation id which will be used for creating task is ([^""]*)")]
    public void GivenCausationIdWhichWillBeUsedForCreatingTaskIs(string causationId)
    {
        _taskRequestModel.CausationId = causationId;
    }

    [Given(@"name of the process which will be used for creating task is ([^""]*)")]
    public void GivenNameOfTheProcessWhichWillBeUsedForCreatingTaskIs(string origin)
    {
        _taskRequestModel.Origin = origin;
    }

    [Given(@"title which will be used for creating task is ([^""]*)")]
    public void GivenTitleWhichWillBeUsedForCreatingTaskIs(string taskTitle)
    {
        _taskRequestModel.Title = taskTitle;
    }

    [Given(@"description which will be used creating task is ([^""]*)")]
    public void GivenDescriptionWhichWillBeUsedCreatingTaskIs(string taskDesc)
    {
        _taskRequestModel.Description = taskDesc;
    }

    [Given(@"icon which will be used for creating task is ([^""]*)")]
    public void GivenIconWhichWillBeUsedForCreatingTaskIs(string taskIcon)
    {
        _taskRequestModel.Icon = taskIcon;
    }

    [Given(@"related entity which will be used for creating task is ([^""]*)")]
    public void GivenRelatedEntityWhichWillBeUsedForCreatingTaskIs(string relatedEntity)
    {
        _taskRequestModel.RelatedEntity = relatedEntity;
    }

    [Given(@"priority which will be used for creating task is ([^""]*)")]
    public void GivenPriorityWhichWillBeUsedForCreatingTaskIs(int priority)
    {
        _taskRequestModel.Priority = priority;
    }

    [Given(@"percent complete which will be used for creating task is ([^""]*)")]
    public void GivenPercentCompleteWhichWillBeUsedForCreatingTaskIs(int percentComplete)
    {
        _taskRequestModel.PercentComplete = percentComplete;
    }

    [Given(@"possible outcomes which will be used for creating task are ([^""]*) ([^""]*)")]
    public void GivenPossibleOutcomesWhichWillBeUsedForCreatingTaskAre(string possibleOutcome1, string possibleOutcome2)
    {
        _taskRequestModel.PossibleOutcomes = new();
        _taskRequestModel.PossibleOutcomes?.Add(possibleOutcome1);
        _taskRequestModel.PossibleOutcomes?.Add(possibleOutcome2);
    }

    [Given(@"chosen outcome which will be used for creating task is ([^""]*)")]
    public void GivenChosenOutcomeWhichWillBeUsedForCreatingTaskIs(string chosenOutcome)
    {
        _taskRequestModel.ChosenOutcome = chosenOutcome;
    }

    [Given(@"assigned to which will be used for creating task is ([^""]*)")]
    public void GivenAssignedToWhichWillBeUsedForCreatingTaskIs(string assignedTo)
    {
        _taskRequestModel.AssignedTo = assignedTo;
    }

    [Given(@"start date time which will be used for creating task is ([^""]*)")]
    public void GivenStartDateTimeWhichWillBeUsedForCreatingTaskIs(string startTime)
    {
        _taskRequestModel.StartDateTime = startTime;
    }

    [Given(@"due date time which will be used for creating task is ([^""]*)")]
    public void GivenDueDateTimeWhichWillBeUsedForCreatingTaskIs(string deadline)
    {
        _taskRequestModel.DueDateTime = deadline;
    }

    [Given(@"require chosen outcome is ([^""]*)")]
    public void GivenRequireChosenOutcomeIs(string value)
    {
        _taskRequestModel.RequireChosenOutcome = value;
    }

    [Given(@"reviewers are ([^""]*) ([^""]*)")]
    public void GivenReviewersAre(string p0, string p1)
    {
        _taskRequestModel.Reviewers = new()
        {
            p0,
            p1
        };
    }

    [Given(@"number of minimum reviewers is ([^""]*)")]
    public void GivenNumberOfMinimumReviewersIs(string p0)
    {
        _taskRequestModel.MinimumReviewers = p0;
    }

    [Given(@"number of days in due_date_offset field is ([^""]*)")]
    public void GivenNumberOfDaysInDue_Date_OffsetFieldIs(string p0)
    {
        _taskRequestModel.DateOffset = new()
        {
            NumberOfDays = p0
        };
    }

    [Given(@"offset type in due_date_offset field is ([^""]*)")]
    public void GivenOffsetTypeInDue_Date_OffsetFieldIs(string p0)
    {
        _taskRequestModel.DateOffset = new()
        {
            OffsetType = p0
        };
    }

    [When(@"post task request is sent")]
    public async Task WhenPostTaskRequestIsSent()
    {
        _response = await _taskRequests.PostTaskAsync(_taskRequestModel, _requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"empty request is sent")]
    public async Task WhenEmptyRequestIsSent()
    {
        _response = await _taskRequests.SendEmptyRequestAsync(_requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from create task should be valid according to json schema")]
    public void ThenResponseBodyFromCreateTaskShouldBeValidAccordingToJsonSchema()
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
        var overdueResponse = taskResponseBody[ResponseConstants.TaskResponse.Overdue]?.ToString();
        _taskId = taskIdResponse!;

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
        overdueResponse.Should().NotBeNullOrEmpty();
    }

    [Then(@"response body from forbidden create task request should has message ""([^""]*)""")]
    public void ThenResponseBodyFromForbiddenCreateTaskRequestShouldHasMessage(string message)
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

    [Then(@"response body from bad create task request should has message ([^""]*) in the field ([^""]*)")]
    public void ThenResponseBodyFromBadCreateTaskRequestShouldHasMessageInTheField(string message, string field)
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

    [Then(@"I delete task")]
    public async Task ThenIDeleteTask()
    {
        await _taskRequests.DeleteTaskByIdAsync(_taskId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue, "DELETE", "testing");
    }
}
