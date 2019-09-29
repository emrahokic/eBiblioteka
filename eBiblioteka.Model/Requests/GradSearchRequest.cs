using System;
using System.Collections.Generic;
using System.Text;

namespace eBiblioteka.Model.Requests
{
    public class GradSearchRequest
    {
        public int? DrzavaId { get; set; }
        public string DrzavaNaziv { get; set; }
    }
}
