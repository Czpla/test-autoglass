namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;
    using Core.Domain.Constants;

    public class CreateProductInputDto
    {
        public int? Id { get; set; }
        public string Description { get; set; } = default!;
        public ProductSituation Situation { get; set; } = default!;

        public static implicit operator Product(CreateProductInputDto input)
        {
            return new Product
            {
                Id = input.Id ?? 1,
                Description = input.Description,
                Situation = input.Situation
            };
        }
    }
}
