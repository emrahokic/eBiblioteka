using AutoMapper;
using eBiblioteka.Model;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Database;
using eBiblioteka.WebAPI.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiblioteka.WebAPI.Services
{
    public class NotifikacijaService : BaseCRUDService<Model.Notifikacija, NotifikacijaSearchRequest, Database.Notifikacija, NotifikacijaInsertRequest, NotifikacijaInsertRequest>
    {
        private IHubContext<SignalRHub> _Hub;
        public NotifikacijaService(eBibliotekaContext context, IMapper mapper, IHubContext<SignalRHub> hub) : base(context, mapper)
        {
            _Hub = hub;
            
        }


        public override Model.Notifikacija Insert(NotifikacijaInsertRequest request, UserIdentity userIdentity)
        {
            int? BibliotekaID = _context.Clan.Where(x => x.Osoba.KorisnikId == userIdentity.UserID).Select(x => x.BibliotekaId).FirstOrDefault();
            int? ClanID = _context.Clan.Where(x => x.Osoba.KorisnikId == userIdentity.UserID).Select(x => x.ClanId).FirstOrDefault();
            if (BibliotekaID == null)
            {
                return null;
            }
            Database.Notifikacija model = new Database.Notifikacija()
            {
                BibliotekaId = BibliotekaID,
                ClanId = ClanID,
                Datum = DateTime.Now,
                Opis= request.Opis

            };
            _context.Notifikacija.Add(model);
            _context.SaveChanges();

            Model.Notifikacija Model = _context.Notifikacija.Where(x => x.NotifikacijaId == model.NotifikacijaId).Select(x => new Model.Notifikacija()
            {
                Clan = x.Clan.Osoba.Ime + " " + x.Clan.Osoba.Prezime,
                ClanImage = x.Clan.Osoba.Korisnik.Slika,
                Datum = x.Datum,
                Opis = x.Opis,
                NotifikacijaId = x.NotifikacijaId
            }).FirstOrDefault();

            _Hub.Clients.All.SendAsync("SendNotification", Model);
            
            return Model;
        }

        public override List<Model.Notifikacija> Get(NotifikacijaSearchRequest search, UserIdentity userIdentity)
        {
            if (userIdentity.Roles.Contains("Korisnik"))
            {
                return null;
            }
            int? BibliotekaID = _context.Clan.Where(x => x.Osoba.KorisnikId == userIdentity.UserID).Select(x => x.BibliotekaId).FirstOrDefault();
            if (BibliotekaID == null)
            {
                return null;
            }
            List<Model.Notifikacija> model = _context.Notifikacija.OrderByDescending(x=>x.Datum).Where(x => x.BibliotekaId == BibliotekaID).Select(x => new Model.Notifikacija()
            {
                Clan = x.Clan.Osoba.Ime + " " + x.Clan.Osoba.Prezime,
                ClanImage = x.Clan.Osoba.Korisnik.Slika,
                Datum = x.Datum,
                Opis = x.Opis,
                NotifikacijaId = x.NotifikacijaId
            }).ToList();

            return model;
        }
    }
}
