using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eBiblioteka.Model;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T, TSearch> : ControllerBase
    {
        private readonly IKorisnikService _korisnikService;
        private UserIdentity _user;
        private readonly IService<T, TSearch> _service;

        public BaseController(IService<T, TSearch> service, IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
            _service = service;
        }

        [HttpGet]
        public virtual List<T> Get([FromQuery]TSearch search)
        {
            _user = _korisnikService.GetUserIdentity(this.User.Identity as ClaimsIdentity);

            return _service.Get(search,_user);
        }



        [HttpGet("{id}")]
        public virtual T GetById(int id)
        {
            _user = _korisnikService.GetUserIdentity(this.User.Identity as ClaimsIdentity);

            return _service.GetById(id, _user);
        }
    }
}