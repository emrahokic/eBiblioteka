using eBiblioteka.Mobile.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eBiblioteka.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly APIService _auth = new APIService("Korisnik/Auth");

        public LoginViewModel()
        {

            LoginCommand = new Command(async () => await Login());
            Console.WriteLine("");
        }
        string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public ICommand LoginCommand { get; set; }

        async Task Login()
        {
            IsBusy = true;

            try
            {
               var _result = await _auth.Auth<Model.Korisnik>(Username, Password);

                if (_result != null)
                {
                    if (!_result.Token.Equals(""))
                    {
                        APIService.Session.JWT = _result.Token;

                        var jwt = APIService.Session.JWT;
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadJwtToken(jwt);
                        if (jsonToken.ValidTo.ToLocalTime().CompareTo(DateTime.Now.ToLocalTime()) < 1)
                        {

                            APIService.Session.JWT = "";
                            APIService.Session.Role = null;
                            return;
                        }

                        APIService.Session.BibliotekaNaziv = _result.BibliotekaNaziv;
                        APIService.Session.ImePrezime = _result.Ime + " " + _result.Prezime;
                        APIService.Session.Slika = _result.Slika;

                        APIService.Session.Role = new List<string>();
                        foreach (var claim in jsonToken.Claims)
                        {
                            if (claim.Type.Equals("role"))
                            {
                                APIService.Session.Role.Add(claim.Value);
                            }

                        }
                        Application.Current.MainPage = new MainPage();


                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}

