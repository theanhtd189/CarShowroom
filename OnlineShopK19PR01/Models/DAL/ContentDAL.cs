using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL
{
    public class ContentDAL : BaseDAL<Content>
    {
        public DBContext db = null;
        public ContentDAL()
        {
            db = new DBContext();
        }
        public override bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public override List<Content> GetAllStatus()
        {
            throw new NotImplementedException();
        }

        public override Content GetById(long id)
        {
            throw new NotImplementedException();
        }

        public override Content GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public override bool Insert(Content entity)
        {
            try
            {
                db.Contents.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public override IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Content entity)
        {
            throw new NotImplementedException();
        }
    }
}
