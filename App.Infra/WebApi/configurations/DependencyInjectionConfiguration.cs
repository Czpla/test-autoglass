namespace App.Infra.WebApi.Configurations
{
    using Infra.Data.Context;
    using Infra.WebApi.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;

    public class DependencyInjectionConfiguration
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var databaseUrl = configuration.GetValue<string>("DatabaseUrl");

            services.AddDbContext<PostgresContext>();

            new ProductDependencyInjection().Configure(services, configuration);
        }

    }
}