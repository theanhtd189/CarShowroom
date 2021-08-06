using Models.DAL;
using Models.Framework;
using System;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{

    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dal = new UserDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(User user)
        {
            if (ModelState.IsValid)
            {
                var dal = new UserDAL();
                user.Status = true;
                var result = dal.Insert(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dal = new UserDAL();
                user.CreateDate = DateTime.Now;
                var result = dal.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dal = new UserDAL();
            var result = dal.GetById(id);
            return View(result);
        }
        [HttpDelete]
        public ActionResult Delete(long Id)
        {
            new UserDAL().Delete(Id);
            return RedirectToAction("Index");
        }
    }
}