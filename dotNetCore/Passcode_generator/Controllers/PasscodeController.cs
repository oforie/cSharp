using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

 
namespace Passcode_generator.Controllers
{
    public class PasscodeController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
            return View("Passcode");
        }

        // [HttpGet]
        // [Route("create")]
        // public IActionResult Create()
        // {
            
        //     Console.WriteLine("This is the object I'm passing to the JSON file" + Passcode + RefreshCount);
        //     return RedirectToAction("Method");
        // }

    //json call route
        [HttpGet]
        [Route("method")]
        public JsonResult Method(){
            int? RefreshCount = HttpContext.Session.GetInt32("Count");
            if(RefreshCount == null) 
            {
                RefreshCount = 0;
            }
            RefreshCount += 1;

            string PossibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string Passcode = "";
            Random Rand = new Random();
            for(int i=0; i<14; i++)
            {
                Passcode = Passcode + PossibleCharacters[Rand.Next(0, PossibleCharacters.Length)];
            }

//returning this json object
            TempData["Passcode"] = Passcode;
            TempData["RefreshCount"] = RefreshCount;
            
           
            HttpContext.Session.SetInt32("Count", (int)RefreshCount);
        
        var AnonObject = new {
            Passcode = Passcode,
            RefreshCount = RefreshCount
        };
            //sending to JS call
            return Json(AnonObject);

        }

    }

}