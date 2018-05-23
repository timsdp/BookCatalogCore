using AutoMapper;
using BC.Data.Entity.Authors;
using BC.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap
{
    public class MapperConfig
    {
        public static void GonfigureMappings()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<AuthorEM, AuthorVM>();
                config.CreateMap<AuthorVM, AuthorEM>();
            }
            );
        }
    }
}
