using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAL
{
    public class ProductDAL
    {
        public WEBContext db = null;
        public ProductDAL()
        {
            db = new WEBContext();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public List<Product> ListRelated(long categoryId, int top)
        {
            return db.Products.Where(x => x.CategoryID == categoryId && x.Status == true).OrderBy(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> ListAllByCategoryID(long categoryId)
        {
            return db.Products.Where(x => x.CategoryID == categoryId && x.Status == true).OrderBy(x => x.CreateDate).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.Status == true).OrderBy(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> ListHotProduct(int top)
        {
            return db.Products.Where(x => x.Status == true).OrderBy(x => x.CreateDate).Take(top).ToList();
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.Where(x => x.Status == true).OrderBy(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Product> ListAllHidden(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.Where(x => x.Status == false).OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }
        public List<Product> GetAll()
        {
            return db.Products.OrderBy(x => x.ID).ToList();
        }
        public List<Product> GetHidden()
        {
            return db.Products.Where(x => x.Status == false).OrderBy(x => x.ID).ToList();
        }
        public bool Insert(Product entity)
        {
            var product = db.Products.SingleOrDefault(x => x.ID == entity.ID);
            if (product == null)
            {
                entity.Status = true;
                db.Products.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.MetaTitle = entity.MetaTitle;
                product.Code = entity.Code;
                product.Description = entity.Description;
                product.Image = entity.Image;
                product.MoreImage = entity.MoreImage;
                product.Price = entity.Price;
                product.PromotionPrice = entity.PromotionPrice;
                if (entity.Quantity == null)
                    product.Quantity = 0;
                else
                    product.Quantity = entity.Quantity;
                product.CategoryID = entity.CategoryID;
                product.Detail = entity.Detail;
                product.Color = entity.Color;
                product.CreateDate = entity.CreateDate;
                product.CreateBy = entity.CreateBy;
                product.ModifiedDate = entity.ModifiedDate;
                product.ModifiedBy = entity.ModifiedBy;
                product.MetaDescription = entity.MetaDescription;
                product.Brand = entity.Brand;
                product.ViewCout = entity.ViewCout;
                product.Status = true;
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
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return true;
        }
    }
}
