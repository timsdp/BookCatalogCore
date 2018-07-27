using BC.Data.Entity.Authors;
using BC.Data.Entity.Books;
using BC.Infrastructure.Context;
using BC.Infrastructure.Interfaces.Repository;
using BC.ViewModel.Books;
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
    public class BookRepository : BaseRepository<int,BookEM>, IBookRepository
    {
        public BookRepository(IRootContext context) : base(context) { }

        // Used sources:
        // http://dapper-tutorial.net/result-multi-mapping
        // https://stackoverflow.com/questions/20492071/simple-inner-join-result-with-dapper
        public IEnumerable<BookEM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
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
                            LEFT JOIN [BooksAuthors] AS BA ON BA.BookId = B.BookId
                            LEFT JOIN [Authors] AS A ON A.AuthorId = BA.AuthorId
                            {whereClause}
                            ORDER BY {orderColumn} {orderDirection}
                            OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY
";

            var bookDictionary = new Dictionary<int, BookEM>();
            using (IDbConnection db = new SqlConnection(Context.ConnectionString))
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
                        if (author!=null)
                        {
                            bookEntry.Authors.Add(author);
                        }
                        

                        Debug.WriteLine("BookEntry:" + bookEntry);
                        Debug.WriteLine("----------------------------------");
                        return bookEntry;
                    }, splitOn:"AuthorId")
                    .Distinct()
                    .ToList();

                totalResultsCount = db.QuerySingle<int>("SELECT COUNT(*) FROM [Books] AS B");
                filteredResultsCount = db.QuerySingle<int>($"SELECT COUNT(*) FROM [Books]  AS B {whereClause}");
            }

            return retVal;
        }

        public BookEM Get(int id)
        {
            BookEM BookEM = null;
            using (IDbConnection db = new SqlConnection(Context.ConnectionString))
            {
                BookEM = db.Query<BookEM>("SELECT * FROM [Books] WHERE BookId = @id", new { id }).FirstOrDefault();
            }
            return BookEM;
        }

        public override void Create(BookEM BookEM)
        {
            using (IDbConnection db = new SqlConnection(Context.ConnectionString))
            {
                var sqlQuery = @"INSERT INTO [Books] (Name, DatePublished, PagesCount, Rating) 
                                 VALUES(@Name, @DatePublished,@PagesCount, @Rating); 
                                 SELECT CAST(SCOPE_IDENTITY() as int)";
                int? id = db.Query<int>(sqlQuery, BookEM).FirstOrDefault();
                BookEM.BookId = id.Value;
            }
        }

        public override void Update(BookEM BookEM)
        {
            using (IDbConnection db = new SqlConnection(Context.ConnectionString))
            {
                var sqlQuery = @"UPDATE [Books] 
                                SET Name = @Name
                                , DatePublished = @DatePublished
                                ,PagesCount = @PagesCount
                                , Rating = @Rating 
                                WHERE BookId = @BookId";
                db.Execute(sqlQuery, BookEM);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(Context.ConnectionString))
            {
                var sqlQuery = "DELETE FROM [Books] WHERE BookId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }


        public IEnumerable<BookEM> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookEM> Get(Func<BookEM, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            using (IDbConnection db = new SqlConnection(Context.ConnectionString))
            {
                var sqlQuery = @"
                    DELETE FROM BooksAuthors WHERE BookId = @id
                    DELETE FROM Books WHERE BookId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public IEnumerable<BookEM> GetByAuthor(int id)
        {
            IEnumerable<BookEM> retVal = null;
            using (IDbConnection db = new SqlConnection(Context.ConnectionString))
            {
                retVal = db.Query<BookEM>("SELECT B.* FROM [Books] AS B INNER JOIN [BooksAuthors] AS AB ON AB.BookId=B.BookId WHERE AB.AuthorId = @id", new { id }).ToList();
            }
            return retVal;
        }

        public void Dispose()
        {
           
        }
    }
}
