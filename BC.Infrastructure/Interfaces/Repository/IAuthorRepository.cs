using BC.Data.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Repository
{
    public interface IAuthorRepository : IGenericRepository<AuthorEM>
    {
    }
}
