using eBiblioteka.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eBiblioteka.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel model = null;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = model = new MainViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.LoadCommand.Execute(null);
            
        }
    }
}
