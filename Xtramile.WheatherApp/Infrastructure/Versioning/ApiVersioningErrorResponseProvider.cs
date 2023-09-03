using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Xtramile.WheatherApp.Model.Response;

namespace Xtramile.WheatherApp.Infrastructure.Versioning
{
    public class ApiVersioningErrorResponseProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            var responseObj = new ApiErrorResponseModel
            {
                ErrorMessages = new string[] { context.Message },
            };

            var response = new ObjectResult(responseObj);
            response.StatusCode = (int)400;

            return response;
        }
    }
}
