using System;
using System.Collections.Generic;
using System.Text;

namespace eBiblioteka.Model.Requests
{
    public class NotifikacijaInsertRequest
    {
        public string Opis { get; set; }
        public DateTime? Datum { get; set; }

        public int? BibliotekaId { get; set; }

    }
}
