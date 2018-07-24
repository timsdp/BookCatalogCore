using BC.Infrastructure.Interfaces.Base;
using BC.Infrastructure.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces
{
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Gets a database connection string
        /// </summary>
        string DbConnection { get; }

        /// <summary>
        /// Gets an instance of current logger
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Gets an instance of repository container
        /// </summary>
        IRepositoryProvider RepositoryContainer { get; }

        /// <summary>
        ///  Provides UoW mechanism
        /// </summary>
        /// <param name="context"></param>
        /// <param name="forceSuppresTransaction"></param>
        /// <param name="action">Handler that should be routed by transaction</param>
        void TransactedFlow(Action<IRequestContext> action, IRequestContext context, bool forceSuppresTransaction);

        /// <summary>
        /// Saves all unsaved data into context
        /// </summary>
        /// <remarks>
        /// <see cref="TransactedFlow"/> calls this method before complete
        /// </remarks>
        void Flush();
    }
}
