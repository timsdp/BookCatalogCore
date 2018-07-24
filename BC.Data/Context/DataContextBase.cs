using BC.Infrastructure.Interfaces;
using BC.Infrastructure.Interfaces.Base;
using BC.Infrastructure.Interfaces.Data;
using BC.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BC.Data.Context
{
    public abstract class DataContextBase : IDataContext
    {
        public DataContextBase(string conString, bool escalateException)
        {
            this.escalateException = escalateException;

            if (escalateException)
            {
                this.escalationLevel = MessageLevel.Warning;
            }

            this.DbConnection = conString;
        }

        #region IDataContext Members

        /// <summary>
        /// Gets a database connection string
        /// </summary>
        public string DbConnection { get; protected set; }

        /// <summary>
        /// Gets instance of Logger
        /// </summary>
        public ILogger Logger { get { return null; } }

        /// <summary>
        /// Gets or sets instance of repository container
        /// </summary>
        public IRepositoryProvider RepositoryContainer { get; set; }

        /// <summary>
        ///  Provides UoW mechanism
        /// </summary>
        /// <param name="context"></param>
        /// <param name="forceSuppresTransaction"></param>
        /// <param name="action">Handler that should be routed by transaction</param>
        /// <returns></returns>
        public virtual void TransactedFlow(Action<IRequestContext> action, IRequestContext context, bool forceSuppresTransaction)
        {
            try
            {
                var scopeOption = forceSuppresTransaction ? TransactionScopeOption.Suppress : TransactionScopeOption.Required;
                using (var tran = new TransactionScope(scopeOption))
                {
                    action(context);
                    this.Flush();
                    tran.Complete();
                }
            }
            catch (Exception err)
            {
                //this.loggerSelector.WriteMessage(err.Message, this.escalationLevel, err);
                if (this.escalateException)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves all unsaved data into context
        /// </summary>
        /// <remarks>
        /// <see cref="TransactedFlow"/> calls this method before complete
        /// </remarks>
        public abstract void Flush();
        #endregion

        #region IDisposable Members

        public abstract void Dispose();

        #endregion

        private readonly bool escalateException = false;
        private readonly MessageLevel escalationLevel = MessageLevel.Error;
        //private readonly LoggerSelector loggerSelector = LoggerSelector.Instance;
    }
}
