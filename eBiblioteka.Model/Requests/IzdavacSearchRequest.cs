﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eBiblioteka.Model.Requests
{
    public class IzdavacSearchRequest
    {
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string Naziv { get; set; }
    }
}
