﻿using AutoMapper;
using eBiblioteka.Model;
using eBiblioteka.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public class BaseCRUDService<TModel, TSearch, TDatabase, TInsert, TUpdate> : BaseService<TModel, TSearch, TDatabase>, ICRUDService<TModel, TSearch, TInsert, TUpdate> where TDatabase : class
    {
        public BaseCRUDService(eBibliotekaContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public virtual TModel Insert(TInsert request, UserIdentity userIdentity)
        {
            var entity = _mapper.Map<TDatabase>(request);

            _context.Set<TDatabase>().Add(entity);
            _context.SaveChanges();

            return _mapper.Map<TModel>(entity);
        }

        public virtual TModel Update(int id, TUpdate request, UserIdentity userIdentity)
        {
            var entity = _context.Set<TDatabase>().Find(id);
            _context.Set<TDatabase>().Attach(entity);
            _context.Set<TDatabase>().Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<TModel>(entity);
        }

        public virtual TModel Delete(int id, UserIdentity userIdentity)
        {
            var entity = _context.Set<TDatabase>().Find(id);

            _context.Set<TDatabase>().Remove(entity);

            _context.SaveChanges();

            return _mapper.Map<TModel>(entity);
        }
    }
}
