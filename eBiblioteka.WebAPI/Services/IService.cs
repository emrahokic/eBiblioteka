using eBiblioteka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public interface IService<T,TSearch>
    {
        List<T> Get(TSearch search, UserIdentity userIdentity);
        T GetById(int id, UserIdentity userIdentity);
    }
}
