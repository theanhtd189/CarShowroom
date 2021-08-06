using Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult CategoryDetail(long cateId)
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProductDetail(long id)
        {
            ViewBag.Slides = new SlideDAL().ListByTop(1);
            var productDAL = new ProductDAL();
            var product = productDAL.ViewDetail(id);
            ViewBag.Product = product;
            var categoryId = product.CategoryID;
            ViewBag.ListRelateProduct = productDAL.ListAllByCategoryID(categoryId);
            return View();
        }
    }
}