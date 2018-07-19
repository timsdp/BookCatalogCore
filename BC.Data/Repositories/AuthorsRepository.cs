using BC.Data.Entity.Authors;
using BC.Infrastructure.Interfaces.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BC.Data.Repositories
{
    public class AuthorsRepository : IAuthorRepository
    {
        string connectionString = string.Empty;

        public AuthorsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<AuthorEM> GetAll()
        {
            List<AuthorEM> authors = new List<AuthorEM>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                authors = db.Query<AuthorEM>("SELECT * FROM Authors").ToList();
            }
            return authors;
        }

        public AuthorEM Get(int id)
        {
            AuthorEM AuthorEM = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                AuthorEM = db.Query<AuthorEM>("SELECT * FROM Authors WHERE AuthorId = @id", new { id }).FirstOrDefault();
            }
            return AuthorEM;
        }

        public void Create(AuthorEM AuthorEM)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Authors (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? id = db.Query<int>(sqlQuery, AuthorEM).FirstOrDefault();
                AuthorEM.AuthorId = id;
            }
        }

        public void Update(AuthorEM entity)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = @"UPDATE [dbo].[Authors]
                       SET [FirstName] = @FirstName
                          ,[LastName] = @LastName
                          ,[YearBorn] = @YearBorn
                          ,[YearDied] = @YearDied
                          ,[Country] = @Country
                          ,[Quote] = @Quote
                          ,[Rating] = @Rating
                          ,[ExtraInfo] = @ExtraInfo
                          ,[WikiUrl]=@WikiUrl
                     WHERE AuthorId=@AuthorId";
                db.Execute(sqlQuery, entity);
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = @"
                    DELETE FROM BooksAuthors WHERE AuthorId = @id
                    DELETE FROM Authors WHERE AuthorId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }


        public IEnumerable<AuthorEM> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorEM> Get(Func<AuthorEM, bool> predicate)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<AuthorEM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<AuthorEM> retVal = new List<AuthorEM>();
            string whereClause = string.Empty;
            if (!string.IsNullOrWhiteSpace(searchBy))
            {
                whereClause = "WHERE ";
                string[] searchWords = searchBy.Split(" ");
                for (int i = 0; i < searchWords.Length; i++)
                {
                    whereClause += $"(A.FirstName LIKE '%{searchWords[i]}%' OR A.LastName LIKE '%{searchWords[i]}%')";
                    if (i != searchWords.Length - 1) whereClause += " AND ";
                }
            }

            string orderColumn = string.Empty;
            switch (sortBy.ToLower())
            {
                case "rating":
                    orderColumn = "A.Rating";
                    break;
                case "country":
                    orderColumn = "A.Country";
                    break;
                case "born":
                    orderColumn = "A.YearBorn";
                    break;
                case "died":
                    orderColumn = "A.YearDied";
                    break;
                case "fullname":
                default:
                    orderColumn = "A.LastName";
                    break;
            }

            string orderDirection = sortDir ? "ASC" : "DESC";



            string query = $@"SELECT A.AuthorId, A.FirstName ,A.LastName, A.YearBorn, A.YearDied, A.Country, A.Rating
                            FROM [Authors] AS A
                            {whereClause}
                            ORDER BY {orderColumn} {orderDirection}
                            OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY
";

            using (IDbConnection db = new SqlConnection(connectionString))
            {

                retVal = db.Query<AuthorEM>(query).ToList();

                totalResultsCount = db.QuerySingle<int>("SELECT COUNT(*) FROM [Authors] AS A");
                filteredResultsCount = db.QuerySingle<int>($"SELECT COUNT(*) FROM [Authors]  AS A {whereClause}");
            }

            return retVal;
        }

        public IEnumerable<AuthorEM> GetByBook(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
