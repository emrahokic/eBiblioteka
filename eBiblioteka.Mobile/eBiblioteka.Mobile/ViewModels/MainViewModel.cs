using eBiblioteka.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static eBiblioteka.Mobile.Services.APIService;

namespace eBiblioteka.Mobile.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private readonly APIService _apiCountries = new APIService("Drzava");


        public MainViewModel()
        {
            _username = Session.ImePrezime;
            LoadCommand = new Command(async () => await Load());
            _data = new ObservableCollection<string>();
        }
        string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }


        async Task Load()
        {
            var _result = await _apiCountries.Get<List<Model.Drzava>>(null);
            _data.Clear();
            foreach (var item in _result)
            {
                _data.Add(item.Naziv);
            }
        }

        public ICommand LoadCommand { get; set; }


        ObservableCollection<string> _data;
        public ObservableCollection<string> data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }
    }
}
