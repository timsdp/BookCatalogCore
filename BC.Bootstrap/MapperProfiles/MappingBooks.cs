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
            mapperConfig.CreateMap<BookEM, BookVM>()
                .ForMember(vm => vm.Id, em => em.MapFrom(s => s.BookId))
                .ForMember(vm => vm.Name, em => em.MapFrom(s => s.Name))
                .ForMember(vm => vm.Published, em => em.MapFrom(s => s.DatePublished))
                .ForMember(vm => vm.Pages, em => em.MapFrom(s => s.PagesCount))
                .ForMember(vm => vm.Authors, em => em.MapFrom(s => s.Authors))
                .ForMember(vm => vm.Rating, em => em.MapFrom(s => s.Rating));
            ;

            mapperConfig.CreateMap<BookVM, BookEM>();
        }
    }
}
