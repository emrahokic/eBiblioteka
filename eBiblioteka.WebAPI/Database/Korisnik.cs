﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Korisnik 
    {
        public Korisnik()
        {
            KorisnikRola = new HashSet<KorisnikRola>();
        }

        public int KorisnikId { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string KorisnickaSifraHash { get; set; }
        public string Salt { get; set; }
        public DateTime LastSalt { get; set; }
        public string Slika { get; set; }
        public int? OsobaId { get; set; }

        public virtual Osoba Osoba { get; set; }
        public virtual ICollection<KorisnikRola> KorisnikRola { get; set; }
        public string Token { get; internal set; }
    }
}
