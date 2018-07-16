using BC.Data.Entity.Authors;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BC.Data.Repositories
{
    public class AuthorsRepository
    {
        string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=BookCatalog;Persist Security Info=True;User ID=sa;Password=Pa$$w0rd;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";
        public List<AuthorEM> GetAuthors()
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

        public AuthorEM Create(AuthorEM AuthorEM)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Authors (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? id = db.Query<int>(sqlQuery, AuthorEM).FirstOrDefault();
                AuthorEM.AuthorId = id;
            }
            return AuthorEM;
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
                     WHERE AuthorId=@AuthorId";
                db.Execute(sqlQuery, entity);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Authors WHERE AuthorId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
