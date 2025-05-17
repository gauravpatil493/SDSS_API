using Microsoft.IdentityModel.Tokens;
using SDSS_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SDSS_API.Controllers
{
    [System.Web.Http.RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public IHttpActionResult Login(LoginModel model)
        {
            if (model.Username == "Gaurav" && model.Password == "1234") // mock validation
            {
                var token = CreateToken(model.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string CreateToken(string username)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("G7eXp8JkL2oB1sMqNvTz9WfC4YeR5uHd");

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "DocumentStorageAPI",
                Audience = "DocumentStorageUsers",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}