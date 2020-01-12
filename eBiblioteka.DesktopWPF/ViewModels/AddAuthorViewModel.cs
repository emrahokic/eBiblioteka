using eBiblioteka.Model.Requests;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static eBiblioteka.Model.Gender;

namespace eBiblioteka.DesktopWPF.ViewModels
{
    public class AddAuthorViewModel : BindableBase
    {
        #region ApiServices

        private readonly APIService _apiCities = new APIService("Grad");
        private readonly APIService _apiAuthor = new APIService("Pisac");

        #endregion

        #region Attributes Pubilc/Private

        //DataInsertModel
        private PisacInsertRequest _pisacInsertRequest;




        public string AuthorFirstName
        {
            get { return _pisacInsertRequest.Ime; }
            set
            {
                SetProperty(ref _pisacInsertRequest.Ime, value);
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
        public string AuthorLastName
        {
            get { return _pisacInsertRequest.Prezime; }
            set
            {
                SetProperty(ref _pisacInsertRequest.Prezime, value);
            }
        }
        public string Biography
        {
            get { return _pisacInsertRequest.Biografija; }
            set
            {
                SetProperty(ref _pisacInsertRequest.Biografija, value);
                LeftMoreInfo = (1000 - _pisacInsertRequest.Biografija.Length) + "/1000";

            }
        }
        public DateTime? BirthDate
        {
            get { return _pisacInsertRequest.GodinaRodjenja; }
            set { 
            
                    SetProperty(ref _pisacInsertRequest.GodinaRodjenja, value);
            }
        }


        public DateTime? DeathDate
        {

            get { return _pisacInsertRequest.GodinaSmrti; }
            set
            {
                SetProperty(ref _pisacInsertRequest.GodinaSmrti, value);
            }
        }

        private string _leftMoreInfo;
        public string LeftMoreInfo
        {
            get { return _leftMoreInfo; }
            set
            {
                SetProperty(ref _leftMoreInfo, value);
            }
        }
        private bool _FRadioButton;
        public bool FRadioButton
        {
            get { return _FRadioButton; }
            set
            {
                SetProperty(ref _FRadioButton, value);
                _pisacInsertRequest.Spol = _FRadioButton ? (int)_Gender.F : (int)_Gender.M;
            }
        }
        private bool _MRadioButton;
        public bool MRadioButton
        {
            get { return _MRadioButton; }
            set
            {
                SetProperty(ref _MRadioButton, value);
                _pisacInsertRequest.Spol = _MRadioButton ? (int)_Gender.M : (int)_Gender.F;
            }
        }

        private bool _IsPassedAway;
        public bool IsPassedAway
        {
            get { return _IsPassedAway; }
            set
            {
                SetProperty(ref _IsPassedAway, value);
                DateVisibility = _IsPassedAway ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _DateVisibility;
        public Visibility DateVisibility
        {
            get { return _DateVisibility; }
            set
            {
                SetProperty(ref _DateVisibility, value);
            }
        }
        

        #endregion

        #region Commands

        public DelegateCommand SelectImageCommand { get; set; }
        public DelegateCommand InsertRequestCommand { get; set; }

        #endregion
        public AddAuthorViewModel()
        {
            SelectedImage = "./SplashScrean – 2.png";
            IsPassedAway = false;
            
            SelectImageCommand = new DelegateCommand(SelectImage, CanExecute);
            InsertRequestCommand = new DelegateCommand(InsertRequest, CanExecuteRequest).ObservesProperty(() => AuthorFirstName)
                                                                                        .ObservesProperty(() => AuthorLastName)
                                                                                        .ObservesProperty(() => BirthDate)
                                                                                        .ObservesProperty(() => DeathDate)
                                                                                        .ObservesProperty(() => Biography)
                                                                                        .ObservesProperty(() => IsPassedAway)
                                                                                        .ObservesProperty(() => FRadioButton)
                                                                                        .ObservesProperty(() => MRadioButton);
            _pisacInsertRequest = new PisacInsertRequest();
        }

        private bool CanExecuteRequest()
        {

            if (IsPassedAway)
            {
                if (DeathDate == null)
                {
                    return false;
                }
            }

            return !string.IsNullOrWhiteSpace(AuthorFirstName) &&
                   !string.IsNullOrWhiteSpace(AuthorLastName) &&
                   !string.IsNullOrWhiteSpace(Biography) &&
                   BirthDate != null && (FRadioButton || MRadioButton);
        }

        private async void InsertRequest()
        {
            if (!IsPassedAway)
            {
                _pisacInsertRequest.GodinaSmrti = null;
            }
            var result = await _apiAuthor.Insert<Model.Pisac>(_pisacInsertRequest);

        }

        private bool CanExecute()
        {
            return true;
        }

       

        private  void SelectImage()
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
                _pisacInsertRequest.SlikaByte = array;

            }
        }
       
    }
}
