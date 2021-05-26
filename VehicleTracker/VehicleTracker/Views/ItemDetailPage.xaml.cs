using System.ComponentModel;
using VehicleTracker.ViewModels;
using Xamarin.Forms;

namespace VehicleTracker.Views
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