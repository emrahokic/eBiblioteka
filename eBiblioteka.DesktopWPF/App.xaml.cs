using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using eBiblioteka.DesktopWPF.ViewModels;
using eBiblioteka.DesktopWPF.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace eBiblioteka.DesktopWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new Login()
            {
                DataContext = new LoginViewModel()
            };

            
            window.ShowDialog();
            AppCenter.Start("59382f16-d319-4e25-8c4f-7b253f24f318",
                   typeof(Analytics), typeof(Crashes));
        }

    }
}
