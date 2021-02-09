using StartupControllerApp;
using StartupControllerApp.Events;
using StartupInterface.Settings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace StartupInterface.Forms
{
    public partial class MainForm : Form
    {
        #region Fields

        private StartupController Controller { get; }
        private readonly ProgramDialog appDialog;
        private bool isSilent = Environment.GetCommandLineArgs().Contains("-silent");
        private readonly DataApp dataApps = null;
        private readonly FormSettingsController formSettings;
        private const string StartupRegistryName = "StartupInterface";

        #endregion

        #region Form

        public MainForm()
        {
            InitializeComponent();

            this.formSettings = new FormSettingsController(this, "position.json");

            this.Controller = new StartupController(Program.GetAppDataDir("Apps"));
            this.dataApps = new DataApp(this.Controller, this.dgvApps);
            this.appDialog = new ProgramDialog(this.Controller);

            this.dgvApps.MouseClick += DgvApps_MouseClick;
            this.dataApps.OnAskEdit += DataApps_OnAskEdit;
            this.dataApps.OnAskRemove += DataApps_OnAskRemove;
            this.Controller.OnStateChanged += Controller_OnStateChanged;
            this.Controller.OnError += Controller_OnError;

            this.tsmiExit.Click += TsmiExit_Click;
            this.tsmiOpen.Click += TsmiOpen_Click;

            this.MiTsmiAdd.Click += TsmiAdd_Click;
            this.MiTsmiExit.Click += TsmiExit_Click;
            this.MiTsmiRefresh.Click += Reload_Click;
            this.MsmTsmiOptions.DropDownOpening += MsmTsmiOptions_DropDownOpening;
            this.MsmTsmiStartup.Click += MsmTsmiStartup_Click;

            this.tsmiAdd.Click += TsmiAdd_Click;
            this.cmsAdd.Click += TsmiAdd_Click;
            this.cmsReload.Click += Reload_Click;
            this.cmsEdit.Click += CmsEdit_Click;
            this.cmsRemove.Click += CmsRemove_Click;
            this.cmsStartStop.Click += CmsStartStop_Click;
            this.cmsIgnoreDay.Click += CmsIgnoreDay_Click;

            this.formSettings.ApplyConfig();

            this.Controller.ReloadApps();
            this.Controller.RunScheduler(TimeSpan.FromSeconds(2.5), false, true, true);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.dataApps.Init();
        }

        protected override void SetVisibleCore(bool value)
        {
            if (isSilent)
            {
                this.isSilent = false;
                this.CreateHandle();
                return;
            }
            base.SetVisibleCore(value);
        }

        #endregion

        #region Methods

        public void EditApp(Guid appId)
        {
            var app = this.Controller[appId];
            if (app == null)
                return;

            var settings = this.appDialog.ShowEditDialog(app.Settings, appId);
            if (settings == null)
                return;

            app.Settings = settings;

            this.Controller.UpdateSettings(app);
        }

        public void RemoveApp(Guid appId)
        {
            var app = this.Controller[appId];
            if (app == null)
                return;

            string message = $"Você deseja realmente remover o programa {app.Name}?";
            if (MessageBox.Show(message, "Remover programa?", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            this.Controller.DeleteApp(appId, false);
        }

        public void ToggleFormVisibility()
        {
            if (this.Visible = !this.Visible)
                this.MoveToForeground();
        }

        private void UpdateIconMenu()
        {
            this.tsmiOpen.Text = this.Visible ? "Ocultar" : "Abrir";
            this.tsmiAdd.Text = this.appDialog.Visible ? "Cancelar adição" : "Adicionar";
        }

        #endregion

        #region Events

        private void DgvApps_MouseClick(object sender, MouseEventArgs e)
        {
            this.dgvApps.ClearSelection();

            var hint = this.dgvApps.HitTest(e.X, e.Y);
            if (hint.Type == DataGridViewHitTestType.Cell)
                this.dgvApps.Rows[hint.RowIndex].Selected = true;

            if (e.Button != MouseButtons.Right)
                return;

            this.cmsApps.Show(Cursor.Position);
        }

        private void Controller_OnError(object sender, AppErrorEventArgs e)
        {
            if (e.IsFatal)
            {
                Program.RunFatalAction(e.Exception);
                return;
            }

            string txtError = $"Não foi possível iniciar o programa \"{e.App.Name}\".";
            MessageBox.Show(txtError, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void DataApps_OnAskRemove(object sender, AppEventArgs e)
        {
            this.RemoveApp(e.Id);
        }

        private void DataApps_OnAskEdit(object sender, AppEventArgs e)
        {
            this.EditApp(e.Id);
        }

        private void CmsIgnoreDay_Click(object sender, EventArgs e)
        {
            if (this.dataApps.CurrentApp is not AppController app)
                return;

            if (IgnoreDialog.ShowIgnoreDialog() is not DateTime date)
                return;

            var dates = app.Settings.IgnoredDates ?? Array.Empty<DateTime>();
            if (dates.Any(ig => ig.EqualsDate(date)))
            {
                MessageBox.Show("Esta data já foi adicionada anteriormente!");
                return;
            }

            Array.Resize(ref dates, dates.Length + 1);
            dates[^1] = date;

            this.Controller.UpdateSettings(app);
        }

        private void TsmiOpen_Click(object sender, EventArgs e)
        {
            this.ToggleFormVisibility();
        }

        private void TsmiExit_Click(object sender, EventArgs e)
        {
            this.Controller.StopScheduler();
            this.formSettings.Save();
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }

            this.formSettings.Save();
        }

        private void ToggleVisibility_Click(object sender, MouseEventArgs e)
        {
            this.ToggleFormVisibility();
        }

        private void TsmiAdd_Click(object sender, EventArgs e)
        {
            if (this.appDialog.Visible)
            {
                this.appDialog.Close();
                return;
            }

            var settings = this.appDialog.ShowCreateDialog();
            if (settings == null)
                return;

            this.Controller.CreateApp(settings);
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            this.Controller.ReloadApps();
            SystemTray.Refresh();

            foreach (var app in this.Controller.Apps)
            {
                try
                {
                    if (!app.IsRunning)
                        app.TryAttach();
                }
                catch
                { }
            }

            this.dataApps.Reload();
        }

        private void CmsApps_Opening(object sender, CancelEventArgs e)
        {
            var app = this.dataApps.CurrentApp;
            bool hasApp = app != null;

            this.cmsEdit.Enabled = hasApp;
            this.cmsRemove.Enabled = hasApp;
            this.cmsStartStop.Enabled = hasApp;
            this.cmsIgnoreDay.Enabled = hasApp;


            this.cmsStartStop.Text = hasApp && app.IsRunning ? "Parar" : "Iniciar";
        }

        private void CmsRemove_Click(object sender, EventArgs e)
        {
            if (this.dataApps.SelectedIndex < 0)
                return;

            if (this.dataApps.CurrentApp is not AppController app)
                return;

            this.RemoveApp(app.Id);
        }

        private void CmsEdit_Click(object sender, EventArgs e)
        {
            if (this.dataApps.SelectedIndex < 0)
                return;

            if (this.dataApps.CurrentApp is not AppController app)
                return;

            this.EditApp(app.Id);
        }

        private void CmsStartStop_Click(object sender, EventArgs e)
        {
            var app = this.dataApps.CurrentApp;
            if (app == null)
                return;

            try
            {
                if (app.IsRunning)
                    app.Close();
                else
                    app.Start(true);
            }
            catch (Exception ex)
            {
                this.Controller_OnError(this.Controller, new AppErrorEventArgs(app, ex, false));
            }
        }

        private void Controller_OnStateChanged(object sender, AppEventArgs e)
        {
            SystemTray.Refresh();
        }

        private void DgvApps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.Reload_Click(sender, e);
            }
        }

        private void IconMenu_Opening(object sender, CancelEventArgs e)
        {
            this.UpdateIconMenu();
        }

        private void AppIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.UpdateIconMenu();
        }

        private void MsmTsmiOptions_DropDownOpening(object sender, EventArgs e)
        {
            this.MsmTsmiStartup.Checked = RegistryStartup.HasProgram(StartupRegistryName);
        }

        private void MsmTsmiStartup_Click(object sender, EventArgs e)
        {
            try
            {
                if (RegistryStartup.HasProgram(StartupRegistryName))
                {
                    RegistryStartup.DeleteProgram(StartupRegistryName);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível remover o programa da inicialização do windows.\n" + ex.Message);
            }

            try
            {
                var cur = System.Diagnostics.Process.GetCurrentProcess();
                var startArgs = $"\"{cur.MainModule.FileName}\" -silent";

                RegistryStartup.SetProgram(StartupRegistryName, startArgs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível adicionar o programa na inicialização do windows.\n" + ex.Message);
            }
        }

        #endregion
    }
}
