using eBiblioteka.Model;
using eBiblioteka.WebAPI.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public interface IGradService : IService<Model.Grad, Model.Requests.GradSearchRequest>
    {
      
    }
}
