using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;

namespace Api.SystemTests.StepDefinitions.Tasks;

[Binding]
public class GetAllTasksStepDefinitions
{
    private readonly TaskRequests _taskRequests = new();
    private RestResponse _response = new();
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private readonly JSchema _getAllTasksResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/GetAllTasksResponseSchema.json"));
    private string _headerUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _requestingUserId = string.Empty;
    private int _limit;
    private string _storageId = string.Empty;
    private string _fields = string.Empty;
    private readonly ScenarioContext _context;
    public GetAllTasksStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"requesting user id which will be used for getting all tasks is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingAllTasksIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user ids which will be used for getting tasks for another user types ([^""]*)")]
    public void GivenRequestingUserIdsWhichWillBeUsedForGettingTasksForAnotherUserTypes(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for getting all tasks is ([^""]*)")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingAllTasksIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for getting all tasks is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForGettingAllTasksIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"wrong header user id which will be used for getting all tasks is ([^""]*)")]
    public void GivenWrongHeaderUserIdWhichWillBeUsedForGettingAllTasksIs(string headerUserId)
    {
        _headerUserId = headerUserId;
    }

    [Given(@"limit which will be used for getting all tasks is ([^""]*)")]
    public void GivenLimitWhichWillBeUsedForGettingAllTasksIs(int limit)
    {
        _limit = limit;
    }

    [Given(@"fields which will be used for getting all tasks is ([^""]*)")]
    public void GivenFieldsWhichWillBeUsedForGettingAllTasksIs(string fields)
    {
        _fields = fields;
    }

    [Given(@"storage id which will be used for getting all storages is ([^""]*)")]
    public void GivenStorageIdWhichWillBeUsedForGettingAllStoragesIs(string storageId)
    {
        _storageId = storageId;
    }

    [When(@"get all tasks request is sent")]
    public async Task WhenGetAllTasksRequestIsSent()
    {
        _response = await _taskRequests.GetAllTasksAsync(_limit, _fields, _storageId, _requestingUserId, _requestingUserType, _headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from get all tasks should be equal to ([^""]*) ([^""]*)")]
    public void ThenResponseBodyFromGetAllTasksFromParticipantAndEmployerUserTypesShouldBeEqualTo(string storageId, string createdBy)
    {
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        var tasksListResponse = (JArray)responseBody[ResponseConstants.PaginationResponse.Items]!;
        var limit = (int)responseBody[ResponseConstants.PaginationResponse.Pagination]![ResponseConstants.PaginationResponse.Limit]!;
        var schemaValidation = responseBody.IsValid(_getAllTasksResponseSchema);
        var index = Enumerable.Range(0, limit);
        if (!string.IsNullOrEmpty(_storageId))
        {
            foreach (var num in index)
            {
                var storageIdList = tasksListResponse[num][ResponseConstants.TaskResponse.StorageId]?.ToString();
                var createdByList = tasksListResponse[num][ResponseConstants.TaskResponse.CreatedBy]?.ToString();
                var taskIdList = tasksListResponse[num][ResponseConstants.TaskResponse.TaskId]?.ToString();

                storageIdList.Should().Be(storageId);
                createdByList.Should().Be(createdBy);
                taskIdList.Should().NotBeNullOrEmpty();
                schemaValidation.Should().BeTrue();
            }
        }

        if (!string.IsNullOrEmpty(_fields))
        {
            foreach (var num in index)
            {
                var priorityResponse = (int?)tasksListResponse[num][ResponseConstants.TaskResponse.Priority];
                priorityResponse.Should().BeInRange(50, 100);
            }
        }
        tasksListResponse.Should().HaveCount(limit);
    }


    [Then(@"forbidden request message from get all tasks request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromGetAllTasksRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var expectedStatusCodeResult = (int)HttpStatusCode.Forbidden;
        var errorResponseBody = JObject.Parse(content);
        var responseMessage = errorResponseBody[ResponseConstants.ErrorResponse.Message]?.ToString();
        var responseStatus = errorResponseBody[ResponseConstants.ErrorResponse.Status]?.ToString();
        var errorSchemaValidation = errorResponseBody.IsValid(_errorResponseSchema);

        responseMessage.Should().Contain(message);
        responseMessage.Should().StartWith(message);
        responseMessage.Should().NotBeNullOrEmpty();
        responseStatus.Should().Be(expectedStatusCodeResult.ToString());
        responseStatus.Should().NotBe(null);
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"bad request message from get all tasks request should have text ([^""]*) in the field ([^""]*)")]
    public void ThenBadRequestMessageFromGetAllTasksRequestShouldHaveTextInTheField(string message, string field)
    {
        var content = _response.Content!;
        var responseBody = JObject.Parse(content);
        var expectedStatusCodeResult = (int)HttpStatusCode.BadRequest;
        var statusResponse = responseBody[ResponseConstants.ErrorResponse.Status]?.ToString();
        var fieldResponse = responseBody[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Field]?.ToString();
        var messageResponse = responseBody[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Message]?.ToString();
        var schemaValidation = responseBody.IsValid(_errorResponseSchema);

        responseBody.Should().NotBeNullOrEmpty();
        statusResponse.Should().Be(expectedStatusCodeResult.ToString());
        statusResponse.Should().NotBeNullOrEmpty();
        fieldResponse.Should().Be(field);
        fieldResponse.Should().NotBeNullOrEmpty();
        messageResponse.Should().Be(message);
        fieldResponse.Should().NotBeNullOrEmpty();
        schemaValidation.Should().BeTrue();
    }
}
