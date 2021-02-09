
namespace StartupInterface.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AppIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.IconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvApps = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.program = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.worktime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabled = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edit = new System.Windows.Forms.DataGridViewLinkColumn();
            this.delete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.cmsApps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsReload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsStartStop = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsIgnoreDay = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.MsmTsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MiTsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MiTsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.MiTss = new System.Windows.Forms.ToolStripSeparator();
            this.MiTsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.MiTsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.MiTss1 = new System.Windows.Forms.ToolStripSeparator();
            this.MiTsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MsmTsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.MsmTsmiStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.IconMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApps)).BeginInit();
            this.cmsApps.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AppIcon
            // 
            this.AppIcon.ContextMenuStrip = this.IconMenu;
            this.AppIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("AppIcon.Icon")));
            this.AppIcon.Text = "Controlador de inicializações customizadas";
            this.AppIcon.Visible = true;
            this.AppIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ToggleVisibility_Click);
            this.AppIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AppIcon_MouseMove);
            // 
            // IconMenu
            // 
            this.IconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.tsmiAdd,
            this.tss1,
            this.tsmiExit});
            this.IconMenu.Name = "nIconMenu";
            this.IconMenu.Size = new System.Drawing.Size(126, 76);
            this.IconMenu.Opening += new System.ComponentModel.CancelEventHandler(this.IconMenu_Opening);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(125, 22);
            this.tsmiOpen.Text = "Abrir";
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(125, 22);
            this.tsmiAdd.Text = "Adicionar";
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(122, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(125, 22);
            this.tsmiExit.Text = "Sair";
            // 
            // dgvApps
            // 
            this.dgvApps.AllowUserToAddRows = false;
            this.dgvApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvApps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.program,
            this.state,
            this.workDays,
            this.worktime,
            this.enabled,
            this.edit,
            this.delete});
            this.dgvApps.Location = new System.Drawing.Point(0, 27);
            this.dgvApps.Name = "dgvApps";
            this.dgvApps.RowHeadersVisible = false;
            this.dgvApps.RowTemplate.Height = 25;
            this.dgvApps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvApps.Size = new System.Drawing.Size(834, 423);
            this.dgvApps.TabIndex = 1;
            this.dgvApps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvApps_KeyDown);
            // 
            // id
            // 
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // program
            // 
            this.program.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.program.HeaderText = "Programa";
            this.program.Name = "program";
            this.program.ReadOnly = true;
            // 
            // state
            // 
            this.state.HeaderText = "Estado";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.state.Width = 60;
            // 
            // workDays
            // 
            this.workDays.HeaderText = "Dias";
            this.workDays.Name = "workDays";
            this.workDays.ReadOnly = true;
            this.workDays.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // worktime
            // 
            this.worktime.HeaderText = "Funcionamento";
            this.worktime.Name = "worktime";
            this.worktime.ReadOnly = true;
            this.worktime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.worktime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // enabled
            // 
            this.enabled.HeaderText = "Ativo";
            this.enabled.Name = "enabled";
            this.enabled.ReadOnly = true;
            this.enabled.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.enabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.enabled.Width = 43;
            // 
            // edit
            // 
            this.edit.HeaderText = "";
            this.edit.Name = "edit";
            this.edit.ReadOnly = true;
            this.edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.edit.Text = "";
            this.edit.Width = 50;
            // 
            // delete
            // 
            this.delete.HeaderText = "";
            this.delete.Name = "delete";
            this.delete.ReadOnly = true;
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.delete.Width = 65;
            // 
            // cmsApps
            // 
            this.cmsApps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsAdd,
            this.cmsReload,
            this.toolStripSeparator2,
            this.cmsEdit,
            this.cmsRemove,
            this.toolStripSeparator3,
            this.cmsStartStop,
            this.cmsIgnoreDay});
            this.cmsApps.Name = "cmsApps";
            this.cmsApps.Size = new System.Drawing.Size(153, 148);
            this.cmsApps.Opening += new System.ComponentModel.CancelEventHandler(this.CmsApps_Opening);
            // 
            // cmsAdd
            // 
            this.cmsAdd.Name = "cmsAdd";
            this.cmsAdd.Size = new System.Drawing.Size(152, 22);
            this.cmsAdd.Text = "Adicionar";
            // 
            // cmsReload
            // 
            this.cmsReload.Name = "cmsReload";
            this.cmsReload.Size = new System.Drawing.Size(152, 22);
            this.cmsReload.Text = "Recarregar";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(152, 22);
            this.cmsEdit.Text = "Editar";
            // 
            // cmsRemove
            // 
            this.cmsRemove.Name = "cmsRemove";
            this.cmsRemove.Size = new System.Drawing.Size(152, 22);
            this.cmsRemove.Text = "Remover";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // cmsStartStop
            // 
            this.cmsStartStop.Name = "cmsStartStop";
            this.cmsStartStop.Size = new System.Drawing.Size(152, 22);
            this.cmsStartStop.Text = "Iniciar/Parar";
            // 
            // cmsIgnoreDay
            // 
            this.cmsIgnoreDay.Name = "cmsIgnoreDay";
            this.cmsIgnoreDay.Size = new System.Drawing.Size(152, 22);
            this.cmsIgnoreDay.Text = "Ignorar um dia";
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MsmTsmiFile,
            this.MsmTsmiOptions});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(834, 24);
            this.msMenu.TabIndex = 2;
            this.msMenu.Text = "menuStrip1";
            // 
            // MsmTsmiFile
            // 
            this.MsmTsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiTsmiAdd,
            this.MiTsmiRefresh,
            this.MiTss,
            this.MiTsmiExport,
            this.MiTsmiImport,
            this.MiTss1,
            this.MiTsmiExit});
            this.MsmTsmiFile.Name = "MsmTsmiFile";
            this.MsmTsmiFile.Size = new System.Drawing.Size(61, 20);
            this.MsmTsmiFile.Text = "&Arquivo";
            // 
            // MiTsmiAdd
            // 
            this.MiTsmiAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MiTsmiAdd.Name = "MiTsmiAdd";
            this.MiTsmiAdd.Size = new System.Drawing.Size(130, 22);
            this.MiTsmiAdd.Text = "&Adicionar";
            // 
            // MiTsmiRefresh
            // 
            this.MiTsmiRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MiTsmiRefresh.Name = "MiTsmiRefresh";
            this.MiTsmiRefresh.Size = new System.Drawing.Size(130, 22);
            this.MiTsmiRefresh.Text = "&Recarregar";
            // 
            // MiTss
            // 
            this.MiTss.Name = "MiTss";
            this.MiTss.Size = new System.Drawing.Size(127, 6);
            // 
            // MiTsmiExport
            // 
            this.MiTsmiExport.Enabled = false;
            this.MiTsmiExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MiTsmiExport.Name = "MiTsmiExport";
            this.MiTsmiExport.Size = new System.Drawing.Size(130, 22);
            this.MiTsmiExport.Text = "&Exportar";
            // 
            // MiTsmiImport
            // 
            this.MiTsmiImport.Enabled = false;
            this.MiTsmiImport.Name = "MiTsmiImport";
            this.MiTsmiImport.Size = new System.Drawing.Size(130, 22);
            this.MiTsmiImport.Text = "Importar";
            // 
            // MiTss1
            // 
            this.MiTss1.Name = "MiTss1";
            this.MiTss1.Size = new System.Drawing.Size(127, 6);
            // 
            // MiTsmiExit
            // 
            this.MiTsmiExit.Name = "MiTsmiExit";
            this.MiTsmiExit.Size = new System.Drawing.Size(130, 22);
            this.MiTsmiExit.Text = "S&air";
            // 
            // MsmTsmiOptions
            // 
            this.MsmTsmiOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MsmTsmiStartup});
            this.MsmTsmiOptions.Name = "MsmTsmiOptions";
            this.MsmTsmiOptions.Size = new System.Drawing.Size(59, 20);
            this.MsmTsmiOptions.Text = "&Opções";
            // 
            // MsmTsmiStartup
            // 
            this.MsmTsmiStartup.CheckOnClick = true;
            this.MsmTsmiStartup.Name = "MsmTsmiStartup";
            this.MsmTsmiStartup.Size = new System.Drawing.Size(186, 22);
            this.MsmTsmiStartup.Text = "&Iniciar com o sistema";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 451);
            this.Controls.Add(this.msMenu);
            this.Controls.Add(this.dgvApps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenu;
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controlador de inicializações customizadas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.IconMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApps)).EndInit();
            this.cmsApps.ResumeLayout(false);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon AppIcon;
        private System.Windows.Forms.ContextMenuStrip IconMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.DataGridView dgvApps;
        private System.Windows.Forms.ContextMenuStrip cmsApps;
        private System.Windows.Forms.ToolStripMenuItem cmsAdd;
        private System.Windows.Forms.ToolStripMenuItem cmsRemove;
        private System.Windows.Forms.ToolStripMenuItem cmsEdit;
        private System.Windows.Forms.ToolStripMenuItem cmsReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmsStartStop;
        private System.Windows.Forms.ToolStripMenuItem cmsIgnoreDay;
        private System.Windows.Forms.ToolStripSeparator tss1;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem MsmTsmiFile;
        private System.Windows.Forms.ToolStripMenuItem MiTsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem MiTsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem MiTsmiExport;
        private System.Windows.Forms.ToolStripMenuItem MsmTsmiStartup;
        private System.Windows.Forms.ToolStripSeparator MiTss;
        private System.Windows.Forms.ToolStripMenuItem MiTsmiImport;
        private System.Windows.Forms.ToolStripSeparator MiTss1;
        private System.Windows.Forms.ToolStripMenuItem MiTsmiExit;
        private System.Windows.Forms.ToolStripMenuItem MsmTsmiOptions;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn program;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn workDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn worktime;
        private System.Windows.Forms.DataGridViewTextBoxColumn enabled;
        private System.Windows.Forms.DataGridViewLinkColumn edit;
        private System.Windows.Forms.DataGridViewLinkColumn delete;
    }
}

