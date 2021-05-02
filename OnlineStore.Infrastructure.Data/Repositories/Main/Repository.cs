using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.Interfaces;
using OnlineStore.Infrastructure.Data.Context;

namespace OnlineStore.Infrastructure.Data.Main
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private ItmContext db;

        private DbSet<TEntity> Entity;

        public Repository(ItmContext context)
        {
            db = context;
            Entity = db.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Entity.Find(id);
        }

        public IQueryable<TEntity> GetList()
        {
            return Entity;
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return Entity.Where(predicate);
        }
        public void Create(TEntity item)
        {
            Entity.Add(item);
        }
        public void Update(TEntity item)
        { 
            db.Entry(item).State = EntityState.Modified;
        }
    }

}

