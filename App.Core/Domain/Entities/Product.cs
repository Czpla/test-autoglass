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
                    new ArgumentException("Invalid situation value", nameof(Situation))
                );

            return Option<Exception>.None;
        }

        // public void Delete()
        // {
        //     Status = Status.Deleted;
        // }

        // public Option<Exception> Delete(string deletedBy)
        // {
        //     if (string.IsNullOrEmpty(deletedBy))
        //         return Option<Exception>.Some(
        //             new ArgumentNullException(nameof(deletedBy))
        //         );

        //     DeletedBy = deletedBy;
        //     Delete();
        //     return Option<Exception>.None;
        // }

        // public void Modify()
        // {
        //     Status = Status.Modified;
        // }

        // public Option<Exception> Modify(string updatedBy)
        // {
        //     if (string.IsNullOrEmpty(updatedBy))
        //         return Option<Exception>.Some(
        //             new ArgumentNullException(nameof(updatedBy))
        //         );

        //     UpdatedBy = updatedBy;
        //     Modify();
        //     return Option<Exception>.None;
        // }

        public override string ToString()
            => $"{base.ToString()}, Description: {Description}, Situation: {Situation}";
    }
}