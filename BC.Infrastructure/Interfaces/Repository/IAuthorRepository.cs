using BC.Data.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Repository
{
    public interface IAuthorRepository : IRepositoryBase<int,AuthorEM>
    {
        IEnumerable<AuthorEM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
        IEnumerable<AuthorEM> GetByBook(int bookId);
        Dictionary<int, string> GetAutocomplete(string query);
        ICollection<AuthorEM> GetByFullNameAndYear(string firstName, string lastName, int bornYear);

    }
}
