using AutoMapper;
using eBiblioteka.Model;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public class IzdavacService : BaseCRUDService<Model.Izdavac, IzdavacSearchRequest, Database.Izdavac, IzdavacInsertRequest, IzdavacInsertRequest>
    {
        public IzdavacService(eBibliotekaContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public override List<Model.Izdavac> Get(IzdavacSearchRequest search, UserIdentity userIdentity)
        {
            //var list = _context.Set<TDatabase>().ToList();
            //return _mapper.Map<List<TModel>>(list);
            var list = _context.Izdavac.Include(x => x.Grad).OrderBy(x => x.Grad.Naziv).Select(y => new Model.Izdavac()
            {
                Naziv = y.Naziv,
                Grad = y.Grad.Naziv,
            }).ToList();

            return list;
        }
        public override Model.Izdavac Insert(IzdavacInsertRequest request, UserIdentity userIdentity)
        {
            var _request = _mapper.Map<Database.Izdavac>(request);

            _request.GradId = _context.Grad.Where(x => x.Naziv == request.Grad_).Select(x => x.GradId).FirstOrDefault();

            _context.Izdavac.Add(_request);
            _context.SaveChanges();

            return _mapper.Map<Model.Izdavac>(_request);
        }
    }
}
