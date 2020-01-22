using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Biblioteka
    {
        public Biblioteka()
        {
            Clan = new HashSet<Clan>();
            Clanarina = new HashSet<Clanarina>();
            Knjiga = new HashSet<Knjiga>();
            Notifikacija = new HashSet<Notifikacija>();
            Uposlenik = new HashSet<Uposlenik>();
        }

        public int BibliotekaId { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string LatLong { get; set; }
        public int? TipId { get; set; }
        public int? GradId { get; set; }
        public string Opis { get; set; }
        public string BrTelefon { get; set; }
        public string Email { get; set; }

        public Grad Grad { get; set; }
        public Tip Tip { get; set; }
        public ICollection<Clan> Clan { get; set; }
        public ICollection<Clanarina> Clanarina { get; set; }
        public ICollection<Knjiga> Knjiga { get; set; }
        public ICollection<Notifikacija> Notifikacija { get; set; }
        public ICollection<Uposlenik> Uposlenik { get; set; }
    }
}
