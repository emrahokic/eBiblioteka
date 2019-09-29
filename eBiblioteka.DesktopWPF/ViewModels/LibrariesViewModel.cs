using eBiblioteka.DesktopWPF.Helper;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace eBiblioteka.DesktopWPF.ViewModels
{
    
    public class LibrariesViewModel : BindableBase
    {
        #region ApiServices

        private readonly APIService _apiLibraries = new APIService("Biblioteka");

        #endregion
        public class Library
        {
            public string Naziv { get; set; }
            public string Grad { get; set; }
            public string Tip { get; set; }
            public string Icona { get; set; }
        }
        public ICollection<Library> Libraries { get; set; }

        private async void GetLibraries()
        {
            var libraries = await _apiLibraries.Get<List<Library>>(null);

            Libraries.Clear();

            foreach (var item in libraries)
            {
             
                Libraries.Add(item);
            }


            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Libraries);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Grad");

            view.GroupDescriptions.Add(groupDescription);
        }

        public LibrariesViewModel()
        {
            Libraries = new ObservableCollection<Library>();
            GetLibraries();

        }
    }
}
