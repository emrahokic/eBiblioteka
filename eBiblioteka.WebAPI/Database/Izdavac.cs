﻿using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Izdavac
    {
        public Izdavac()
        {
            Knjiga = new HashSet<Knjiga>();
        }

        public int IzdavacId { get; set; }
        public string Naziv { get; set; }
        public int? GradId { get; set; }

        public Grad Grad { get; set; }
        public ICollection<Knjiga> Knjiga { get; set; }
    }
}
