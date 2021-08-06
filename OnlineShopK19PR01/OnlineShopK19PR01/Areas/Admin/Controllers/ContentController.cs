using Models.DAL;
using Models.Framework;
using OnlineShopK19PR01.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Insert()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Insert(Content model)
        {
            if (ModelState.IsValid)
            {
                var dal = new ContentDAL();
                model.MetaTitle = new ConvertToUnSign().ConvertToUnsign(model.Name);
                model.Status = true;
                model.CreateDate = DateTime.Now;
                var result = dal.Insert(model);
                if (result)
                {
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công!");
                }
            }
            SetViewBag();
            return View();
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dal = new CategoryDAL();
            ViewBag.CategoryID = new SelectList(dal.ListAll(), "ID", "Name", selectedID);
        }
    }
}