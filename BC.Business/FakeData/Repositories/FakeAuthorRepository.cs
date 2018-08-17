using BC.Data.Entity.Authors;
using BC.Infrastructure.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BC.Business.FakeData.Repositories
{
    public class FakeAuthorRepository : FakeRepositoryBase<int, AuthorEM>, IAuthorRepository
    {
        public IEnumerable<AuthorEM> GetAllFiltered(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> GetAutocomplete(string query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorEM> GetByBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public ICollection<AuthorEM> GetByFullNameAndYear(string firstName, string lastName, int bornYear)
        {
            return Data.Where(f =>
            f.FirstName.ToLower() == firstName.ToLower()
            && f.LastName.ToLower() == lastName.ToLower()
            && f.YearBorn == bornYear
            ).ToList();
        }
    }
}
