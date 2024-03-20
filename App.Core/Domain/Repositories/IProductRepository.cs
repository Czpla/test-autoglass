namespace App.Core.Domain.Repositories
{
    using System;
    using Base;
    using Core.Domain.Entities;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IProductRepository : IRepositoryBase<Product>
    {
        public Task<Product> UpdateSituationToInactive(Product product);
        public Task<IEnumerable<Product>> GetPaginatedBy(int skip, int amount, Expression<Func<Product, bool>> where = default!, Func<Product, dynamic> orderBy = default!);
    }
}
