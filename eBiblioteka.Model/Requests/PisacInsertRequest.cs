using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eBiblioteka.Model.Requests
{
    public class PisacInsertRequest
    {
        public string Ime;
        public string Prezime;
        public DateTime? GodinaRodjenja;
        public DateTime? GodinaSmrti;
        public FileStream imageStream;
        public string Biografija;
        public int? Spol;
        public byte[] SlikaByte;

    }
}
