using System;
using System.Windows.Forms;

namespace StartupInterface.Settings
{
    public class FormSettingsController : SettingsController<FormSettings>
    {
        private Form Form { get; }

        public FormSettingsController(Form form, string fileName) : base(fileName)
        {
            this.Form = form ?? throw new ArgumentNullException(nameof(form));
        }

        public override void Save()
        {
            if (this.Settings == null)
                this.Settings = new FormSettings();

            if (this.Form.WindowState == FormWindowState.Minimized)
                return;

            this.Settings.IsMaximized = this.Form.WindowState == FormWindowState.Maximized;
            if (!this.Settings.IsMaximized)
            {
                this.Settings.MainWindowLocation = this.Form.Location;
                this.Settings.MainWindowSize = this.Form.Size;
            }

            base.Save();
        }

        public bool ApplyConfig()
        {
            if (!this.HasSettingsFile)
                return false;

            if (this.NeedReload)
                this.Reload();

            var loc = this.Settings.MainWindowLocation;
            var size = this.Settings.MainWindowSize;

            this.Form.StartPosition = FormStartPosition.Manual;

            if (loc.X >= 0 && loc.Y >= 0)
                this.Form.SetDesktopLocation(loc.X, loc.Y);

            if (size.Width >= 0 && size.Width >= 0)
                this.Form.Size = size;
            this.Form.SetBounds(loc.X, loc.Y, size.Width, size.Height);

            var state = this.Settings.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
            if (this.Form.WindowState != state)
                this.Form.WindowState = state;

            return true;
        }
    }
}
