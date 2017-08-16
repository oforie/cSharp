using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TheWall.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TheWall.Controllers
{
    public class UserController : Controller
    {
        private WallContext _context;

        public UserController(WallContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        { 
            ViewBag.Errors = new List<string>();
            return View("Index");
        }


        [HttpGet]
        [Route("login")]
        public IActionResult Servelogin()
        {
            
            return View("Login");
        }

        [HttpPost]
        [Route("user/login")]
        public IActionResult Login(string email, string password)
        {
            if( email != null && password != null)
            {
               User ReturnedUser = _context.User.SingleOrDefault(user => user.Email == email); 
            
                if(ReturnedUser != null)
                    {
                        if(ReturnedUser.Password == password)
                        {
                            HttpContext.Session.SetInt32("LoggedInUser", ReturnedUser.UserId);
                            return RedirectToAction("Wall");
                        }
                    }
            }
            ViewBag.LoginErrors = new List<string>{
                         "Invalid Name or Password"
                         };  
            return View("Login");
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUser model)
        {
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
                _context.User.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("Wall");
            }
            ViewBag.Errors = ModelState.Values;
            return View("Index");
        }

        [HttpGet]
        [Route("login/wall")]
        public IActionResult Wall()
        {
            int? CurrentUser = HttpContext.Session.GetInt32("LoggedInUser");
            if(CurrentUser == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.CurrentUser = CurrentUser;
           return View("Wall");
        } 
        
    }
}
