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
            mapperConfig.CreateMap<AuthorEM, AuthorVM>()
            .ForMember(vm => vm.Id, em => em.MapFrom(s => s.AuthorId))
            .ForMember(vm => vm.FullName, em => em.MapFrom(s => $"{s.FirstName} {s.LastName}"))
            .ForMember(vm => vm.Died, em => em.MapFrom(s => s.YearDied))
            .ForMember(vm => vm.Born, em => em.MapFrom(s => s.YearBorn));


            mapperConfig.CreateMap<AuthorVM, AuthorEM>()
                .ForMember(em => em.AuthorId, vm => vm.MapFrom(s => s.Id))
            .ForMember(em => em.YearBorn, vm => vm.MapFrom(s => s.Born))
            .ForMember(em => em.YearDied, vm => vm.MapFrom(s => s.Died));
        }
    }
}
