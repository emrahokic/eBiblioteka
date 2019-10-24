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
using System.Windows.Forms;

namespace eBiblioteka.DesktopWPF.ViewModels
{
    public class AddAuthorViewModel : BindableBase
    {
        #region firebaseStorage creds
        private readonly string ApiKey = "AIzaSyAM2tQJ6ae_6F6gUJcPhzWo4HQmFr9cvt0";
        private readonly string Bucket = "ebiblioteka2019.appspot.com";
        private readonly string AuthEmail = "ebiblioteka@rs2.fit.ba";
        private readonly string AuthPassword = "NekiKulJakPrejakPass";
        #endregion

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

        public string BirthDate
        {
            get { return _pisacInsertRequest.GodinaRodjenja; }
            set
            {
                SetProperty(ref _pisacInsertRequest.GodinaRodjenja, value);
            }
        }

        public string DeathDate
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


        #endregion

        #region Commands

        public DelegateCommand SelectImageCommand { get; set; }
        public DelegateCommand InsertRequestCommand { get; set; }

        #endregion
        public AddAuthorViewModel()
        {
            SelectedImage = "./SplashScrean – 2.png";

            SelectImageCommand = new DelegateCommand(SelectImage, CanExecute);
            InsertRequestCommand = new DelegateCommand(InsertRequest, CanExecuteRequest);
            _pisacInsertRequest = new PisacInsertRequest();
        }

        private bool CanExecuteRequest()
        {
            return true;

        }

        private async void InsertRequest()
        {

            var result = await _apiAuthor.Insert<Model.Pisac>(_pisacInsertRequest);
        }

        private bool CanExecute()
        {
            return true;
        }

        private string x;

        private async void SelectImage()
        {

        }
    }
}
