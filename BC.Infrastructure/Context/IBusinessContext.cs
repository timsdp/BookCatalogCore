using BC.Infrastructure.DI;
using BC.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Context
{
    public interface IBusinessContext
    {
        IRootContext RootContext { get; set; }
        IServiceProviderFactory Factory { get; }
        IMapperService Mapper { get; }
    }
}
