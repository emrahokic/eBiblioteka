using System;
using System.Collections.Generic;
using System.Text;

namespace eBiblioteka.Model
{
    public class Notifikacija
    {
        public int NotifikacijaId { get; set; }
        public string Opis { get; set; }
        public DateTime? Datum { get; set; }
        public string Clan { get; set; }
        public byte[] ClanImageByte { get; set; }
        public string ClanImage { get; set; }

    }
}
