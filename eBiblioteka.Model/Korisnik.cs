using System;

namespace eBiblioteka.Model
{
    public class Korisnik
    {
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BibliotekaNaziv{ get; set; }
        public string Slika { get; set; }
        
        public string Token { get; set; }
    }
}
