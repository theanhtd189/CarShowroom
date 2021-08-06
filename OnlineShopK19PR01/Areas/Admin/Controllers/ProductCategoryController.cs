using Models.DAL;
using Models.Framework;
using OnlineShopK19PR01.Common;
using System;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new ProductCategoryDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        public ActionResult Hidden(string searchString, int page = 1, int pageSize = 10)
        {
            var dal = new ProductCategoryDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAllHidden(searchString, page, pageSize);
            return View("Index", model);
        }
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Insert(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var dal = new ProductCategoryDAL();
                model.MetaTitle = new ConvertToUnSign().ConvertToUnsign(model.Name);
                model.CreateDate = DateTime.Now;
                var result = dal.Insert(model);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công!");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dal = new ProductCategoryDAL();
            var result = dal.ViewDetail(id);
            return View(result);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dal = new ProductCategoryDAL();
                productcategory.MetaTitle = new ConvertToUnSign().ConvertToUnsign(productcategory.Name);
                productcategory.CreateDate = DateTime.Now;
                var result = dal.Update(productcategory);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long Id)
        {
            new ProductCategoryDAL().Delete(Id);
            return RedirectToAction("Index");
        }
    }
}