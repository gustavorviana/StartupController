using System;

namespace StartupControllerApp.Events
{
    public class AppEventArgs : EventArgs
    {
        public Guid Id { get; }

        public AppController App { get; }

        public AppEventArgs(AppController app)
        {
            this.App = app;
            this.Id = app?.Id ?? Guid.Empty;
        }

        public AppEventArgs(Guid id)
        {
            this.Id = id;
        }
    }
}
