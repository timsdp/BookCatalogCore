using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Data
{
    /// <summary>
    /// Defines a mechanism for retrieving a service object; that is, an object that provides custom support to Repository objects.
    /// </summary>
    public interface IRepositoryProvider : IServiceProvider
    {
        /// <summary>
        /// Gets the repository object of the specified type.
        /// </summary>
        /// <typeparam name="TService">An object that specifies the type of service object to get</typeparam>
        /// <param name="constructParams">List of construction arguments</param>
        /// <returns>A service object of type serviceType.-or- null if there is no service object	of type serviceType.</returns>
        TService GetService<TService>(params object[] constructParams);
    }
}
