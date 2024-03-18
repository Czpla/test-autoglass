namespace App.Infra.Data.Repositories
{
    using Infra.Data.Context;
    using Core.Domain.Entities;
    using Core.Domain.Repositories;
    using Infra.Data.Repositories.Base;

    public class ProductRepository : RepositoryBase<PostgresContext, Product>, IProductRepository
    {
        public ProductRepository(PostgresContext context) : base(context) { }
    }
}