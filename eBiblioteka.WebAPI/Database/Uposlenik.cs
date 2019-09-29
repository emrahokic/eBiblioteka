using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Uposlenik
    {
        public int UposlenikId { get; set; }
        public int? OsobaId { get; set; }
        public int? BibliotekaId { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public int? Aktivan { get; set; }
        public string BrojRadnika { get; set; }

        public virtual Biblioteka Biblioteka { get; set; }
        public virtual Osoba Osoba { get; set; }
    }
}
