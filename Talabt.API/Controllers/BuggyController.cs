using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabt.API.Errors;
using Talabat.Repository.Data;

namespace Talabt.API.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet("NotFound")]
        //base/api/buggy/notfound
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(100);
            if (product == null) return NotFound(new ApiResponses(404));
            return Ok(product);
        }
        [HttpGet("ServerError")]
        public ActionResult GetServererror()
        {
            var product = _context.Products.Find(100);
            var productToreturn = product.ToString();
            return Ok(productToreturn);
        }

        [HttpGet("BadReqeust")]
        public ActionResult GetBadRequest()
        { return BadRequest(); }

        [HttpGet("BadReqeust/{id}")]

        public ActionResult GetBadRequest( int id)
        { return Ok(); }

    } 
}
