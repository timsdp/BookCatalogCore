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
            List<BookEM> entries = new List<BookEM>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                entries = db.Query<BookEM>("SELECT * FROM [Books]").ToList();
            }
            return entries;
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
