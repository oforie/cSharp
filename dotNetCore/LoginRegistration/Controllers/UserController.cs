using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;
using System.Linq;

namespace LoginRegistration.Controllers
{
    public class UserController : Controller
    {

        //   List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM users");
        // GET: /Home/

        // private readonly DbConnector _dbConnector;
      private UserContext _context;
        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors = new List<string>();
            return View("Index");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            int? CurrentUser = HttpContext.Session.GetInt32("LoggedInUser");
            ViewBag.CurrentUser = CurrentUser;
            return View("Success");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUser model)
        {
            System.Console.WriteLine("********************We are here line 24", model);
            TryValidateModel(model);
            if(ModelState.IsValid)
            {
                User newUser = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                System.Console.WriteLine($"**********************Line 46 this is the new {newUser}");
                _context.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Success");
            }
            ViewBag.Errors = ModelState.Values;
            // var Something = ViewBag.Errors;
            System.Console.WriteLine($"****************VieweBags on line 32 {ViewBag.Errors}");
            
        return View("Index"); 
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
             ViewBag.LoginErrors = new List<string>();
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            if(email != null && password != null)
            {
                User ReturnedUser = _context.Users.SingleOrDefault(user => user.Email == email);
                if(ReturnedUser != null)
                {
                    if(ReturnedUser.Password == password)
                    {
                        HttpContext.Session.SetInt32("LoggedInUser", ReturnedUser.UserId);
                        return RedirectToAction("Success");
                    }

                }
            }
            ViewBag.LoginErrors = new List<string>{
                "Invalid Name or Password"
            };
            return View("Login");
        }
    }
}
