using Models.DAL;
using Models.Framework;
using OnlineShopK19PR01.Common;
using System;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    //[Authorize(Roles ="Admin,Biên tập viên")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new ProductDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        public ActionResult Hidden(string searchString, int page = 1, int pageSize = 10)
        {
            var dal = new ProductDAL();
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
        public ActionResult Insert(Product model)
        {
            if (ModelState.IsValid)
            {
                var dal = new ProductDAL();
                model.MetaTitle = new ConvertToUnSign().ConvertToUnsign(model.Name);
                model.Status = true;
                model.CreateDate = DateTime.Now;
                var result = dal.Insert(model);
                if (result)
                {
                    return RedirectToAction("Index", "Product");
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
            var dal = new ProductCategoryDAL();
            ViewBag.CategoryID = new SelectList(dal.ListAll(), "ID", "Name", selectedID);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dal = new ProductDAL();
            var result = dal.ViewDetail(id);
            return View(result);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                var dal = new ProductDAL();
                product.MetaTitle = new ConvertToUnSign().ConvertToUnsign(product.Name);
                product.CreateDate = DateTime.Now;
                var result = dal.Update(product);
                if (result)
                {
                    return RedirectToAction("Index", "Product");
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
            new ProductDAL().Delete(Id);
            return RedirectToAction("Index");
        }
    }
}