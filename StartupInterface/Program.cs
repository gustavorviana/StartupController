using SingleSharpInstance;
using System;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace StartupInterface
{
    class Program
    {
        private static SingleSharp Context = null;
        private static Forms.MainForm MainForm;
        public const string AppName = "Controlador de Inicializações Customizadas";
        public static string AppDataDir
        {
            get
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(appData, "StartupCtl");
            }
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Context = SingleSharp.From<Program>(security: CreatePipeSecurity());

                Context.OnReceiveActivation += Single_OnReceiveActivation;
                Context.OnException += OnThreadException;

                Context.SendActivation(Environment.GetCommandLineArgs());
            }
            catch (Exception ex)
            {
                RunFatalAction(ex);
            }
        }

        private static void Single_OnReceiveActivation(object sender, SingleSharpInstance.Events.ActivationEventArgs e)
        {
            if (!e.IsFirstActivation)
            {
                MainForm.Invoke(new Action(() => MainForm.MoveToForeground()));
                return;
            }

            Application.ThreadException += OnThreadException;
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainForm = new Forms.MainForm());
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            RunFatalAction(e.Exception);
        }

        public static DirectoryInfo GetAppDataDir(params string[] subPath)
        {
            var path = AppDataDir + Path.DirectorySeparatorChar + Path.Combine(subPath);
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
                dir.Create();

            return dir;
        }

        private static PipeSecurity CreatePipeSecurity()
        {
            PipeSecurity pipeSecurity = new PipeSecurity();

            var id = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);

            // Allow Everyone read and write access to the pipe. 
            pipeSecurity.SetAccessRule(new PipeAccessRule(id, PipeAccessRights.ReadWrite, AccessControlType.Allow));

            return pipeSecurity;
        }

        internal static void RunFatalAction(Exception ex)
        {
            string message = "Ocorreu um erro inesperado e o aplicativo teve que ser encerrado.";

#if DEBUG
            message += "\n\nMensagem de erro:\n" + ex?.Message;
#endif


            ShowNewThreadMessageBox(message, MessageBoxIcon.Stop);

            Application.ExitThread();

            var dir = GetAppDataDir("dumps");
            var fileName = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".dmp";
            var path = Path.Combine(dir.FullName, fileName);

            MiniDump.Write(path, MiniDump.Option.Normal, true);

            try
            {
                Context.Shutdown();
            }
            catch (Exception)
            { }
        }

        internal static void ShowNewThreadMessageBox(string message, MessageBoxIcon icon)
        {
            new Thread(() => MessageBox.Show(message, AppName, MessageBoxButtons.OK, icon)).Start();
        }
    }
}
