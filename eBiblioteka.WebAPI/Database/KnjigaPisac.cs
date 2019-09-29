using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class KnjigaPisac
    {
        public int KnjigaId { get; set; }
        public int PisacId { get; set; }

        public virtual Knjiga Knjiga { get; set; }
        public virtual Pisac Pisac { get; set; }
    }
}
