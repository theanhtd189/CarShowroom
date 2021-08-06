using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAL
{
    public class ContentDAL : BaseDAL<Content>
    {
        public WEBContext db = null;
        public ContentDAL()
        {
            db = new WEBContext();
        }
        public Content ViewDetail(long id)
        {
            return db.Contents.Find(id);
        }
        public Content GetByLink(string link)
        {
            return db.Contents.SingleOrDefault(x=>x.MetaTitle==link);
        }
        public override bool Delete(long id)
        {
            var Content = db.Contents.Find(id);
            db.Contents.Remove(Content);
            db.SaveChanges();
            return true;
        }
        public List<Content> GetAll()
        {
            return db.Contents.OrderBy(x => x.CreateDate).ToList();
        }
        public override List<Content> GetAllStatus()
        {
            throw new NotImplementedException();
        }

        public override Content GetById(long id)
        {
            return db.Contents.Find(id);
        }

        public override Content GetByName(string name)
        {
            return db.Contents.Where(x => x.Name.Contains(name)).SingleOrDefault();
        }

        public override bool Insert(Content entity)
        {
            var product = db.Contents.SingleOrDefault(x => x.ID == entity.ID);
            if (product == null)
            {
                entity.Status = true;
                db.Contents.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public override IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.Where(x => x.Status == true).OrderBy(x => x.Name).ToPagedList(page, pageSize);
        }

        public override bool Update(Content entity)
        {
            try
            {
                var Content = db.Contents.Find(entity.ID);
                Content.Name = entity.Name;
                Content.MetaTitle = entity.MetaTitle;
                Content.Description = entity.Description;
                Content.Image = entity.Image;
                Content.MoreImage = entity.MoreImage;
                Content.CategoryID = 0;
                Content.Detail = entity.Detail;
                Content.CreateDate = entity.CreateDate;
                Content.CreateBy = entity.CreateBy;
                Content.ModifiedDate = entity.ModifiedDate;
                Content.ModifiedBy = entity.ModifiedBy;
                Content.MetaDescription = entity.MetaDescription;
                Content.ViewCout = entity.ViewCout;
                Content.MetaDescriptions = entity.MetaDescriptions;
                Content.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Content> ListFeaturedContent(int top)
        {
            return db.Contents.Where(x => x.Status == true).OrderBy(x => x.CreateDate).Take(top).ToList();
        }
        public List<Content> ListAllContent()
        {
            return db.Contents.Where(x => x.Status == true).OrderBy(x => x.CreateDate).ToList();
        }
    }
}
