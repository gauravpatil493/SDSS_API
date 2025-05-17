using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(SDSS_API.Startup))]
namespace SDSS_API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var issuer = "DocumentStorageAPI";
            var audience = "DocumentStorageUsers";
            var secret = Encoding.UTF8.GetBytes("G7eXp8JkL2oB1sMqNvTz9WfC4YeR5uHd");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                }
            });

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}