using eBiblioteka.Model;
using Flurl.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eBiblioteka.Mobile.Services
{
    class APIService
    {
        public static class Session
        {

            public static string ImePrezime { get; set; }
            public static string BibliotekaNaziv { get; set; }
            public static string Slika { get; set; }
            public static string JWT { get; set; }
            public static List<string> Role { get; set; }
        }

        private readonly string _route;

#if DEBUG
        private string _apiUrl = "https://ebibliotekawebapi2-prod.us-west-2.elasticbeanstalk.com/api";
#endif
#if RELEASE
        private string _apiUrl = "https://ebibliotekawebapi2-prod.us-west-2.elasticbeanstalk.com/api";

        //private string _apiUrl = "https://mywebsite.azure.com/api/";
#endif

        public APIService(string route)
        {
            _route = route;
        }

        public async Task<T> Auth<T>(string username, string password)
        {
            var url = $"{_apiUrl}/{_route}";

            try
            {

                return await url.WithBasicAuth(username, password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    //MessageBox.Show("Niste authentificirani");
                    await Application.Current.MainPage.DisplayAlert("Greška", "Niste authentificirani", "OK");
                }
                return default(T);
            }
        }


        public async Task<T> Get<T>(object search)
        {
            var url = $"{_apiUrl}/{_route}";

            try
            {
                if (search != null)
                {
                    url += "?";
                    url += await search.ToQueryString();
                }

                return await url.WithOAuthBearerToken(Session.JWT).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    //MessageBox.Show("Niste authentificirani");
                    await Application.Current.MainPage.DisplayAlert("Greška", "Niste authentificirani", "OK");
                }
                throw;
            }
        }

        public async Task<T> GetById<T>(object id)
        {
            var url = $"{_apiUrl}/{_route}/{id}";

            return await url.WithOAuthBearerToken(Session.JWT).GetJsonAsync<T>();
        }

        public async Task<T> Insert<T>(object request)
        {
            var url = $"{_apiUrl}/{_route}";

            try
            {
                return await url.WithOAuthBearerToken(Session.JWT).PostJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }

                await Application.Current.MainPage.DisplayAlert("Greška", stringBuilder.ToString(), "OK");
                return default(T);
            }

        }

        public async Task<T> Update<T>(int id, object request)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";

                return await url.WithOAuthBearerToken(Session.JWT).PutJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, string[]>>();

                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine($"{error.Key}, ${string.Join(",", error.Value)}");
                }

                await Application.Current.MainPage.DisplayAlert("Greška", stringBuilder.ToString(), "OK");
                return default(T);
            }

        }
    }
}