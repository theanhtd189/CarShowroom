using Models.DAL;
using Models.Framework;
using System.Web.Mvc;
namespace OnlineShopK19PR01.Controllers
{
    public class HomeController : Controller
    {
        public WEBContext db = new WEBContext();
        public ActionResult Index()
        {
            var arrSlides = new SlideDAL().ListByTop(4);

            ViewBag.Slides =/* new SlideDAL().ListByTop(4);*/ arrSlides;
            var productCategory = new ProductCategoryDAL();
            ViewBag.ProductCategory = productCategory.ListAll(3);
            var product = new ProductDAL();
            ViewBag.ListFeatureProduct = product.ListFeatureProduct(4);
            ViewBag.ListTopHotProduct = product.ListHotProduct(4);
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDAL().ListByGroupID(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDAL().ListByGroupID(2);
            return PartialView(model);
        }
    }
}