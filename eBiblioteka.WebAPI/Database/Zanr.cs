using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Zanr
    {
        public Zanr()
        {
            KnjigaZanr = new HashSet<KnjigaZanr>();
        }

        public int ZanrId { get; set; }
        public string Naziv { get; set; }

        public ICollection<KnjigaZanr> KnjigaZanr { get; set; }
    }
}
