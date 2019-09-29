using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Osoba
    {
        public Osoba()
        {
            Clan = new HashSet<Clan>();
            Korisnik = new HashSet<Korisnik>();
            Uposlenik = new HashSet<Uposlenik>();
        }

        public int OsobaId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public int? GradId { get; set; }
        public string Jmbg { get; set; }
        public int? Spol { get; set; }

        public virtual Grad Grad { get; set; }
        public virtual ICollection<Clan> Clan { get; set; }
        public virtual ICollection<Korisnik> Korisnik { get; set; }
        public virtual ICollection<Uposlenik> Uposlenik { get; set; }
    }
}
