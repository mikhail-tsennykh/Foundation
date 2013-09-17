using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers {
  public class BaseController : Controller {

    public BaseController() {
      ViewBag.SiteTitle = ConfigurationManager.AppSettings.Get("SiteName");
    }

  }
}