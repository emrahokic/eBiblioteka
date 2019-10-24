using AutoMapper;
using eBiblioteka.Model;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace eBiblioteka.WebAPI.Services
{
    public class PisacService : BaseCRUDService<Model.Pisac, PisacSearchRequest, Database.Pisac, PisacInsertRequest, PisacInsertRequest>
    {
        public PisacService(eBibliotekaContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public override List<Model.Pisac> Get(PisacSearchRequest search, UserIdentity userIdentity)
        {

            var list = _context.Pisac.OrderBy(x => x.Ime).ToList();


            return _mapper.Map<List<Model.Pisac>>(list);
        }
        public override Model.Pisac Insert(PisacInsertRequest request, UserIdentity userIdentity)
        {
            //var _request = _mapper.Map<Database.Izdavac>(request);

            //convertAndSaveImage(null);sad cu viditi sta je to

            //_request.GradId = _context.Grad.Where(x => x.Naziv == request.Grad_).Select(x => x.GradId).FirstOrDefault();

            //_context.Izdavac.Add(_request);
            //_context.SaveChanges();
            //return _mapper.Map<Model.Pis>(_request);
            return null;
        }

       
    }
}
