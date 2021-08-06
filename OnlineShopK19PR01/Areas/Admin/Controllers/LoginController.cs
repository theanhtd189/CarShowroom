using Models.DAL;
using OnlineShopK19PR01.Areas.Admin.Models;
using OnlineShopK19PR01.Common;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return View();
            else
                return RedirectToAction("", "User");
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dal = new AdminDAL();

                var user = dal.GetByName(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else
                {
                    

                    var result = dal.Login(user.Username, model.Password);
                    if (result == 1)
                    {
                        var userSession = new UserLogin();
                        userSession.UserName = user.Username;
                        userSession.UserID = user.Id;
                        Session.Add("admin", userSession);
                        return RedirectToAction("Index", "User");

                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng!");
                    }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại!");
                    }
                    else if (result == -2)
                    {
                        ModelState.AddModelError("", "Tài khoản đang bị khóa!");
                    }
                }

            }
            return View("Index");
        }
        public ActionResult LogOut()
        {
            Session["admin"] = null;
            return RedirectToAction("Index");
        }
    }
}