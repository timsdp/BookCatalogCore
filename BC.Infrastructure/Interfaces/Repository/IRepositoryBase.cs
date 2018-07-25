using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Repository
{
    public interface IRepositoryBase<TKey,TEntity> : IDisposable where TEntity : class
    {
        void Create(TEntity item);
        TEntity Get(TKey id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TKey id);
        void Update(TEntity item);
    }
}
