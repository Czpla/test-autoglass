namespace App.Infra.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Infra.WebApi.ViewModels.Product;
    using Infra.WebApi.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using ControllerBase = Base.ControllerBase;
    using Core.Domain.Business.Product;
    using Core.Domain.Business.Product.DataTransferObjects;
    using System;

    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] CreateProductInputDto input)
        {
            var created = await _productBusiness.Create(input);

            return created.Match(
                ok: output => Ok<ProductViewModel>(output),
                error: error => BadRequest<ErrorViewModel>(error)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetProductInputDto input)
        {
            var product = await _productBusiness.Get(input);

            return product.Match(
                ok: output => Ok<ProductViewModel>(output),
                error: error => BadRequest<ErrorViewModel>(error)
            );
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateProductInputDto input)
        {
            input.Id = id;

            var updated = await _productBusiness.Update(input);
        
            return updated.Match(
                ok: output => Ok<ProductViewModel>(output),
                error: error => BadRequest<ErrorViewModel>(error)
            );
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _productBusiness.Delete(new DeleteProductInputDto { Id = id });
        
            return deleted.Match(
                ok: output => Ok<ProductViewModel>(output),
                error: error => BadRequest<ErrorViewModel>(error)
            );
        }

        // [HttpGet]
        // public async Task<IActionResult> Get([FromQuery] GetPaginatedByProductInputDto input)
        // {
        //     var product = await _productBusiness.GetPaginatedBy(input);

        //     return product.Match(
        //         ok: output => Ok<ProductViewModel>(output),
        //         error: error => BadRequest<ErrorViewModel>(error)
        //     );
        // }

    }
}
