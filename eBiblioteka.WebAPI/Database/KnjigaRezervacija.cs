using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class KnjigaRezervacija
    {
        public int KnjigaRezervacija1 { get; set; }
        public int? KnjigaId { get; set; }
        public int? ClanId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int Aktivna { get; set; }

        public virtual Clan Clan { get; set; }
        public virtual Knjiga Knjiga { get; set; }
    }
}
