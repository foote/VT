using BDF.VehicleTracker.MobileUI.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BDF.VehicleTracker.MobileUI.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}