using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> Query();

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(object id);
    }
}