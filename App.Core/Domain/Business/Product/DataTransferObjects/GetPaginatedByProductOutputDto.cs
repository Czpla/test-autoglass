namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;
    using System.Collections.Generic;

    public class GetPaginatedByProductOutputDto { 

        public IEnumerable<Product> Products { get; set; }

        public int Total { get; set; }

        public GetPaginatedByProductOutputDto(IEnumerable<Product> products, int total)
        {
            Products = products;
            Total = total;
        }
    }
}