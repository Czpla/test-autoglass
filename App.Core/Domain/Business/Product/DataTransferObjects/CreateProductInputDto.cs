namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;
    using Core.Domain.Constants;
    using System;

    public class CreateProductInputDto
    {
        public string Description { get; set; } = default!;
        public string Situation { get; set; } = default!;

        public static implicit operator Product(CreateProductInputDto input)
        {
            return new Product
            {
                Description = input.Description,
                Situation = input.Situation
            };
        }
    }
}
