using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OAuthDemo.Models
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var data = await context.Request.ReadFormAsync();
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            EcoColaborador colaborador = new EcoColaborador();/* { 
                NumeroNomina = int.Parse(data.Get("userName")),
                Password = data.Get("password"),
                DispositivoMovil = data.Get("dm"),
                Chip = data.Get("chip")
            };*/
            if (colaborador.IniciarSesion())
            {
                context.SetError("invalid_grant", "El usuario o el password son incorrectos.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}