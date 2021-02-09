namespace StartupControllerApp.Events
{
    public class AppStateEventArgs : AppEventArgs
    {
        public AppState LastState { get; }

        public AppState CurrentState => this.App.State;

        public AppStateEventArgs(AppController app, AppState lastState) : base(app)
        {
            this.LastState = lastState;
        }
    }
}