using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAL
{
    public class ProductCategoryDAL
    {
        public WEBContext db = null;
        public ProductCategoryDAL()
        {
            db = new WEBContext();
        }
        public String GetNameProductCategory(long id)
        {
            if (db.ProductCategories.Find(id) != null)
            {
                return ViewDetail(id).Name;
            }
            else
                return "Chưa chọn";
        }
        public String GetNameProductCategory(long? id)
        {
            if (db.ProductCategories.Find(id) != null)
            {
                return ViewDetail((long)id).Name;
            }
            else
                return "Chưa chọn";
        }
        public List<long> GetListId()
        {
            return db.ProductCategories.Select(x => x.ID).ToList();
        }
        public List<long> GetListParentId(long id_except)
        {
            return db.ProductCategories.Where(x => x.ID != id_except).Select(x => x.ID).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public List<ProductCategory> ListAll(int top)
        {
            return db.ProductCategories.OrderBy(x => x.DisplayOrder).Take(top).ToList();
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.Where(x => x.Status == true).OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }
        public IEnumerable<ProductCategory> ListAllHidden(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.Where(x => x.Status == false).OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }
        public bool Insert(ProductCategory entity)
        {
            var product = db.ProductCategories.SingleOrDefault(x => x.ID == entity.ID);
            if (product == null)
            {
                entity.Status = true;
                db.ProductCategories.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(ProductCategory entity)
        {
            try
            {
                var product = db.ProductCategories.Find(entity.ID);
                product.Name = entity.Name;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Status = true;
                product.ParentID = entity.ParentID;
                product.DisplayOrder = entity.DisplayOrder;
                product.SeoTitle = entity.SeoTitle;
                product.CreateBy = entity.CreateBy;
                product.CreateDate = entity.CreateDate;
                product.ModifiedBy = entity.ModifiedBy;
                product.ModifiedDate = entity.ModifiedDate;
                product.MetaKeywords = entity.MetaKeywords;
                product.ShowOnHome = entity.ShowOnHome;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            var product = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(product);
            db.SaveChanges();
            return true;
        }
    }
}
