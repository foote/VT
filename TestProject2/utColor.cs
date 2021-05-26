using BDF.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xunit;

namespace TestProject2
{
    public class utColor
    {
        public HttpClient client { get; }

        public utColor()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<BDF.VehicleTracker.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();

        }

        [Fact]
        public void LoadColorsTest()
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44394/");
            //client.DefaultRequestHeaders.Add("x-apikey", "12345");

            HttpResponseMessage response;
            string result;
            dynamic items;

            // Call the api
            response = client.GetAsync("Color").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Color> colors = items.ToObject<List<BDF.VehicleTracker.BL.Models.Color>>();

            Assert.True(colors.Count > 0);
        }
    }
}
