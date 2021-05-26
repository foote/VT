using System;
using System.Collections.Generic;
using System.ComponentModel;
using VehicleTracker.Models;
using VehicleTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VehicleTracker.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}