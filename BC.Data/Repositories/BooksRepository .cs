using BC.Data.Entity.Authors;
using BC.Data.Entity.Books;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BC.Data.Repositories
{
    public class BooksRepository
    {
        string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=BookCatalog;Persist Security Info=True;User ID=sa;Password=Pa$$w0rd;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";


        // Used sources:
        // http://dapper-tutorial.net/result-multi-mapping
        // https://stackoverflow.com/questions/20492071/simple-inner-join-result-with-dapper
        public List<BookEM> GetAll(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<BookEM> retVal = new List<BookEM>();
            string whereClause = string.Empty;
            if (!string.IsNullOrWhiteSpace(searchBy))
            {
                whereClause = "WHERE ";
                string[] searchWords = searchBy.Split(" ");
                for (int i = 0; i < searchWords.Length; i++)
                {
                    whereClause += $"B.Name LIKE '%{searchWords[i]}%'";
                    if (i != searchWords.Length - 1) whereClause += " AND ";
                }
            }

            string orderColumn = string.Empty;
            switch (sortBy.ToLower())
            {
                case "pages":
                    orderColumn = "B.PagesCount";
                    break;
                case "rating":
                    orderColumn = "B.Rating";
                    break;
                case "date":
                    orderColumn = "B.DatePublished";
                    break;
                case "name":    
                default:
                    orderColumn = "B.Name";
                    break;
            }

            string orderDirection = sortDir ? "ASC" : "DESC";



            string query = $@"SELECT B.*, A.AuthorId, A.FirstName ,A.LastName
                            FROM [Books] AS B
                            INNER JOIN [BooksAuthors] AS BA ON BA.BookId = B.BookId
                            INNER JOIN [Authors] AS A ON A.AuthorId = BA.AuthorId
                            {whereClause}
                            ORDER BY {orderColumn} {orderDirection}
                            OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY
";

            var bookDictionary = new Dictionary<int, BookEM>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                
                retVal = db.Query<BookEM,AuthorEM,BookEM>(
                    query
                    ,(book,author) =>
                    {
                        Debug.WriteLine("Book:" + book);
                        Debug.WriteLine("Author:" + author);

                        BookEM bookEntry;
                        if (!bookDictionary.TryGetValue(book.BookId, out bookEntry))
                        {
                            bookDictionary.Add(book.BookId, bookEntry=book);
                        }
                        if (bookEntry.Authors==null)
                        {
                            bookEntry.Authors = new List<AuthorEM>();
                        }
                        bookEntry.Authors.Add(author);

                        Debug.WriteLine("BookEntry:" + bookEntry);
                        Debug.WriteLine("----------------------------------");
                        return bookEntry;
                    }, splitOn:"AuthorId")
                    .Distinct()
                    .ToList();

                totalResultsCount = db.QuerySingle<int>("SELECT COUNT(*) FROM [Books]");
                filteredResultsCount = retVal.Count();
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
