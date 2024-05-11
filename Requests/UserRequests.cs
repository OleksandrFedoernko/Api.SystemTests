using RestSharp;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using static Api.SystemTests.Constants.ApiConstants.Routes.V1.Endpoints;

namespace Api.SystemTests.Requests;

public class UserRequests
{
    private readonly RestClient _client = new(TestConfiguration.BaseUrl);

    public async Task<RestResponse> PostUserAsync(UserRequestModel user, string userId, string requestingUserId, string requestingUserType, string headerUserId)
    {
        var request = new RestRequest(Users.UserEndpoint)
        {
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        };
        Guid idempotencyKeyValue = Guid.NewGuid();
        request.AddQueryParameter(HttpQueryParams.UserId, userId);
        if (!string.IsNullOrWhiteSpace(TestConfiguration.EnvCode))
        {
            request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        }

        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, headerUserId);
        request.AddHeader(HttpHeaders.IdempotencyKey, idempotencyKeyValue);
        request.AddBody(user);
        var response = await _client.ExecuteAsync<UserRequestModel>(request);
        return response;
    }

    public async Task<RestResponse> UpdateUserAsync(UserRequestModel user, string userId, string requestingUserId, string requestingUserType, string headerUserId)
    {
        var request = new RestRequest(Users.UserEndpoint + $"/{userId}")
        {
            Method = Method.Put,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, headerUserId);
        request.AddBody(user);
        var response = await _client.ExecuteAsync<UserRequestModel>(request);
        return response;
    }

    public async Task<RestResponse> SendEmptyRequestBody(string requestingUserId, string requestingUserType, string headerUserId)
    {
        var request = new RestRequest(Users.UserEndpoint)
        {
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        };
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, headerUserId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> GetUserByIdAsync(string requestingUserId, string requestingUserType, string headerUserId, string pathUserId)
    {
        var request = new RestRequest(Users.UserEndpoint + $"/{pathUserId}")
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, headerUserId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> GetAllUsersAsync(string requestingUserId, string requestingUserType, string headerUserId)
    {
        var request = new RestRequest(Users.UserEndpoint)
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, headerUserId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }
    public async Task<RestResponse> SendUserRequestsWithoutKeysAsync(string pathUserId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Tasks.TaskEndpoint, Method.Get);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        await RequestHelpers.ExecuteRequestsWithoutKeyAsync(request, TestConfiguration.BaseUrl, Users.UserEndpoint, pathUserId);
        return await _client.ExecuteAsync(request);
    }
}
