namespace App.Infra.WebApi.Controllers.Base
{
    using Microsoft.AspNetCore.Mvc;

    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected IActionResult Ok<TViewModel>(TViewModel viewmodel) => base.Ok(viewmodel);
        protected IActionResult BadRequest<TViewModel>(TViewModel viewmodel) => base.Ok(viewmodel);
    }
}
