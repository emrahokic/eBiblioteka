using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Clan
    {
        public Clan()
        {
            Clanarina = new HashSet<Clanarina>();
            KnjigaIzdavanje = new HashSet<KnjigaIzdavanje>();
            KnjigaRezervacija = new HashSet<KnjigaRezervacija>();
        }

        public int ClanId { get; set; }
        public int? OsobaId { get; set; }
        public string ClanskiBroj { get; set; }
        public DateTime DatumUclanjivanja { get; set; }
        public int Aktivan { get; set; }
        public int? BibliotekaId { get; set; }

        public Biblioteka Biblioteka { get; set; }
        public Osoba Osoba { get; set; }
        public ICollection<Clanarina> Clanarina { get; set; }
        public ICollection<KnjigaIzdavanje> KnjigaIzdavanje { get; set; }
        public ICollection<KnjigaRezervacija> KnjigaRezervacija { get; set; }
    }
}
