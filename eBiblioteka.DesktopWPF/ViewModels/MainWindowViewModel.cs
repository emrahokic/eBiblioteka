using eBiblioteka.DesktopWPF.Helper;
using eBiblioteka.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eBiblioteka.DesktopWPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private readonly APIService _auth = new APIService("Korisnik/Auth");
        private readonly APIService _apiNotifikacija = new APIService("Notifikacija");
        public IMyNavigation myNavigation;
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                SetProperty(ref _fullName, value);
            }

        }

        public string BibliotekaNaziv
        {
            get { return APIService.Session.BibliotekaNaziv; }
        }

        public string KorisnikSlika
        {
            get { return APIService.Session.Slika; }
        }

        public class NavbarItem
        {
            public string Name { get; set; }
            public string TextStyle { get; set; }
        }

        public ICollection<NavbarItem> navlist { get; set; }

        private int? _numberOfNotifications;
        public int? NumberOfNotifications
        {
            get { return _numberOfNotifications; }
            set
            {
                SetProperty(ref _numberOfNotifications, value);
                NumberOfNotificationsOnIcon = NumberOfNotifications > 99 ? "99" : NumberOfNotifications.ToString();
            }

        }

        private string _numberOfNotificationsOnIcon;
        public string NumberOfNotificationsOnIcon
        {
            get { return _numberOfNotificationsOnIcon; }
            set
            {
                SetProperty(ref _numberOfNotificationsOnIcon, value);

            }

        }


        public class InAppNotification : BindableBase
        {
            public InAppNotification()
            {
                _notifikacija = new Notifikacija();

            }
            bool _nova;
            public bool Nova
            {
                get { return _nova; }
                set
                {
                    SetProperty(ref _nova, value);
                    IsNew = _nova ? Visibility.Visible : Visibility.Collapsed;
                    
                }
            }
            Model.Notifikacija _notifikacija;
            public Model.Notifikacija NotifikacijaObj
            {
                get { return _notifikacija; }
                set
                {
                    SetProperty(ref _notifikacija, value);
                }
            }

            Visibility _isNew;
            public Visibility IsNew {
                get { return _isNew; }
                set
                {
                    SetProperty(ref _isNew, value);
                }

            }
        }

        private ObservableCollection<InAppNotification> _notifikacije;
        public ObservableCollection<InAppNotification> Notifikacije
        {
            get { return _notifikacije; }
            set
            {
                SetProperty(ref _notifikacije, value);
            }
        }

        HubConnection hubConnection;
        bool connected;

        public MainWindowViewModel()
        {
            FullName =  APIService.Session.ImePrezime;
            NumberOfNotifications = 0;
            navlist = new ObservableCollection<NavbarItem>();
            Notifikacije = new ObservableCollection<InAppNotification>();
            if (APIService.Session.Role.Contains(Stringovi.SuperAdmin))
            {
                navlist.Add(new NavbarItem() { Name = Stringovi.Home, TextStyle ="Bold"});
                navlist.Add(new NavbarItem() { Name = Stringovi.Libraries, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddLibrary, TextStyle = "Regular" });

            }
            else if (APIService.Session.Role.Contains(Stringovi.Admin))
            {
                navlist.Add(new NavbarItem() { Name = Stringovi.Publishers, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddPublisher, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Authors, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddAuthor, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Books, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddBook, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Reservations, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddGenre, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Members, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddMember, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Employees, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddEmployee, TextStyle = "Regular" });

            }
            else if (APIService.Session.Role.Contains(Stringovi.Uposlenik))
            {
                navlist.Add(new NavbarItem() { Name = Stringovi.Authors, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddAuthor, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Books, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddBook, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Reservations, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddGenre, TextStyle = "Regular" });
                navlist.Add(new NavbarItem() { Name = Stringovi.Members, TextStyle = "Bold" });
                navlist.Add(new NavbarItem() { Name = Stringovi.AddMember, TextStyle = "Regular" });
            }

            getNotifications();

            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:57049/notifications")
                .Build();

            hubConnection.On<Model.Notifikacija>("SendNotification", (item) =>
            {

                if (item != null)
                {
                    if (NumberOfNotifications == null)
                    {
                        NumberOfNotifications = 0;
                    }

                    var x = new InAppNotification();
                    x.NotifikacijaObj.ClanImage = item.ClanImage;
                    x.NotifikacijaObj.Clan = item.Clan;
                    x.NotifikacijaObj.Datum = item.Datum;
                    x.Nova = true;
                    x.NotifikacijaObj.Opis = item.Opis;
                    x.NotifikacijaObj.NotifikacijaId = item.NotifikacijaId;
                    Notifikacije.Insert(0,x);
                    NumberOfNotifications++;

                }
                myNavigation.Change(NumberOfNotificationsOnIcon);
                ToastNotifikacija.Noti(item.Clan,item.Opis,"","Rezervacija",item.ClanImage);
            });

            connect();

        }

       

        private bool CanExecute()
        {
            return true;
        }

        private async void connect()
        {
            try
            {
                if (!connected)
                {
                    await hubConnection.StartAsync();
                }
                connected = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                throw;
            }
        }

        private async void getNotifications()
        {

            
            List<Model.Notifikacija> notifikacije = await _apiNotifikacija.Get<List<Model.Notifikacija>>(null);
            
            if (notifikacije !=null)
            {
                foreach (var item in notifikacije)
                {
                    var x = new InAppNotification();
                    x.NotifikacijaObj.ClanImage = item.ClanImage;
                    x.NotifikacijaObj.Clan = item.Clan;
                    x.NotifikacijaObj.Datum = item.Datum;
                    x.Nova = true;
                    x.NotifikacijaObj.Opis = item.Opis;
                    x.NotifikacijaObj.NotifikacijaId = item.NotifikacijaId;
                    Notifikacije.Add(x);
                    if ((bool)item.Datum?.ToString("dd.MM.yyyy").Equals(DateTime.Now.ToString("dd.MM.yyyy")))
                    {
                        NumberOfNotifications++;
                    }
                }
              myNavigation.Change(NumberOfNotificationsOnIcon);
                
            }

        }
    }
}
