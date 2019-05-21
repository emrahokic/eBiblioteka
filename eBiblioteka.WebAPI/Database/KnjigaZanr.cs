using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class KnjigaZanr
    {
        public int KnjigaId { get; set; }
        public int ZanrId { get; set; }

        public Knjiga Knjiga { get; set; }
        public Zanr Zanr { get; set; }
    }
}
