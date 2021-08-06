using System.Collections.Generic;

namespace Models.DAL
{
    public abstract class BaseDAL<model>
    {
        public abstract bool Insert(model entity);
        public abstract bool Update(model entity);
        public abstract bool Delete(long id);
        public abstract model GetByName(string name);
        public abstract model GetById(long id);
        public abstract List<model> GetAllStatus();
        public abstract IEnumerable<model> ListAllPaging(string searchString, int page, int pageSize);

    }
}
