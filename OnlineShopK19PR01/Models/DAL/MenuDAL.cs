using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL
{
   public class MenuDAL
    {
        public DBContext db = null;
        public MenuDAL()
        {
            db = new DBContext();
        }
        public List<Menu>ListByGroupID(long id)
        {
            return db.Menus.Where(x => x.TypeID == id && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
