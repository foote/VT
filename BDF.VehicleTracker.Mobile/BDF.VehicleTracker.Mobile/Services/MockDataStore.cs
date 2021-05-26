using BDF.VehicleTracker.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;  
using System.Threading.Tasks;

namespace BDF.VehicleTracker.Mobile.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            //items = new List<Item>()
            //{
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            //};

            // Get Courses
            HttpClient client = InitializeClient();

            HttpResponseMessage response = client.GetAsync("Color").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            List<Color> colors = JsonConvert.DeserializeObject<List<Color>>(result);

            items = new List<Item>();
            foreach (var c in colors)
            {
                items.Add(new Item { Id = c.Id.ToString(), Description = c.Description, Code = c.Code });
            }
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-ApiKey", "456789");
            Uri baseAddress = new Uri("https://vehicletrackerapi.azurewebsites.net/api/");
            //Uri baseAddress = new Uri("http://localhost:60247/api/");

            client.BaseAddress = baseAddress;
            return client;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}