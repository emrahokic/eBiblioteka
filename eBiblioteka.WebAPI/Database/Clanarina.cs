﻿using System;
using System.Collections.Generic;

namespace eBiblioteka.WebAPI.Database
{
    public partial class Clanarina
    {
        public int ClanarinaId { get; set; }
        public int? ClanId { get; set; }
        public int? BibliotekaId { get; set; }
        public DateTime DatumClanarine { get; set; }
        public DateTime DatumIsteka { get; set; }
        public decimal Iznos { get; set; }

        public Biblioteka Biblioteka { get; set; }
        public Clan Clan { get; set; }
    }
}
