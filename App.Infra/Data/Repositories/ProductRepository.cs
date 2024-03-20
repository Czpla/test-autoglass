namespace App.Infra.Data.Repositories
{
    using Infra.Data.Context;
    using Core.Domain.Entities;
    using Core.Domain.Repositories;
    using Infra.Data.Repositories.Base;
    using Core.Domain.Constants;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System;
    using System.Linq;

    public class ProductRepository : RepositoryBase<PostgresContext, Product>, IProductRepository
    {
        public ProductRepository(PostgresContext context) : base(context) {  }

        public async Task<Product> UpdateSituationToInactive(Product product)
        {
            var existingProduct = await _context.Set<Product>().FindAsync(product.Id);

            if (existingProduct is not null) 
            {
                existingProduct.Situation = ProductSituation.Inactive.ToString();

                _context.Set<Product>().Update(existingProduct);

                await _context.SaveChangesAsync();

                return existingProduct;
            }

            return product;
        }

        public virtual Task<IEnumerable<Product>> GetPaginatedBy(int skip, int amount, Expression<Func<Product, bool>> where = default!, Func<Product, dynamic> orderBy = default!)
        {
            return Task.Run(
                () => _context
                        .Set<Product>()
                        .Where(where)
                        .OrderBy(orderBy)
                        .Skip(skip)
                        .Take(amount)
                        .ToArray()
                        .ToList() as IEnumerable<Product>
            );
        }
    }
}