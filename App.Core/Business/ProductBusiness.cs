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
            };
        }

        public async Task<Result<GetProductOutputDto, Exception>> Get(GetProductInputDto input)
        {
            var result = await _repository.GetByIdAsync(input.Id);

            if (result is null)
                return new Exception("Product not found");

            return new GetProductOutputDto
            {
                Id = result.Id,
                Description = result.Description,
                Situation = result.Situation,
            };
        }

        public async Task<Result<UpdateProductOutputDto, Exception>> Update(UpdateProductInputDto input)
        {
            // TODO: Não precisar atualizar todos os campos
            
            Product product = input;

            var productValidation = product.Validate();

            if (productValidation.IsSome())
                return productValidation.Value;

            await _repository.UpdateAsync(input);

            return new UpdateProductOutputDto
            {
                Id = product.Id,
                Description = product.Description,
                Situation = product.Situation,
            };
        }

        public async Task<Result<GetPaginatedProductOutputDto, Exception>> GetPaginated(GetPaginatedProductInputDto input)
        {
            var result = await _repository.GetPaginated(1, 5);

            Console.WriteLine(result);

            return new Exception("Not implemented");

            // return new GetPaginatedProductOutputDto
            // {
            //     Products = result.Products,
            //     Total = result.Total,
            // };
        }
    }
}