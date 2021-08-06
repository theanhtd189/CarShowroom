using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAL
{
    public class CategoryDAL : BaseDAL<Category>
    {
        WEBContext db = null;
        public CategoryDAL()
        {
            db = new WEBContext();
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public override bool Delete(long id)
        {
            var cate = db.Categories.Find(id);
            if (cate != null)
            {
                db.Categories.Remove(cate);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public override List<Category> GetAllStatus()
        {
            return db.Categories.ToList();
        }

        public override Category GetById(long id)
        {
            return db.Categories.SingleOrDefault(x => x.ID == id);
        }

        public override Category GetByName(string name)
        {
            return db.Categories.SingleOrDefault(x => x.Name == name);
        }

        public override bool Insert(Category entity)
        {
            var cate = db.Categories.SingleOrDefault(x => x.Name == entity.Name);
            if (cate == null)
            {
                db.Categories.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.Where(x => x.Status == true).OrderBy(x => x.Name).ToPagedList(page, pageSize);

        }

        public override bool Update(Category entity)
        {
            try
            {
                var cate = db.Categories.Find(entity.ID);
                cate.Name = entity.Name;
                cate.ParentID = entity.ParentID;
                cate.SeoTitle = entity.SeoTitle;
                cate.ShowOnHome = entity.ShowOnHome;
                cate.ModifiedDate = DateTime.Now;
                cate.ModifiedBy = entity.ModifiedBy;
                cate.MetaKeywords = entity.MetaKeywords;
                cate.MetaDescription = entity.MetaDescription;
                cate.DisplayOrder = entity.DisplayOrder;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //ghi log
                return false;
            }
        }
    }
}
