namespace App.Core.Domain.Entities.Base
{
    using Shared.Either;
    using System;

    public interface IEntityBase
    {
        public void Modify();
        public Option<Exception> Modify(string updatedBy);
        public void Delete();
        public Option<Exception> Delete(string deletedBy);
        public Option<Exception> Validate();
    }
}
