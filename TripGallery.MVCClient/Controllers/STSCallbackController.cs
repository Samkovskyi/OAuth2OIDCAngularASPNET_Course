using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityModel.Client;

namespace TripGallery.MVCClient.Controllers
{
    public class STSCallbackController: Controller
    {
        public async Task<ActionResult> Index()
        {
            var authCode = Request.QueryString["code"];

            var client = new TokenClient(
                TripGallery.Constants.TripGallerySTSTokenEndpoint,
                "tripgalleryauthcode",
                TripGallery.Constants.TripGalleryClientSecret);

            var tokenResponse = await client.RequestAuthorizationCodeAsync(
                authCode,
                TripGallery.Constants.TripGalleryMVCSTSCallback);

            Response.Cookies["TripGalleryCookie"]["access_token"] = tokenResponse.AccessToken;
           
            return Redirect(Request.QueryString["state"]);
        }
    }
}