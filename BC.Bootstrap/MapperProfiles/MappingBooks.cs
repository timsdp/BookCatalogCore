using AutoMapper;
using BC.Data.Entity.Books;
using BC.ViewModel.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.MapperProfiles
{
    internal class MappingBooks
    {
        public static void Init(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<BookEM, BookVM>();
            mapperConfig.CreateMap<BookVM, BookEM>();
        }
    }
}
