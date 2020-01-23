using eBiblioteka.DesktopWPF.Helper;
using eBiblioteka.DesktopWPF.ViewModels;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.ApplicationModel.Activation;
using static eBiblioteka.DesktopWPF.ViewModels.MainWindowViewModel;

namespace eBiblioteka.DesktopWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IMyNavigation
    {

        private NotifyIcon _notifyIcon;

        private MainWindowViewModel _model;
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
            //_notifyIcon = new NotifyIcon()
            //{
            //    BalloonTipText = @"Hello, NotifyIcon!",
            //    Text = @"Hello, NotifyIcon!",
            //    Icon = new Icon("logoCube.ico"),
            //    Visible = true
                
            //};
            //_notifyIcon.ShowBalloonTip(1000);

        }

        protected override void OnActivated(EventArgs args)
        {
            string launchString = args.ToString();
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

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
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
                            if (_model.NumberOfNotifications == 0)
                            {
                            _model.NumberOfNotifications = null;

                            }
                        }
                        
                        _model.Notifikacije[i].Nova = false;

                    }

                }
            }
            Change(_model.NumberOfNotificationsOnIcon);
        }

      
        public void Change(string Count)
        {
            //Console.WriteLine("HelloFromInterface");
            //ListViewMenu.SelectedItem = null;
            //MoveCursorMenu(0);
            //G_root.Children.Clear();
            //G_root.Children.Add(new AddPublisher());
            
            int iconWidth = 20;
            int iconHeight = 20;

            string countText = Count;

            RenderTargetBitmap bmp =
                new RenderTargetBitmap(iconWidth, iconHeight, 96, 96, PixelFormats.Default);

            ContentControl root = new ContentControl();
            
            root.ContentTemplate = ((DataTemplate)Resources["OverlayIcon"]);
            root.Content = countText;
            root.Arrange(new Rect(0, 0, iconWidth, iconHeight));
            if (Count.Equals(""))
            {
                root.Visibility = Visibility.Hidden;

            }
            bmp.Render(root);

            TaskbarItemInfo.Overlay = (ImageSource)bmp;
        }
    }
}
