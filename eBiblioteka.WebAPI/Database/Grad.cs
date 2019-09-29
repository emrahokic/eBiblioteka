using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Grad
    {
        public Grad()
        {
            Biblioteka = new HashSet<Biblioteka>();
            Izdavac = new HashSet<Izdavac>();
            Osoba = new HashSet<Osoba>();
        }

        public int GradId { get; set; }
        public string Naziv { get; set; }
        public int? DrzavaId { get; set; }

        public virtual Drzava Drzava { get; set; }
        public virtual ICollection<Biblioteka> Biblioteka { get; set; }
        public virtual ICollection<Izdavac> Izdavac { get; set; }
        public virtual ICollection<Osoba> Osoba { get; set; }
    }
}
