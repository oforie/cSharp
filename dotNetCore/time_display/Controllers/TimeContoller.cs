using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
 
namespace time_display
{
    public class TimeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Time()
        {
            return View();
        }
    }
}