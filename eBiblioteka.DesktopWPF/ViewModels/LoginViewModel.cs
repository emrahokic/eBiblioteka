using eBiblioteka;
using eBiblioteka.DesktopWPF.Views;
using eBiblioteka.MVVM;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace eBiblioteka.DesktopWPF.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly APIService _auth = new APIService("Korisnik/Authenticate");


        private string _email;
        private string _password;

        private bool _rememberMe;

        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email,value);
            }
        }

        [Required]
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value);
            }

        }



        public bool RememberMe
        {
            get { return _rememberMe; }
            set { _rememberMe = value; SetProperty(ref _rememberMe, value); }
        }

        public bool IsValid
        {
            get
            {
                return !String.IsNullOrWhiteSpace(Email) && Email.Contains("@") && Email.Contains(".") &&
                       !String.IsNullOrWhiteSpace(Password);
            }
        }

        public DelegateCommand LoginCommand { get; set; }

    

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(ExecuteAsync, CanExecute).ObservesProperty(()=>Email).ObservesProperty(()=>Password);
        }

        public async void ExecuteAsync()
        {
            Console.WriteLine("hi");
            var result = await _auth.Auth<Model.Korisnik>(Email,Password);
            if (result != null)
            {
                if (!result.Token.Equals(""))
                {
                    APIService.Session.JWT = result.Token;

                    var jwt = APIService.Session.JWT;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadJwtToken(jwt);
                    if (jsonToken.ValidTo.ToLocalTime().CompareTo(DateTime.Now.ToLocalTime())<1)
                    {

                        APIService.Session.JWT = "";
                        APIService.Session.Role = null;
                        return;
                    }

                    APIService.Session.BibliotekaNaziv = result.BibliotekaNaziv;
                    APIService.Session.ImePrezime = result.Ime + " " + result.Prezime;
                    APIService.Session.Slika= result.Slika;

                    APIService.Session.Role = new List<string>();
                    foreach (var claim in jsonToken.Claims)
                    {
                        if (claim.Type.Equals("role"))
                        {
                            APIService.Session.Role.Add(claim.Value);
                        }

                    }

                    var mv = new MainWindow();
                    mv.Show();

                    Application.Current.MainWindow.Close();
                }
            }
           
        }

        public bool CanExecute()
        {
            return !String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(Password);
        }

        private void Login(string email, string password, bool remeberMe)
        {
            //call api

        }
        //protected override string OnValidate(string propertyName)
        //{
        //    if (Password == null || Password?.Length < 4 && propertyName.Equals("Password"))
        //    {
        //        return "Password prekratak";
        //    }
        //    if (Email == null || Email?.Length < 1 && propertyName.Equals("Email"))
        //    {
        //        return "Email required";
        //    }
        //    return base.OnValidate(propertyName);
        //}
    }
}
