using RestSharp;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using static Api.SystemTests.Constants.ApiConstants.Routes.V1.Endpoints;

namespace Api.SystemTests.Requests;

public class TaskRequests
{
    private readonly RestClient _client = new(TestConfiguration.BaseUrl);
    public async Task<RestResponse> PostTaskAsync(TaskRequestModel task, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Tasks.TaskEndpoint)
        {
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        };
        Guid idempotencyKey = Guid.NewGuid();
        if (!string.IsNullOrWhiteSpace(TestConfiguration.EnvCode))
        {
            request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        }
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        request.AddHeader(HttpHeaders.IdempotencyKey, idempotencyKey);
        request.AddBody(task);
        var response = await _client.ExecuteAsync<TaskRequestModel>(request);
        return response;
    }

    public async Task<RestResponse> GetTaskByIdAsync(string taskId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Tasks.TaskEndpoint + $"/{taskId}")
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        var response = await _client.ExecuteAsync(request);
        return response;

    }

    public async Task<RestResponse> GetAllTasksAsync(int limit, string fields, string storageId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Tasks.TaskEndpoint)
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        if (limit != 0)
        {
            request.AddQueryParameter(HttpQueryParams.Limit, limit);
        }
        if (!string.IsNullOrEmpty(fields))
        {
            request.AddQueryParameter(HttpQueryParams.Fields, fields);
        }
        if (!string.IsNullOrEmpty(storageId))
        {
            request.AddQueryParameter(HttpQueryParams.StorageId, storageId);
        }
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> GetTaskHistoryByIdAsync(string taskId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Tasks.TaskEndpoint + $"/{taskId}/history")
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> UpdateTaskAsync(TaskRequestModel task, string taskId, string requestingUserId, string requestingUserType, string userId, string comment)
    {
        var request = new RestRequest(Tasks.TaskEndpoint + $"/{taskId}")
        {
            Method = Method.Put,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        if (!string.IsNullOrEmpty(comment))
        {
            request.AddQueryParameter(HttpQueryParams.Comment, comment);
        }
        request.AddBody(task);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> DeleteTaskByIdAsync(string taskId, string requestingUserId, string requestingUserType, string userId, string mode, string comment)
    {
        var request = new RestRequest(Tasks.TaskEndpoint + $"/{taskId}")
        {
            Method = Method.Delete,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.DeleteMode, mode);
        request.AddQueryParameter(HttpQueryParams.Comment, comment);
        if (!string.IsNullOrWhiteSpace(TestConfiguration.EnvCode))
        {
            request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        }

        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> SendEmptyRequestAsync(string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Tasks.TaskEndpoint)
        {
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        };
        Guid idempotencyKey = Guid.NewGuid();
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        request.AddHeader(HttpHeaders.IdempotencyKey, idempotencyKey);
        var response = await _client.ExecuteAsync<TaskRequestModel>(request);

        return response;
    }

    public async Task<RestResponse> SendTaskRequestsWithoutKeysAsync(string taskId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Tasks.TaskEndpoint, Method.Get);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        await RequestHelpers.ExecuteRequestsWithoutKeyAsync(request, TestConfiguration.BaseUrl, Tasks.TaskEndpoint, taskId);
        return await _client.ExecuteAsync(request);
    }
}
