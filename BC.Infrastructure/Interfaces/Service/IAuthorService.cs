using BC.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Service
{
    public interface IAuthorService
    {
        AuthorVM Get(int id);
        IEnumerable<AuthorVM> GetAll();
        IEnumerable<AuthorVM> GetAllFiltered();
        void Update(AuthorVM entity);
    }
}
