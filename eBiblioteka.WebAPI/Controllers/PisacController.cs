using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisacController : BaseCRUDController<Model.Pisac, PisacSearchRequest, PisacInsertRequest, PisacInsertRequest>
    {


        public PisacController(ICRUDService<Model.Pisac, PisacSearchRequest, PisacInsertRequest, PisacInsertRequest> service, IKorisnikService korisnikService) : base(service, korisnikService)
        {

        }


    }
}