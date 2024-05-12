using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Microsoft.Extensions.Configuration;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Infrastructure;

public class Configuration
{
    public static IDetectionBoosterApi Configure(IConfigurationRoot configuration)
    {
        return new DetectionBoosterApi(
                Environment.GetEnvironmentVariable("API_URL") ?? configuration["API_URL"],
                Environment.GetEnvironmentVariable("API_KEY") ?? configuration["API_KEY"]
        );
    }
}