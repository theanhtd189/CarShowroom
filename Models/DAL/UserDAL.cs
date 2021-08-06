using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;


namespace Models.DAL
{
    public class UserDAL : BaseDAL<User>
    {
        public WEBContext db = null;
        public UserDAL()
        {
            db = new WEBContext();
        }

        public UserGroup GetUserGroup(long id)
        {
            return db.UserGroups.Find(id);
        }

        public int Login(string userName, string password)
        {
            var result = db.Users.SingleOrDefault(model => model.UserName == userName);
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
            try
            {
                var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
                if (user == null)
                {
                    entity.GroupID = 1;
                    entity.CreateDate = DateTime.Now;
                    entity.Status = true;
                    db.Users.Add(entity);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
        public UserGroup GetRole(long id)
        {
            return db.UserGroups.SingleOrDefault(x => x.ID == id);
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
        public bool ChangePassword(long id, string password)
        {
            try
            {
                var user = db.Users.Find(id);
                user.Password = password;
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
