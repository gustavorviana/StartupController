namespace StartupControllerApp.Events
{
    /// <summary>
    /// This argument is normally used by the StartupController class to allow the user to cancel the termination of an application due to a work timeout.
    /// </summary>
    public class AppEndWorkEventArgs : AppEventArgs
    {
        /// <summary>
        /// Signals whether the application shutdown should be cancele.
        /// </summary>
        public bool IsCancelled { get; set; }

        public AppEndWorkEventArgs(AppController app, bool cancelled) : base(app)
        {
            this.IsCancelled = cancelled;
        }
    }
}
