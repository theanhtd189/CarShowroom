using Models.DAL;
using System.Web.Mvc;

namespace OnlineShopK19PR01.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        public ActionResult Index()
        {
            var dal = new ProductDAL();
            var list = dal.GetAll();
            ViewBag.listall = list;
            return View();
        }
        public ActionResult Category(long id)
        {
            var dal = new ProductCategoryDAL();
            var pro = new ProductDAL();
            var get_pro = pro.ListAllByCategoryID(id);
            var detail = dal.ViewDetail(id);
            ViewBag.category = detail;
            ViewBag.product = get_pro;
            return View();
        }
        [HttpGet]
        public ActionResult ProductDetail(long id)
        {
            var product = new ProductDAL().ViewDetail(id);
            if (product != null)
            {
                ViewBag.Slides = new SlideDAL().ListByTop(1);
                var productDAL = new ProductDAL();
                ViewBag.Product = product;
                var categoryId = product.CategoryID;
                ViewBag.ListRelateProduct = productDAL.ListRelated(categoryId, 3);
                return View("Product");
            }
            else
                return RedirectToAction("", "Trangchu");

        }
    }
}