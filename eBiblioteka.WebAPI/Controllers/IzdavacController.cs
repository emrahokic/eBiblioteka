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
    public class IzdavacController : BaseCRUDController<Model.Izdavac, IzdavacSearchRequest, IzdavacInsertRequest, IzdavacInsertRequest>
    {


        public IzdavacController(ICRUDService<Model.Izdavac, IzdavacSearchRequest, IzdavacInsertRequest, IzdavacInsertRequest> service, IKorisnikService korisnikService) : base(service, korisnikService)
        {

        }

        
    }
}