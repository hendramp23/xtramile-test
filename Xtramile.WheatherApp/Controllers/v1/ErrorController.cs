using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xtramile.WheatherApp.Controllers.v1
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {

        #region Public Methods

        [Route("error")]
        public IActionResult ErrorAsync()
        {
            // we can catch the error here and store it in error log database or send to kafka or email notification.

            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion Public Methods
    }
}
