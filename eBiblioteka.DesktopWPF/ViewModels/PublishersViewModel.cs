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
    public class PublishersViewModel : BindableBase
    {
        #region ApiServices

        private readonly APIService _apiLibraries = new APIService("Izdavac");

        #endregion
        public class Publisher
        {
            public string Naziv { get; set; }
            public string Grad { get; set; }

        }
        public ICollection<Publisher> Publishers { get; set; }

        private async void GetLibraries()
        {
            var libraries = await _apiLibraries.Get<List<Publisher>>(null);

            Publishers.Clear();
            /*bug
            *
            *
            *
            *
            *
            *
            *
            *
            *
            *
            */

            foreach (var item in libraries)
            {
                Publishers.Add(item);
            }
           
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Publishers);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Grad");

            view.GroupDescriptions.Add(groupDescription);
        }

        public PublishersViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
            GetLibraries();
            //gg
        }
        
    }
}
