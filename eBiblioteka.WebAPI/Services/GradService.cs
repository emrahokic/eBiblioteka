using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using eBiblioteka.Model;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Database;
using Microsoft.AspNetCore.Authorization;

namespace eBiblioteka.WebAPI.Services
{
    public class GradService : BaseCRUDService<Model.Grad,GradSearchRequest,Database.Grad,GradUpsertRequest,GradUpsertRequest>
    {
       

        public GradService(eBibliotekaContext context, IMapper mapper):base(context,mapper)
        {
           
        }

        public override List<Model.Grad> Get(GradSearchRequest search, UserIdentity userIdentity)
        {


            var query = _context.Set<Database.Grad>().AsQueryable();
            if (search?.DrzavaId.HasValue == true)
            {
                query = query.Where(x => x.DrzavaId== search.DrzavaId);
            }

            if (!String.IsNullOrWhiteSpace(search.DrzavaNaziv) == true)
            {
                query = query.Where(x => x.Drzava.Naziv== search.DrzavaNaziv);
            }
            
            query = query.OrderBy(x => x.Naziv);

            var list = query.ToList();
            var c = _mapper.Map<List<Model.Grad>>(list);
            return c;
        }
    }
}
