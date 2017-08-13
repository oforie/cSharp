using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;

namespace PokeInfo.Controllers
{

    
    public class HomeController : Controller
    {

        public static async Task GetPokemonDataAsync(int PokeId, Action<Dictionary<string, object>> Callback)
        {
            // Create a temporary HttpClient connection.
            using (var Client = new HttpClient())
            {
                try
                {
                    Client.BaseAddress = new Uri($"http://pokeapi.co/api/v2/pokemon/{PokeId}");
                    HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
                    Response.EnsureSuccessStatusCode(); // Throw error if not successful.
                    string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.
                     
                    // Then parse the result into JSON and convert to a dictionary that we can use.
                    // DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
                    Dictionary<string, object> JsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(StringResponse);
                    // System.Console.WriteLine(JsonResponse["name"]);
                     
                    // Finally, execute our callback, passing it the response we got.
                    Callback(JsonResponse);
                }
                catch (HttpRequestException e)
                {
                    // If something went wrong, display the error.
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
        }
    
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("poke")]
        public IActionResult Poke()
        {
            return View("Poke");
        }

        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult QueryPoke(int pokeid)

        {
            
            var PokeInfo = new Dictionary<string, object>();
            GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    // PokeInfo = ApiResponse;
                    TempData["Pokemon"] = ApiResponse;
                    System.Console.WriteLine(ViewBag.Pokemon["name"]);
                    // ViewBag.Pokemon = ApiResponse;
                }
            ).Wait();
            // Other code
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("getData")]
        public JsonResult GetData()
        {
            var PokemonObject = TempData["Pokemon"]; 
            System.Console.WriteLine(PokemonObject);
            return Json(PokemonObject);
        }

         
    }
}
