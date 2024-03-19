namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;

    public class GetPaginatedProductOutputDto { 

        public Product[] Products { get; set; }

        public int Total { get; set; }

        public GetPaginatedProductOutputDto(Product[] products, int total)
        {
            Products = products;
            Total = total;
        }
    }
}