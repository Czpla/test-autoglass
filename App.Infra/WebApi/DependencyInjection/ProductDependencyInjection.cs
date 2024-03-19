namespace App.Infra.WebApi.DependencyInjection
{
    using Base;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Core.Business;
    using Infra.Data.Repositories;
    using Core.Domain.Repositories;
    using Core.Domain.Business.Product;

    public class ProductDependencyInjection : IDependencyInjectionBase
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
        }
    }
}