using BC.Data.Resolvers;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dapper.FluentMap.Mapping;
using Dommel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Data.Context
{
    public class DapperContext : DataContextBase
    {
        public DapperContext(string conString) : base(conString, true)
        {
            // Initialization of the entity mappings.
            this.InitializeEntityMappings();
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public override void Flush()
        {
        }

        private void InitializeEntityMappings()
        {
            FluentMapper.Initialize(config =>
            {
                //// TODO: add entity mapping here
                //AddMapping(config, new ContactMap());
                //AddMapping(config, new PersonDiagnosisMap());
                //AddMapping(config, new ProviderDetailMap());

                config.ForDommel();
                DommelMapper.SetKeyPropertyResolver(new PrimaryKeyResolver());
                DommelMapper.SetTableNameResolver(new TableNameResolver());
            });
        }

        private void AddMapping<TEntity>(Dapper.FluentMap.Configuration.FluentMapConfiguration config, IEntityMap<TEntity> map) where TEntity : class
        {
            if (FluentMapper.EntityMaps.TryAdd(map.GetType(), map))
            {
                config.AddMap(map);
            }
        }
    }
}
