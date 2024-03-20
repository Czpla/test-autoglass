namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;

    public class GetPaginatedByProductInputDto
    {
        public int Id { get; set; } = default!;
        public string Situation { get; set; } = default!;

        public static implicit operator Product(GetPaginatedByProductInputDto input)
        {
            return new Product
            {
                Id = input.Id,
                Situation = input.Situation
            };
        }
    }
}
