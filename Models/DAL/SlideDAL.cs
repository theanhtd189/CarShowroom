using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DAL
{
    public class SlideDAL
    {
        public WEBContext db = null;
        public SlideDAL()
        {
            db = new WEBContext();
        }
        public int GetListOrder()
        {
            return db.Slides.Count();
        }
        public int? SetDisplayOrder()
        {
            return db.Slides.Max(x => x.DisplayOrder) + 1;
        }
        public Slide GetDisplayOrder(int display)
        {
            return db.Slides.Where(x => x.DisplayOrder == display).SingleOrDefault();
        }
        public Slide ViewDetail(long id)
        {
            return db.Slides.SingleOrDefault(x => x.ID == id);
        }
        public List<Slide> ListByTop(int top)
        {
            return db.Slides.OrderBy(x => x.DisplayOrder).Take(top).ToList();
        }
        public List<Slide> ListAll()
        {
            return db.Slides.OrderByDescending(x => x.DisplayOrder).ToList();
        }
        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Description.Contains(searchString));
            }
            return model.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }
        public bool Insert(Slide entity)
        {
            var Slide = db.Slides.SingleOrDefault(x => x.ID == entity.ID);
            if (Slide == null)
            {
                entity.Status = true;
                if(GetListOrder()>1)
                {
                    if (entity.DisplayOrder != SetDisplayOrder())
                    {
                        GetDisplayOrder((int)entity.DisplayOrder).DisplayOrder = SetDisplayOrder();
                    }
                }    
                db.Slides.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Slide entity)
        {
            try
            {
                var Slide = ViewDetail(entity.ID);
                Slide.Description = entity.Description;
                Slide.Image = entity.Image;
                Slide.MoreImage = entity.MoreImage;
                Slide.CategoryID = entity.CategoryID;
                Slide.Detail = entity.Detail;
                if (entity.DisplayOrder != Slide.DisplayOrder)
                {
                    GetDisplayOrder((int)entity.DisplayOrder).DisplayOrder = Slide.DisplayOrder;
                    Slide.DisplayOrder = entity.DisplayOrder;
                }
                Slide.Link = entity.Link;
                Slide.CreateDate = entity.CreateDate;
                Slide.CreateBy = entity.CreateBy;
                Slide.ModifiedDate = entity.ModifiedDate;
                Slide.ModifiedBy = entity.ModifiedBy;
                Slide.Status = true;
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
            var Slide = db.Slides.SingleOrDefault(x=>x.ID==id);
            db.Slides.Remove(Slide);
            db.SaveChanges();
            return true;
        }
    }
}

