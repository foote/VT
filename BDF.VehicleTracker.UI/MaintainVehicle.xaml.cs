using BDF.VehicleTracker.BL;
using BDF.VehicleTracker.BL.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BDF.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for MaintainVehicle.xaml
    /// </summary>
    public partial class MaintainVehicle : Window
    {
        ucMaintainVehicle[] attributes = new ucMaintainVehicle[4];
        Vehicle vehicle;
        bool newVehicle;

        // Use this constructor for new vehicle situation
        // New Vehicle
        public MaintainVehicle(Vehicle vehicle)
        {
            InitializeComponent();
            this.vehicle = vehicle;
            if (Guid.Empty == vehicle.Id)
            {
                this.Title = "New Vehicle";
                newVehicle = true;
            }
            else
            {
                this.Title = "Edit Vehicle";
                newVehicle = false;
                txtVIN.Text = vehicle.VIN;
            }
            btnUpdate.IsEnabled = !newVehicle;
            btnInsert.IsEnabled = newVehicle;

            DrawScreen();
        }

        private void DrawScreen()
        {
            // Add the custom controls.
            ucMaintainVehicle ucColors = new ucMaintainVehicle(ControlMode.Color, vehicle.ColorId);
            ucMaintainVehicle ucMakes = new ucMaintainVehicle(ControlMode.Make, vehicle.MakeId);
            ucMaintainVehicle ucModels = new ucMaintainVehicle(ControlMode.Model, vehicle.ModelId);
            ucMaintainVehicle ucYears = new ucMaintainVehicle(ControlMode.Year, vehicle.Year);

            ucColors.Margin = new Thickness(40, 25, 0, 0);
            ucMakes.Margin = new Thickness(40, 60, 0, 0);
            ucModels.Margin = new Thickness(40, 95, 0, 0);
            ucYears.Margin = new Thickness(40, 130, 0, 0);

            ucColors.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;

            grdVehicle.Children.Add(ucColors);
            this.grdVehicle.Children.Add(ucMakes);
            this.grdVehicle.Children.Add(ucModels);
            this.grdVehicle.Children.Add(ucYears);

            attributes[0] = ucColors;
            attributes[1] = ucMakes;
            attributes[2] = ucModels;
            attributes[3] = ucYears;

            lblVIN.Margin = new Thickness(40, 170, 0, 0);
            txtVIN.Margin = new Thickness(85, 170, 0, 0);
            btnInsert.Margin = new Thickness(36, 200, 0, 0);
            btnUpdate.Margin = new Thickness(116, 200, 0, 0);
            btnDelete.Margin = new Thickness(196, 200, 0, 0);

            

        }

        private void ImgDelete_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //int row = Convert.ToInt32(((Image)sender).Tag.ToString());
            foreach(var w in Application.Current.Windows)
            {
                Window window = (Window)w;
               // MessageBox.Show(window.Name);
                if (window.Name == "MaintainVehicle")
                {
                    window.Title = "Picked the Image : " + DateTime.Now.ToString();
                }
            }
            
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;

            SetValues();

            int results = 0;

            Task.Run(async () =>
            {
                results = await VehicleManager.Insert(vehicle);
            });

            MessageBox.Show(results.ToString());
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Get the color

            int index = 0;

            index = SetValues();

            int results = 0;

            Task.Run(async () =>
            {
                results = await VehicleManager.Update(vehicle);
            });

            MessageBox.Show(results.ToString());
        }

        private int SetValues()
        {
            int index;
            vehicle.ColorId = attributes[(int)ControlMode.Color].AttributeId;
            index = attributes[(int)ControlMode.Color].cboAttribute.SelectedIndex;
            vehicle.ColorName = attributes[(int)ControlMode.Color].Colors[index].Description;

            vehicle.MakeId = attributes[(int)ControlMode.Make].AttributeId;
            index = attributes[(int)ControlMode.Make].cboAttribute.SelectedIndex;
            vehicle.MakeName = attributes[(int)ControlMode.Make].Makes[index].Description;

            vehicle.ModelId = attributes[(int)ControlMode.Model].AttributeId;
            index = attributes[(int)ControlMode.Model].cboAttribute.SelectedIndex;
            vehicle.ModelName = attributes[(int)ControlMode.Model].Models[index].Description;

            vehicle.Year = Convert.ToInt32(attributes[(int)ControlMode.Year].AttributeText);
            vehicle.VIN = txtVIN.Text;
            return index;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int results = 0;

            Task.Run(async () =>
            {
                results = await VehicleManager.Delete(vehicle.Id);
            });

            MessageBox.Show(results.ToString());
        }
    }
}
