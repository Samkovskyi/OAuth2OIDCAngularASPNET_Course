using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using TripGallery.MVCClient.Helpers;


[assembly: OwinStartup(typeof(TripGallery.MVCClient.Startup))]
namespace TripGallery.MVCClient
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityModel.JwtClaimTypes.Name;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "tripgalleryhybrid",
                Authority = Constants.TripGallerySTS,
                RedirectUri = Constants.TripGalleryMVC,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token",
                Scope = "openid profile",
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        TokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);

                        var subClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.Subject);

                        var nameClaim = new Claim(IdentityModel.JwtClaimTypes.Name,
                            Constants.TripGalleryIssuerUri + subClaim.Value);

                        var givenNameClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.GivenName);

                        var familyNameClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.FamilyName);

                        var newClaimsIdentity = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            IdentityModel.JwtClaimTypes.Name,
                            IdentityModel.JwtClaimTypes.Role);

                        if (nameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(nameClaim);
                        }

                        if (givenNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(givenNameClaim);
                        }

                        if (familyNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(familyNameClaim);
                        }

                        n.AuthenticationTicket = new AuthenticationTicket(
                            newClaimsIdentity,
                            n.AuthenticationTicket.Properties);
                    }
                }
            });
        }
    }
}
