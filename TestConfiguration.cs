namespace Api.SystemTests;

public static class TestConfiguration
{
    public static string BaseUrl { get => AppSettingsManager.Configuration!["ApiBaseUrl"]!; }
    public static string EnvCode { get => AppSettingsManager.Configuration!["EnvCode"]!; }
}
