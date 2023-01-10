using Ecom.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
           _context = context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var anything = _context.Products.Find(100);
            if(anything is null)
                return NotFound();
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError() {
            var anything = _context.Products.Find(100);
            var something = anything.ToString();

            return Ok();

        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest() {
            return BadRequest();
                }

    }
}
