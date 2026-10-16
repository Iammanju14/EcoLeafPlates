using EcoLeafPlates.Models;
using EcoLeafPlates.Models.DBManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoLeafPlates.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [Authorize(Roles = "Admin,User")]
        public ActionResult CartInfo(int ProductId)
        {
            clsCartDetails clsCartDetails= new clsCartDetails();
            clsCartDetails.userId = Convert.ToInt16(Session["userId"]);
            clsCartDetails.productId = ProductId;
            clsCartDetails.price = CartManager.getProductPrice(ProductId);
            clsCartDetails.quantity = 1;
            CartManager.addToCart(clsCartDetails);
            return RedirectToAction("getCartDetails");
        }
        [Authorize(Roles="Admin,User")]
        public ActionResult getCartDetails()
        {
            
            List<clsCartDetails> lstCartDetails = new List<clsCartDetails>();
            List<clsOrderDetails> lstOrderDetails = new List<clsOrderDetails>();
            lstCartDetails = CartManager.displayCartDetails();
            int total = 0;

            List<clsProductDetails> lstProductDetails = ProductManager.displayProductDetails();
            List<clsCartDetailsToDisplay> lstCartDetailsToDisplay = new List<clsCartDetailsToDisplay>();
                foreach(clsCartDetails cartObj in  lstCartDetails)
                {
                    clsCartDetailsToDisplay cartDispObject = new clsCartDetailsToDisplay();
                    clsOrderDetails OrderDetails = new clsOrderDetails();

                    clsProductDetails objProductDetails = (clsProductDetails)lstProductDetails.Single(x => x.productId == cartObj.productId);
                    cartDispObject.ProductName = objProductDetails.productName;

                    // Order Details
                    OrderDetails.orderItem = objProductDetails.productName;
                    OrderDetails.orderQuantity = cartObj.quantity;
                    OrderDetails.price = cartObj.price;
                    lstOrderDetails.Add(OrderDetails);
                    //Cart Display
                    cartDispObject.quantity = cartObj.quantity;
                    cartDispObject.price = cartObj.price;
                    cartDispObject.cartId = cartObj.cartId;
                    cartDispObject.total += cartDispObject.price;
                    lstCartDetailsToDisplay.Add(cartDispObject);

                    
                    total += cartObj.price;

                }
                ViewBag.Total = total;
                Session["TotalAmt"] = total;

                //Order Details
                Session["OrderDetails"] = lstOrderDetails;
                List<clsUserDetails> lstUserDetails = UserManager.displayUserDetails();
                clsUserDetails objUser = (clsUserDetails)lstUserDetails.Single(u => u.userId == Convert.ToInt16(Session["userId"]));

                ViewBag.Address = objUser.name + "<br/>" + objUser.address + "<br/>" + objUser.phoneNo;
                
                return View(lstCartDetailsToDisplay);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult EditCartDetails(int? cartId)
        {
            List<clsCartDetails> lstCartDetails = CartManager.displayCartDetails();
            clsCartDetails cartDetail = lstCartDetails.Single(x => x.cartId == cartId);
            clsCartDetailsToDisplay cartDetailsToDisplay = new clsCartDetailsToDisplay();
            cartDetailsToDisplay.cartId = cartDetail.cartId;
             List<clsProductDetails> lstProductDetails = ProductManager.displayProductDetails();
             clsProductDetails objProductDetails = (clsProductDetails)lstProductDetails.Single(x => x.productId == cartDetail.productId);
             cartDetailsToDisplay.ProductName = objProductDetails.productName;
             Session["ProductPrice"] = objProductDetails.productPrice;
             cartDetailsToDisplay.quantity = cartDetail.quantity;
             cartDetailsToDisplay.price = cartDetail.price;
             return View(cartDetailsToDisplay);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult EditCartDetails(clsCartDetailsToDisplay cartDetails)
        {
            cartDetails.price = Convert.ToInt16(Session["ProductPrice"]) * cartDetails.quantity;
            CartManager.editCartDetails(cartDetails);
            return RedirectToAction("getCartDetails");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult DeleteCartDetails(int cartId)
        {
            CartManager.deleteCartDetails(cartId);
            return RedirectToAction("getCartDetails");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult PlaceAnOrder()
        {
            clsOrder clsOrderDetails = new clsOrder();
            clsOrderDetails.orderAmt = Convert.ToInt16(Session["TotalAmt"]);
            clsOrderDetails.userId = Convert.ToInt16(Session["userId"]);

            var dateTimeNow = DateTime.Now;
            dateTimeNow.ToShortDateString();
            clsOrderDetails.date = dateTimeNow.ToShortDateString();
            clsOrderDetails.isDelivered = 0;

            List<clsOrderDetails> lstOrderDetials = (List<clsOrderDetails>)Session["OrderDetails"];
            int isOrdered = OrderManager.addAnOrder(clsOrderDetails, lstOrderDetials);
            return RedirectToAction("DisplayPlacedOrderDetails");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult DisplayPlacedOrderDetails()
        {
            List<clsOrderDetails> lstPlacedOrders = new List<clsOrderDetails>();
            lstPlacedOrders = OrderManager.displayPlacedOrderDetails();
            return View(lstPlacedOrders);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPlacedOrderDetails(int? orderDetailId)
        {
            List<clsOrderDetails> lstOrderDetails = OrderManager.displayPlacedOrderDetails();
            clsOrderDetails OrderDetail = lstOrderDetails.Single(x => x.orderDetailId == orderDetailId);
            return View(OrderDetail);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPlacedOrderDetails(clsOrderDetails OrderDetail)
        {
            OrderManager.EditPlacedOrderDetails(OrderDetail);
            return RedirectToAction("DisplayPlacedOrderDetails");
        }


       

    }
}