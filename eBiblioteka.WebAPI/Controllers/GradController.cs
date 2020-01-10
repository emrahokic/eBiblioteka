using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Database;
using eBiblioteka.WebAPI.Helpers;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
  
    public class GradController :  BaseCRUDController<Model.Grad,GradSearchRequest,GradUpsertRequest,GradUpsertRequest>
    {
        
       
        public GradController(ICRUDService<Model.Grad,GradSearchRequest,GradUpsertRequest,GradUpsertRequest> service, IKorisnikService korisnikService) : base(service,korisnikService)
        {
            
        }

        [Authorize]
        public override List<Model.Grad> Get([FromQuery] GradSearchRequest t)
        {
            return base.Get(t);
        }


        public override Model.Grad GetById(int id)
        {
            return base.GetById(id);
        }
    }
}