using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers {
  public class HomeController : BaseController {

    //[Authorize]
    public ActionResult Index() {
      return View();
    }


    // Authentication example

    [Authorize(Roles = "admin")]
    public ActionResult AdminOnlyPage() {
      return View();
    }

  }
}