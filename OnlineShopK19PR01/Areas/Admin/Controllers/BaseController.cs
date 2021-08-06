using OnlineShopK19PR01.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShopK19PR01.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session["admin"];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    (new { action = "Index", controller = "Login", area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}