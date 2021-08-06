using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.DAL;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class CartController : Controller
    {
        // GET: Admin/Cart
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new OrderDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }

        // GET: Admin/Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult Accepted(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new OrderDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAccepted(searchString, page, pageSize);
            return View(model);
        }
        public ActionResult Pending(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new OrderDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListPending(searchString, page, pageSize);
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(long Id)
        {
            new OrderDAL().Delete(Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Active(long Id)
        {
            var r = new OrderDAL().Active(Id);
            return RedirectToAction("Index");
        }
        public ActionResult Deactive(long Id)
        {
            var r = new OrderDAL().Deactive(Id);
                return RedirectToAction("Index");
        }
    }
}
