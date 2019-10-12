using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Pisac
    {
        public Pisac()
        {
            KnjigaPisac = new HashSet<KnjigaPisac>();
        }

        public int PisacId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime? GodinaRodjenja { get; set; }
        public DateTime? GodinaSmrti { get; set; }

        public string Biografija { get; set; }
        public int? Spol { get; set; }

        public string Slika { get; set; }

        public virtual ICollection<KnjigaPisac> KnjigaPisac { get; set; }
    }
}
