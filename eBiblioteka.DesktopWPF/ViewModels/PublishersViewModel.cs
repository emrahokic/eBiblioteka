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
        public ICollection<Publisher> Libraries { get; set; }

        private async void GetLibraries()
        {
            var libraries = await _apiLibraries.Get<List<Publisher>>(null);

            Libraries.Clear();
            for (int i = 0; i < 50; i++)
            {
                Libraries.Add(libraries[i]);

            }
           
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Libraries);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Grad");

            view.GroupDescriptions.Add(groupDescription);
        }

        public PublishersViewModel()
        {
            Libraries = new ObservableCollection<Publisher>();
            GetLibraries();

        }
        
    }
}
