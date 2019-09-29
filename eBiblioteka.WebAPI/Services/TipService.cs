using AutoMapper;
using eBiblioteka.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public class TipService : BaseService<Model.Tip,Model.Tip,Database.Tip>
    {
        public TipService(eBibliotekaContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
