using StartupControllerApp;
using StartupControllerApp.Events;
using System;
using System.Linq;
using System.Windows.Forms;

namespace StartupInterface
{
    public class DataApp
    {
        #region Events

        public event EventHandler<AppEventArgs> OnAskRemove;
        public event EventHandler<AppEventArgs> OnAskEdit;

        #endregion

        #region Fields

        private readonly DataGridView dataGrid;
        private readonly StartupController controller;

        public int SelectedIndex
        {
            get
            {
                var selectds = this.dataGrid.SelectedRows;
                if (selectds.Count == 0)
                    return -1;

                return selectds[0].Index;
            }
        }

        public AppController CurrentApp
        {
            get
            {
                if (this.SelectedIndex < 0)
                    return null;

                return this[this.SelectedIndex];
            }
        }

        public AppController this[int index]
        {
            get
            {
                if (index < 0 || index > this.dataGrid.Rows.Count)
                    throw new IndexOutOfRangeException();

                var id = (Guid)this.dataGrid.Rows[index].Cells[0].Value;
                return this.controller[id];
            }
        }

        private bool _init = false;

        #endregion

        public DataApp(StartupController controller, DataGridView dataGrid)
        {
            //Inicializa os campos
            this.dataGrid = dataGrid;
            this.controller = controller;
        }

        #region Methods/Events

        public void Init()
        {
            if (this._init)
                throw new InvalidOperationException();

            this._init = true;

            //Configura o evento dos botões editar e remover
            this.dataGrid.CellContentClick += (object sender, DataGridViewCellEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex > this.dataGrid.Rows.Count)
                    return;

                var colunmName = this.dataGrid.Columns[e.ColumnIndex].Name;
                if (colunmName != "edit" && colunmName != "delete")
                    return;

                var args = new AppEventArgs(this[e.RowIndex]);
                if (colunmName == "edit")
                {
                    this.OnAskEdit?.Invoke(this, args);
                    return;
                }

                this.OnAskRemove?.Invoke(this, args);
            };

            //Configura o evento de remoção de app
            this.controller.OnAppRemoved += (sender, e) => this.Invoke(() =>
            {
                int index = this.IndexOf(e.Id);
                if (index < 0)
                    return;

                this.dataGrid.Rows.RemoveAt(index);
            });
            this.controller.OnAppAdded += (sender, e) => this.Invoke(() => this.dataGrid.Rows.Add(this.CreateRow(e.App)));
            this.controller.OnConfigChanged += Controller_Update;
            this.controller.OnStateChanged += Controller_Update;

            this.Reload();
        }

        public void Reload()
        {
            this.dataGrid.Rows.Clear();

            DataGridViewRow[] rows = new DataGridViewRow[this.controller.Apps.Count];
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = this.CreateRow(this.controller.Apps[i]);
            }

            this.dataGrid.Rows.AddRange(rows);
            this.dataGrid.ClearSelection();
        }

        private void Controller_Update(object sender, AppEventArgs e)
        {
            this.Invoke(() =>
            {
                var index = IndexOf(e.App.Id);
                if (index < 0)
                    return;

                SetValue(this.dataGrid.Rows[index], e.App);
            });
        }

        private DataGridViewRow CreateRow(AppController app)
        {
            //Cria a row e coloca os valores de app
            var row = new DataGridViewRow();
            row.CreateCells(this.dataGrid);
            SetValue(row, app);

            //Define as células com alinhamento central
            var center = DataGridViewContentAlignment.MiddleCenter;
            row.Cells[2].Style.Alignment = center;
            row.Cells[3].Style.Alignment = center;
            row.Cells[4].Style.Alignment = center;
            row.Cells[5].Style.Alignment = center;

            //Recupera os botões de editar e remover
            var btnEdit = row.Cells[6] as DataGridViewLinkCell;
            var btnRemove = row.Cells[7] as DataGridViewLinkCell;

            //Define com texto central
            btnEdit.Style.Alignment = center;
            btnRemove.Style.Alignment = center;

            //Remove a cor de "link visitado"
            btnEdit.VisitedLinkColor = btnEdit.LinkColor;
            btnRemove.VisitedLinkColor = btnEdit.VisitedLinkColor;

            //Retorna a row configurada
            return row;
        }

        private void Invoke(Action action)
        {
            if (!this._init)
                return;

            try
            {
                if (this.dataGrid.InvokeRequired)
                {
                    this.dataGrid.Invoke(action);
                    return;
                }

                action.Invoke();
            }
            catch (InvalidOperationException)
            {
            }
        }

        public int IndexOf(Guid id)
        {
            for (int i = 0; i < this.dataGrid.Rows.Count; i++)
            {
                var row = this.dataGrid.Rows[i].Cells[0].Value;
                if (row is Guid rId && rId == id)
                    return i;
            }

            return -1;
        }

        private static void SetValue(DataGridViewRow row, AppController app)
        {
            var days = app.Settings.WeekDays;

            row.Cells[0].Value = app.Id;
            row.Cells[1].Value = app.Name;
            row.Cells[2].Value = app.State.GetName();
            row.Cells[3].Value = days?.Length > 0 ? string.Join(", ", days.Select(x => x.GetDayOfWeekName())) : "N/A";
            row.Cells[4].Value = app.Settings.WorkTime.ToString();
            row.Cells[5].Value = app.Settings.Enabled ? "Sim" : "Não";
            row.Cells[6].Value = "Editar";
            row.Cells[7].Value = "Remover";
        }

        #endregion
    }
}