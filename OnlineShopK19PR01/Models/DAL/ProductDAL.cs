using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL
{
    public class ProductDAL
    {
        public DBContext db = null;
        public ProductDAL()
        {
            db = new DBContext();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public List<Product>ListAllByCategoryID(long categoryId)
        {
            return db.Products.Where(x => x.CategoryID == categoryId && x.Status == true).OrderBy(x => x.CreateDate).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.Status == true).OrderBy(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> ListHotProduct(int top)
        {
            return db.Products.Where(x => x.Status == true&&x.TopHot>DateTime.Now).OrderBy(x => x.CreateDate).Take(top).ToList();
        }
    }
}
