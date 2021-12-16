using BLL.Excpetions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorHandlingController : ControllerBase
    {

        [Route("/error-dev")]
        public ActionResult GetDevError([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            Exception error = context?.Error;

            if (error is BussinesException) {
                BussinesException bussinesException = (BussinesException)error;
                return Problem(
                                  detail: bussinesException.StackTrace,
                                  title: bussinesException.Message,
                                  statusCode: bussinesException.StatusCode
                               );
            }

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }
    }
}
