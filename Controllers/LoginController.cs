using EcoLeafPlates.Models;
using EcoLeafPlates.Models.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EcoLeafPlates.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login _login, string returnUrl)
        {
            try
            {
                List<clsUserDetails> regVM = UserManager.displayUserDetails().ToList();
                regVM = regVM.Where(x => x.isActive == true).ToList();
                var validUser = regVM.Where(x => x.phoneNo == _login.PhoneNo && x.passowrd == _login.Password).FirstOrDefault();

               


                if (validUser != null)
                {
                    Session["userId"] = validUser.userId;
                    FormsAuthentication.SetAuthCookie(validUser.phoneNo.ToString(), false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") &&
                        !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Eco");
                    }

                }
                else
                {
                    ViewBag.IsValiduser = false;

                    //ModelState.AddModelError("", "Invalid User Name/Password");
                }
            }
            catch (Exception ex)
            {
                
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            Session["userId"] = 0;
            FormsAuthentication.SignOut();
          
            return RedirectToAction("Index", "Eco");
        }
    }
}