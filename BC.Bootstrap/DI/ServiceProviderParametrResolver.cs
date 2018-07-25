using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap
{
    public class ServiceProviderParameterResolver : ResolverOverride
    {
        private readonly Queue<InjectionParameterValue> _parameterValues;

        public ServiceProviderParameterResolver(IEnumerable<object> parameterValues)
        {
            _parameterValues = new Queue<InjectionParameterValue>();
            foreach (var parameterValue in parameterValues)
            {
                _parameterValues.Enqueue(InjectionParameterValue.ToParameter(parameterValue));
            }
        }

        public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
        {
            if (_parameterValues.Count < 1)
                return null;

            var value = _parameterValues.Dequeue();
            return value.GetResolverPolicy(dependencyType);
        }
    }
}
