using BC.Infrastructure.Context;
using BC.Infrastructure.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Data.Repositories
{
    public abstract class BaseRepository<TKey, TEntity> : IRepositoryBase<TKey, TEntity> where TEntity : class
    {
        protected IRootContext Context { get; set; }

        public BaseRepository(IRootContext context)
        {
            Context = context;
        }

        public virtual void Create(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity item)
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
