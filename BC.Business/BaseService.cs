using BC.Business.Context;
using BC.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Business
{
    public class BaseService : IDisposable
    {
        protected IBusinessContext Context { get; set; }

        #region Constructors
        public BaseService(IRootContext context)
        {
            Context = new BusinessContext(context);
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
