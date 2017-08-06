using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace Portfolio.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        [Route("contacts")]
        public IActionResult Contacts()
        {
            return View("Contact");
        }
    }
}