using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace Web.Controllers {
  public class AccountController : BaseController {
 
    public ActionResult Login() {
      return View();
    }
    
    [HttpPost]
    public ActionResult Login(string userName, string password, bool keepLoggedIn, string returnUrl) {
      if (Membership.ValidateUser(userName, password)) {
        FormsAuthentication.SetAuthCookie(userName, keepLoggedIn);

        if (!string.IsNullOrEmpty(returnUrl)) {
          return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
      }
      return View();
    }

    public ActionResult Logout() {
      FormsAuthentication.SignOut();
      return RedirectToAction("Index", "Home");
    }

  }
}