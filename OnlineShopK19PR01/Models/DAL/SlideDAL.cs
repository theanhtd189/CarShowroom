using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL
{
    public class SlideDAL
    {
        public DBContext db = null;
        public SlideDAL()
        {
            db = new DBContext();
        }
        public List<Slide>ListByTop(int top)
        {
            return db.Slides.OrderBy(x => x.DisplayOrder).Take(top).ToList();
        }
    }
}
//cái này ở view
