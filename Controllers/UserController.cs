using EcoLeafPlates.Models;
using EcoLeafPlates.Models.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoLeafPlates.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddUser(clsUserDetails userDetails)
        {
            List<clsUserDetails> regVMC = UserManager.displayUserDetails().ToList();
            String textPhoneNo = (userDetails.phoneNo);
            regVMC = regVMC.Where(r => r.phoneNo.Contains(textPhoneNo)).ToList();

            if (regVMC.Count == 0)
            {

                userDetails = UserManager.addUserDefaultEntries(userDetails);
                int result = UserManager.addUser(userDetails);
                if (result > 0)
                {
                    ViewBag.IsRegistred = true;
                }
                else
                {
                    ViewBag.IsRegistred = false;
                }

            }
            else
            {
                ViewBag.IsDuplicatePhonoNo = true;

            }
            return View();
        }
           [Authorize(Roles = "Admin")]
        public ActionResult DisplayUserDetails()
        {
            List<clsUserDetails> lstUserDetails = UserManager.displayUserDetails();
            return View(lstUserDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUserDetails(int? UserId)
        {
            List<clsUserDetails> lstUserDetails = UserManager.displayUserDetails();
            clsUserDetails userDetail = lstUserDetails.Single(x => x.userId == UserId);
            return View(userDetail);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUserDetails(clsUserDetails userDetails)
        {
            UserManager.editUserDetails(userDetails);
            return RedirectToAction("DisplayUserDetails");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUserDetails(int UserId)
        {
            UserManager.deleteUserDetails(UserId);
            return RedirectToAction("DisplayUserDetails");
        }

    }
}