using BC.Infrastructure.Context;
using BC.Infrastructure.DI;
using BC.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Business.Context
{
    public class BusinessContext : IBusinessContext
    {
        #region Constructor
        public BusinessContext(IRootContext context)
        {
            RootContext = context;
        }
        #endregion


        public IServiceProviderFactory Factory => RootContext.Factory;

        public IRootContext RootContext { get; set; }

        public IMapperService Mapper => RootContext.Mapper;
    }
}
