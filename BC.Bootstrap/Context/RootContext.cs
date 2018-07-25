using BC.Infrastructure.Context;
using BC.Infrastructure.DI;
using BC.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.Context
{
    public class RootContext : IRootContext
    {
        private string connectionString;
        private IServiceProviderFactory factory;
        private IMapperService mapper;

        public string ConnectionString => connectionString;

        public IServiceProviderFactory Factory => factory;
        public IMapperService Mapper => mapper;

        public RootContext(string connectionString)
        {
            this.connectionString = connectionString;
            this.factory = UnitySetup.CreateServiceProviderFactory();
            this.mapper = new MapperService();
        }
    }
}
