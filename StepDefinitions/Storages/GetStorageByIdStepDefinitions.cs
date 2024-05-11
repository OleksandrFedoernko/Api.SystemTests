using System.Net;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using Api.SystemTests.Requests;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using TechTalk.SpecFlow;

namespace Api.SystemTests.StepDefinitions.Storages;

[Binding]
public class GetStorageByIdStepDefinitions
{
    private readonly StorageRequests _storageRequests = new();
    private readonly StorageRequestModel _storageRequestModelForBoth = new();
    private readonly StorageRequestModel _storageRequestModelForEmployer = new();
    private readonly StorageRequestModel _storageRequestModelForParticipant = new();
    private RestResponse _response = new();
    private readonly JSchema _storageResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/StorageResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _storageId = string.Empty;
    private readonly Random _random = new();
    private string _employerStorageId = string.Empty;
    private string _partStorageId = string.Empty;
    private string _nonExistingStorageId = string.Empty;
    private string _userId = string.Empty;
    private string _requestingUserType = string.Empty;
    private string _requestingUserId = string.Empty;
    private readonly ScenarioContext _context;
    public GetStorageByIdStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"ids which will be used for creating storages are ""([^""]*)"",""([^""]*)"",""([^""]*)""")]
    public void GivenIdsWhichWillBeUsedForCreatingstoragesAre(string forBothId, string participantId, string employerId)
    {
        var endId = _random.Next(1, 10001);

        _partStorageId = participantId + endId.ToString();
        _employerStorageId = employerId + endId.ToString();
    }

    [Given(@"name which will be used for creating storages are ""([^""]*)"", ""([^""]*)"", ""([^""]*)""")]
    public void GivenNameWhichWillBeUsedForCreatingstoragesAre(string nameForBoth, string nameForParticipant, string nameForEmployer)
    {
        _storageRequestModelForBoth.Name = nameForBoth;
        _storageRequestModelForParticipant.Name = nameForParticipant;
        _storageRequestModelForEmployer.Name = nameForEmployer;
    }

    [Given(@"icon which will be used for creating storages are ""([^""]*)"", ""([^""]*)"", ""([^""]*)""")]
    public void GivenIconWhichWillBeUsedForCreatingstoragesAre(string iconForBoth, string iconForParticipant, string iconForEmployer)
    {
        _storageRequestModelForBoth.Icon = iconForBoth;
        _storageRequestModelForParticipant.Icon = iconForParticipant;
        _storageRequestModelForEmployer.Icon = iconForEmployer;
    }

    [Given(@"requesting user id which will be used for creating storage is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForCreatingstorageIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for creating storage is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForCreatingstorageIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for creating storage is ""([^""]*)""")]
    public void GivenHeaderuserIdWhichWillBeUsedForCreatingstorageIs(string headeruserId)
    {
        _userId = headeruserId;
    }

    [When(@"post storage request with different ids is sent")]
    public async Task WhenPoststorageRequestWithDifferentIdsIsSent()
    {
        _response = await _storageRequests.PostStorageAsync(_storageRequestModelForParticipant, _partStorageId, _requestingUserId, _requestingUserType, _userId);
        _response = await _storageRequests.PostStorageAsync(_storageRequestModelForEmployer, _employerStorageId, _requestingUserId, _requestingUserType, _userId);
    }

    [Then(@"I save created storage ids")]
    public void ThenISaveCreatedstorageIds()
    {
        var responseBody = JObject.Parse(_response.Content!);
        var storageId = responseBody[ResponseConstants.StorageResponse.StorageId]?.ToString();
        _storageId = storageId!;
    }

    [Given(@"id which will be used for getting storage is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForGettingstorageIs(string placeholder)
    {
        _ = _storageId;
    }

    [Given(@"requesting user ids which are ([^""]*)")]
    public void GivenRequestingUserIdsWhichAre(string userId)
    {
        _requestingUserId = userId;
    }

    [Given(@"requesting user type which is ([^""]*)")]
    public void GivenRequestingUserTypeWhichIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"id which will be used for getting storage with employer type is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForGettingstorageWithEmployerTypeIs(string placeholder)
    {
        _ = _employerStorageId;
    }

    [Given(@"id which will be used for getting storage with participant type is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForGettingstorageWithParticipantTypeIs(string placeholder)
    {
        _ = _partStorageId;
    }
    [Given(@"non-existent id which will be used for getting storage is ([^""]*)")]
    public void GivenNonExistingIdWhichWillBeUsedForGettingstorageIs(string notFoundId)
    {
        _nonExistingStorageId = notFoundId;
    }

    [Given(@"requesting user id which will be used for getting storage with employer type is the ending of a employer storage id")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingstorageWithEmployerTypeIsIsTheEndingOfAEmployerstorageId()
    {
        _requestingUserId = _employerStorageId;

    }
    [Given(@"requesting user id which will be used for getting storage with participant type is the ending of a participant storage id")]
    public void GivenRequestingUserIdWhichWillBeUsedForGettingstorageWithParticipantTypeIsIsTheEndingOfAEmployerstorageId()
    {
        _requestingUserId = _partStorageId;
    }


    [Given(@"requesting user type which will be used for getting storage is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForGettingstorageWithEmployerTypeIsIs(string userType)
    {
        _requestingUserType = userType;
    }

    [When(@"get storage by id request is sent")]
    public async Task WhenGetstorageByIdRequestIsSent()
    {
        _response = await _storageRequests.GetStorageByIdAsync(_storageId, _requestingUserId, _requestingUserType, _userId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get storage by id request with employer type is sent")]
    public async Task WhenGetstorageByIdRequestWithEmployerTypeIsSent()
    {
        _response = await _storageRequests.GetStorageByIdAsync(_employerStorageId, _requestingUserId.Remove(0, 16), _requestingUserType, _userId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get storage by id request with participant type is sent")]
    public async Task WhenGetstorageByIdRequestWithParticipantTypeIsSent()
    {
        _response = await _storageRequests.GetStorageByIdAsync(_partStorageId, _requestingUserId.Remove(0, 19), _requestingUserType, _userId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [When(@"get storage request with non-existent id is sent")]
    public async Task WhenGetstorageRequestWithNonExistingIdIsSent()
    {
        _response = await _storageRequests.GetStorageByIdAsync(_nonExistingStorageId, _requestingUserId, _requestingUserType, _userId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from get storage by id equals ([^""]*), ([^""]*)")]
    public void ThenResponseBodyFromGetstorageByIdEquals(string name, string icon)
    {
        var content = _response.Content!;
        var storages = JObject.Parse(content);
        var storageIdResponse = storages[ResponseConstants.StorageResponse.StorageId]?.ToString();
        var storageNameResponse = storages[ResponseConstants.StorageResponse.Name]?.ToString();
        var storageIconResponse = storages[ResponseConstants.StorageResponse.Icon]?.ToString();
        var schemaValidation = storages.IsValid(_storageResponseSchema);

        storageNameResponse.Should().Be(name);
        storageIconResponse.Should().Be(icon);
        storageIdResponse.Should().NotBeNull();
        storageNameResponse.Should().NotBeNull();
        storageIconResponse.Should().NotBeNull();
        schemaValidation.Should().BeTrue();
    }

    [Then(@"status codes after unsuccessful get storage by id should be Not Found and Forbidden")]
    public void ThenStatusCodesAfterUnsuccessfulGetstorageByIdShouldBeNotFoundAndForbidden()
    {
        
        var actualResponseMessageResult = new HttpResponseMessage(_response.StatusCode);
        if (_requestingUserType != "CLIENT_SYSTEM")
        {
            actualResponseMessageResult.Should().HaveStatusCode(HttpStatusCode.Forbidden);
        }
        else
        {
            actualResponseMessageResult.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }
    }

    [Then(@"error message from get storage by id request should have text ([^""]*)")]
    public void ThenForbiddenRequestMessageFromGetstorageByIdRequestShouldHaveText(string message)
    {
        var content = _response.Content!;
        var errorResponse = JObject.Parse(content);
        errorResponse.Should().NotBeNull();

        var errorSchemaValidation = errorResponse?.IsValid(_errorResponseSchema);
        var errorResponseMessage = errorResponse?[ResponseConstants.ErrorResponse.Message]?.ToString();
        errorResponse.Should().NotBeNull();
        errorResponseMessage.Should().Be(message);
        errorResponseMessage.Should().NotBeNull();
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"I delete created storage")]
    public async Task ThenIDeleteCreatedstorage()
    {
        await _storageRequests.DeleteStorageByIdAsync(_employerStorageId, _requestingUserId, HttpHeadersValues.RequestingUserTypeValue, _userId);
        await _storageRequests.DeleteStorageByIdAsync(_partStorageId, _requestingUserId, HttpHeadersValues.RequestingUserTypeValue, _userId);
    }
}
