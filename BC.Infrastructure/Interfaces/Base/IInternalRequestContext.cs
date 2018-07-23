using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Base
{
    public interface IInternalRequestContext : IDisposable
    {
        /// <summary>
        /// Gets instance of ORM entity
        /// </summary>
        IDataContext DataContext { get; }

        /// <summary>
        /// Gets an instance of current logger
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Gets an instance of domain model /repository factory
        /// </summary>
        IServiceProviderFactory Factory { get; }
    }
}
