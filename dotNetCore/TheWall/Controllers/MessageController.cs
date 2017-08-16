using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using TheWall.Models;

namespace TheWall.Controllers
{
    public class MessageController : Controller
    {
         private WallContext _context;

        public MessageController(WallContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Servelogin", "User");
        }


        [HttpPost]
        [Route("addmessage")]
        public IActionResult NewMessage(string content)
        {
            if(content == null)
            {
                ViewBag.MessageErrors = new List<string> {"Please enter text into the message field"};
                return View("Wall", "User");
            }

            int? CurrentUser = HttpContext.Session.GetInt32("LoggedInUser");
            if(CurrentUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            Message newMessage = new Message(content);
            newMessage.UserId = (int)CurrentUser;
            System.Console.WriteLine($"Line 39*****************this is the new message {newMessage}");
            _context.Message.Add(newMessage);
            _context.SaveChanges();
            return RedirectToAction("Wall", "User");
        }
    }
}