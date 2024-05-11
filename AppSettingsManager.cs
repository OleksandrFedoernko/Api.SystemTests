using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace Api.SystemTests;

public static class AppSettingsManager
{
    public static IConfiguration? Configuration { get; set; }

    [ScenarioDependencies]
    public static IServiceCollection Manager()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var services = new ServiceCollection();

        services.AddSingleton(Configuration);

        return services;
    }


}
