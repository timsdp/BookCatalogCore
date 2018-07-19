using BC.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Service
{
    public interface IAuthorService
    {
        AuthorVM GetById(int id);
        IEnumerable<AuthorVM> GetAll();
        IEnumerable<AuthorVM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
        IEnumerable<AuthorVM> GetByBook(int bookId);
        void Add(AuthorVM entity);
        void Remove(int authorId);
        void Update(AuthorVM entity);
        bool CheckExist(AuthorVM vm);
    }
}
