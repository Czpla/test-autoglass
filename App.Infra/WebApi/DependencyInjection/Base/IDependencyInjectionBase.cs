namespace App.Infra.WebApi.DependencyInjection.Base
{
    public interface IDependencyInjectionBase
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
