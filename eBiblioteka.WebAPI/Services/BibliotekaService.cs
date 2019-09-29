using AutoMapper;
using eBiblioteka.Model;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI;
using eBiblioteka.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public class BibliotekaService : BaseCRUDService<Model.Biblioteka, BibliotekaSearchRequest, Database.Biblioteka, BibliotekaInsertRequest, BibliotekaInsertRequest>
    {
        public BibliotekaService(eBibliotekaContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public override List<Model.Biblioteka> Get(BibliotekaSearchRequest search, UserIdentity userIdentity)
        {
            //var list = _context.Set<TDatabase>().ToList();
            //return _mapper.Map<List<TModel>>(list);
            var list = _context.Biblioteka.Include(x=>x.Grad).Include(y=>y.Tip).OrderBy(x=>x.Grad.Naziv).Select(y => new Model.Biblioteka() {
                Naziv = y.Naziv,
                Grad = y.Grad.Naziv,
                Icona = y.Tip.Icona,
                Tip = y.Tip.Naziv
            }).ToList();

            return list;
        }
        public override Model.Biblioteka Insert(BibliotekaInsertRequest request, UserIdentity userIdentity)
        {
            var _request = _mapper.Map<Database.Biblioteka>(request);
            
            _request.GradId = _context.Grad.Where(x => x.Naziv == request.Grad_).Select(x => x.GradId).FirstOrDefault();
            _request.TipId = _context.Tip.Where(x => x.Naziv == request.Tip_).Select(x => x.TipId).FirstOrDefault();

            _context.Biblioteka.Add(_request);
            _context.SaveChanges();

            return _mapper.Map<Model.Biblioteka>(_request);

        }
    }
}
