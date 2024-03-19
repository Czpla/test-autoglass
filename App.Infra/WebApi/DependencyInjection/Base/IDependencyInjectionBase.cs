namespace App.Infra.WebApi.DependencyInjection.Base
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;

    public interface IDependencyInjectionBase
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
