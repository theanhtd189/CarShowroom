using Models.DAL;
using Models.Framework;
using System;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Controllers
{
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View("User_Profile");
        }
        [HttpPost]
        public String UpdateInfo()
        {
            long id = (long)Session["idnguoidung"];
            var dal = new UserDAL();
            var upstt = false;
            String name = Request.Form["input-name"];
            String email = Request.Form["input-email"];
            String phone = Request.Form["input-phone"];
            String address = Request.Form["input-address"];

            var user = new User();
            user.ID = id;
            user.Name = name;
            user.Email = email;
            user.Phone = phone;
            user.Address = address;
            user.UserName = dal.GetById(id).UserName;
            upstt = dal.Update(user);
            if (upstt)
            {
                return "Thành công";
            }
            else
                return "Thất bại";
        }

        
        [HttpPost]
        public string ChangePassword()
        {
            long id = (long)Session["idnguoidung"];
            var dal = new UserDAL();
            String new_pass = Request.Form["input_password"];
            if (new_pass != null && !String.IsNullOrEmpty(new_pass))
            {
                var upstt = dal.ChangePassword(id, new_pass);
                if (upstt)
                {
                    return "Thành công";
                }
                else
                    return "Thất bại";
            }
            else
                return "Thất bại";
        }
        public ActionResult LogOut()
        {
            Session["idnguoidung"] = null;
            return RedirectToAction("", "trangchu");
        }
    }
}