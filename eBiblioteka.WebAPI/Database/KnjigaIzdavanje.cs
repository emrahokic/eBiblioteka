using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class KnjigaIzdavanje
    {
        public int KnjigaIzdavanje1 { get; set; }
        public int? KnjigaId { get; set; }
        public int? ClanId { get; set; }
        public DateTime DatumPreuzimanja { get; set; }
        public DateTime? DatumPovratka { get; set; }

        public virtual Clan Clan { get; set; }
        public virtual Knjiga Knjiga { get; set; }
    }
}
