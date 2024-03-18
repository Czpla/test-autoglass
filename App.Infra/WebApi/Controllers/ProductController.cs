namespace App.Infra.WebApi.Controllers
{
    using Infra.WebApi.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc;
    using ControllerBase = Base.ControllerBase;
    using Core.Domain.Business.Product;
    using Core.Domain.Business.Product.DataTransferObjects;

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
    }
}
