using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VcubWatcher.Models;

namespace VcubWatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private static List<BikeStation> GetBikeStationFromAPI()
        {
            using (var client = new HttpClient())
            {
                // GET data from URL
                var response = client.GetAsync("https://api.alexandredubois.com/vcub-backend/vcub.php");
                // GET http result en string
                var StringResult = response.Result.Content.ReadAsStringAsync();
                // Conversion du JSON en collection d'objets
                var res = JsonConvert.DeserializeObject<List<BikeStation>>(StringResult.Result);
                return res;
            }

        }
        public IActionResult StationList()
        {
            var stations = GetBikeStationFromAPI();
            return View(stations);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}