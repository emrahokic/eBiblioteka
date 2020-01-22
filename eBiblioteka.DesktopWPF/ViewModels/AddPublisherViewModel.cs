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
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.IO;
using eBiblioteka.DesktopWPF.Helper;

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

        private string _selectedImage;
        public string SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                SetProperty(ref _selectedImage, value);
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


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get { return _isVisible; }
            set
            {
                SetProperty(ref _isVisible, value);
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

        private string _dialogtext;
        public string DialogText
        {
            get { return _dialogtext; }
            set
            {
                SetProperty(ref _dialogtext, value);
            }
        }
        private bool _isDialogOpen;
        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                SetProperty(ref _isDialogOpen, value);
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
            if (_selectedComboBoxCountry != null )
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
            else
            {
                Cities.Clear();
                CitiesIsEnabled = false;


            }

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
        public DelegateCommand SelectImageCommand { get; set; }
        public DelegateCommand MouseEnter { get; set; }

        #endregion
        public AddPublisherViewModel()
        {
            IsDialogOpen = false;

            IsBusy = false;
            IsVisible = Visibility.Hidden;
            SelectedImage = "../Images/border.png";
            CreateCommand = new DelegateCommand(CreatePublisherAsync, CanExecute).ObservesProperty(() => PublisherName)
                                                                         .ObservesProperty(() => SelectedItemCity);
            SelectImageCommand = new DelegateCommand(SelectImage, CanExecute2);
            MouseEnter = new DelegateCommand(MouseEnterCmnd, CanExecute2);

            _publisherInsertRequest = new IzdavacInsertRequest();

            Cities = new ObservableCollection<ComboBoxItem>();
            Countries = new ObservableCollection<ComboBoxItem>();
            GetCountries();
        }

        private void MouseEnterCmnd()
        {
            Console.WriteLine("HI");
        }

        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(PublisherName) &&
                   !string.IsNullOrWhiteSpace(SelectedItemCity?.Content.ToString());
        }
        private bool CanExecute2()
        {
            return true;
        }
        private async void CreatePublisherAsync()
        {
            IsBusy = true;
            IsVisible = Visibility.Visible;
            var result = await _apiPublishers.Insert<Model.Izdavac>(_publisherInsertRequest);
            if (result != null)
            {
                this.PublisherName = "";
                SelectedItemCountry = null;
                SelectedItemCity = null;
                IsBusy = false;
                IsVisible = Visibility.Hidden;
                IsDialogOpen = true;
                DialogText = "Publisher is successuful added!";
                return;
            }
            IsBusy = false;
            IsVisible = Visibility.Hidden;
            DialogText = "Publisher is successuful added!";

        }

        private void SelectImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            var result = op.ShowDialog();
            if (result == DialogResult.OK)
            {
                var img = new BitmapImage(new Uri(op.FileName));
                SelectedImage = op.FileName;
                byte[] array = File.ReadAllBytes(op.FileName);
                _publisherInsertRequest.SlikaByte = array;

            }
        }
    }
}
