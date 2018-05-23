using AutoMapper;
using BC.Data.Entity.Authors;
using BC.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<AuthorEM, AuthorVM>();
        }
    }
}
