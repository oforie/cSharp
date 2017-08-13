
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dojodachi.Controllers
{
      
            public class PlayerController : Controller
        {
                [HttpGet]
                [Route("")]
                public IActionResult Index()
                {

                    if (HttpContext.Session.GetObjectFromJson<Player>("CurrentPlayer") == null) 
                    {
                        HttpContext.Session.SetObjectAsJson("CurrentPlayer", new Player());
                    }
                    // List<Player> CurrentPlayer = new List<Player>();
                    ViewBag.Player = HttpContext.Session.GetObjectFromJson<Player>("CurrentPlayer");
                    ViewBag.GameStatus = "running";
                    System.Console.WriteLine("This is the current player" + ViewBag.Player);
                    return View("Player");

                }

                // [HttpGet]
                // [Route("update")]
                // public JsonResult Update()
                // {
                //     List<Player> GetCurrentPlayer = HttpContext.Session.GetObjectFromJson<List<Player>>("CurrentPlayer");
                //     ViewBag.Player = GetCurrentPlayer;
                //     return Json(GetCurrentPlayer);
                // }

                [HttpPost]
                [Route("performAction")]
                public IActionResult PerformAction(string action)
                {
                    //retrieve current player data in session and modify based on action
                    Player EditPlayer = HttpContext.Session.GetObjectFromJson<Player>("CurrentPlayer");
                    Random Rand = new Random();
                    ViewBag.GameStatus = "running";

                    //introduce a if statement  based on what action is being passed

                    if (action == "feed")
                    {
                        if (EditPlayer.meals > 0)
                        {
                            EditPlayer.meals -= 1;
                            if(Rand.Next(1,5) > 1)
                            {
                                var amt = Rand.Next(5, 11);
                                EditPlayer.fullness += amt;
                                EditPlayer.status = $"I've just been fed and I'm {amt} fuller, Delicious!";
                            }
                            else
                            {
                                EditPlayer.status = "Might as well keep your food!";
                            }
                        }
                        else 
                        {
                            EditPlayer.message = "My meal stores are all out, can't be fed now I guess";
                        }
                    }


                    else if (action == "play")
                    {
                        if (EditPlayer.energy > 4)
                        {
                            EditPlayer.energy -= 5;
                            if(Rand.Next(1,5) > 1)
                            {
                                var amt = Rand.Next(5, 11);
                                EditPlayer.happiness += amt;
                                EditPlayer.status = $"I've just been played with and I'm {amt} levels happier!";
                            }
                            else
                            {
                                EditPlayer.status = "Thanks for trying bro but not really in the mood";
                            }
                        }
                        else
                        {
                            EditPlayer.status = "I don't have enough energy to play right now, sorry :(";
                        }
                        
                    }

                    else if (action == "work")
                    {       
                        if (EditPlayer.energy > 4)
                        {
                            EditPlayer.energy -= 5;
                            var amt = Rand.Next(1, 4);
                            EditPlayer.meals += amt;
                            EditPlayer.status = $"Earning my living! Worked and earned {amt} meal(s)!";
                        }
                        else
                        {
                            EditPlayer.status = "I don't have enough energy to work right now, sorry :(";
                        }
                        
                    }
                    
                    else if (action == "sleep")
                    {       
                        if (EditPlayer.fullness > 4 & EditPlayer.happiness > 4)
                        {
                            
                            EditPlayer.fullness -= 5;
                            EditPlayer.happiness -=5;
                            EditPlayer.energy += 15;
                            
                            EditPlayer.status = $"Slept and boosted my energy by 15!";
                        }
                        else
                        {
                            EditPlayer.status = "Not enough happiness/fullness credit to sleep:(";
                        }
 
                    }

                    // else if (action == "sleep")
                    // {
                    //     //do something else
                    // }
                HttpContext.Session.SetObjectAsJson("CurrentPlayer", EditPlayer);
                ViewBag.Player = EditPlayer;
                ViewBag.GameStatus = "running";
                    if (EditPlayer.energy > 100 && EditPlayer.fullness > 100 && EditPlayer.happiness > 100)
                    {
                        ViewBag.GameStatus = "win";
                    }

                    else if (EditPlayer.fullness < 0 || EditPlayer.happiness < 0)
                    {
                        ViewBag.GameStatus = "lose";
                    } 
                return View("Player");
                }

            
                [HttpGet]
                [Route("restart")]
                public IActionResult Restart()
                {
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index");
                } 

        }

        public static class SessionExtensions
        {
            // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
            public static void SetObjectAsJson(this ISession session, string key, object value)
            {
                // This helper function simply serializes theobject to JSON and stores it as a string in session
                session.SetString(key, JsonConvert.SerializeObject(value));
            }
            
            // generic type T is a stand-in indicating that we need to specify the type on retrieval
            public static T GetObjectFromJson<T>(this ISession session, string key)
            {
                string value = session.GetString(key);
                // Upone retrieval the object is deserialized based on the type we specified
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
        }
      
}