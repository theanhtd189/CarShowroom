using Models.DAL;
using Models.Framework;
using OnlineShopK19PR01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

public class GiohangController : Controller
{

    // GET: Cart
    public ActionResult Index()
    {
        var cart = Session["dathang"];
        var list = new List<CartItem>();
        if (cart != null)
        {
            list = (List<CartItem>)cart;
        }
        return View(list);
    }
    public ActionResult Checkout(long id, int quantity=1)
    {
        Session["redirect"] = true;
        var dal = new ProductDAL();
        Session["current_id_product"] = id;
        var product = dal.ViewDetail(id);
        var categoryId = product.CategoryID;
        ViewBag.ListRelateProduct = dal.ListRelated(categoryId, 3);
        ViewBag.Quantity = quantity;
        if (Session["idnguoidung"] != null)
        {
            return View(product);
        }
        else
            return RedirectToAction("", "dangnhap");
        
    }
/*    public ActionResult AddCart(long id, int quantity = 1)
    {
        var cart = Session["dathang"];
        var listItem = new List<CartItem>();
        var productDAL = new ProductDAL();
        var product = productDAL.ViewDetail(id);
        if (cart != null)
        {
            listItem = (List<CartItem>)cart;
            if (listItem.Exists(x => x.Product.ID == product.ID))
            {
                foreach (var item in listItem)
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
            Session["dathang"] = listItem;
        }
        else
        {
            var item = new CartItem();
            item.Product = product;
            item.quantity = quantity;

            listItem.Add(item);
            Session["dathang"] = listItem;
        }
        return RedirectToAction("Index", "Cart");
    }
*/
    public String CreateOrder(long CustomerID, string ShipName, string ShipMobile, string ShipEmail, string ShipAddress, int Quantity, int Price, long ProductID)
    {
        var dal = new OrderDAL();
        var detail_dal = new OrderDetailDAL();
        var order = new Order();
        var detail = new OrderDetail();
        order.CustomerID = CustomerID;
        order.ShipName = ShipName;
        order.ShipEmail = ShipEmail;
        order.ShipMobile = ShipMobile;
        order.ShipAddress = ShipAddress;
        order.CreatedDate = DateTime.Now;
        order.Status = false;
        var upstt = dal.Insert(order);
        if (upstt != -1)
        {
            detail.OrderID = upstt;
            detail.ProductID = ProductID;
            detail.Quantity = Quantity;
            detail.Price = Price.ToString();
            var stt = detail_dal.Insert(detail);
            if (stt)
            {
                return upstt.ToString();
            }
            else
                return "Lỗi tạo chi tiết đơn hàng";
        }
        else
            return "Lỗi tạo đơn mới";

    }
/*    public JsonResult DeleteAll()
    {
        Session["dathang"] = null;
        return Json(new
        {
            status = true
        });
    }
*/
/*    public JsonResult Delete(long id)
    {
        var sessionCart = (List<CartItem>)Session["dathang"];
        sessionCart.RemoveAll(x => x.Product.ID == id);
        Session["dathang"] = sessionCart;
        return Json(new
        {
            status = true
        });
    }
    public JsonResult Update(string cartModel)
    {
        var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
        var sessionCart = (List<CartItem>)Session["dathang"];

        foreach (var item in sessionCart)
        {
            var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
            if (jsonItem != null)
            {
                item.quantity = jsonItem.quantity;
            }
        }
        Session["dathang"] = sessionCart;
        return Json(new
        {
            status = true
        });
    }
*/
    public ActionResult Success()
    {
        return View();
    }
}