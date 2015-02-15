using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using NInsight.Core.Domain;
using NInsight.Core.Repositories;

namespace NInsight.Persistence.EF
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        protected readonly IDbSet<T> _dbset;

        protected DbContext _entities;

        public GenericRepository(DbContext context)
        {
            this._entities = context;
            this._dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this._dbset.AsEnumerable();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = this._dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            var result = this._dbset.Add(entity);
            this.Save();
            return result;
        }

        public virtual T Delete(T entity)
        {
            var result = this._dbset.Remove(entity);
            this.Save();
            return result;
        }

        public virtual void Edit(T entity)
        {
            this._entities.Entry(entity).State = EntityState.Modified;
            this.Save();
        }

        private void Save()
        {
            this._entities.SaveChanges();
        }
    }
}