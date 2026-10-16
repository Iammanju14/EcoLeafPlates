using EcoLeafPlates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
--------------------------------------------------------------------------------------------
































































































































































































































namespace EcoLeafPlates.Controllers
{
    public class EcoController : Controller
    {
        // GET: Eco
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Infrastructure()
        {
            return View();
        }

        public ActionResult ManufactureProcess()
        {
            return View();
        }
        public ActionResult About()
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
            try
            {
                if (ModelState.IsValid)
                {
                    //MailMessage msz = new MailMessage();
                    //msz.From = new MailAddress(vm.Email);//Email which you are getting 
                    ////from contact us page 
                    //msz.To.Add("mkgdinnerware@gmail.com");//Where mail will be sent 
                    //msz.Subject = "Contact From Site:" + vm.Subject;
                    //msz.Body = "Name :" + vm.Name + "\n"
                    //    + "Mail Id:" + vm.Email + "\n"
                    //    + "Phone No:" + vm.PhoneNo + "\n"
                    //    + "Message:" + vm.Message + "\n";
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587; //587 25 465
                    //smtp.Credentials = new System.Net.NetworkCredential
                    //("mkgdinnerware@gmail.com", "mkgdinnerware2017");
                    //smtp.EnableSsl = true;
                    //smtp.Send(msz);
                    //ModelState.Clear();
                    //ViewBag.Message = "Thank you for Contacting us ";
                }
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ViewBag.Message = "Sorry we are facing problem in contacting,please try after some time" + ex.Message;
            }
            return View();
        }
    }
}