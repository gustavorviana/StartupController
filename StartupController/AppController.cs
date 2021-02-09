using StartupControllerApp.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StartupControllerApp
{
    public class AppController : RuntimeController
    {
        #region Events

        public event EventHandler<AppStateEventArgs> OnStateChanged;

        #endregion

        #region Linked Fields

        private AppSettings _setting;
        /// <summary>
        /// App settings.
        /// </summary>
        public AppSettings Settings
        {
            get => this._setting;
            set => this._setting = value ?? throw new ArgumentNullException(nameof(value));
        }

        private Process _process;
        /// <summary>
        /// App-linked process.
        /// </summary>
        public Process Process => this._process;

        private AppState _state;
        /// <summary>
        /// Current state of the process.
        /// </summary>
        public AppState State => _state;

        #endregion

        #region Fields

        /// <summary>
        /// App id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// App name
        /// </summary>
        public string Name => Settings.Name;

        /// <summary>
        /// Signals if the app process is running.
        /// </summary>
        public bool IsRunning => this.Process != null && (this.State == AppState.Attached || this.State == AppState.Launched);

        /// <summary>
        /// If the app is exited, returns the exit code, otherwise returns null.
        /// </summary>
        public int? ExitCode => (this.Process?.HasExited ?? false) ? this.Process.ExitCode : null;

        /// <summary>
        /// Signals whether the app is enabled for StartupController to manage it.
        /// </summary>
        public bool Enabled => this.Settings.Enabled;

        /// <summary>
        /// Signals if the app is at work time
        /// </summary>
        public bool InWorkingTime => this.IsAllowedAt(DateTime.Now);

        /// <summary>
        /// Signals if the app was started by the user.
        /// </summary>
        public bool StartedByUser { get; protected set; }

        protected override WorkTime WorkTime => this.Settings.WorkTime;

        protected override DayOfWeek[] WeekDays => this.Settings.WeekDays;

        protected override DateTime[] IgnoredDates => this.Settings.IgnoredDates;

        /// <summary>
        /// Signals whether the app has started outside of working time
        /// </summary>
        public bool StartedOutOfWorktime
        {
            get
            {
                //Se StartTime for null
                //Retorna false
                if (this.StartTime is not DateTime start)
                    return false;

                //Se start for menor que a data de trabalho do dia atual
                //retorna true
                if (start < this.GetRuntimeOf(DateTime.Now, true))
                    return true;

                //Se WorkTime.End == null
                //Retorna false
                if (this.WorkTime.End == null)
                    return false;

                //Se start for maior que a hora de fim de trabalho, retorna true
                return start > DateTime.Now.SetTimeOfDay(this.WorkTime.End.Value);
            }
        }

        #endregion

        /// <summary>
        /// Creates a new class of AppController
        /// </summary>
        /// <param name="id">App id</param>
        /// <param name="settings">Not null app settings.</param>
        public AppController(Guid id, AppSettings settings)
        {
            this.Settings = settings;
            this.Id = id;
        }

        /// <summary>
        /// If the app isn't running, reset sand states and unlink your process.
        /// </summary>
        public void ResetState()
        {
            //Se o app estiver rodando, lança um erro
            if (this.IsRunning)
                throw new InvalidOperationException("It is not possible to reset the status of a running program");

            //Define os campos do processo para os valores padrões
            this._state = AppState.Closed;
            this.StartedByUser = false;
            this.StartTime = null;
            this._process = null;
            this.ResetSignals();
        }

        #region App Process

        /// <summary>
        /// Kills the application process.
        /// </summary>
        public void Kill()
        {
            //Se o aplicativo não estiver rodando, lança um erro
            if (!this.IsRunning)
                throw new InvalidCastException("It is not possible to end a process that is not working.");

            //Desativa o evento Exited do processo
            this.Process.Exited -= this.AppProcess_Exited;

            //Mata o processo
            this.Process.Kill();
            //Sinaliza que o processo foi encerrado com o estado Kill
            this.SendClose(true);
        }

        /// <summary>
        /// Search for similar processes with the information entered in the settings.
        /// </summary>
        /// <param name="byAppExecPath">Signals whether one or more processes with the path to the same executable inserted in the settings should be returned.</param>
        /// <returns></returns>
        public Process[] FindSimilarProcesses(bool byAppExecPath = false)
        {
            //Se o processo deve ser retornado pelo caminho do executável
            if (byAppExecPath && !string.IsNullOrEmpty(this.Settings.Program))
            {
                List<Process> processes = new List<Process>();
                try
                {
                    //Recupera todos os processos abertos no sistema
                    foreach (var proc in Process.GetProcesses())
                    {
                        //Verifica se o nome do arquivo do módulo principal é igual a Settings.Program
                        //Se sim, adiciona na lista
                        if (proc.MainModule?.FileName == this.Settings.Program)
                            processes.Add(proc);
                    }
                    return processes.ToArray();
                }
                catch { }
            }

            //Recupera os processos com nome o nome igual a this.Settings?.ProcessName
            return Process.GetProcessesByName(this.Settings?.ProcessName ?? "");
        }

        /// <summary>
        /// Tries to attach the app to a process similar to the data entered in the settings.
        /// </summary>
        /// <param name="byUser">Signals whether the user is trying to attach the process.</param>
        /// <param name="byAppExecPath">Signals whether similar ones should have the same executable path in the settings.</param>
        /// <returns>True if it managed to attach, false if no process was found.</returns>
        public bool TryAttach(bool byUser = false, bool byAppExecPath = false)
        {
            //Recupera os processos similares ao do app
            //Se nenhum processo for encontrado, retorna false
            if (FindSimilarProcesses(byAppExecPath).FirstOrDefault() is not Process proc)
                return false;

            //Sinaliza uma inicialização com o estado Attached
            this.SendStart(proc, true, byUser);
            return true;
        }

        /// <summary>
        /// Try to close the process without killing, but if it is not possible to close gracefully, the process will be killed.
        /// </summary>
        public void Close()
        {
            //Se o app não estiver sido iniciado, lança um erro
            if (!this.IsRunning)
                throw new InvalidCastException("It is not possible to end a process that is not working.");

            //Recupera a janela principal do processo
            var mainWindow = this.Process.MainWindowHandle;
            //Se houver uma janela
            if (mainWindow != IntPtr.Zero)
            {
                //Fecha a janela principal do processo
                this.Process.CloseMainWindow();
                //Aguarda 15 segundos até o processo sinalizar que o processo foi encerrado
                //Se o resultado de WaitForExit for true, ignora o próximo passo
                if (this.Process.WaitForExit(15000))
                    return;
            }

            //Mata o processo
            this.Kill();
        }

        /// <summary>
        /// Starts the app.
        /// </summary>
        /// <param name="byUser">Signals whether the user is trying to launch the app.</param>
        public void Start(bool byUser = false)
        {
            //Lança um erro se o aplicativo estiver rodando
            if (this.IsRunning)
                throw new Exception("The application has already started.");

            //Tenta encontrar e vincular o processo correspondente ao app
            if (this.TryAttach(byUser, true))
                return;

            //Recupera os argumentos de inicialização
            var startInfo = this.OnCreateInfo();

            Process _process;
            try
            {
                _process = Process.Start(startInfo);
            }
            catch (SystemException)
            {
                this.UpdateState(AppState.Errored);
                throw;
            }

            //Adiciona um novo processo ao app, sinalizando que foi um lançamento
            this.SendStart(_process, false, byUser);
        }

        protected virtual ProcessStartInfo OnCreateInfo()
        {
            //Recupera os argumentos de inicialização definidos no arquivos de configuração
            string args = this.Settings.Args == null ? "" : string.Join(' ', this.Settings.Args);
            //Cria um ProcessStartInfo com os argumentos de inicialização e program da configuração do app
            var startInfo = new ProcessStartInfo(this.Settings.Program, args);

            //Se um diretório de trabalho for definido nas configurações, passe o diretório para o startInfo
            if (this.Settings.WorkingDirectory != null)
                startInfo.WorkingDirectory = this.Settings.WorkingDirectory;

            return startInfo;
        }

        private void SendStart(Process process, bool isAttach, bool byUser)
        {
            //Sinaliza o começo do trabalho do aplicativo
            this.SignalStartWork();

            //Atualiza o processo do aplicativo
            this._process = process;

            //Ativa o callback Exited de Process
            this._process.EnableRaisingEvents = true;
            this.Process.Exited += AppProcess_Exited;
            var state = isAttach ? AppState.Attached : AppState.Launched;

            //Processa o horário de inicialização do processo
            this.ProcessStartTime(state);

            //Sinaliza se o processo foi inicializado pelo usuário
            this.StartedByUser = byUser;

            //Atualiza o estado do aplicativo e chama o eventhandler
            this.UpdateState(state);
        }

        private void SendClose(bool isKill)
        {
            //Sinaliza que o processo não foi iniciado pelo usuário
            this.StartedByUser = false;

            //Sinaliza que o trabalho do processo foi encerrado
            this.SignalEndWork();

            //Se isKilled, define estado do processo como Killed, caso contrário, Closed
            this.UpdateState(isKill ? AppState.Killed : AppState.Closed);
        }

        private void UpdateState(AppState newState)
        {
            //Recupera o estado anterior do app
            var lastState = this._state;

            //Atualiza o estado do aplicativo
            this._state = newState;

            //Chama o event handler de atualização
            this.OnStateChanged?.Invoke(this, new AppStateEventArgs(this, lastState));
        }

        private void AppProcess_Exited(object sender, EventArgs e)
        {
            //Remove os ouvidores de evento e sinaliza o encerramento como Closed
            this.Process.Exited -= this.AppProcess_Exited;
            this.SendClose(false);
        }

        private void ProcessStartTime(AppState state)
        {
            //Se o estado for Launched
            if (state == AppState.Launched)
            {
                //Define o horário de início para a hora atual
                this.StartTime = DateTime.Now;
                return;
            }

            //Se o estado não for Attached
            if (state != AppState.Attached)
            {
                //Define o horário de inicialização como nulo
                this.StartTime = null;
                return;
            }

            try
            {
                //Tenta recuperar o horário de inicialização do app
                this.StartTime = this.Process.StartTime;
            }
            catch
            {
                //Se não conseguir, define para o horário atual
                this.StartTime = DateTime.Now;
            }
        }

        public override string ToString()
        {
            return $"{this.Name}: {this.State}";
        }

        #endregion
    }
}
