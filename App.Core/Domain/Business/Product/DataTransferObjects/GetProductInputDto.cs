namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;

    public class GetProductInputDto
    {
        public int Id { get; set; } = default!;

        public static implicit operator Product(GetProductInputDto input)
        {
            return new Product
            {
                Id = input.Id,
            };
        }
    }
}
