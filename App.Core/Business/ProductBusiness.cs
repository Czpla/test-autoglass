namespace App.Core.Business
{
    using Shared.Either;
    using Core.Domain.Entities;
    using Domain.Repositories;
    using Core.Domain.Business.Product;
    using Core.Domain.Business.Product.DataTransferObjects;
    using System.Threading.Tasks;
    using System;

    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _repository;

        public ProductBusiness(IProductRepository repository) { _repository = repository; }

        public async Task<Result<CreateProductOutputDto, Exception>> Create(CreateProductInputDto input)
        {
            Product product = input;

            var productValidation = product.Validate();

            if (productValidation.IsSome())
                return productValidation.Value;

            var result = await _repository.AddAsync(input);

            return new CreateProductOutputDto
            {
                Id = result.Id,
                Description = result.Description,
                Situation = result.Situation,
                ManufacturingDate = result.ManufacturingDate,
                ExpirationDate = result.ExpirationDate,
                SupplierCode = result.SupplierCode,
                SupplierDescription = result.SupplierDescription,
                SupplierCnpj = result.SupplierCnpj,
            };
        }

        public async Task<Result<GetProductOutputDto, Exception>> Get(GetProductInputDto input)
        {
            var result = await _repository.GetByIdAsync(input.Id);

            if (result is null)
                return new Exception("Product not found.");

            return new GetProductOutputDto
            {
                Id = result.Id,
                Description = result.Description,
                Situation = result.Situation,
                ManufacturingDate = result.ManufacturingDate,
                ExpirationDate = result.ExpirationDate,
                SupplierCode = result.SupplierCode,
                SupplierDescription = result.SupplierDescription,
                SupplierCnpj = result.SupplierCnpj,
            };
        }

        public async Task<Result<UpdateProductOutputDto, Exception>> Update(UpdateProductInputDto input)
        {
            Product product = input;

            var productValidation = product.Validate();

            if (productValidation.IsSome())
                return productValidation.Value;

            await _repository.UpdateAsync(input);

            return new UpdateProductOutputDto
            {
                Id = input.Id,
                Description = input.Description,
                Situation = input.Situation,
                ManufacturingDate = input.ManufacturingDate,
                ExpirationDate = input.ExpirationDate,
                SupplierCode = input.SupplierCode,
                SupplierDescription = input.SupplierDescription,
                SupplierCnpj = input.SupplierCnpj,
            };
        }

        public async Task<Result<DeleteProductOutputDto, Exception>> Delete(DeleteProductInputDto input)
        {
            var result = await _repository.UpdateSituationToInactive(input);

            if (result is null)
                return new Exception("Product not found.");

            return new DeleteProductOutputDto
            {
                Id = result.Id,
                Description = result.Description,
                Situation = result.Situation,
                ManufacturingDate = result.ManufacturingDate,
                ExpirationDate = result.ExpirationDate,
                SupplierCode = result.SupplierCode,
                SupplierDescription = result.SupplierDescription,
                SupplierCnpj = result.SupplierCnpj,
            };
        }

        public async Task<Result<GetPaginatedByProductOutputDto, Exception>> GetPaginatedBy(GetPaginatedByProductInputDto input)
        {
            var result = await _repository.GetPaginatedBy(1, 5);

            Console.WriteLine(result);

            if (result is null)
                return new Exception("Products not found.");

            return new Exception("Method not implemented.");

            // return new GetPaginatedByProductOutputDto
            // {
            //     Products = result,
            //     Total = result.Count(),
            // };
        }
    }
}