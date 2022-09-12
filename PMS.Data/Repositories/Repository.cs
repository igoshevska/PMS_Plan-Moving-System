using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Data.Repositories
{
    /// <summary>
    /// Generic repository implementation for all CRUD operation
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext DbContext;


        public Repository(DataContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public void Create(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            DbContext.Entry(entity).State = EntityState.Added;
            DbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
        }

        public void DeleteById(object id)
        {
            var entity = this.GetById(id)
;
            if (entity != null)
            {
                this.Delete(entity);
                DbContext.SaveChanges();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(object id)
        {
            return DbContext.Set<TEntity>().Find(id)
;
        }

        public IQueryable<TEntity> Query()
        {
            return DbContext.Set<TEntity>().AsQueryable<TEntity>();
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
    }
}
