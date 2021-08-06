using Models.DAL;
using Models.Framework;
using System.Web.Mvc;
namespace OnlineShopK19PR01.Controllers
{
    public class TrangchuController : Controller
    {
        public WEBContext db = new WEBContext();
        public ActionResult Index()
        {
            var arrSlides = new SlideDAL().ListByTop(4);
            var arrNews = new ContentDAL().ListFeaturedContent(6);
            var product = new ProductDAL();
            ViewBag.ListFeatureProduct = product.ListFeatureProduct(12);
            ViewBag.Slides = arrSlides;
            ViewBag.ListNews = arrNews;
            return View();
        }

    }
}