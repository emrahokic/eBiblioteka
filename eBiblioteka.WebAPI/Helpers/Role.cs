using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Helpers
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Korisnik = "Korisnik";
        public const string Uposlenik = "Uposlenik";
        public const string SuperAdmin = "SuperAdmin";
        public const string All = "SuperAdmin, Admin, Korisnik, Uposlenik";
        public const string UposlenikAdmin = "Admin, Uposlenik";
    }
}
