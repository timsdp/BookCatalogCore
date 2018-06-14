using BC.Data.Entity.Authors;
using BC.Data.Entity.Books;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BC.Data.Repositories
{
    public class BooksRepository
    {
        string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=BookCatalog;Persist Security Info=True;User ID=sa;Password=Pa$$w0rd;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";
        public List<BookEM> GetAll()
        {
            List<BookEM> retVal = new List<BookEM>();
            string query = @"SELECT B.*, A.AuthorId, A.FirstName ,A.LastName
                            FROM [Books] AS B
                            INNER JOIN [BooksAuthors] AS BA ON BA.BookId = B.BookId
                            INNER JOIN [Authors] AS A ON A.AuthorId = BA.AuthorId";
            var bookDictionary = new Dictionary<int, BookEM>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var entries = db.Query<BookEM,AuthorEM,BookEM>(
                    query
                    ,(book,author) =>
                    {
                        BookEM bookEntry=null;
                        if (bookDictionary.TryGetValue(book.BookId, out book))
                        {
                            bookEntry = book;
                            bookEntry.Authors = new List<AuthorEM>();
                            bookDictionary.Add(bookEntry.BookId, bookEntry);
                        }
                        bookEntry.Authors.Add(author);
                        return bookEntry;
                    }, splitOn: "BookId")
                    .Distinct()
                    .ToList();
            }
            return retVal;
        }

        public BookEM Get(int id)
        {

            BookEM BookEM = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                BookEM = db.Query<BookEM>("SELECT * FROM [Books] WHERE BookId = @id", new { id }).FirstOrDefault();
            }
            return BookEM;
        }

        public BookEM Create(BookEM BookEM)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO [Books] (Name, DatePublished, PagesCount, Rating) VALUES(@Name, @DatePublished,@PagesCount, @Rating); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? id = db.Query<int>(sqlQuery, BookEM).FirstOrDefault();
                BookEM.BookId = id.Value;
            }
            return BookEM;
        }

        public void Update(BookEM BookEM)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE [Books] SET Name = @Name, DatePublished = @DatePublished,PagesCount = @PagesCount, Rating = @Rating WHERE BookId = @Id";
                db.Execute(sqlQuery, BookEM);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM [Books] WHERE BookId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
