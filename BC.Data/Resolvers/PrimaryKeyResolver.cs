using Dapper.Contrib.Extensions;
using Dommel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BC.Data.Resolvers
{
    public class PrimaryKeyResolver : DommelMapper.IKeyPropertyResolver
    {
        public PropertyInfo ResolveKeyProperty(Type type)
        {
            return type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)) || Attribute.IsDefined(prop, typeof(ExplicitKeyAttribute))).FirstOrDefault();
        }

        public PropertyInfo ResolveKeyProperty(Type type, out bool isIdentity)
        {
            isIdentity = true;
            var property = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).FirstOrDefault();
            if (property == null)
            {
                isIdentity = false;
                property = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ExplicitKeyAttribute))).FirstOrDefault();
            }
            return property;
        }
    }
}
