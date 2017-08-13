using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;
using System;
using QuotingDojo.Models;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
// Other code
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(string name)
        {
            User NewUser = new User(name);
            TryValidateModel(NewUser);
            ViewBag.NameErrors = ModelState.Values;

            
            string CurrUserQuery = $"SELECT * FROM users WHERE Name='{name}'";
            var UserQuery = DbConnector.Query(CurrUserQuery);
            if (UserQuery.Count == 0)
                {
                    string dbUser = $"INSERT INTO users ( Name, CreatedAt, UpdatedAt) VALUE( '{name}', '{DateTime.Now}', '{DateTime.Now}')";
                    DbConnector.Execute(dbUser);
                    var NewUserQuery = DbConnector.Query(CurrUserQuery); 
                    System.Console.WriteLine(NewUserQuery[0]["UserId"]);
                    if (NewUserQuery.Count == 1)
                    {
                        var SavedUser = NewUserQuery[0]["UserId"];
                       
                        System.Console.WriteLine($"****************line 43 saved user {SavedUser}");
                        TempData["SavedUser"] = SavedUser;
                        HttpContext.Session.SetInt32("UserId", (int)SavedUser);
                        // HttpContext.Session.SetInt32("UserId", (int)SavedUser);  
                    }
                }

            else if(UserQuery.Count > 0)
            {
               var ExistingUserQuery = DbConnector.Query(CurrUserQuery); 
               var SavedUser = ExistingUserQuery[0]["UserId"];
               HttpContext.Session.SetInt32("UserId", (int)SavedUser);
               TempData["SavedUser"] = SavedUser;
             
            }
            return RedirectToAction("ViewQuotes");
        }

        // GET: /Home/a
        [HttpPost]
        [Route("addQuote")]
        public IActionResult Quote(string message)
        {
            Message NewMessage = new Message(message);
            TryValidateModel(NewMessage);
            ViewBag.MessageErrors = ModelState.Values;
            int? SessionUser = HttpContext.Session.GetInt32("UserId");
            System.Console.WriteLine($"***********************This is the user in session id {SessionUser}");
            string dbMessage = $"INSERT INTO messages (UserId, Content, CreatedAt) VALUE('{SessionUser}','{message}', '{DateTime.Now}')";
            DbConnector.Execute(dbMessage);
        
            return RedirectToAction("ViewQuotes");
        }

        [HttpGet]
        [Route("viewQuotes")]
        public IActionResult ViewQuotes()
        {
            
            // Other code
            System.Console.WriteLine("****************Entering the all user query zone");
            var AllQuery = "SELECT * FROM messages"; // JOIN  users ON users.UserId = messages.UserId ORDER BY messages.content DESC";
            List<Dictionary<string, object>> AllUsers = DbConnector.Query(AllQuery);
            ViewBag.Quotes = AllUsers;
            ViewBag.User = TempData["SavedUser"];
            int? SessionUser = HttpContext.Session.GetInt32("UserId");
            ViewBag.SessionUser = SessionUser;
            // System.Console.WriteLine("********************************", AllUsers);
            return View("Quotes");
        }

        
    }
}
