using BC.Data.Entity.Authors;
using BC.Data.Entity.Books;
using BC.Data.Repositories;
using BC.Infrastructure.Context;
using BC.Infrastructure.Interfaces.Repository;
using BC.Infrastructure.Interfaces.Service;
using BC.ViewModel;
using BC.ViewModel.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Business.Book
{
    public class BookService :BaseService, IBookService
    {
        public BookService(IRootContext context) : base(context) { }

        public void Add(BookVM entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookVM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            using (var bookRepository = Context.Factory.GetService<IBookRepository>(Context.RootContext))
            {
                IEnumerable<BookEM> entities = bookRepository.GetAllFiltered(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
                return Context.Mapper.MapTo<IEnumerable<BookVM>, IEnumerable<BookEM>>(entities);
            }
            
        }

        public IEnumerable<BookVM> GetByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public BookVM GetById(int id)
        {
            using (var bookRepository = Context.Factory.GetService<IBookRepository>(Context.RootContext))
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                BookEM entity = bookRepository.Get(id);
                IEnumerable<AuthorEM> authorsEm = authorRepository.GetByBook(id);
                IEnumerable<AuthorVM> authorsVm = Context.Mapper.MapTo<IEnumerable<AuthorVM>, IEnumerable<AuthorEM>>(authorsEm);
                BookVM retVal = Context.Mapper.MapTo<BookVM, BookEM>(entity);
                retVal.Authors = new List<AuthorVM>(authorsVm);
                return retVal;
            }
        }

        public void Remove(int bookId)
        {
            using (var bookRepository = Context.Factory.GetService<IBookRepository>(Context.RootContext))
            {
                bookRepository.Remove(bookId);
            }
        }

        public void Update(BookVM entity)
        {
            throw new NotImplementedException();
        }
    }
}
