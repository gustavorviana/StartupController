using System;
using System.Runtime.InteropServices;

namespace StartupInterface
{
    /**
     * Reference url:
     * https://stackoverflow.com/questions/8342614/refreshing-system-tray-icons-programmatically
     * */
    public static class SystemTray
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        private static IntPtr GetToolbarHandle()
        {
            IntPtr trayWnd = FindWindow("Shell_TrayWnd", null);
            IntPtr trayNotifyWnd = FindWindowEx(trayWnd, IntPtr.Zero, "TrayNotifyWnd", null);
            IntPtr sysPager = FindWindowEx(trayNotifyWnd, IntPtr.Zero, "SysPager", null);
            return FindWindowEx(sysPager, IntPtr.Zero, "ToolbarWindow32", null);
        }

        public static void Refresh()
        {
            IntPtr windowHandle = GetToolbarHandle();

            const uint wmMousemove = 0x0200;
            if (!GetClientRect(windowHandle, out RECT rect))
                return;

            for (var x = 0; x < rect.right; x += 5)
                for (var y = 0; y < rect.bottom; y += 5)
                    SendMessage(windowHandle, wmMousemove, 0, (y << 16) + x);
        }
    }
}
