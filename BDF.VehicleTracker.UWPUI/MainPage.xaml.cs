using BDF.VehicleTracker.UWPBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BDF.VehicleTracker.UWPUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        public async void LoadColorsAsync()
        {
            using (var client = new HttpClient())
            {
                // Add necessary header values
                client.DefaultRequestHeaders.Add("x-apikey", "12345");
                var response = string.Empty;

                //await Task.Run(async () =>
                //{
                //    response = await client.GetStringAsync(BaseUri);
                //});

                //HttpResponseMessage response1;
                //await Task.Run(async () =>
                //{
                //    response1 = await client.GetAsync(BaseUri);
                //});

                //Send the GET request asynchronously and retrieve the response as a string.
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                string httpResponseBody = "";
                Uri requestUri = new Uri("https://vehicletrackerapi.azurewebsites.net/Color");
                try
                {
                    //Send the GET request
                    httpResponse = await client.GetAsync(requestUri);
                    httpResponse.EnsureSuccessStatusCode();
                    httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                }


                //var ignored = Window.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                //{
                //    response = client.GetStringAsync(BaseUri);
                //});


                cboColors.ItemsSource = JsonConvert.DeserializeObject<List<Color>>(httpResponseBody);
                cboColors.SelectedValuePath = "Id";
                cboColors.DisplayMemberPath = "Description";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadColorsAsync();
        }


    }
}
