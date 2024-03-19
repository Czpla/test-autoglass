namespace App.Core.Domain.Business.Product
{
    using Shared.Either;
    using Product.DataTransferObjects;
    using System.Threading.Tasks;
    using System;

    public interface IProductBusiness
    {
        public Task<Result<CreateProductOutputDto, Exception>> Create(CreateProductInputDto input);
        public Task<Result<GetProductOutputDto, Exception>> Get(GetProductInputDto input);
        public Task<Result<UpdateProductOutputDto, Exception>> Update(UpdateProductInputDto input);
        public Task<Result<GetPaginatedProductOutputDto, Exception>> GetPaginated(GetPaginatedProductInputDto input);
        // public Task<Result<DeleteProductOutputDto, Exception>> Delete(DeleteProductInputDto input);
    }
}
