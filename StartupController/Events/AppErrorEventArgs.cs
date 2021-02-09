using System;

namespace StartupControllerApp.Events
{
    public class AppErrorEventArgs : AppEventArgs
    {
        public Exception Exception { get; }
        public bool IsFatal { get; }

        public AppErrorEventArgs(AppController app, Exception exception, bool isFatal) : base(app)
        {
            this.Exception = exception;
            this.IsFatal = isFatal;
        }

        public AppErrorEventArgs(Guid id, Exception exception, bool isFatal) : base(id)
        {
            this.Exception = exception;
            this.IsFatal = isFatal;
        }
    }
}
