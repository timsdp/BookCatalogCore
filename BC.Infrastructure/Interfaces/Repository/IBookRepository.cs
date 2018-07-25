using BC.Data.Entity.Books;
using BC.ViewModel.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Repository
{
    public interface IBookRepository : IRepositoryBase<int,BookEM>
    {
        IEnumerable<BookEM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
        IEnumerable<BookEM> GetByAuthor(int authorId);
    }
}
