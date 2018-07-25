using BC.Infrastructure.DI;
using BC.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Context
{
    public interface IRootContext
    {
        string ConnectionString { get; }
        IServiceProviderFactory Factory { get; }
        IMapperService Mapper { get; }
    }
}
