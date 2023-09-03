using Microsoft.AspNetCore.Mvc;
using Xtramile.WheatherApp.Model.Response;

namespace Xtramile.WheatherApp.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult GetApiError(string[] errorMessages, int? httpStatusCode = null)
        {
            var actualStatusCode = httpStatusCode.HasValue ? httpStatusCode.Value : 400;
            var responseObj = new ApiErrorResponseModel
            {
                ErrorMessages = errorMessages,
            };

            return this.StatusCode(actualStatusCode, responseObj);
        }

        protected IActionResult GetApiError(string errorMessage, int? httpStatusCode = null)
        {
            var actualStatusCode = httpStatusCode.HasValue ? httpStatusCode.Value : 400;
            var responseObj = new ApiErrorResponseModel
            {
                ErrorMessages = new string[] { errorMessage },
            };

            return this.StatusCode(actualStatusCode, responseObj);
        }
    }
}
