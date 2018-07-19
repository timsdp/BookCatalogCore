using AutoMapper;
using BC.Data.Entity.Authors;
using BC.Data.Entity.Books;
using BC.Data.Repositories;
using BC.Infrastructure.Interfaces.Repository;
using BC.Infrastructure.Interfaces.Service;
using BC.ViewModel;
using BC.ViewModel.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Business.Author
{
    public class AuthorService : IAuthorService
    {
        IAuthorRepository authorRepository = null;
        IBookRepository bookRepository = null;
        public AuthorService(string connectionString)
        {
            this.authorRepository = new AuthorsRepository(connectionString);
            this.bookRepository = new BookRepository(connectionString);
        }


        public AuthorVM GetById(int id)
        {
            AuthorEM entityModel = authorRepository.Get(id);
            IEnumerable<BookEM> booksEm = bookRepository.GetByAuthor(id);

            AuthorVM authorVm = Mapper.Map<AuthorVM>(entityModel);
            authorVm.TopBooks= Mapper.Map<IEnumerable<BookVM>>(booksEm);

            return authorVm;
        }

        public IEnumerable<AuthorVM> GetAll()
        {
            IEnumerable<AuthorEM> entities = authorRepository.Get();
            return Mapper.Map<IEnumerable<AuthorVM>>(entities);
        }

        public void Update(AuthorVM viewModel)
        {
            AuthorEM entityModel = Mapper.Map<AuthorEM>(viewModel);
            authorRepository.Update(entityModel);
        }

        public IEnumerable<AuthorVM> GetByBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public void Add(AuthorVM entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int authorId)
        {
            authorRepository.Remove(authorId);
        }

        public IEnumerable<AuthorVM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public bool CheckExist(AuthorVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
