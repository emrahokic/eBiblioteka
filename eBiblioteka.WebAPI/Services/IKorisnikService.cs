using eBiblioteka.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public interface IKorisnikService
    {
        Model.Korisnik Authenticate(string username, string password);
        Model.UserIdentity GetUserIdentity(ClaimsIdentity claimsIdentity);
        IEnumerable<Korisnik> GetAll();
    }
}
