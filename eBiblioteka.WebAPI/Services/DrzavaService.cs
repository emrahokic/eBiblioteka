using AutoMapper;
using eBiblioteka.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public class DrzavaService : BaseService<Model.Drzava,Model.Drzava,Database.Drzava>
    {
        public DrzavaService(eBibliotekaContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
