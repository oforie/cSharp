using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using TheWall.Models;
using Microsoft.EntityFrameworkCore;

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
        [Route("wall")]
        public IActionResult Wall()
        {
            int? CurrentUser = HttpContext.Session.GetInt32("LoggedInUser");
            if(CurrentUser == null)
            {
                return View("Login", "User");
            }
            ViewBag.CurrentUser = CurrentUser;
            var AllMessage = _context.Message.Include(message => message.Comments).ThenInclude(comment => comment.User).Include(message => message.User);
            // List<Message> OrderedMessage = AllMessage.OrderByAscending((message) => message.CreatedAt).ToList();
            ViewBag.OrderedMessage = AllMessage;
            System.Console.WriteLine($"***************line 32 this is the message query {AllMessage}");
            return View("Wall");
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
                return View("Wall");
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
            return RedirectToAction("Wall");
        }

        [HttpPost]
        [Route("postcomment")]
        public IActionResult AddComment(string content, int MessageId)
        { 
            System.Console.WriteLine($"Line 76***********************This is the messaged id {MessageId}");
            if(content == null)
            {
                ViewBag.CommentErrors = new List<string>{"Comment field cannot be empty"};
                return View("Wall");
            }
            

            int? CurrentUser = HttpContext.Session.GetInt32("LoggedInUser");
            if(CurrentUser == null)
            {
                return RedirectToAction("Login", "User");
            }
            
            Comment NewComment = new Comment()
            {
                    Content = content,
                    MessageId = MessageId,
                    UserId =  (int)CurrentUser,
            };
            _context.Comment.Add(NewComment);
            _context.SaveChanges();
            return RedirectToAction("Wall");
        }
    }
}