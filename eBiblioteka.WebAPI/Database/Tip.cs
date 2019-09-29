using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Database
{
    public class Tip
    {
        public Tip()
        {
            Biblioteka = new HashSet<Biblioteka>();
        }
        public int TipId { get; set; }
        public string Naziv { get; set; }
        public string Icona { get; set; }

        public virtual ICollection<Biblioteka> Biblioteka { get; set; }

    }
}
