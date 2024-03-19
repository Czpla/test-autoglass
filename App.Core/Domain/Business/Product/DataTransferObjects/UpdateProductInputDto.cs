namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;

    public class UpdateProductInputDto
    {
        public int Id { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Situation { get; set; } = default!;

        public static implicit operator Product(UpdateProductInputDto input)
        {
            return new Product
            {
                Id = input.Id,
                Description = input.Description,
                Situation = input.Situation
            };
        }
    }
}
