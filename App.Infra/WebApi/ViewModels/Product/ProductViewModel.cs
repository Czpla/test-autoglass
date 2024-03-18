namespace App.Infra.WebApi.ViewModels.Product
{
    using System;
    using System.Text.Json.Serialization;

    using Core.Domain.Entities;

    public class ProductViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;

        [JsonPropertyName("situation")]
        public string Situation { get; set; } = default!;

        public static implicit operator ProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Situation = product.Situation
            };
        }
    }
}