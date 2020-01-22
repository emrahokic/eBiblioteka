using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Osoba
    {
        public Osoba()
        {
            Clan = new HashSet<Clan>();
            Uposlenik = new HashSet<Uposlenik>();
        }

        public int OsobaId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public int? GradId { get; set; }
        public string Jmbg { get; set; }
        public int? Spol { get; set; }
        public int? KorisnikId { get; set; }

        public Grad Grad { get; set; }
        public Korisnik Korisnik { get; set; }
        public ICollection<Clan> Clan { get; set; }
        public ICollection<Uposlenik> Uposlenik { get; set; }
    }
}
