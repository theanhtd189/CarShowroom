using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL
{
    public class ProductCategoryDAL
    {
        public DBContext db = null;
        public ProductCategoryDAL()
        {
            db = new DBContext();
        }
        public List<ProducCategory>ListAll(int top) // thiếu t
        {
            return db.ProducCategories.OrderBy(x => x.DisplayOrder).Take(top).ToList();
        }
    }
}
