using BC.ViewModel.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Service
{
    public interface IBookService:IDisposable
    {
        IEnumerable<BookVM> GetAll();
        IEnumerable<BookVM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
        BookVM GetById(int id);
        IEnumerable<BookVM> GetByAuthor(int authorId);
        void Create(BookVM entity);
        void Remove(int bookId);
        void Update(BookVM entity);
    }
}
