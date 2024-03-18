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

            await _repository.AddAsync(input);

            return new CreateProductOutputDto
            {
                Id = product.Id,
                Description = product.Description,
                Situation = product.Situation,
            };
        }
    }
}