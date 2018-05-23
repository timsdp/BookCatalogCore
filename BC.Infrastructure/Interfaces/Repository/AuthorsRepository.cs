using BC.Data.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Repository
{
    class AuthorsRepository : IGenericRepository<AuthorEM>, IAuthorsRepository
    {
        public void Create(AuthorEM item)
        {
            throw new NotImplementedException();
        }

        public AuthorEM Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorEM> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorEM> Get(Func<AuthorEM, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(AuthorEM item)
        {
            throw new NotImplementedException();
        }

        public void Update(AuthorEM item)
        {
            throw new NotImplementedException();
        }
    }
}
