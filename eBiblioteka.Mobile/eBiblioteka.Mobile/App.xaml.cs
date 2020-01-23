using eBiblioteka.Mobile.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eBiblioteka.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {

            AppCenter.Start("0668f51e-af2c-48f1-98d6-2a3f832a29b2",
                   typeof(Push));
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
