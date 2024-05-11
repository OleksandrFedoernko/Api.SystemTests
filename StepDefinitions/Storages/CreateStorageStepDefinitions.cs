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
public class CreateStorageStepDefinitions
{
    private readonly StorageRequests _storageRequests = new();
    private readonly StorageRequestModel _storage = new();
    private RestResponse _response = new();
    private readonly Random _number = new();
    private readonly JSchema _storageResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/StorageResponseSchema.json"));
    private readonly JSchema _errorResponseSchema = JSchema.Parse(File.ReadAllText(@"Schema/ErrorResponseSchema.json"));
    private string _storageId = string.Empty;  
    private readonly ScenarioContext _context;

    public CreateStorageStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"id which will be used for creating storage is ([^""]*)")]
    public void GivenIdWhichWillBeUsedForCreatingStorageIs(string storageId)
    {
        var endId = _number.Next(1, 10001);
        _storageId = storageId + endId;
    }

    [Given(@"id which will be used for creating storage with same id is ""([^""]*)""")]
    public void GivenIdWhichWillBeUsedForCreatingStorageWithSameIdIs(string storageId)
    {
        _storageId = storageId;
    }

    [Given(@"id which will be used for creating bad storage is ([^""]*)")]
    public void GivenIdWhichWillBeUsedForCreatingBadStorageIs(string storageId)
    {
        _storageId = storageId;
    }

    [Given(@"name which will be used for creating storage is ([^""]*)")]
    public void GivenNameWhichWillBeUsedForCreatingStorageIs(string name)
    {
        _storage.Name = name;
    }

    [Given(@"icon which will be used for creating storage is ([^""]*)")]
    public void GivenIconWhichWillBeUsedForCreatingStorageIs(string icon)
    {
        _storage.Icon = icon;
    }

    [When(@"post storage request is sent")]
    public async Task WhenPostStorageRequestIsSentAsync()
    {
        var headerUserId = _context.Get<string>("user_id");
        var requestingUserType = _context.Get<string>("requesting_user_type");
        var requestingUserId = _context.Get<string>("requesting_user_id");
        _response = await _storageRequests.PostStorageAsync(_storage, _storageId, requestingUserId, requestingUserType, headerUserId);
        _context.Add("code", _response.StatusCode);
        var content = _response.Content!;
        var errorResponseBody = JObject.Parse(content);
        var errorCodeFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.ErrorCode]?.ToString();
        _context.Add("error_code", errorCodeFromResponse);
    }

    [Then(@"response body from post storage equals ([^""]*), ([^""]*), ([^""]*)")]
    public void ThenResponseBodyFromPostStorageEquals(string storageId, string name, string icon)
    {
        var content = _response.Content!;
        var storageResponseBody = JObject.Parse(content);
        var nameResponse = storageResponseBody[ResponseConstants.StorageResponse.Name]?.ToString();
        var storageIdResponse = storageResponseBody[ResponseConstants.StorageResponse.StorageId]?.ToString();
        var iconResponse = storageResponseBody[ResponseConstants.StorageResponse.Icon]?.ToString();
        var responseSchemaValidation = storageResponseBody.IsValid(_storageResponseSchema);

        storageIdResponse.Should().NotBeNull();
        storageIdResponse.Should().Contain(storageId);

        if (!string.IsNullOrWhiteSpace(_storage.Name))
        {
            nameResponse.Should().NotBeNullOrEmpty();
            nameResponse.Should().Be(name);
        }
        else
        {
            nameResponse.Should().BeNullOrEmpty();
        }

        if (!string.IsNullOrWhiteSpace(_storage.Icon))
        {
            iconResponse.Should().Be(icon);
            iconResponse.Should().NotBeNullOrEmpty();
        }
        else
        {
            iconResponse.Should().BeNullOrEmpty();
        }

        responseSchemaValidation.Should().BeTrue();
    }

    [Then(@"forbidden request message from create storage request should have text ""([^""]*)""")]
    public void ThenForbiddenRequestMessageFromCreateStorageRequestShouldHaveText(string message)
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

    [Then(@"bad request message from create storage request should have text ([^""]*) in field ([^""]*)")]
    public void ThenBadRequestMessageFromCreateStorageRequestShouldHaveTextInField(string message, string field)
    {
        var content = _response.Content!;
        var actualStatusCode = (int)HttpStatusCode.BadRequest;
        var errorResponseBody = JObject.Parse(content);
        var responseField = errorResponseBody[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Field]?.ToString();
        var statusFromResponse = errorResponseBody[ResponseConstants.ErrorResponse.Status]?.ToString();
        var validationMessage = errorResponseBody[ResponseConstants.ErrorResponse.ValidationMessages]?[0]?[ResponseConstants.ErrorResponse.Message]?.ToString();
        var errorSchemaValidation = errorResponseBody.IsValid(_errorResponseSchema);
        if (field == null)
        {
            responseField.Should().BeEmpty();
        }
        statusFromResponse.Should().NotBeNull();
        statusFromResponse.Should().Be(actualStatusCode.ToString());
        validationMessage.Should().NotBeNullOrWhiteSpace();
        validationMessage.Should().Be(message);
        errorSchemaValidation.Should().BeTrue();
    }

    [Then(@"I delete storage")]
    public async Task ThenIDeleteStorage()
    {
        await _storageRequests.DeleteStorageByIdAsync(_storageId, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue);
    }
}
