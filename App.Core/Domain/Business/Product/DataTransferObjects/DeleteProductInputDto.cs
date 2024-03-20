namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;
    using Core.Domain.Constants;

    public class DeleteProductInputDto
    {
        public int Id { get; set; } = default!;
        public string Situation { get; set; } = default!;

        public static implicit operator Product(DeleteProductInputDto input)
        {
            return new Product
            {
                Id = input.Id,
                Situation = ProductSituation.Inactive.ToString()
            };
        }
    }
}
