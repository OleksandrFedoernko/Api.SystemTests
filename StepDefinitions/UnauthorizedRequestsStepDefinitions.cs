using Api.SystemTests.Requests;
using RestSharp;
using TechTalk.SpecFlow;

namespace Api.SystemTests.StepDefinitions;

[Binding]
public class UnauthorizedRequestsStepDefinitions
{
    private readonly StorageRequests _storageRequests = new();
    private readonly UserRequests _userRequests = new();
    private readonly TaskRequests _taskRequests = new();
    private RestResponse _response = new();
    private string _id = string.Empty;
    private string _userId = string.Empty;
    private string _requestingUserId = string.Empty;
    private string _requestingUserType = string.Empty;
    private readonly ScenarioContext _context;
    public UnauthorizedRequestsStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"requesting user id which will be used for unauthorized requests is ""([^""]*)""")]
    public void GivenRequestingUserIdWhichWillBeUsedForUnauthorizedRequestsIs(string requestingUserId)
    {
        _requestingUserId = requestingUserId;
    }

    [Given(@"requesting user type which will be used for unauthorized requests is ""([^""]*)""")]
    public void GivenRequestingUserTypeWhichWillBeUsedForUnauthorizedRequestsIs(string requestingUserType)
    {
        _requestingUserType = requestingUserType;
    }

    [Given(@"header user id which will be used for unauthorized requests is ""([^""]*)""")]
    public void GivenHeaderUserIdWhichWillBeUsedForUnauthorizedRequestsIs(string userId)
    {
        _userId = userId;
    }

    [Given(@"id which will be used as a path parameter in the unauthorized requests is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedAsAPathParameterInTheUnauthorizedRequestsIs(string pathId)
    {
        _id = pathId;
    }

    [When(@"unauthorized requests are sent")]
    public async Task WhenUnauthorizedRequestsAreSent()
    {
        _response = await _storageRequests.SendRStorageequestsWithoutKeysAsync(_id, _requestingUserId, _requestingUserType, _userId);
        _response = await _taskRequests.SendTaskRequestsWithoutKeysAsync(_id, _requestingUserId, _requestingUserType, _userId);
        _response = await _userRequests.SendUserRequestsWithoutKeysAsync(_id, _requestingUserId, _requestingUserType, _userId);
        _context.Add("code", _response.StatusCode);

    }
}
