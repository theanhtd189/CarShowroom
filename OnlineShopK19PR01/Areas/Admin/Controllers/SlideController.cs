using Models.DAL;
using Models.Framework;
using System;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new SlideDAL();
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
        public ActionResult Insert(Slide model)
        {
            if (ModelState.IsValid)
            {
                var dal = new SlideDAL();
                model.Status = true;
                model.CreateDate = DateTime.Now;
                var result = dal.Insert(model);
                if (result)
                {
                    return RedirectToAction("Index");
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
            var dal = new SlideDAL();
            ViewBag.CategoryID = new SelectList(dal.ListAll(), "ID", "Name", selectedID);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dal = new SlideDAL();
            var result = dal.ViewDetail(id);
            return View(result);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Slide product)
        {
            if (ModelState.IsValid)
            {
                var dal = new SlideDAL();
                product.CreateDate = DateTime.Now;
                var result = dal.Update(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            else
            {
                return View("edit", product);
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long Id)
        {
            new SlideDAL().Delete(Id);
            return RedirectToAction("Index");
        }
    }
}