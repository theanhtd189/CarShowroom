using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAL
{
    public class OrderDAL
    {
        WEBContext db = null;
        public OrderDAL()
        {
            db = new WEBContext();
        }
        public long Insert(Order entity)
        {
            var order = db.Orders.SingleOrDefault(x => x.ID == entity.ID);
            if (order == null)
            {
                db.Orders.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
                return -1;
        }

        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString));
            }
            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<Order> ListPending(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString));
            }
            return model.Where(x => x.Status == false).OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Order> ListAccepted(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString));
            }
            return model.Where(x => x.Status == true).OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Order GetFirst()
        {
            return db.Orders.Where(x => x.Status == true).OrderByDescending(x => x.ID).Take(1).SingleOrDefault();
        }

        public bool Delete(long id)
        {
            var product = db.Orders.Find(id);
            db.Orders.Remove(product);
            db.SaveChanges();
            return true;
        }

        public Order ViewDetail(long id)
        {
            return db.Orders.Find(id);
        }
        public bool Active(long id)
        {
            var result = db.Orders.Find(id);
            if (result != null)
            {
                result.Status = true;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool Deactive(long id)
        {
            var result = ViewDetail(id);
            if (result != null)
            {
                result.Status = false;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
