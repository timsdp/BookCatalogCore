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

namespace BC.Business.Author
{
    public class AuthorService : BaseService, IAuthorService
    {
        public AuthorService(IRootContext context) : base(context) { }

        public AuthorVM GetById(int id)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            using (var bookRepository = Context.Factory.GetService<IBookRepository>(Context.RootContext))
            {
                    AuthorEM entityModel = authorRepository.Get(id);
                    IEnumerable<BookEM> booksEm = bookRepository.GetByAuthor(id);

                    AuthorVM authorVm = Context.Mapper.MapTo<AuthorVM,AuthorEM>(entityModel);
                    authorVm.TopBooks = Context.Mapper.MapTo<IEnumerable<BookVM>, IEnumerable<BookEM>>(booksEm);

                    return authorVm;
            }
        }

        public IEnumerable<AuthorVM> GetAll()
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                IEnumerable<AuthorEM> entities = authorRepository.Get();
                return Context.Mapper.MapTo<IEnumerable<AuthorVM>, IEnumerable<AuthorEM>>(entities); 
            }
               
        }

        public void Update(AuthorVM viewModel)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                AuthorEM entityModel = Context.Mapper.MapTo<AuthorEM, AuthorVM>(viewModel);
                authorRepository.Update(entityModel);
            }  
        }

        public IEnumerable<AuthorVM> GetByBook(int bookId)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                throw new NotImplementedException();
            }
        }

        public void Add(AuthorVM entity)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                throw new NotImplementedException();
            }
        }

        public void Remove(int authorId)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                authorRepository.Remove(authorId);
            }
        }

        public IEnumerable<AuthorVM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                IEnumerable<AuthorEM> entities = authorRepository.GetAllFiltered(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
                return Context.Mapper.MapTo<IEnumerable<AuthorVM>, IEnumerable<AuthorEM>>(entities);
            }
        }

        public bool CheckExist(AuthorVM vm)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                var authors = authorRepository.GetByFullNameAndYear(vm.FirstName,vm.LastName,vm.Born);
                if (authors.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Dictionary<int, string> GetAutocomplete(string query)
        {
            using (var authorRepository = Context.Factory.GetService<IAuthorRepository>(Context.RootContext))
            {
                return authorRepository.GetAutocomplete(query);
            }
        }
    }
}
