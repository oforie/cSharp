using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace Portfolio.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet]
        [Route("projects")]
        public IActionResult Projects()
        {
            return View("Project");
        }
    }
}