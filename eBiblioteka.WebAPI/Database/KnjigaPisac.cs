using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class KnjigaPisac
    {
        public int KnjigaId { get; set; }
        public int PisacId { get; set; }

        public Knjiga Knjiga { get; set; }
        public Pisac Pisac { get; set; }
    }
}
