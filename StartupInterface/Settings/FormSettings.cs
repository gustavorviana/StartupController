using System.Drawing;

namespace StartupInterface.Settings
{
    public class FormSettings
    {
        public bool IsMaximized { get; set; }

        public Point MainWindowLocation { get; set; }

        public Size MainWindowSize { get; set; }
    }
}
