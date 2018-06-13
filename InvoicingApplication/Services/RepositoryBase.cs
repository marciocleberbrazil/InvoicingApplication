using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace InvoicingApplication.Services
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        protected readonly AppContext _db;

        public RepositoryBase(AppContext db)
        {
            _db = db;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _db.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
            {
                _db.Set<TEntity>().Attach(entityToDelete);
            }
            _db.Set<TEntity>().Remove(entityToDelete);
        }

        public virtual void Insert(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }

        public virtual TEntity GetByID(object id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual void Update(TEntity ojb)
        {
            _db.Set<TEntity>().Attach(ojb);
            _db.Entry(ojb).State = EntityState.Modified;
        }
    }
}