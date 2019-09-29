using eBiblioteka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public interface ICRUDService<T, TSearch, TInsert, TUpdate> : IService<T, TSearch>
    {
        T Insert(TInsert request,UserIdentity userIdentity);

        T Update(int id, TUpdate request, UserIdentity userIdentity);
        T Delete(int id, UserIdentity userIdentity);
    }
}
