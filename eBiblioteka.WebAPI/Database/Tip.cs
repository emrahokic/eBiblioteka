using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Tip
    {
        public Tip()
        {
            Biblioteka = new HashSet<Biblioteka>();
        }

        public string Naziv { get; set; }
        public int TipId { get; set; }
        public string Icona { get; set; }

        public ICollection<Biblioteka> Biblioteka { get; set; }
    }
}
