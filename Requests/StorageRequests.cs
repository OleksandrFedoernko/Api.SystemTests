using RestSharp;
using Api.SystemTests.Constants;
using Api.SystemTests.Models;
using static Api.SystemTests.Constants.ApiConstants.Routes.V1.Endpoints;

namespace Api.SystemTests.Requests;

public class StorageRequests
{
    private readonly RestClient _client = new(TestConfiguration.BaseUrl);
    public async Task<RestResponse> PostStorageAsync(StorageRequestModel storage, string storageId, string requestingUserId,
                                                     string requestingUserTypeValue, string userId)
    {
        var request = new RestRequest(Storages.StorageEndpoint)
        {
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        request.AddQueryParameter(HttpQueryParams.StorageId, storageId);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserTypeValue, userId);
        request.AddBody(storage);
        var response = await _client.ExecuteAsync<StorageRequestModel>(request);
        return response;
    }

    public async Task<RestResponse> GetStorageByIdAsync(string storageId, string requestingUserId, 
                                                        string requestingUserTypeValue, string userId)
    {
        var request = new RestRequest(Storages.StorageEndpoint + $"/{storageId}")
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserTypeValue, userId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> DeleteStorageByIdAsync(string storageId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Storages.StorageEndpoint + $"/{storageId}")
        {
            Method = Method.Delete,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> UpdateStorageByIdAsync(StorageRequestModel storage, string storageId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Storages.StorageEndpoint + $"/{storageId}")
        {
            Method = Method.Put,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        request.AddBody(storage);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> GetAllStoragesAsync(int limit, string name, string requestingUserId, string requestingUserType, string userId)
    {

        var request = new RestRequest(Storages.StorageEndpoint)
        {
            Method = Method.Get,
            RequestFormat = DataFormat.Json
        };
        request.AddQueryParameter(HttpQueryParams.Code, TestConfiguration.EnvCode);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        if (limit != 0)
        {
            request.AddQueryParameter(HttpQueryParams.Limit, limit);
        }
        if (name != null && name != string.Empty)
        {
            request.AddQueryParameter(HttpQueryParams.Name, name);
        }
        var response = await _client.ExecuteAsync(request);
        return response;

    }

    public async Task<RestResponse> UpdateStorageByIdEmptyRequestAsync(string storageId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Storages.StorageEndpoint + $"/{storageId}")
        {
            Method = Method.Put,
            RequestFormat = DataFormat.Json
        };
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> PostStorageEmptyRequestBodyAsync(string storageId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Storages.StorageEndpoint + $"/{storageId}")
        {
            Method = Method.Post,
            RequestFormat = DataFormat.Json
        };
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        var response = await _client.ExecuteAsync(request);
        return response;
    }

    public async Task<RestResponse> SendRStorageequestsWithoutKeysAsync(string storageId, string requestingUserId, string requestingUserType, string userId)
    {
        var request = new RestRequest(Storages.StorageEndpoint, Method.Get);
        RequestHelpers.AddHeaderParams(request, requestingUserId, requestingUserType, userId);
        await RequestHelpers.ExecuteRequestsWithoutKeyAsync(request, TestConfiguration.BaseUrl, Storages.StorageEndpoint, storageId);
        return await _client.ExecuteAsync(request);
    }
}
