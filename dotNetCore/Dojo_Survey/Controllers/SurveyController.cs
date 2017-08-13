using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace Dojo_Survey.Controllers
{
    public class SurveyController : Controller
    {
        [HttpGet]
        [Route("")]
        public  IActionResult Index()
        {
            return View("Survey");
        }

        [HttpPost]
        [Route("method")]
        public  IActionResult Method(string nameField, string locationField, string languageField, string commentsField)
        {
            ViewBag.nameField = nameField;
            ViewBag.locationField = locationField;
            ViewBag.languageField = languageField;
            ViewBag.commentsField = commentsField;

            return View("Success");
        }


    }
}