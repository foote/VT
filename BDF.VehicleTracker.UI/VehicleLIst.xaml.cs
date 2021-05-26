using BDF.VehicleTracker.BL;
using BDF.VehicleTracker.BL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BDF.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class VehicleList : Window
    {
        BL.Models.Color color;
        List<BL.Models.Color> colors;
        List<Vehicle> vehicles;
        MySettings mySettings;
        private readonly ILogger<VehicleList> _logger;

        public VehicleList(ILogger<VehicleList> logger)
        {
           
            _logger = logger;
            InitializeComponent();
        }

        public VehicleList()
        {

            InitializeComponent();
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/");
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            return client;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dataGrid.ItemsSource = ColorManager.Load();

            Reload();

            mySettings = App.Configuration.GetSection("MySettings").Get<MySettings>();
            this.Title = mySettings.Text;
            this.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(mySettings.BackColor.R,
                                                                                            mySettings.BackColor.G,
                                                                                            mySettings.BackColor.B));
            this.dgVehicles.Background = this.Background;
            this.Width = mySettings.Size.Width;
            this.Height = mySettings.Size.Height;

        }


        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            new MaintainColors().ShowDialog();

            _logger.LogDebug("BtnColor_Click running at: {time}", DateTimeOffset.Now);
        }

        private void BtnMakes_Click(object sender, RoutedEventArgs e)
        {
            new MaintainAttributes(ScreenMode.Make).ShowDialog();
            _logger.LogInformation("BtnMakes_Click running at: {time}", DateTimeOffset.Now);
        }

        private void BtnModels_Click(object sender, RoutedEventArgs e)
        {
            new MaintainAttributes(ScreenMode.Model).ShowDialog();
            _logger.LogWarning("BtnModels_Click running at: {time}", DateTimeOffset.Now);
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void BtnNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle vehicle = new Vehicle();
            MaintainVehicle maintainVehicle = new MaintainVehicle(vehicle);
            maintainVehicle.Owner = this;
            maintainVehicle.ShowDialog();
            vehicles.Add(vehicle);
            Rebind();
        }

        private void BtnEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle vehicle = vehicles[dgVehicles.SelectedIndex];
            MaintainVehicle maintainVehicle = new MaintainVehicle(vehicle);
            maintainVehicle.Owner = this;
            maintainVehicle.ShowDialog();
            vehicles[dgVehicles.SelectedIndex] = vehicle;
            Rebind();
        }

        private void Rebind()
        {
            dgVehicles.ItemsSource = null;
            dgVehicles.ItemsSource = vehicles;

            dgVehicles.Columns[0].Visibility = Visibility.Hidden;
            dgVehicles.Columns[1].Visibility = Visibility.Hidden;
            dgVehicles.Columns[2].Visibility = Visibility.Hidden;
            dgVehicles.Columns[3].Visibility = Visibility.Hidden;

            dgVehicles.Columns[6].Header = "Color";
            dgVehicles.Columns[7].Header = "Make";
            dgVehicles.Columns[8].Header = "Model";

            cboFilter.ItemsSource = null;
            cboFilter.ItemsSource = colors;
            cboFilter.DisplayMemberPath = "Description";
            cboFilter.SelectedValuePath = "Id";


            // Change font property of the Column headers
            Style headerStyle = new Style();
            DataGridColumnHeader header = new DataGridColumnHeader();
            headerStyle.TargetType = header.GetType();


            Setter setter = new Setter();
            setter.Property = FontSizeProperty;
            setter.Value = 10.0;

            headerStyle.Setters.Add(setter);

            headerStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = Brushes.LightYellow });
            headerStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Verdana") });
            headerStyle.Setters.Add(new Setter { Property = Control.FontWeightProperty, Value = FontWeights.Bold });
            headerStyle.Setters.Add(new Setter { Property = Control.FontStyleProperty, Value = FontStyles.Italic });
            headerStyle.Setters.Add(new Setter { Property = Control.BorderThicknessProperty, Value = new Thickness(1) });
            headerStyle.Setters.Add(new Setter { Property = Control.BorderBrushProperty, Value = Brushes.Black });
            headerStyle.Setters.Add(new Setter { Property = Control.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });

            dgVehicles.Columns[4].HeaderStyle = headerStyle;
            dgVehicles.Columns[5].HeaderStyle = headerStyle;
            dgVehicles.Columns[6].HeaderStyle = headerStyle;
            dgVehicles.Columns[7].HeaderStyle = headerStyle;
            dgVehicles.Columns[8].HeaderStyle = headerStyle;

            Setter setterRow = new Setter();
            setterRow.Property = FontSizeProperty;
            setterRow.Value = 18.0;
            setterRow.Property = Control.ForegroundProperty;
            setterRow.Value = Brushes.Blue;
            setterRow.Property = Control.BackgroundProperty;
            setterRow.Value = Brushes.Pink;

        }

        private async void Reload()
        {
            try
            {
                vehicles = (List<Vehicle>)await VehicleManager.Load();

                dgVehicles.ItemsSource = null;
                dgVehicles.ItemsSource = vehicles;

                dgVehicles.Columns[0].Visibility = Visibility.Hidden;
                dgVehicles.Columns[1].Visibility = Visibility.Hidden;
                dgVehicles.Columns[2].Visibility = Visibility.Hidden;
                dgVehicles.Columns[3].Visibility = Visibility.Hidden;

                dgVehicles.Columns[6].Header = "Color";
                dgVehicles.Columns[7].Header = "Make";
                dgVehicles.Columns[8].Header = "Model";

                colors = (List<BL.Models.Color>)await ColorManager.Load();
                cboFilter.ItemsSource = null;
                cboFilter.ItemsSource = colors;
                cboFilter.DisplayMemberPath = "Description";
                cboFilter.SelectedValuePath = "Id";


                // Change font property of the Column headers
                Style headerStyle = new Style();
                DataGridColumnHeader header = new DataGridColumnHeader();
                headerStyle.TargetType = header.GetType();


                Setter setter = new Setter();
                setter.Property = FontSizeProperty;
                setter.Value = 10.0;

                headerStyle.Setters.Add(setter);

                headerStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = Brushes.LightYellow });
                headerStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Verdana") });
                headerStyle.Setters.Add(new Setter { Property = Control.FontWeightProperty, Value = FontWeights.Bold });
                headerStyle.Setters.Add(new Setter { Property = Control.FontStyleProperty, Value = FontStyles.Italic });
                headerStyle.Setters.Add(new Setter { Property = Control.BorderThicknessProperty, Value = new Thickness(1) });
                headerStyle.Setters.Add(new Setter { Property = Control.BorderBrushProperty, Value = Brushes.Black });
                headerStyle.Setters.Add(new Setter { Property = Control.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });

                dgVehicles.Columns[4].HeaderStyle = headerStyle;
                dgVehicles.Columns[5].HeaderStyle = headerStyle;
                dgVehicles.Columns[6].HeaderStyle = headerStyle;
                dgVehicles.Columns[7].HeaderStyle = headerStyle;
                dgVehicles.Columns[8].HeaderStyle = headerStyle;

                //< Style TargetType = "{x:Type DataGridRow}" >
                //        < Setter Property = "FontSize" Value = "14" />
                //           < Setter Property = "FontFamily" Value = "Arial" />
                //              < Setter Property = "FontWeight" Value = "Bold" />
                //                 < Setter Property = "Foreground" Value = "Blue" />
                //                    < Setter Property = "Background" Value = "LightBlue" />

                //                       < Style.Triggers >

                //                           < Trigger Property = "IsSelected" Value = "True" >

                //                                  < Setter Property = "Foreground" Value = "Red" />

                //                                     < Setter Property = "Background" Value = "Blue" />

                //                                    </ Trigger >

                //                                </ Style.Triggers >

                //                            </ Style >

                Setter setterRow = new Setter();
                setterRow.Property = FontSizeProperty;
                setterRow.Value = 18.0;
                setterRow.Property = Control.ForegroundProperty;
                setterRow.Value = Brushes.Blue;
                setterRow.Property = Control.BackgroundProperty;
                setterRow.Value = Brushes.Pink;


                dgVehicles.RowStyle.Setters.Add(setterRow);
                throw new Exception("Brian was here. ");


            }
            catch (Exception ex)
            {
                _logger.LogError("Rebind Error: " + ex.Message);
                this.Title = ex.Message;                
            }


        }


        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            if (vehicles != null)
            {
                VehicleManager.Export(vehicles);
            }
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboFilter.SelectedIndex > -1)
            {
                vehicles = vehicles.Where(v => v.ColorId == colors[cboFilter.SelectedIndex].Id).ToList();
                Reload();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
