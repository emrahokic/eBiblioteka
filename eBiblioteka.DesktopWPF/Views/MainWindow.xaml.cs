using eBiblioteka.DesktopWPF.Helper;
using eBiblioteka.DesktopWPF.ViewModels;
using Prism.Mvvm;
using Prism.Unity;
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
using static eBiblioteka.DesktopWPF.ViewModels.MainWindowViewModel;

namespace eBiblioteka.DesktopWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IMyNavigation
    {


        private  MainWindowViewModel _model;
        public IMyNavigation myNavigation;
        public MainWindow()
        {
            
            InitializeComponent();
            _model = DataContext as MainWindowViewModel;
            _model.myNavigation = myNavigation = this;
        }
        private void Shutdown(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void Drag(object sender, RoutedEventArgs e)
        {

            this.DragMove();
        }
        private void Maximize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            MaximizeWindowBtn.Visibility = Visibility.Collapsed;
            RestoreWindowBtn.Visibility = Visibility.Visible;
        }
        private void Restore(object sender, RoutedEventArgs e)
        {
            
            this.WindowState = WindowState.Normal;
            MaximizeWindowBtn.Visibility = Visibility.Visible;
            RestoreWindowBtn.Visibility = Visibility.Collapsed;
        }
        
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var index = ListViewMenu.SelectedIndex;
            var item = ListViewMenu.SelectedItem as ViewModels.MainWindowViewModel.NavbarItem;
            if (item == null)
            {
                return;
            }
            var name = item.Name;

            MoveCursorMenu(index);

            switch (name)
            {
                case Stringovi.Home:
                    {
                        //G_root.Children.Clear();
                        //G_root.Children.Add(new Home());
                    }
                    break;
                case Stringovi.Libraries:
                    {
                        G_root.Children.Clear();
                        G_root.Children.Add(new Libraries());
                    }
                    break;
                case Stringovi.AddLibrary:
                    {
                        G_root.Children.Clear();
                        G_root.Children.Add(new AddLibrary());
                    } break;
                case Stringovi.Publishers:
                    {
                        G_root.Children.Clear();
                        G_root.Children.Add(new Publishers());
                    } break;
                case Stringovi.AddPublisher:
                    {
                        G_root.Children.Clear();
                        G_root.Children.Add(new AddPublisher());
                    } break;
                case Stringovi.AddAuthor:
                    {
                        G_root.Children.Clear();
                        G_root.Children.Add(new AddAuthor());
                    } break;
                default: { } break;
            }



        }


    
        private void MoveCursorMenu(int index)
        {
            TrainsitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 0, 0, 385 - (50 * index));
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewMenu.SelectedItem = null;
            G_root.Children.Clear();
            G_root.Children.Add(new AddAuthor());
            var item = (sender as StackPanel).DataContext as InAppNotification;
           
            for (int i = 0; i < _model.Notifikacije.Count; i++)
            {
                if (_model.Notifikacije[i].NotifikacijaObj.NotifikacijaId == item.NotifikacijaObj.NotifikacijaId )
                {
                    if (_model.Notifikacije[i].Nova)
                    {
                        if (_model.NumberOfNotifications > 0)
                        {
                        _model.NumberOfNotifications--;
                        }
                        else
                        {
                            _model.NumberOfNotifications = null;
                        }
                        _model.Notifikacije[i].Nova = false;

                    }

                }
            }
        }

        public void Change()
        {
            Console.WriteLine("HelloFromInterface");
            ListViewMenu.SelectedItem = null;
            MoveCursorMenu(0);
            G_root.Children.Clear();
            G_root.Children.Add(new AddPublisher());
        }
    }
}
