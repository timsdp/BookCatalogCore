﻿using AutoMapper;
using BC.Data.Entity.Books;
using BC.Data.Repositories;
using BC.Infrastructure.Interfaces.Repository;
using BC.Infrastructure.Interfaces.Service;
using BC.ViewModel.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Business.Book
{
    public class BookService : IBookService
    {
        IAuthorRepository authorRepository = null;
        IBookRepository bookRepository = null;
        public BookService(string connectionString)
        {
            this.authorRepository = new AuthorsRepository(connectionString);
            this.bookRepository = new BookRepository(connectionString);
        }

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
            IEnumerable<BookEM> entities = bookRepository.GetAllFiltered(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            return Mapper.Map<IEnumerable<BookVM>>(entities);
        }

        public IEnumerable<BookVM> GetByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public BookVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int bookId)
        {
            throw new NotImplementedException();
        }

        public void Update(BookVM entity)
        {
            throw new NotImplementedException();
        }
    }
}