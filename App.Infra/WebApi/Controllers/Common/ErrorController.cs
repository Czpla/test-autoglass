namespace App.Infra.WebApi.Controllers.Common
{
    using Microsoft.AspNetCore.Mvc;
    using ControllerBase = App.Infra.WebApi.Controllers.Base.ControllerBase;

    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {

        [Route("404")]
        public new IActionResult NotFound() => Problem(
            statusCode: 404,
            title: "Not Found",
            detail: "The requested resource was not found.",
            type: "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/404"
        );

        [Route("401")]
        public new IActionResult Unauthorized() => Problem(
            statusCode: 401,
            title: "Unauthorized",
            detail: "You aren’t authenticated, either not authenticated at all or authenticated incorrectly, but please reauthenticate and try again.",
            type: "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/401"
        );

        [Route("403")]
        public new IActionResult Forbid() => Problem(
            statusCode: 403,
            title: "Forbid",
            detail: "I’m sorry. I know who you are, I believe who you say you are, but you just don’t have permission to access this resource. Maybe if you ask the system administrator nicely, you’ll get permission. But please don’t bother me again until your predicament changes.",
            type: "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/403"
        );

        [Route("500")]
        public IActionResult InternalServerError() => Problem(
            statusCode: 500,
            title: "Internal Server Error",
            detail: "An error occurred while processing your request.",
            type: "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/500"
        );
    }
}