using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMembership.Controllers
{
    public class PublicController : Controller
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Logs()
        {
            return View();
        }
    }
}
