using Dommel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BC.Data.Resolvers
{
    public class TableNameResolver : DommelMapper.ITableNameResolver
    {
        public string ResolveTableName(Type type)
        {
            var tableAttribute = Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            return tableAttribute != null ? ((TableAttribute)tableAttribute).Name : null;
        }
    }
}
