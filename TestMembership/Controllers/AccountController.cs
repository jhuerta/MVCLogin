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
        //[HttpGet]
        //public ActionResult Login(string returnUrl)
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (new WesMembershipProvider().ValidateUser(login.UserName, login.Password))
                {
                    var userId = new Random().Next(100, 500);
                    FormsAuthentication.SetAuthCookie(userId.ToString(), login.RememberMe);

                    if (Request.IsAjaxRequest()) {
                        return Json( new {
                                uservalidated = true,
                                username = login.UserName,
                                userid = new Random().Next(1, 50),
                                redirecturl = ReturnUrl
                            });
                    }

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToProperUrl(ReturnUrl);
                    }
                }
                else
                {
                    if (Request.IsAjaxRequest()) {
                        return Json( new {
                                uservalidated = false
                            });
                    }
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(login);
        }

        public PartialViewResult Welcome(int userid)
        {
            return PartialView("_Welcome", userid);
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

        public PartialViewResult Logout()
        {
            FormsAuthentication.SignOut();

            return PartialView("_LoginForm");
        }
    }

    public class UserName
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
