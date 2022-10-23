using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace TeamHunterBackend.Controllers 
{
    [Route("demo")]
    [EnableCors("Policy")]
    public class DemoController : Controller
    {
        [Route("index")]
        [Route("")]
        [Route("~/")]
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
        [Authorize(Roles = "User")]
        [Route("index2")]
        [HttpGet]
        public IActionResult Index2()
        {
            return View("Index2");
        }
        [Authorize(Roles = "Admin")]
        [Route("index3")]
        [HttpGet]
        public IActionResult Index3()
        {
            return View("Index3");
        }
    }
}