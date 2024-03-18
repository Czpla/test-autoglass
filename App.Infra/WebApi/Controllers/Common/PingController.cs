namespace App.Infra.WebApi.Controllers.Common
{
    using Microsoft.AspNetCore.Mvc;
    using ControllerBase = App.Infra.WebApi.Controllers.Base.ControllerBase;

    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Pong");
    }
}
