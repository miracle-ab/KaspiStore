using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicat);

    }
}
