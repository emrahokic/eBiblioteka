using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eBiblioteka.Model;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
    public class BaseCRUDController<T, TSearch, TInsert, TUpdate> : BaseController<T, TSearch>
    {
        private readonly ICRUDService<T, TSearch, TInsert, TUpdate> _service = null;
        private readonly IKorisnikService _korisnikService = null;
        private  UserIdentity _user;

        public BaseCRUDController(ICRUDService<T, TSearch, TInsert, TUpdate> service,IKorisnikService korisnikService) : base(service, korisnikService)
        {
            _service = service;
            _korisnikService = korisnikService;

        }

        [HttpPost]
        public T Insert(TInsert request)
        {
            _user = _korisnikService.GetUserIdentity(this.User.Identity as ClaimsIdentity);

            return _service.Insert(request, _user);
        }

        [HttpDelete("{id}")]
        public T Delete(int id)
        {
            _user = _korisnikService.GetUserIdentity(this.User.Identity as ClaimsIdentity);

            return _service.Delete(id, _user);
        }

        [HttpPut("{id}")]
        public T Update(int id, [FromBody]TUpdate request)
        {
            _user = _korisnikService.GetUserIdentity(this.User.Identity as ClaimsIdentity);

            return _service.Update(id, request, _user);
        }
    }
}