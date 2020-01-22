using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class KorisnikRola
    {
        public int RolaId { get; set; }
        public int KorisnikId { get; set; }
        public DateTime DatumIzmjene { get; set; }

        public Korisnik Korisnik { get; set; }
        public Rola Rola { get; set; }
    }
}
