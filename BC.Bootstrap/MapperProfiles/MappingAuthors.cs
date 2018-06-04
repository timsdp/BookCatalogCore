using AutoMapper;
using BC.Bootstrap.MapperSettings.Resolvers;
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
            mapperConfig.CreateMap<AuthorEM, AuthorVM>().ForMember(vm=>vm.FullName, opt => opt.MapFrom(s=>$"{s.FirstName} {s.LastName}"));
            mapperConfig.CreateMap<AuthorVM, AuthorEM>();
            
        }
    }
}
