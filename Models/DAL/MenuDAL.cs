using Models.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAL
{
    public class MenuDAL
    {
        public WEBContext db = null;
        public MenuDAL()
        {
            db = new WEBContext();
        }
        public List<Menu> ListByGroupID(long id)
        {
            return db.Menus.Where(x => x.TypeID == id && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
