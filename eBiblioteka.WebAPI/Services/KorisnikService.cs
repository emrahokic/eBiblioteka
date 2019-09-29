using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eBiblioteka.WebAPI.Database;
using eBiblioteka.WebAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace eBiblioteka.WebAPI.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly eBibliotekaContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public KorisnikService(IOptions<AppSettings> appSettings, eBibliotekaContext eBibliotekaContext, IMapper mapper)
        {
            _context = eBibliotekaContext;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public Model.Korisnik Authenticate(string username, string password)
        {
            var user = _context.Korisnik.Where(x => x.KorisnickoIme == username || x.Email == username).Include(y=>y.Osoba).Select(y => new Korisnik()
            {
                KorisnikId = y.KorisnikId,
                KorisnickoIme = y.KorisnickoIme,
                Email = y.Email,
                OsobaId = y.OsobaId,
                Slika = y.Slika,
                Osoba = y.Osoba,
                KorisnickaSifraHash = y.KorisnickaSifraHash,
                Salt = y.Salt
            }).FirstOrDefault();

            user.KorisnikRola = _context.KorisnikRola.Where(x => x.KorisnikId == user.KorisnikId).Include(x=>x.Rola).ToList();

            var _korisnik = new Model.Korisnik() {
                Email = user.Email,
                Ime = user.Osoba.Ime,
                Prezime = user.Osoba.Prezime,
                KorisnickoIme = user.KorisnickoIme,
                Slika = user.Slika
            };

            if (user.KorisnikRola.Select(x=>x.Rola.Naziv).Contains(Role.Uposlenik) || user.KorisnikRola.Select(x => x.Rola.Naziv).Contains(Role.Admin))
            {
                _korisnik.BibliotekaNaziv = _context.Uposlenik.Where(x => x.OsobaId == user.OsobaId).Include(x => x.Biblioteka).Select(y => y.Biblioteka.Naziv).FirstOrDefault();
            }else if (user.KorisnikRola.Select(x => x.Rola.Naziv).Contains(Role.Korisnik))
            {
                _korisnik.BibliotekaNaziv = _context.Clan.Where(x => x.OsobaId == user.OsobaId).Include(x => x.Biblioteka).Select(y => y.Biblioteka.Naziv).FirstOrDefault();
            }
            else
            {
                _korisnik.BibliotekaNaziv = "eBiblioteka";
            }

            if (user != null)
            {
                var newHash = GenerateHash(user.Salt, password);
                if (newHash == user.KorisnickaSifraHash)
                {
                    // authentication successful so generate jwt token
                    var jwt = generateJwt(user);
                    _korisnik.Token = jwt;
                    // remove password before returning
                    user.KorisnickaSifraHash = null;
                    return _korisnik;
                }
            }



            return null;
        }

        public Model.UserIdentity GetUserIdentity(ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var roles = claimsIdentity.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();

            return new Model.UserIdentity(userId,roles);
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA512");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        private string generateJwt(Korisnik user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.KorisnikId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            foreach (var item in user.KorisnikRola)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item.Rola.Naziv));
            }
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<Korisnik> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
