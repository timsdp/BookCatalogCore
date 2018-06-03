using AutoMapper;
using BC.Data.Entity.Authors;
using BC.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.MapperProfiles
{
    internal class MappingAuthors
    {
        public static void Init(IMapperConfigurationExpression mapperConfig)
        {
            //mapperConfig.CreateMap<AuthorEM, AuthorVM>().ForMember(vm=>vm.FullName, opt => opt.ResolveUsing<Bootstrap.MapperConfig.Resolvers.AuthorFirstNameLastNameResolver>(a=>a.FirstName));
            mapperConfig.CreateMap<AuthorVM, AuthorEM>();
            
        }
    }
}
