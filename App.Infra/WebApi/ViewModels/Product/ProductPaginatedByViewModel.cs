namespace App.Infra.WebApi.ViewModels.Product
{
    using System;
    using System.Text.Json.Serialization;
    using Core.Domain.Entities;
    using Core.Domain.Constants;
    using System.Collections.Generic;

    public class ProductPaginatedByViewModel
    {
        [JsonPropertyName("products")]
        public IEnumerable<ProductViewModel> Products { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}