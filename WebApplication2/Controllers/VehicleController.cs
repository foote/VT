using BDF.VehicleTracker.BL;
using BDF.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class VehicleController : Controller
    {

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/");
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            return client;
        }

        // GET: VehicleController
        public ActionResult Index()
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("Vehicle").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Vehicle> vehicles = items.ToObject<List<Vehicle>>();

            return View(vehicles);
        }

        // GET: VehicleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();

        }

        // GET: VehicleController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Color color)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("Color").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Color> colors = items.ToObject<List<Color>>();

                // Call the api
                response = client.GetAsync("Vehicle/" + color.Description).Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Vehicle> vehicles = items.ToObject<List<Vehicle>>();

                VehicleColors vehicleColors = new VehicleColors();
                vehicleColors.Vehicle = vehicles.FirstOrDefault();
                vehicleColors.Colors = colors;


                return View(nameof(Edit), vehicleColors);

            }
            catch (Exception ex)
            {
                return View(color);
            }
        }




        // GET: VehicleController/Edit/5
        public ActionResult Edit(Guid id)
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("Color").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Color> colors = items.ToObject<List<Color>>();

            // Call the api
            response = client.GetAsync("Vehicle/" + id).Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = JsonConvert.DeserializeObject(result);
            Vehicle vehicle = items.ToObject<Vehicle>();

            VehicleColors vehicleColors = new VehicleColors();
            vehicleColors.Vehicle = vehicle;
            vehicleColors.Colors = colors;


            return View(nameof(Edit), vehicleColors);
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehicleColors vehicleColor)
        {
            try
            {
                HttpClient client = InitializeClient();
                string result;
                dynamic items;

                // Call the api
                string serializedVehicle = JsonConvert.SerializeObject(vehicleColor.Vehicle);
                var content = new StringContent(serializedVehicle);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Vehicle?id=" + id, content).Result;

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                
                return View(vehicleColor.Vehicle);
            }
        }

        // GET: VehicleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
