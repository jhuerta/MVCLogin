using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMembership.Controllers
{
    public class PrivateController : Controller
    {
     
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
