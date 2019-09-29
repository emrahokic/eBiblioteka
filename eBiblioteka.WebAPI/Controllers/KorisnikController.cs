using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eBiblioteka.WebAPI.Database;
using eBiblioteka.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eBiblioteka.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private IKorisnikService _userService;


        public KorisnikController(IKorisnikService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("Authenticate")]
        public ActionResult<Model.Korisnik> Authenticate()
        {
            
            if(!Request.Headers.ContainsKey("Authorization"))
                return BadRequest(new { message = "Missing Authorization Header" });
            Model.Korisnik user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                var username = credentials[0];
                var password = credentials[1];
                user = _userService.Authenticate(username,password);

            }
            catch
            {
                
                return BadRequest(new { message = "Invalid Authorization Header" });
            }


            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}