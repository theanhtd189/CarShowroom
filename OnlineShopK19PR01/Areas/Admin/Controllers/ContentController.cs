using Models.DAL;
using Models.Framework;
using OnlineShopK19PR01.Common;
using System;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new ContentDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Insert(Content model)
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
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dal = new ContentDAL();
            var result = dal.ViewDetail(id);
            return View(result);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Content Content)
        {
            if (ModelState.IsValid)
            {
                var dal = new ContentDAL();
                Content.CreateDate = DateTime.Now;
                Content.MetaTitle = new ConvertToUnSign().ConvertToUnsign(Content.Name);
                var result = dal.Update(Content);
                if (result)
                {
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long Id)
        {
            new ContentDAL().Delete(Id);
            return RedirectToAction("Index");
        }
    }
}