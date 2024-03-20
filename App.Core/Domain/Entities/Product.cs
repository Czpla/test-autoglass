namespace App.Core.Domain.Entities
{
    using Base;
    using Shared.Either;
    using Behavior;
    using Constants;
    using System;

    public class Product : Entity, ILogicallyDeletable
    {
        public string Description { get; set; } = default!;
        public string Situation { get; set; } = default!;
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? SupplierCode { get; set; }
        public string? SupplierDescription { get; set; }
        public string? SupplierCnpj { get; set; }
 

        public override Option<Exception> Validate()
        {
            if (string.IsNullOrEmpty(Description))
                return Option<Exception>.Some(
                    new ArgumentNullException(nameof(Description))
                );

            if (string.IsNullOrEmpty(Situation))
                return Option<Exception>.Some(
                    new ArgumentNullException(nameof(Situation))
                );

            if (!Enum.IsDefined(typeof(ProductSituation), Situation))
                return Option<Exception>.Some(
                    new ArgumentException("Invalid situation value.", nameof(Situation))
                );

            if (ManufacturingDate.HasValue && ExpirationDate.HasValue && ManufacturingDate >= ExpirationDate)
                return Option<Exception>.Some(
                    new ArgumentException("Manufacturing date cannot be greater than or equal to expiration date.")
                );

            return Option<Exception>.None;
        }

        public override string ToString()
            => $"{base.ToString()}, Description: {Description}, Situation: {Situation}, ManufacturingDate: {ManufacturingDate}, ExpirationDate: {ExpirationDate}, SupplierCode: {SupplierCode}, SupplierDescription: {SupplierDescription}, SupplierCnpj: {SupplierCnpj}";
    }
}