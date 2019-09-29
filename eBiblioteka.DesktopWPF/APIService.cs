using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flurl.Http;
using Flurl;
using System.Threading.Tasks;
using System.Windows;
using eBiblioteka.Model;

namespace eBiblioteka.DesktopWPF
{
    public class APIService
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
        public APIService(string route)
        {
            _route = route;
        }


        public async Task<T> Auth<T>(string username,string password)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}";

            try
            {
                return await url.WithBasicAuth(username, password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Niste authentificirani");
                }
                MessageBox.Show("Netacan username ili password");
                return default(T);
            }
        }
        public async Task<T> Get<T>(object search)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}";

            try
            {
                if (search != null)
                {
                    url += "?";
                    url += await search.ToQueryString();
                }

                var o = await url.WithOAuthBearerToken(Session.JWT).GetJsonAsync<T>();
                return o;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Niste authentificirani");
                }
                Console.WriteLine(ex.Message);
                return default(T);
            }
        }
        public async Task<T> GetById<T>(object id)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}/{id}";
            try
            {
                return await url.WithOAuthBearerToken(Session.JWT).GetJsonAsync<T>();

            }
            catch (FlurlHttpException ex)
            {
              
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Niste authentificirani");
                }
                return default(T);
            }
        }

        public async Task<T> Insert<T>(object request)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}";

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

                MessageBox.Show(stringBuilder.ToString(), "Greška", MessageBoxButton.OK,MessageBoxImage.Error);
                return default(T);
            }

        }

        public async Task<T> Update<T>(int id, object request)
        {
            try
            {
                var url = $"{Properties.Settings.Default.APIUrl}/{_route}/{id}";

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

                MessageBox.Show(stringBuilder.ToString(), "Greška", MessageBoxButton.OK,MessageBoxImage.Error);
                return default(T);
            }

        }

    }
}
