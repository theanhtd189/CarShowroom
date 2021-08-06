using Models.DAL;
using OnlineShopK19PR01.Areas.Admin.Models;
using OnlineShopK19PR01.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var dal = new UserDAL();

                var user = dal.GetByName(model.UserName);
                if(user==null)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else
                {
                    user.Password = Encryptor.GetHash(model.Password);

                    var result = dal.Login(user.UserName, user.Password);
                    if (result == 1)
                    {
                        var userSession = new UserLogin();
                        userSession.UserName = user.UserName;
                        userSession.UserID = user.ID;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");

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
    }
}