using BDF.VehicleTracker.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BDF.VehicleTracker.Mobile.Views
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