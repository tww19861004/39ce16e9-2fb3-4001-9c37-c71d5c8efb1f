using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Disposable1
{
    public class DatabaseConnectionWrapper : IDisposable
    {
        private int refCount;

        public DbConnection Connection
        {
            get;
            private set;
        }

        public bool IsDisposed
        {
            get
            {
                return this.refCount == 0;
            }
        }

        public DatabaseConnectionWrapper(DbConnection connection)
        {
            this.Connection = connection;
            this.refCount = 1;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Interlocked.Decrement(ref this.refCount) == 0)
            {
                this.Connection.Close();
                this.Connection.Dispose();
                this.Connection = null;
                GC.SuppressFinalize(this);
            }
        }

        public DatabaseConnectionWrapper AddRef()
        {
            Interlocked.Increment(ref this.refCount);
            return this;
        }
    }
}
