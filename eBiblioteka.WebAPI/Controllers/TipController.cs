using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
    public class TipController : BaseController<Model.Tip, Model.Tip>
    {
        public TipController(IService<Model.Tip, Model.Tip> service, IKorisnikService korisnikService) : base(service, korisnikService)
        {

        }
    }
}