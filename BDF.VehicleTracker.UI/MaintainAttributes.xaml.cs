using BDF.VehicleTracker.BL;
using BDF.VehicleTracker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BDF.VehicleTracker.UI
{

    public enum ScreenMode
    {
        Make = 1,
        Model = 2
    }

    /// <summary>
    /// Interaction logic for MaintainAttributes.xaml
    /// </summary>
    public partial class MaintainAttributes : Window
    {
        List<Model> models;
        List<Make> makes;
        ScreenMode screenMode;

        public MaintainAttributes(ScreenMode screenmode)
        {
            InitializeComponent();
            screenMode = screenmode;

            Reload();

            cboAttribute.DisplayMemberPath = "Description";
            cboAttribute.SelectedValuePath = "Id";
            lblAttribute.Content = screenMode.ToString() + "s:";
            this.Title = "Maintain " + screenMode.ToString() + "s";
        }

        private async void Reload()
        {
            cboAttribute.ItemsSource = null;

            switch (screenMode)
            {
                case ScreenMode.Make:
                    makes = (List<Make>)await MakeManager.Load();
                    cboAttribute.ItemsSource = makes;
                    break;
                case ScreenMode.Model:
                    models = (List<Model>)await ModelManager.Load();
                    cboAttribute.ItemsSource = models;
                    break;
            }
            cboAttribute.DisplayMemberPath = "Description";
            cboAttribute.SelectedValuePath = "Id";
        }

        private void Rebind(int index)
        {
            cboAttribute.ItemsSource = null;

            switch (screenMode)
            {
                case ScreenMode.Make:
                    cboAttribute.ItemsSource = makes;
                    break;
                case ScreenMode.Model:
                    cboAttribute.ItemsSource = models;
                    break;
                default:
                    break;
            }
            cboAttribute.DisplayMemberPath = "Description";
            cboAttribute.SelectedValuePath = "Id";
            cboAttribute.SelectedIndex = index;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Make:
                    Make make = new Make { Description = txtDescription.Text };

                    //int results = MakeManager.Insert(new Make { Description = txtDescription.Text }).Result;

                    //Task task = MakeManager.Insert(make);

                    Task.Run(async () =>
                    {
                        int results = await MakeManager.Insert(make);
                    });

                    //var results = await MakeManager.Insert(make);
                    makes.Add(make);
                    Rebind(makes.Count - 1);
                    break;
                case ScreenMode.Model:
                    Model model = new Model { Description = txtDescription.Text };

                    Task.Run(async () =>
                    {
                        int results = await ModelManager.Insert(model);
                    });

                    models.Add(model);
                    Rebind(makes.Count - 1);
                    break;
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Make:
                    Make make = makes[cboAttribute.SelectedIndex];
                    make.Description = txtDescription.Text;
                    Task.Run(async () =>
                    {
                        await MakeManager.Update(make);
                    });
                    makes[cboAttribute.SelectedIndex].Description = txtDescription.Text;
                    Rebind(cboAttribute.SelectedIndex);
                    break;
                case ScreenMode.Model:
                    Model model = models[cboAttribute.SelectedIndex];
                    model.Description = txtDescription.Text;
                    Task.Run(async () =>
                    {
                        await ModelManager.Update(model);
                    });
                    Rebind(cboAttribute.SelectedIndex);
                    models[cboAttribute.SelectedIndex].Description = txtDescription.Text;
                    break;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int results = 0;
                switch (screenMode)
                {
                    case ScreenMode.Make:
                        Make make = makes[cboAttribute.SelectedIndex];
                        Task.Run(async () =>
                        {
                            results = await MakeManager.Delete(make.Id);

                        });
                        makes.Remove(make);
                        Rebind(0);
                        break;
                    case ScreenMode.Model:
                        Model model = models[cboAttribute.SelectedIndex];
                        Task.Run(async () =>
                        {
                            await ModelManager.Delete(model.Id);
                        });
                        models.Remove(model);
                        Rebind(0);
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CboAttribute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAttribute.SelectedIndex > -1)
            {
                if (screenMode == ScreenMode.Make)
                    txtDescription.Text = makes[cboAttribute.SelectedIndex].Description;
                else
                    txtDescription.Text = models[cboAttribute.SelectedIndex].Description;
            }
        }
    }
}
