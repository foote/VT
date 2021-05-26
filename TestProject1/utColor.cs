using BDF.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace TestProject1
{
    [TestClass]
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

        [TestMethod]
        public void LoadTest()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            // Call the api
            response = client.GetAsync("Color").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Color> colors = items.ToObject<List<BDF.VehicleTracker.BL.Models.Color>>();

            Assert.IsTrue(colors.Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            Color color = new Color();
            color.Description = "New Color";
            color.Code = BitConverter.ToInt32(new byte[] { 125, 125, 125, 0x00 }, 0);

            bool rollback = true;

            string serializedColor = JsonConvert.SerializeObject(color);
            var content = new StringContent(serializedColor);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Color/" + rollback, content).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(result, "1");
        }

    }
}
