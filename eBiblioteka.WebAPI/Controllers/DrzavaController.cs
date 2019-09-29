using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
    
    public class DrzavaController : BaseController<Model.Drzava,Model.Drzava>
    {
        public DrzavaController(IService<Model.Drzava, Model.Drzava> service, IKorisnikService korisnikService) : base(service,korisnikService)
        {

        }
    }
}