using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Rola
    {
        public Rola()
        {
            KorisnikRola = new HashSet<KorisnikRola>();
        }

        public int RolaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<KorisnikRola> KorisnikRola { get; set; }
    }
}
