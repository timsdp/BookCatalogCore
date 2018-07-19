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


    }
}
