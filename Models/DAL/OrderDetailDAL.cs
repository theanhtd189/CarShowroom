using Models.Framework;
using System.Linq;

namespace Models.DAL
{
    public class OrderDetailDAL
    {
        WEBContext db = null;
        public OrderDetailDAL()
        {
            db = new WEBContext();
        }
        public OrderDetail ViewDetail(long id)
        {
            return db.OrderDetails.SingleOrDefault(x=>x.OrderID==id);
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
