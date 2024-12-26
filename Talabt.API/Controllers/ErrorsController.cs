using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabt.API.Errors;

namespace Talabt.API.Controllers
{
    [Route("error/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult  Error( int Code)
        {
            return NotFound(new ApiResponses(Code));
        }



    }
}
