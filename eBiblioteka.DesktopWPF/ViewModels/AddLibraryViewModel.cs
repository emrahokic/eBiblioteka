using eBiblioteka.DesktopWPF.Views;
using eBiblioteka.Model.Requests;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace eBiblioteka.DesktopWPF.ViewModels
{
    public class AddLibraryViewModel : BindableBase
    {
      
        #region ApiServices

        private readonly APIService _apiCities = new APIService("Grad");
        private readonly APIService _apiTip = new APIService("Tip");
        private readonly APIService _apiCountries = new APIService("Drzava");
        private readonly APIService _apiLibraries = new APIService("Biblioteka");

        #endregion

        #region Private SelectedComboBoxItems
        
        private ComboBoxItem _selectedComboBoxType;
        private ComboBoxItem _selectedComboBoxCity;
        private ComboBoxItem _selectedComboBoxCountry;

        #endregion

        #region Collections of ComboBoxes

        public ICollection<ComboBoxItem> Types { get; set; }
        public ICollection<ComboBoxItem> Cities { get; set; }
        public ICollection<ComboBoxItem> Countries { get; set; }

        #endregion


        #region Attributes Pubilc/Private, Public ComboBoxes

        //DataInsertModel
        private BibliotekaInsertRequest _bibliotekaInsert;

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

        private bool _typesIsEnabled;
        public bool TypesIsEnabled
        {
            get { return _typesIsEnabled; }
            set
            {
                SetProperty(ref _typesIsEnabled, value);
            }
        }


        public string LibraryName
        {
            get { return _bibliotekaInsert.Naziv; }
            set
            {
                SetProperty(ref _bibliotekaInsert.Naziv, value);
            }
        }
        
        public string Address
        {
            get { return _bibliotekaInsert.Adresa; }
            set
            {
                SetProperty(ref _bibliotekaInsert.Adresa, value);
            }
        }

        public string PhoneNumber
        {
            get { return _bibliotekaInsert.BrTelefon; }
            set
            {
                SetProperty(ref _bibliotekaInsert.BrTelefon, value);
            }
        }

        public string GPSCoordinates
        {
            get { return _bibliotekaInsert.LatLong; }
            set
            {
                SetProperty(ref _bibliotekaInsert.LatLong, value);
            }
        }

        [Required]
        public string Email
        {
            get { return _bibliotekaInsert.Email; }
            set
            {
                SetProperty(ref _bibliotekaInsert.Email, value);
            }
        }
        public string Opis
        {
            get { return _bibliotekaInsert.Opis; }
            set
            {
                SetProperty(ref _bibliotekaInsert.Opis, value);
                LeftMoreInfo = (300 - _bibliotekaInsert.Opis.Length) + "/300";

            }
        }

        public ComboBoxItem SelectedItemType
        {
            get { return _selectedComboBoxType; }
            set
            {
                SetProperty(ref _selectedComboBoxType, value);
                _bibliotekaInsert.Tip_ = value.Content.ToString();
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
                _bibliotekaInsert.Grad_ = value?.Content.ToString();

            }
        }

        private string _leftMoreInfo;
        public  string LeftMoreInfo
        {
            get { return _leftMoreInfo; }
            set
            {
                SetProperty(ref _leftMoreInfo, value);
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
        private async void GetTip()
        {
            var tip = await _apiTip.Get<List<Model.Tip>>(null);
            Types.Clear();
            foreach (var item in tip)
            {
                Types.Add(new ComboBoxItem() { Content = item.Naziv});

            }
            TypesIsEnabled = true;

        }

        #endregion

        #region Commands

        public DelegateCommand CreateCommand { get; set; }

        #endregion


        #region Create Library Method

        private async void CreateLibrary()
        {
          
            var result = await _apiLibraries.Insert<Model.Biblioteka>(_bibliotekaInsert);
            if (result!=null)
            {
                this.Address = "";
                this.Email = "";
                this.LibraryName = "";
                this.Opis = "";
                this.GPSCoordinates = "";
                this.PhoneNumber = "";
                MessageBox.Show("Library is successuful added!");
                return;

            }
            MessageBox.Show("Something went worng");

        }

        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(LibraryName) &&
                   !string.IsNullOrWhiteSpace(Address) &&
                   !string.IsNullOrWhiteSpace(PhoneNumber)  &&
                   !string.IsNullOrWhiteSpace(Email)  &&
                   !string.IsNullOrWhiteSpace(Opis) &&
                   !string.IsNullOrWhiteSpace(GPSCoordinates) &&
                   !string.IsNullOrWhiteSpace(SelectedItemType?.Content.ToString()) &&
                   !string.IsNullOrWhiteSpace(SelectedItemCity?.Content.ToString());
        }
        #endregion
        public AddLibraryViewModel()
        {
            CreateCommand = new DelegateCommand(CreateLibrary, CanExecute).ObservesProperty(() => LibraryName)
                                                                          .ObservesProperty(() => Address)
                                                                          .ObservesProperty(() => PhoneNumber)
                                                                          .ObservesProperty(() => Email)
                                                                          .ObservesProperty(() => Opis)
                                                                          .ObservesProperty(() => GPSCoordinates)
                                                                          .ObservesProperty(() => SelectedItemCity)
                                                                          .ObservesProperty(() => SelectedItemType);
            _bibliotekaInsert = new BibliotekaInsertRequest();

            Types = new ObservableCollection<ComboBoxItem>();
            Cities = new ObservableCollection<ComboBoxItem>();
            Countries = new ObservableCollection<ComboBoxItem>();
            GetTip();
            GetCountries();

            
        }
    }
}
