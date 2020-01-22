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
        public DateTime Godina { get; set; }
        public int? IzdavacId { get; set; }
        public int? BibliotekaId { get; set; }
        public string Slika { get; set; }
        public byte[] SlikaByte { get; set; }
        public int? Stranice { get; set; }
        public int? JazikId { get; set; }

        public Biblioteka Biblioteka { get; set; }
        public Izdavac Izdavac { get; set; }
        public Jezik Jazik { get; set; }
        public ICollection<KnjigaIzdavanje> KnjigaIzdavanje { get; set; }
        public ICollection<KnjigaPisac> KnjigaPisac { get; set; }
        public ICollection<KnjigaRezervacija> KnjigaRezervacija { get; set; }
        public ICollection<KnjigaZanr> KnjigaZanr { get; set; }
    }
}
