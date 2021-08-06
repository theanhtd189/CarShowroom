using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    //[Authorize(Roles ="Admin,Biên tập viên")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}