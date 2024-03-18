namespace App.Infra.WebApi.Configurations
{
    using Shared.DotEnv;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System;

    public class EnvironmentsConfiguration
    {
        public void Configure(IConfigurationBuilder configuration)
        {
            DotEnv.Load(
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env")
            );

            configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

            configuration.AddEnvironmentVariables();
        }
    }
}
