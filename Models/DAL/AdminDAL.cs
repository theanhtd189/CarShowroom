using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL
{

    public class AdminDAL
    {
        public WEBContext db = null;
        public AdminDAL()
        {
            db = new WEBContext();
        }
        public Admin GetByName(string name)
        {
            return db.Admins.SingleOrDefault(x => x.Username == name);
        }
        public int Login(string userName, string password)
        {
            var result = db.Admins.SingleOrDefault(model => model.Username == userName);
            if (result == null)
            {
                return -1;
            }
            else if (password.Equals(result.Password))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public bool ChangePassword(long id, string password)
        {
            try
            {
                var user = db.Admins.Find(id);
                user.Password = password;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
