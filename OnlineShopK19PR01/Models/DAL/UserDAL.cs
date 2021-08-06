using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;


namespace Models.DAL
{
    public class UserDAL : BaseDAL<User>
    {
        public DBContext db = null;
        public UserDAL()
        {
            db = new DBContext();
        }

        public int Login(string userName, string password)
        {
            var result = db.Users.SingleOrDefault
                (model => model.UserName == userName);
            if (result == null)
            {
                return -1;
            }
            else if (result.Status == false)
            {
                return -2;
            }
            else if (result.Password != password)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public override bool Insert(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.UserName = entity.UserName;
                user.Name = entity.Name;
                user.Address = entity.Address;
                user.Phone = entity.Phone;
                user.Email = entity.Email;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {                
                return false;
            }
        }

        public override bool Delete(long id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return true;
        }

        public override User GetByName(string name)
        {
            return db.Users.SingleOrDefault(x => x.UserName == name);
        }

        public override User GetById(long id)
        {
            return db.Users.Find(id);
        }

        public override List<User> GetAllStatus()
        {
            return db.Users.ToList();
        }

        public override IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.Where(x => x.Status == true).OrderBy(x => x.UserName).ToPagedList(page, pageSize);

        }
    }
}
