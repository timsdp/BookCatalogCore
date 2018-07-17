using AutoMapper;
using BC.Data.Entity.Authors;
using BC.Data.Repositories;
using BC.Infrastructure.Interfaces.Service;
using BC.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Business.Author
{
    public class AuthorService : IAuthorService
    {
        AuthorsRepository authorRepository = null;
        BooksRepository bookRepository = null;
        public AuthorService(string connectionString)
        {
            this.authorRepository = new AuthorsRepository(connectionString);
            this.bookRepository = new BooksRepository(connectionString);
        }


        public AuthorVM Get(int id)
        {
            AuthorEM entityModel = authorRepository.Get(id);
            return Mapper.Map<AuthorVM>(entityModel);
        }

        public IEnumerable<AuthorVM> GetAll()
        {
            IEnumerable<AuthorEM> entities = authorRepository.GetAll();
            return Mapper.Map<IEnumerable<AuthorVM>>(entities);
        }

        public IEnumerable<AuthorVM> GetAllFiltered()
        {
            throw new NotImplementedException();
        }

        public void Update(AuthorVM viewModel)
        {
            AuthorEM entityModel = Mapper.Map<AuthorEM>(viewModel);
            authorRepository.Update(entityModel);
        }
    }
}
