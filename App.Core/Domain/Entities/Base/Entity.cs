namespace App.Core.Domain.Entities.Base
{
    using Shared.Either;
    using System;

    public class Entity : IEntityBase
    {
        public int Id { get; init; }
        public string CreatedBy { get; init; } = "application";
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "application";
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public void Modify() { UpdatedAt = DateTime.Now; }
        public Option<Exception> Modify(string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(updatedBy))
                return Option<Exception>.Some(new ArgumentNullException(nameof(updatedBy)));

            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.Now;

            return Option<Exception>.None;
        }

        public void Delete() { DeletedAt = DateTime.Now; }
        public Option<Exception> Delete(string deletedBy)
        {
            if (string.IsNullOrWhiteSpace(deletedBy))
                return Option<Exception>.Some(new ArgumentNullException(nameof(deletedBy)));

            DeletedBy = deletedBy;
            DeletedAt = DateTime.Now;

            return Option<Exception>.None;
        }

        public override string ToString()
        {
            return $"Id: {Id}, CreatedBy: {CreatedBy}, CreatedAt: {CreatedAt}, UpdatedBy: {UpdatedBy}, UpdatedAt: {UpdatedAt}, DeletedBy: {DeletedBy}, DeletedAt: {DeletedAt}";
        }

        public virtual Option<Exception> Validate() => Option<Exception>.None;
    }
}
