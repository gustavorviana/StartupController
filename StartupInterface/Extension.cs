using StartupControllerApp;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StartupInterface
{
    public static class Extension
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(int hWnd);

        public static void MoveToForeground(this Form form)
        {
            if (!form.Visible)
                form.Visible = true;

            if (form.WindowState == FormWindowState.Minimized)
                form.WindowState = FormWindowState.Normal;

            _ = SetForegroundWindow(form.Handle.ToInt32());
        }

        public static string GetName(this AppState state)
        {
            switch (state)
            {
                case AppState.Closed:
                    return "Fechado";
                case AppState.Killed:
                    return "Fechado";
                case AppState.Launched:
                    return "Aberto";
                case AppState.Attached:
                    return "Aberto";
                case AppState.Errored:
                    return "Erro";
                default:
                    return "Desconhecido";
            }
        }
    }
}
