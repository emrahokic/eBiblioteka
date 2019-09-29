using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
    public class BibliotekaController : BaseCRUDController<Model.Biblioteka, BibliotekaSearchRequest, BibliotekaInsertRequest, BibliotekaInsertRequest>
    {


        public BibliotekaController(ICRUDService<Model.Biblioteka, BibliotekaSearchRequest, BibliotekaInsertRequest, BibliotekaInsertRequest> service, IKorisnikService korisnikService) : base(service, korisnikService)
        {

        }

       
    }
}