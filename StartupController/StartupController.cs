using Newtonsoft.Json;
using StartupControllerApp.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace StartupControllerApp
{
    public class StartupController
    {
        #region Events

        public event EventHandler<AppEventArgs> OnAppAdded;
        public event EventHandler<AppEventArgs> OnAppRemoved;
        public event EventHandler<AppEventArgs> OnConfigChanged;
        public event EventHandler<AppStateEventArgs> OnStateChanged;
        public event EventHandler<AppErrorEventArgs> OnError;
        public event EventHandler<AppEndWorkEventArgs> OnEndWork;

        #endregion

        #region Fields

        /// <summary>
        /// Extension of app settings file
        /// </summary>
        public const string AppExtension = ".japp";

        private readonly List<AppController> apps = new List<AppController>();

        /// <summary>
        /// Returns all apps.
        /// </summary>
        public ReadOnlyCollection<AppController> Apps { get; }

        /// <summary>
        /// Directory where application data should be save.
        /// </summary>
        public DirectoryInfo AppsDir { get; }

        private Thread schedulerThread;

        #endregion

        /// <summary>
        /// Launch a new instance of StartupController.
        /// </summary>
        /// <param name="dataDir">Directory where application data should be save.</param>
        public StartupController(DirectoryInfo appsDir)
        {
            if (!appsDir.Exists)
                throw new InvalidOperationException("The settings folder is invalid.");

            this.AppsDir = appsDir;
            this.Apps = this.apps.AsReadOnly();

            if (!this.AppsDir.Exists)
                this.AppsDir.Create();
        }

        /// <summary>
        /// Retrieves an app by id.
        /// </summary>
        /// <param name="appId">App id.</param>
        /// <returns></returns>
        public AppController this[Guid appId] => apps.FirstOrDefault(app => app.Id == appId);

        #region Process Scheduler

        /// <summary>
        /// Starts the task responsible for starting and closing apps according to your settings.
        /// </summary>
        /// <param name="schedulerDelay">Delay para atualizar os apps.</param>
        /// <param name="autoReload">Automatically recovers new apps added to the StartupController.AppsDir folder.</param>
        /// <param name="runInBackground">Signals whether the thread should run in the background.</param>
        /// <param name="closeAfterWorktime">Signals whether the application should be automatically terminated by the task when the working time comes to an end and AppController.ForceCloseAfterWorktime = true.</param>
        public void RunScheduler(TimeSpan schedulerDelay, bool autoReload, bool runInBackground, bool closeAfterWorktime = true)
        {
            if (this.schedulerThread != null)
                throw new InvalidOperationException("The scheduler thread has already started.");

            void thStart()
            {
                while (true)
                {
                    AppController lastApp = null;
                    try
                    {
                        if (autoReload)
                            this.ReloadApps();

                        foreach (var app in this.Apps)
                        {
                            //Define qual é o app que está sendo processado
                            lastApp = app;

                            //Se as configurações do app estiverem desativadas ou se o app estiver com erro:
                            //Ignorar os próximos passos
                            if (!app.Enabled || app.State == AppState.Errored)
                                continue;

                            try
                            {
                                //Se o app não estiver funcionando, TryAttach() = false e InWorkingTime = true:
                                //Inicia o app
                                if (!app.IsRunning && !app.TryAttach() && app.InWorkingTime)
                                    app.Start();
                            }
                            catch (SystemException ex)
                            {
                                this.OnError.Invoke(this, new AppErrorEventArgs(lastApp, ex, true));
                                continue;
                            }

                            //Se o controlador não tem permissão para encerrar os apps depois do tempo limite
                            //Ou o aplicativo não está aberto ou o aplicativo está no tempo de trabalho
                            //Ou o aplicativo foi iniciado pelo usuário
                            //Ou o aplicativo não deve ser encerrado após o tempo de trabalho
                            //Ou o aplicativo não tem um horário de início definido
                            //Ou o aplicativo foi iniciado após o tempo limite:
                            //Ignorar o próximo passo
                            if (!closeAfterWorktime || !app.IsRunning || app.InWorkingTime || app.StartedByUser ||
                                !app.Settings.ForceCloseAfterWorktime || app.StartTime == null || app.StartedOutOfWorktime)
                                continue;

                            var args = new AppEndWorkEventArgs(app, false);

                            this.OnEndWork?.Invoke(this, args);

                            if (args.IsCancelled)
                                continue;

                            app.Close();
                        }
                        Thread.Sleep(schedulerDelay);
                    }
                    catch (ThreadInterruptedException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        this.OnError.Invoke(this, new AppErrorEventArgs(lastApp, ex, true));
                        break;
                    }
                }
            }

            this.schedulerThread = new Thread(thStart)
            {
                IsBackground = runInBackground
            };
            this.schedulerThread.Start();
        }

        /// <summary>
        /// Terminates the thread responsible for starting and terminating apps.
        /// </summary>
        public void StopScheduler()
        {
            if (this.schedulerThread == null)
                throw new InvalidOperationException("The scheduler thread has not been started.");

            this.schedulerThread.Interrupt();
            _ = this.schedulerThread;
        }

        #endregion

        #region App

        /// <summary>
        /// Reload the settings files.
        /// </summary>
        /// <param name="stopInvalidApps">Signals whether manually removed application processes should be stop.</param>
        public void ReloadApps(bool stopInvalidApps = false)
        {
            var registredApps = this.GetAvaliableApps();
            foreach (var id in registredApps)
            {
                if (this[id] == null)
                {
                    this.LoadApp(id);
                    continue;
                }

                this.ReloadApp(id);
            }

            var toRemove = new List<AppController>();
            foreach (var app in this.Apps)
            {
                if (registredApps.Contains(app.Id))
                    continue;

                toRemove.Add(app);
            }

            foreach (var app in toRemove)
            {
                apps.Remove(app);
                this.OnAppRemoved(this, new AppEventArgs(app));

                if (!stopInvalidApps)
                    continue;

                app.Close();
            }
        }

        /// <summary>
        /// Loads a new file from the settings file.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <returns></returns>
        public AppController LoadApp(Guid appId)
        {
            return this.ProcessApp(appId, null);
        }

        /// <summary>
        /// Reloads an app's settings.
        /// </summary>
        /// <param name="appId">App id.</param>
        public void ReloadApp(Guid appId)
        {
            if (this[appId] == null)
            {
                string error = $"Cannot reload \"{appId}\" application, it has not been previously loaded";
                throw new InvalidOperationException(error);
            }

            var settings = this.LoadSettings(appId);
            this[appId].Settings = settings ?? throw new FileNotFoundException($"Could not find \"{appId}\" configuration file", this.GetAppPath(appId));
        }

        /// <summary>
        /// Retrieves the ids of the apps available in the apps folder.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Guid> GetAvaliableApps()
        {
            foreach (var item in this.AppsDir.GetFiles())
            {
                if (item.Extension != AppExtension)
                    continue;

                var strId = item.Name.Substring(0, item.Name.Length - AppExtension.Length);

                if (Guid.TryParse(strId, out Guid id))
                    yield return id;
            }
        }

        /// <summary>
        /// Removes an app's configuration in the apps folder. It may or may not be loaded.
        /// </summary>
        /// <param name="appId">App id.</param>
        /// <param name="forceStop">Signals whether the app should be terminated if it is working.</param>
        /// <returns></returns>
        public bool DeleteApp(Guid appId, bool forceStop)
        {
            var path = GetAppPath(appId);
            var deleted = false;

            if (File.Exists(path))
            {
                File.Delete(path);
                deleted = true;
            }

            var app = this[appId];
            if (app == null)
                return deleted;

            app.OnStateChanged -= this.App_OnStateChanged;

            this.OnAppRemoved?.Invoke(this, new AppEventArgs(app));

            if (app.IsRunning && forceStop)
                app.Kill();

            this.apps.Remove(app);

            return deleted;
        }

        /// <summary>
        /// Create a new app from a setting (The app name must be unique).
        /// </summary>
        /// <param name="settings">App settings.</param>
        /// <returns></returns>
        public AppController CreateApp(AppSettings settings)
        {
            this.ValidateSettings(settings, true);

            var id = Guid.NewGuid();
            this.WriteSettings(id, settings);
            return this.ProcessApp(id, settings);
        }

        private AppController ProcessApp(Guid id, AppSettings settings = null)
        {
            if (this[id] != null)
                throw new Exception($"\"{id}\" Application has already been loaded.");

            if (settings == null)
                settings = this.LoadSettings(id);

            if (settings == null)
                return null;

            var app = new AppController(id, settings);
            this.apps.Add(app);

            try
            {
                this.OnAppAdded?.Invoke(this, new AppEventArgs(app));
            }
            finally
            {
                app.OnStateChanged += App_OnStateChanged;
            }

            return app;
        }

        /// <summary>
        /// throws an error if the app configuration is invalid.
        /// </summary>
        /// <param name="settings">App settings</param>
        /// <param name="checkSimilarName">Signals whether a similar name should be searched</param>
        /// <param name="appId">Configuration app id</param>
        public void ValidateSettings(AppSettings settings, bool checkSimilarName, Guid? appId = null)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrEmpty(settings.GetSafeName()))
                throw new Exception("The program name must be at least 1 character.");

            if (checkSimilarName && this.NameInUse(settings.GetSafeName(), appId))
                throw new Exception("An application with this name has already been registered.");

            if (settings.Program == null)
                throw new Exception("Enter a valid program path.");

            if (!File.Exists(settings.Program))
                throw new FileNotFoundException($"The {settings.Program} program was not found");

        }

        #endregion

        #region Events Handler

        private void App_OnStateChanged(object sender, AppStateEventArgs e)
        {
            this.OnStateChanged?.Invoke(this, e);

            if (!e.IsStartEvent() || !e.App.IsRunning)
                return;

            var procName = e.App.Process.ProcessName;
            if (e.App.Settings.ProcessName == procName)
                return;

            e.App.Settings.ProcessName = procName;
            this.WriteSettings(e.Id, e.App.Settings);
        }

        #endregion

        #region Settings

        /// <summary>
        /// Write in the settings file the changes in the app settings.
        /// </summary>
        /// <param name="app">App controller.</param>
        public void UpdateSettings(AppController app)
        {
            //Se o app for nulo, lança um erro
            if (app == null)
                throw new ArgumentNullException(nameof(app), "The inserted application is invalid");

            //Se a configuração não existir, lança um erro
            if (!this.ExistSettings(app.Id))
                throw new FileNotFoundException("The application configuration was not found.");

            //Valida a canfiguração
            this.ValidateSettings(app.Settings, true, app.Id);
            //Salva a configuração em um arquivo
            this.WriteSettings(app.Id, app.Settings);
            //Chama o event handler de "configuração atualizada"
            this.OnConfigChanged?.Invoke(this, new AppEventArgs(app));
        }

        /// <summary>
        /// Checks to see if the app configuration file exists.
        /// </summary>
        /// <param name="appId">App id</param>
        /// <returns></returns>
        public bool ExistSettings(Guid appId)
        {
            return File.Exists(this.GetAppPath(appId));
        }

        private void WriteSettings(Guid appId, AppSettings settings)
        {
            var settingsStr = JsonConvert.SerializeObject(settings);

            var path = this.GetAppPath(appId);
            File.WriteAllText(path, settingsStr);
        }

        /// <summary>
        /// Checks if the requested name is already in use by any app.
        /// </summary>
        /// <param name="safeName">Secure app name</param>
        /// <param name="appId">App id</param>
        /// <returns>True if the app name is in use and is not by the id entered, false if the app id is not in use.</returns>
        public bool NameInUse(string safeName, Guid? appId = null)
        {
            return apps.Any(app => app.Settings.GetSafeName() == safeName && (appId == null || appId != app.Id));
        }

        private AppSettings LoadSettings(Guid appId)
        {
            try
            {
                string file = this.GetAppPath(appId);

                if (!File.Exists(file))
                    return null;

                string serialized = File.ReadAllText(file);

                return JsonConvert.DeserializeObject<AppSettings>(serialized);
            }
            catch (Exception ex)
            {
                this.OnError?.Invoke(this, new AppErrorEventArgs(appId, ex, false));
                return null;
            }
        }

        #endregion

        private string GetAppPath(Guid appId)
        {
            return Path.Combine(this.AppsDir.FullName, appId + AppExtension);
        }
    }
}
