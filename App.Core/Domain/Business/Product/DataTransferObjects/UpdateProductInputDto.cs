namespace App.Core.Domain.Business.Product.DataTransferObjects
{
    using Core.Domain.Entities;
    using System;

    public class UpdateProductInputDto
    {
        public int Id { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Situation { get; set; } = default!;
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? SupplierCode { get; set; }
        public string? SupplierDescription { get; set; }
        public string? SupplierCnpj { get; set; }

        public static implicit operator Product(UpdateProductInputDto input)
        {
            return new Product
            {
                Id = input.Id,
                Description = input.Description,
                Situation = input.Situation,
                ManufacturingDate = input.ManufacturingDate,
                ExpirationDate = input.ExpirationDate,
                SupplierCode = input.SupplierCode,
                SupplierDescription = input.SupplierDescription,
                SupplierCnpj = input.SupplierCnpj
            };
        }
    }
}
