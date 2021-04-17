using StartupControllerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StartupInterface.Forms
{
    public partial class ProgramDialog : Form
    {
        private readonly StartupController controller;
        private readonly List<DateTime> ignoredDates = new List<DateTime>();
        private AppSettings settings = null;
        private Guid? appId;

        #region ProgramDialog Show

        public ProgramDialog(StartupController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        public AppSettings ShowEditDialog(AppSettings settings, Guid? appId = null)
        {
            this.DialogResult = DialogResult.None;
            this.tabControl.SelectTab(0);
            this.txtName.Focus();
            this.appId = appId;

            this.SetSettings(settings);
            this.ShowDialog();

            this.ApplyChanges();

            return this.DialogResult == DialogResult.OK ? this.settings : null;
        }

        public AppSettings ShowCreateDialog()
        {
            return this.ShowEditDialog(new AppSettings()
            {
                Enabled = true
            });
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            this.SetCenter();
            base.OnVisibleChanged(e);
            if (!this.Visible)
                return;

            this.dtpStartAt.Checked = this.settings.WorkTime.Start.HasValue;
            this.dtpEntAt.Checked = this.settings.WorkTime.End.HasValue;
        }

        private void SetCenter()
        {
            if (!(this.Visible && this.Top == 0 && this.Left == 0))
                return;

            if (!(Screen.FromControl(this) is Screen screen))
                return;

            var area = screen.WorkingArea;
            int width = (area.Width / 2) - (this.Width / 2);
            int height = (area.Height / 2) - (this.Height / 2);

            this.SetDesktopLocation(width, height);
        }

        #endregion

        #region Settings

        private void SetSettings(AppSettings settings)
        {
            this.settings = settings;

            this.txtName.Text = settings.Name ?? "";
            this.txtProgramPath.Text = settings.Program ?? "";
            this.cbActive.SelectedIndex = settings.Enabled ? 1 : 0;

            this.txtWorkdir.Text = settings.WorkingDirectory ?? "";

            this.txtAppArgs.Text = string.Join('\n', settings.Args ?? Array.Empty<string>());
            this.CheckDaysOfWeek(settings);

            var times = settings.WorkTime;

            SetTime(dtpStartAt, times.Start, null);
            SetTime(dtpEntAt, times.End, DateTime.Now.AddMinutes(40));
            this.cbAutoClose.Checked = settings.ForceCloseAfterWorktime;

            this.ignoredDates.Clear();

            if (settings.IgnoredDates != null)
                this.ignoredDates.AddRange(settings.IgnoredDates);

            ReorderDates();
        }

        private void ReorderDates()
        {
            this.lbIgnoredDays.Items.Clear();
            if (this.ignoredDates.Count < 1)
                return;

            this.ignoredDates.Sort();
            string[] dates = new string[this.ignoredDates.Count];
            for (int i = 0; i < dates.Length; i++)
                dates[i] = ToFormat(this.ignoredDates[i]);

            this.lbIgnoredDays.Items.AddRange(dates);
        }

        private void ApplyChanges()
        {
            this.settings.Name = this.txtName.Text;
            this.settings.Program = this.txtProgramPath.Text;
            this.settings.Enabled = this.cbActive.SelectedIndex == 1;
            this.settings.WorkingDirectory = this.txtWorkdir.Text;
            this.settings.Args = this.txtAppArgs.Text.Split('\n');

            this.settings.WorkTime = new WorkTime(GetTime(this.dtpStartAt), GetTime(this.dtpEntAt));

            settings.ForceCloseAfterWorktime = this.cbAutoClose.Checked;

            var checkeds = this.clbDays.CheckedIndices;
            this.settings.WeekDays = new DayOfWeek[checkeds.Count];

            for (int i = 0; i < checkeds.Count; i++)
                this.settings.WeekDays[i] = (DayOfWeek)checkeds[i];

            this.settings.IgnoredDates = this.ignoredDates.ToArray();
        }

        private void CheckSettings()
        {
            if (string.IsNullOrEmpty(this.txtName.Text))
                throw new Exception("O nome do programa deve ter pelo menos 1 caractere!");

            if (this.controller.NameInUse(this.txtName.Text, appId))
                throw new Exception("O nome do programa já está em uso!");

            if (string.IsNullOrEmpty(this.txtProgramPath.Text))
                throw new Exception("O caminho do programa é inválido!");

            var fileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(this.txtProgramPath.Text);
            if (fileInfo.ProductName == null)
                throw new Exception("O arquivo não foi reconhecido como um app executável!");

            if (!System.IO.File.Exists(this.txtProgramPath.Text))
                throw new Exception("O programa não foi encontrado!");

            var workDir = this.txtWorkdir.Text;
            if (!string.IsNullOrEmpty(workDir) && !System.IO.File.Exists("workDir"))
                throw new Exception("A pasta de trabalho do programa é inválida!");
        }

        private void CheckDaysOfWeek(AppSettings settings)
        {
            for (int i = 0; i < this.clbDays.Items.Count; i++)
                this.clbDays.SetItemCheckState(i, CheckState.Unchecked);

            if (settings.WeekDays == null)
                return;

            foreach (var item in settings.WeekDays)
                this.clbDays.SetItemCheckState((int)item, CheckState.Checked);
        }

        #endregion

        #region Date/Time

        private static void SetTime(DateTimePicker picker, TimeSpan? time, DateTime? blankTime)
        {
            picker.Checked = true;
            if (!time.HasValue)
            {
                picker.Value = blankTime ?? DateTime.Now;
                return;
            }

            picker.Value = DateTime.Now.SetTimeOfDay(time.Value);
        }

        private static TimeSpan? GetTime(DateTimePicker picker)
        {
            if (!picker.Checked)
                return null;

            var time = picker.Value.TimeOfDay;
            return new TimeSpan(time.Hours, time.Minutes, 0);
        }

        private static string ToFormat(DateTime date) => date.ToString("d");

        #endregion

        #region Events

        private void lbIgnoredDays_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                this.llRemove_LinkClicked(sender, null);
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var index = lbIgnoredDays.SelectedIndex;
            if (index < 0)
                return;

            string message = string.Format("Você deseja realmente remover a data \"{0}\"?", ToFormat(this.ignoredDates[index]));
            if (MessageBox.Show(message, "Remover data da lista de ignorados", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            //Remove date
            this.ignoredDates.RemoveAt(index);
            this.lbIgnoredDays.Items.RemoveAt(index);
        }

        private void llAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IgnoreDialog.ShowIgnoreDialog() is not DateTime date)
                return;

            if (this.ignoredDates.Any(ig => ig.Date == date.Date))
            {
                MessageBox.Show("Esta data já foi adicionada anteriormente!");

                //Seleciona a data adicionada
                this.lbIgnoredDays.SelectedIndex = this.ignoredDates.IndexOf(date);
                return;
            }

            this.ignoredDates.Add(date);
            this.ReorderDates();

            //Seleciona a data adicionada
            this.lbIgnoredDays.SelectedIndex = this.ignoredDates.IndexOf(date);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.CheckSettings();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuração inválida!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnFindApp_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Arquivo executável |*.exe"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var fileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ofd.FileName);
            if (fileInfo.ProductName == null)
            {
                MessageBox.Show("O arquivo não foi reconhecido como um app executável!", Program.AppName);
                return;
            }

            this.txtProgramPath.Text = ofd.FileName;
            if (string.IsNullOrEmpty(this.txtName.Text))
                this.txtName.Text = fileInfo.ProductName;
        }

        private void btnFindDir_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                this.txtProgramPath.Text = fbd.SelectedPath;
        }

        #endregion
    }
}