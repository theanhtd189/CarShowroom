using Models.DAL;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(string searchString,int page = 1, int pageSize = 1)
        {
            var dal = new UserDAL();
            ViewBag.SearchString = searchString;
            var model = dal.ListAllPaging(searchString,page, pageSize);
            return View(model);
        }
        [HttpGet]
        
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(User user)
        {
            if(ModelState.IsValid)
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
                    ModelState.AddModelError("", "Thêm mơi không thành công");
                }
            }
            return View("Index");            
        }
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dal = new UserDAL();
                //var user1 = dal.GetID(user.ID);
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
        [HttpDelete]
        public ActionResult Delete(long Id)
        {
            new UserDAL().Delete(Id);
            return RedirectToAction("Index");
        }
    }
}