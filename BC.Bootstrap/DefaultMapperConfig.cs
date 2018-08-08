using AutoMapper;
using BC.Bootstrap.MapperProfiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap
{
    public class DefaultMapperConfig
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorsProfile>();
            });
            return config;
        }
    }
}
