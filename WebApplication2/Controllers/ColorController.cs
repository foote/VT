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
using BDF.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication2.Controllers
{
    //[Authorize]
    public class ColorController : Controller
    {
        private readonly ILogger<ColorController> _logger;

        public ColorController(ILogger<ColorController> logger)
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
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            //string accessToken = await HttpContext.GetTokenAsync("access_token");
            //string refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            //return Content($"Current user: <span id=\"UserIdentityName\">{User.Identity.Name ?? "anonymous"}</span><br/>" +
            //    $"<div>Access token: {accessToken}</div><br/>" +
            //    $"<div>Refresh token: {refreshToken}</div><br/>"
            //    , "text/html");



            // Call the api
            response = client.GetAsync("Color").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Color> colors = items.ToObject<List<BDF.VehicleTracker.BL.Models.Color>>();

            _logger.LogError("Index running at: {time}", DateTimeOffset.Now);

            //return View(await ColorManager.Load());
            return View(colors);
        }

        public async Task<IActionResult> Details(Guid id)

        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("Color/" + id).Result;

            string result = response.Content.ReadAsStringAsync().Result;
            Color color = JsonConvert.DeserializeObject<Color>(result);

            
            //return View(await ColorManager.Load());
            return View(color);
        }

        public IActionResult Create()
        {
            Color color = new Color();
            return View(color);
        }

        [HttpPost]
        public IActionResult Create(Color color)
        {
            try
            {
                int results;
                //Task.Run(async () =>
                //{
                //    results = await ColorManager.Insert(color);
                //});

                HttpClient client = InitializeClient();
                string serializedColor = JsonConvert.SerializeObject(color);
                var content = new StringContent(serializedColor);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("Color", content).Result;

                return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex)
            {
                return View(color);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Color color = null;
            await Task.Run(async () =>
            {
                color = await ColorManager.LoadById(id);
            });

            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Color color)
        {
            try
            {
                //int results;
                //await Task.Run(async () =>
                //{
                //    results = await ColorManager.Update(color);
                //});

                HttpClient client = InitializeClient();
                string serializedColor = JsonConvert.SerializeObject(color);
                var content = new StringContent(serializedColor);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Color/" + color.Id, content).Result;

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View(color);
            }
        }

        public IActionResult Delete(Guid id)
        {
            Color color = null;
            Task.Run(async () =>
            {
                color = await ColorManager.LoadById(id);
            });

            return View(color);
        }

        [HttpPost]
        public IActionResult Delete(Guid id, Color color)
        {
            try
            {
                //int results;
                //Task.Run(async () =>
                //{
                //    results = await ColorManager.Delete(color.Id);
                //});
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Color/" + color.Id).Result;

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View(color);
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
