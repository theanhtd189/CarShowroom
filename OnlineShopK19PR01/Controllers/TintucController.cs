using Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Controllers
{
    public class TintucController : Controller
    {
        // GET: Tintuc
        public ActionResult Index()
        {
            var dal = new ContentDAL();
            var list = dal.GetAll();
            ViewBag.listall = list;
            return View();
        }
        [Route("tintuc/detail/{id:long}")]
        public ActionResult Detail(long id)
        {
            var dal = new ContentDAL();
            var detail = dal.ViewDetail(id);
            var related = dal.ListFeaturedContent(4);
            if (detail != null)
            {
                ViewBag.related = related;
                ViewBag.content = detail;
                return View();
            }
            else
                return RedirectToAction("Index");       
        }
        public ActionResult Detail(string link)
        {
            var dal = new ContentDAL();
            var detail = dal.GetByLink(link);
            var related = dal.ListFeaturedContent(4);
            if (detail != null)
            {
                ViewBag.related = related;
                ViewBag.content = detail;
                return View();
            }
            else
                return RedirectToAction("Index");
        }

    }
}