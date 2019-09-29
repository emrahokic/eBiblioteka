using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Knjiga
    {
        public Knjiga()
        {
            KnjigaIzdavanje = new HashSet<KnjigaIzdavanje>();
            KnjigaPisac = new HashSet<KnjigaPisac>();
            KnjigaRezervacija = new HashSet<KnjigaRezervacija>();
            KnjigaZanr = new HashSet<KnjigaZanr>();
        }

        public int KnjigaId { get; set; }
        public string Naziv { get; set; }
        public string Signatura { get; set; }
        public string InventarniBroj { get; set; }
        public string Godina { get; set; }
        public int? IzdavacId { get; set; }
        public int? BibliotekaId { get; set; }
        public string Slika { get; set; }
        public int? Stranice { get; set; }
        public int? JazikId { get; set; }

        public virtual Biblioteka Biblioteka { get; set; }
        public virtual Izdavac Izdavac { get; set; }
        public virtual Jezik Jazik { get; set; }
        public virtual ICollection<KnjigaIzdavanje> KnjigaIzdavanje { get; set; }
        public virtual ICollection<KnjigaPisac> KnjigaPisac { get; set; }
        public virtual ICollection<KnjigaRezervacija> KnjigaRezervacija { get; set; }
        public virtual ICollection<KnjigaZanr> KnjigaZanr { get; set; }
    }
}
