using AutoMapper;
using eBiblioteka.Model;
using eBiblioteka.Model.Requests;
using eBiblioteka.WebAPI.Database;
using eBiblioteka.WebAPI.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public class BibliotekaVM
        {
            public string HubID;
            public int id;
          
        }
        public  override Model.Notifikacija Insert(NotifikacijaInsertRequest request, UserIdentity userIdentity)
        {
            BibliotekaVM B = _context.Clan.Where(x => x.Osoba.KorisnikId == userIdentity.UserID).Select(x =>new BibliotekaVM()
            {
                HubID = x.Biblioteka.Adresa,
                id = x.Biblioteka.BibliotekaId
            }).FirstOrDefault();

            int? ClanID = _context.Clan.Where(x => x.Osoba.KorisnikId == userIdentity.UserID).Select(x => x.ClanId).FirstOrDefault();
            if (B.id == null)
            {
                return null;
            }
            Database.Notifikacija model = new Database.Notifikacija()
            {
                BibliotekaId = B.id,
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



            _Hub.Clients.Client(B.HubID).SendAsync("SendNotification", Model);

            var xy =  sendNotificationToAppCenter(Model.Opis, Model.Clan);
            return Model;
        }


       public class notification_content
        {
           public string name;
            public string title;
          public  string body;
        }

        public class PayLoad
        {

           public  notification_content notification_content;

        }
        public async Task<string> sendNotificationToAppCenter(string a,string b)
        {

            PayLoad p = new PayLoad()
            {
                notification_content = new notification_content()
                {
                    body = b,
                    title = a,
                    name = "eBiblioteka"
                }
            };

            string  payload = Newtonsoft.Json.JsonConvert.SerializeObject(p);

            //HttpWebRequest httpRequest = WebRequest.Create("https://appcenter.ms/api/v0.1/apps/emrah.okic-edu.fit.ba/eBiblioteka.Android/push/notifications");
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create("https://appcenter.ms/api/v0.1/apps/emrah.okic-edu.fit.ba/eBiblioteka.Android/push/notifications");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.Headers.Add("X-API-Token", "301e68de61b7130f30f11f51b9b1e817357f4520");
            httpRequest.ContentLength = payload.Length;

            var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(payload);
            streamWriter.Close();
            var data = await httpRequest.GetResponseAsync();
            return data.ToString();
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
