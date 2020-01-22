using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Database;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace eBiblioteka.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifikacijaController : BaseCRUDController<Model.Notifikacija, NotifikacijaSearchRequest, NotifikacijaInsertRequest, NotifikacijaInsertRequest>
    {

        public NotifikacijaController(ICRUDService<Model.Notifikacija, NotifikacijaSearchRequest, NotifikacijaInsertRequest, NotifikacijaInsertRequest> service, IKorisnikService korisnikService) : base(service, korisnikService)
        {
        }
    }
}