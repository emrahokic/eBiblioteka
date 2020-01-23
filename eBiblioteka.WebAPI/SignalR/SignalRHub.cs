using eBiblioteka.WebAPI.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.SignalR
{
    [Authorize]
    public class SignalRHub : Hub
    {
        eBibliotekaContext _db;
        public SignalRHub(eBibliotekaContext db)
        {
            _db = db;
        }
        public override Task OnConnectedAsync()
        {

            var id = Context.UserIdentifier;

            var y = Context.User;
            var c = Context.ConnectionId;

            List<string> k = _db.KorisnikRola.Include(x=>x.Korisnik).Include(x=>x.Rola).Where(x => x.KorisnikId == int.Parse(id)).Select(x=>x.Rola.Naziv).ToList();

            if (k.Contains("Uposlenik") ||k.Contains("Admin"))
            {
                Biblioteka b = _db.Uposlenik.Where(x => x.Osoba.KorisnikId == int.Parse(id)).Select(x => x.Biblioteka).FirstOrDefault();
                if (b!=null)
                {
                    b.Adresa = c;
                    _db.SaveChanges();
                }
            }


            return base.OnConnectedAsync();
        }

    }
}
