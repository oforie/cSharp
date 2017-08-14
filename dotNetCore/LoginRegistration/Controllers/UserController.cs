using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;
using LoginRegistration;

namespace LoginRegistration.Controllers
{
    public class UserController : Controller
    {

        //   List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM users");
        // GET: /Home/

        private readonly DbConnector _dbConnector;
 
        public UserController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors = new List<string>();
            return View("Index");
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
                    EmailAddress = model.EmailAddress,
                    Password = model.Password
                };
                return View("Success");
            }
            ViewBag.Errors = ModelState.Values;
            // var Something = ViewBag.Errors;
            System.Console.WriteLine($"****************VieweBags on line 32 {ViewBag.Errors}");
            
        return View("Index"); 
        }
    }
}
