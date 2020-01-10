using eBiblioteka.DesktopWPF.Helper;
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

namespace eBiblioteka.DesktopWPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
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

        public MainWindowViewModel()
        {
            
            FullName =  APIService.Session.ImePrezime;

            navlist = new ObservableCollection<NavbarItem>();
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
        }

    }
}
