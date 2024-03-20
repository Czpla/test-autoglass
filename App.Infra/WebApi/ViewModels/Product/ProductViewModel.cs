namespace App.Infra.WebApi.ViewModels.Product
{
    using System;
    using System.Text.Json.Serialization;
    using Core.Domain.Entities;
    using Core.Domain.Constants;

    public class ProductViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;

        [JsonPropertyName("situation")]
        public string Situation { get; set; } = default!;

        [JsonPropertyName("manufacturingDate")]
        public DateTime? ManufacturingDate { get; set; }

        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }

        [JsonPropertyName("supplierCode")]
        public int? SupplierCode { get; set; }

        [JsonPropertyName("supplierDescription")]
        public string? SupplierDescription { get; set; }

        [JsonPropertyName("supplierCnpj")]
        public string? SupplierCnpj { get; set; }

        public static implicit operator ProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Situation = product.Situation,
                ManufacturingDate = product.ManufacturingDate,
                ExpirationDate = product.ExpirationDate,
                SupplierCode = product.SupplierCode,
                SupplierDescription = product.SupplierDescription,
                SupplierCnpj = product.SupplierCnpj
            };
        }
    }
}