using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Logging
{
    public enum MessageLevel : byte
    {
        /// <summary>
        /// Not classified user message
        /// </summary>
        /// 
        Unspecified = 0,
        /// <summary>
        /// Represents information level message
        /// </summary>
        Information = 1,

        /// <summary>
        /// Represents warning level message
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Represents exception level message that can be handled by application
        /// </summary>
        Error = 4,

        /// <summary>
        /// Represents exception level message that can't be handled by application
        /// </summary>
        Critical = 8
    }
}
