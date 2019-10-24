using eBiblioteka.DesktopWPF.Helper;
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

namespace eBiblioteka.DesktopWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static  MainWindow _mainWindowsInstance;
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
