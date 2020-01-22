using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Notifikacija
    {
        public int NotifikacijaId { get; set; }
        public string Opis { get; set; }
        public DateTime? Datum { get; set; }
        public int? BibliotekaId { get; set; }
        public int? ClanId { get; set; }

        public Biblioteka Biblioteka { get; set; }
        public Clan Clan { get; set; }
    }
}
