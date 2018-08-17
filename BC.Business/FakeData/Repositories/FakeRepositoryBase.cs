using BC.Infrastructure.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BC.Business.FakeData.Repositories
{
    public class FakeRepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity> where TEntity : class
    {
        public List<TEntity> Data = new List<TEntity>(); 
        public void Create(TEntity item)
        {
            Data.Add(item);
        }

        public void Dispose()
        {
           
        }

        public TEntity Get(TId id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(TId id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
