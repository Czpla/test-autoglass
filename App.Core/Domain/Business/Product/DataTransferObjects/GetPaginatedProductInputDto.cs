namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;

    public class GetPaginatedProductInputDto
    {
        public int Id { get; set; } = default!;
        public string Situation { get; set; } = default!;

        public static implicit operator Product(GetPaginatedProductInputDto input)
        {
            return new Product
            {
                Id = input.Id,
                Situation = input.Situation
            };
        }
    }
}
