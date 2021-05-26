using BDF.VehicleTracker.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/");
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            return client;
        }

        public async Task<IActionResult> Index()

        {
            //HttpClient client = InitializeClient();
            //HttpResponseMessage response;
            //string result;
            //dynamic items;

            //// Call the api
            //response = client.GetAsync("Color").Result;
            //result = response.Content.ReadAsStringAsync().Result;
            //items = (JArray)JsonConvert.DeserializeObject(result);
            //List<BDF.VehicleTracker.BL.Models.Color> colors = items.ToObject<List<BDF.VehicleTracker.BL.Models.Color>>();

            _logger.LogWarning("Index running at: {time}", DateTimeOffset.Now);

            return View(await ColorManager.Load());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
