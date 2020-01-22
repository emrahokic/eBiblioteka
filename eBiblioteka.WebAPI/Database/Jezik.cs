using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Jezik
    {
        public Jezik()
        {
            Knjiga = new HashSet<Knjiga>();
        }

        public int JezikId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Knjiga> Knjiga { get; set; }
    }
}
