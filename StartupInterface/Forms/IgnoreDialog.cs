using StartupControllerApp;
using System;
using System.Windows.Forms;

namespace StartupInterface.Forms
{
    public partial class IgnoreDialog : Form
    {
        protected DateTime SelectedDate => this.calendar.SelectionRange.Start;

        private IgnoreDialog()
        {
            InitializeComponent();
        }

        public static DateTime? ShowIgnoreDialog()
        {
            var dialog = new IgnoreDialog();

            var now = DateTime.Now;
            dialog.calendar.MinDate = new DateTime(now.Year, now.Month, 1);
            dialog.ShowDialog();

            if (dialog.DialogResult == DialogResult.OK)
                return dialog.SelectedDate;

            return null;
        }

        #region Events

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.SelectedDate < DateTime.Now.Date)
            {
                MessageBox.Show("A data selecionada precisa ser maior ou igual a atual!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion
    }
}
