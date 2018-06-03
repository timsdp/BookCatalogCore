using AutoMapper;
using BC.Bootstrap.MapperProfiles;
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
                MappingAuthors.Init(config);
            }
            );
        }
    }
}
