using RestSharp;
using Api.SystemTests.Constants;

namespace Api.SystemTests.Requests;

public static class RequestHelpers
{
    internal static RestRequest AddHeaderParams(RestRequest request, string requestingUserId, string requestingUserType, string headerUserId)
    {
        return request.AddHeader(HttpHeaders.RequestingUserId, requestingUserId)
            .AddHeader(HttpHeaders.RequestingUser, HttpHeadersValues.RequestingUserValue)
            .AddHeader(HttpHeaders.RequestingUserType, requestingUserType)
            .AddHeader(HttpHeaders.UserId, headerUserId);
    }

    internal static async Task<RestResponse> PrepareRequestsWithoutKeysAsync(RestRequest request, string baseUrl, string resource, Method method)
    {
        var client = new RestClient(baseUrl);
        request = new RestRequest(resource, method);
        AddHeaderParams(request, HttpHeadersValues.RequestingUserIdValue, HttpHeadersValues.RequestingUserTypeValue, HttpHeadersValues.UserIdValue);
        return await client.ExecuteAsync(request);
    }

    internal static async Task<RestRequest> ExecuteRequestsWithoutKeyAsync(RestRequest request, string baseUrl, string endpoint, string? id)
    {
        await PrepareRequestsWithoutKeysAsync(request, baseUrl, endpoint, Method.Post);
        await PrepareRequestsWithoutKeysAsync(request, baseUrl, endpoint + $"/{id}", Method.Put);
        await PrepareRequestsWithoutKeysAsync(request, baseUrl, endpoint + $"/{id}", Method.Get);
        if (!endpoint.Contains(ApiConstants.Routes.V1.Endpoints.Users.UserEndpoint))
        {
            await PrepareRequestsWithoutKeysAsync(request, baseUrl, endpoint + $"/{id}", Method.Delete);
        }
        return request;
    }
}

