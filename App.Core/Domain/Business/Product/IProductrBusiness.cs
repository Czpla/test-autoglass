namespace App.Core.Domain.Business.Product
{
    using Shared.Either;
    using Product.DataTransferObjects;
    using System.Threading.Tasks;
    using System;

    public interface IProductBusiness
    {
        public Task<Result<CreateProductOutputDto, Exception>> Create(CreateProductInputDto input);
    }
}
