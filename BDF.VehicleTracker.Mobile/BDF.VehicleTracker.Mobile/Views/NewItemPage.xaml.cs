using BDF.VehicleTracker.Mobile.Models;
using BDF.VehicleTracker.Mobile.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = BDF.VehicleTracker.Mobile.Models.Color;

namespace BDF.VehicleTracker.Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        private Color color;

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }


        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://vehicletrackerapi.azurewebsites.net/api/");
            return client;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            color = new Color();
            color.Code = txtCode.Text;
            color.Description = txtDescription.Text;

            HttpClient client = InitializeClient();
            string serializedObject = JsonConvert.SerializeObject(color);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Color", content).Result;

        }
    }
}   