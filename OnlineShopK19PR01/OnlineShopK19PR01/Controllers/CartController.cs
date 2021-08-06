using Models.DAL;
using Models.Framework;
using OnlineShopK19PR01.Common;
using OnlineShopK19PR01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShopK19PR01.Controllers
{
    public class CartController : Controller
    {
       
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddCart(long id, int quantity=1)
        {
            var cart = Session[CommonConstants.CartSession];
            var listItem = new List<CartItem>();
            var productDAL = new ProductDAL();
            var product = productDAL.ViewDetail(id);
            ///  listItem = (List<CartItem>)cart; = null  để như cũ thí listItem = null rồi ;
            if (cart != null)
            {
                // thầy gán nhầm kkk , check tồn tại giỏ hàng đã 
                listItem = (List<CartItem>)cart;
                // xong mới convert xang list
                if (listItem.Exists(x=>x.Product.ID == product.ID))
                {
                    foreach(var item in listItem)
                    {
                        if (item.Product.ID == product.ID)
                        {
                            item.quantity += quantity;
                        }
                    }
                }        
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.quantity = quantity;
                    listItem.Add(item);
                }
                Session[CommonConstants.CartSession] = listItem;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.quantity = quantity;
                // nếu ko tồn tại thì add
                listItem.Add(item);
                Session[CommonConstants.CartSession] = listItem;
            }
            return View("Index");
        }
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }//k kéo đc thì ơphari

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.quantity = jsonItem.quantity;
                }
            }
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        
        public ActionResult Success()
        {
            return View();
        }
    }
}