using EcoLeafPlates.Models;
using EcoLeafPlates.Models.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoLeafPlates.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact vm)
        {

            int result = ContactManager.addContact(vm);
            if (result > 0)
                ViewBag.Message = "Thank you for Contacting us ";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ContactList()
        {
            List<Contact> lstContact = new List<Contact>();
            lstContact = ContactManager.displayContactDetails().ToList();
            return View(lstContact);
        }

        [HttpGet]
         [Authorize(Roles = "Admin")]
        public ActionResult EditContact(int? ConId)
        {
            List<Contact> lstMessage = new List<Contact>();
            lstMessage = ContactManager.displayContactDetails().ToList();
            Contact con = new Contact();
            con = lstMessage.Single(e => e.ConId == ConId);
            return View(con);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult EditContact(Contact con)
        {
            ContactManager.editContactDetails(con);
            return RedirectToAction("ContactList");
        }

        [HttpGet]
         [Authorize(Roles = "Admin")]
        public ActionResult DeleteContact(int ConId)
        {
            ContactManager.deleteContactDetails(ConId);
            return RedirectToAction("ContactList");
        }
    }
}