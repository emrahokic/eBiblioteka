using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class KorisnikRola
    {
        
        public int RolaId { get; set; }
        public int KorisnikId { get; set; }
        public DateTime DatumIzmjene { get; set; }

        public virtual Korisnik Korisnik { get; set; }
        public virtual Rola Rola { get; set; }
    }
}
