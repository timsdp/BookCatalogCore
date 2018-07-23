using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Interfaces.Base
{
    /// <summary>
    /// Represents object provides access to current operation context
    /// </summary>
    public interface IRequestContext : IInternalRequestContext, IDisposable
    {
        /// <summary>
        /// Gets an identified of current context
        /// </summary>
        string Id { get; }
    }
}
