using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BC.Data.Entity.Authors;
using BC.ViewModel;

namespace BC.Bootstrap.Mapper.Resolvers
{
    public class AuthorFirstNameLastNameResolver : IValueResolver<AuthorEM, AuthorVM, string>
    {
        public string Resolve(AuthorEM source, AuthorVM destination, string member, ResolutionContext context)
        {
            return source.FirstName + source.LastName;
        }

    }
}
    