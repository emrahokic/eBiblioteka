using eBiblioteka.Model.Requests;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace eBiblioteka.DesktopWPF.ViewModels
{
    public class AddPublisherViewModel : BindableBase
    {
        #region ApiServices

        private readonly APIService _apiCities = new APIService("Grad");
        private readonly APIService _apiPublishers = new APIService("Izdavac");
        private readonly APIService _apiCountries = new APIService("Drzava");

        #endregion

        #region Private SelectedComboBoxItems

        private ComboBoxItem _selectedComboBoxCity;
        private ComboBoxItem _selectedComboBoxCountry;

        #endregion

        #region Collections of ComboBoxes

        public ICollection<ComboBoxItem> Cities { get; set; }
        public ICollection<ComboBoxItem> Countries { get; set; }

        #endregion


        #region Attributes Pubilc/Private, Public ComboBoxes

        //DataInsertModel
        private IzdavacInsertRequest _publisherInsertRequest;
        private bool _citiesIsEnabled;
        public bool CitiesIsEnabled
        {
            get { return _citiesIsEnabled; }
            set
            {
                SetProperty(ref _citiesIsEnabled, value);
            }
        }

        private bool _countriesIsEnabled;
        public bool CountriesIsEnabled
        {
            get { return _countriesIsEnabled; }
            set
            {
                SetProperty(ref _countriesIsEnabled, value);
            }
        }

        public string PublisherName
        {
            get { return _publisherInsertRequest.Naziv; }
            set
            {
                SetProperty(ref _publisherInsertRequest.Naziv, value);
            }
        }

   


    
        public ComboBoxItem SelectedItemCountry
        {
            get { return _selectedComboBoxCountry; }
            set
            {
                SetProperty(ref _selectedComboBoxCountry, value);
                GetCities();
            }
        }
        public ComboBoxItem SelectedItemCity
        {
            get { return _selectedComboBoxCity; }
            set
            {
                SetProperty(ref _selectedComboBoxCity, value);
                _publisherInsertRequest.Grad_ = value?.Content.ToString();

            }
        }

    
        #endregion

        #region Async_getMethods

        private async void GetCities()
        {
            var gradovi = await _apiCities.Get<List<Model.Grad>>(new GradSearchRequest()
            {
                DrzavaNaziv = _selectedComboBoxCountry.Content.ToString()
            });
            Cities.Clear();

            foreach (var item in gradovi)
            {
                Cities.Add(new ComboBoxItem() { Content = item.Naziv, Tag = item.GradId });

            }
            CitiesIsEnabled = true;
        }
        private async void GetCountries()
        {
            var drzave = await _apiCountries.Get<List<Model.Drzava>>(null);
            Countries.Clear();
            foreach (var item in drzave)
            {
                Countries.Add(new ComboBoxItem() { Content = item.Naziv });
            }
            CountriesIsEnabled = true;
        }
    

        #endregion

        #region Commands

        public DelegateCommand CreateCommand { get; set; }

        #endregion
        public AddPublisherViewModel()
        {
            CreateCommand = new DelegateCommand(CreatePublisherAsync, CanExecute).ObservesProperty(() => PublisherName)
                                                                         .ObservesProperty(() => SelectedItemCity);
            _publisherInsertRequest = new IzdavacInsertRequest();

            Cities = new ObservableCollection<ComboBoxItem>();
            Countries = new ObservableCollection<ComboBoxItem>();
            GetCountries();
        }

        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(PublisherName) &&
                   !string.IsNullOrWhiteSpace(SelectedItemCity?.Content.ToString());
        }

        private async void CreatePublisherAsync()
        {
            var result = await _apiPublishers.Insert<Model.Izdavac>(_publisherInsertRequest);
            if (result != null)
            {
                this.PublisherName = "";
                MessageBox.Show("Publisher is successuful added!");
                return;
            }
            MessageBox.Show("Something went worng");
        }
    }
}
