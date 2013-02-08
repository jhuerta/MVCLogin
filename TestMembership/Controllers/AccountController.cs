using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TestMembership.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var userIsAuthenticated = new WesMembershipProvider().ValidateUser(login.UserName, login.Password);
                
                if (userIsAuthenticated)
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);

                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { uservalidated = true, returnurl = ReturnUrl, username = login.UserName });
                    }

                    return RedirectToProperUrl(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(login);
        }

        private ActionResult RedirectToProperUrl(string ReturnUrl)
        {
            if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToAction("LoggedIn", "Home", null);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return View();
        }
    }
}
