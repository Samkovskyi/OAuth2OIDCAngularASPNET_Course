﻿using System.Web;
using System.Web.Mvc;

namespace TripGallery.MVCClient.Controllers
{
    public class UserAccountController : Controller
    {
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}