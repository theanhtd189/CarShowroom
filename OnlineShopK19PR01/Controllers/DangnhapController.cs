using Models.DAL;
using Models.Framework;
using OnlineShopK19PR01.Areas.Admin.Models;
using OnlineShopK19PR01.Common;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Controllers
{
    public class DangnhapController : Controller
    {
        // GET: Dangnhap
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var dal = new UserDAL();
                var user = dal.GetByName(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else
                {
                    var pass = Encryptor.GetHash(model.Password);

                    var result = dal.Login(user.UserName, pass);
                    if (result == 1)
                    {
                        var userSession = new UserLogin();
                        userSession.UserName = user.UserName;
                        userSession.UserID = user.ID;
                        Session.Add("idnguoidung", userSession.UserID);
                        if (Session["redirect"] == null)
                        {
                            return RedirectToAction("index", "nguoidung");
                        }
                        else
                        {
                            var id_product = Session["current_id_product"];
                            return Redirect(Url.Action("checkout", "giohang") + "/" + id_product);
                        }

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
        [HttpPost]
        public int LogIn(string UserName, string Password)
        {
            var tPassword = Encryptor.GetHash(Password);
            var dal = new UserDAL();
            var upstt = dal.Login(UserName,tPassword);
            if (upstt==1)
            {
                var user = dal.GetByName(UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add("idnguoidung", userSession.UserID);
                    if (Session["redirect"] != null)
                    {
                        var id_product = Session["current_id_product"];
                        return 1;
                    }
                return -1;            
            }
            return 0;
        }
        [HttpPost]
        public bool SignUp(string input_username, string input_name, string input_password, string input_email)
        {
            var user = new User();
            user.UserName = input_username;
            user.Name = input_name;
            user.Password = Encryptor.GetHash(input_password);
            user.Email = input_email;
            var dal = new UserDAL();
            var upstt = dal.Insert(user);
            if (upstt)
            {
                Session.Add("idnguoidung",user.ID);
                return true;
            }
            else
                return false;

        }
    }
}