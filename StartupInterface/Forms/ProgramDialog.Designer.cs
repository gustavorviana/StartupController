
namespace StartupInterface.Forms
{
    partial class ProgramDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramDialog));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.dtpEntAt = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.dtpStartAt = new System.Windows.Forms.DateTimePicker();
            this.clbDays = new System.Windows.Forms.CheckedListBox();
            this.txtAppArgs = new System.Windows.Forms.TextBox();
            this.lblStartArgs = new System.Windows.Forms.Label();
            this.btnFindDir = new System.Windows.Forms.Button();
            this.txtWorkdir = new System.Windows.Forms.TextBox();
            this.lblWorkDir = new System.Windows.Forms.Label();
            this.btnFindApp = new System.Windows.Forms.Button();
            this.txtProgramPath = new System.Windows.Forms.TextBox();
            this.lblProgramPath = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpApp = new System.Windows.Forms.TabPage();
            this.lblActive = new System.Windows.Forms.Label();
            this.cbActive = new System.Windows.Forms.ComboBox();
            this.tpWorking = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAutoClose = new System.Windows.Forms.CheckBox();
            this.lblDaysOfWeek = new System.Windows.Forms.Label();
            this.tpAdvanced = new System.Windows.Forms.TabPage();
            this.tpIgnored = new System.Windows.Forms.TabPage();
            this.llRemove = new System.Windows.Forms.LinkLabel();
            this.llAdd = new System.Windows.Forms.LinkLabel();
            this.lbIgnoredDays = new System.Windows.Forms.ListBox();
            this.tabControl.SuspendLayout();
            this.tpApp.SuspendLayout();
            this.tpWorking.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpAdvanced.SuspendLayout();
            this.tpIgnored.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(281, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(200, 215);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 24;
            this.btSave.Text = "Salvar";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(6, 73);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(27, 15);
            this.lblEndTime.TabIndex = 13;
            this.lblEndTime.Text = "Fim";
            // 
            // dtpEntAt
            // 
            this.dtpEntAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEntAt.CustomFormat = "HH:mm";
            this.dtpEntAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEntAt.Location = new System.Drawing.Point(6, 91);
            this.dtpEntAt.Name = "dtpEntAt";
            this.dtpEntAt.ShowCheckBox = true;
            this.dtpEntAt.ShowUpDown = true;
            this.dtpEntAt.Size = new System.Drawing.Size(171, 23);
            this.dtpEntAt.TabIndex = 14;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(6, 27);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(36, 15);
            this.lblStartTime.TabIndex = 11;
            this.lblStartTime.Text = "Início";
            // 
            // dtpStartAt
            // 
            this.dtpStartAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartAt.CustomFormat = "HH:mm";
            this.dtpStartAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartAt.Location = new System.Drawing.Point(6, 45);
            this.dtpStartAt.Name = "dtpStartAt";
            this.dtpStartAt.ShowCheckBox = true;
            this.dtpStartAt.ShowUpDown = true;
            this.dtpStartAt.Size = new System.Drawing.Size(171, 23);
            this.dtpStartAt.TabIndex = 12;
            // 
            // clbDays
            // 
            this.clbDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbDays.FormattingEnabled = true;
            this.clbDays.Items.AddRange(new object[] {
            "Domingo",
            "Segunda-Feira",
            "Terça-Feira",
            "Quarta-Feira",
            "Quinta-Feira",
            "Sexta-Feira",
            "Sábado"});
            this.clbDays.Location = new System.Drawing.Point(5, 24);
            this.clbDays.Name = "clbDays";
            this.clbDays.Size = new System.Drawing.Size(152, 148);
            this.clbDays.TabIndex = 9;
            // 
            // txtAppArgs
            // 
            this.txtAppArgs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppArgs.Location = new System.Drawing.Point(5, 75);
            this.txtAppArgs.Multiline = true;
            this.txtAppArgs.Name = "txtAppArgs";
            this.txtAppArgs.Size = new System.Drawing.Size(341, 100);
            this.txtAppArgs.TabIndex = 20;
            // 
            // lblStartArgs
            // 
            this.lblStartArgs.AutoSize = true;
            this.lblStartArgs.Location = new System.Drawing.Point(5, 57);
            this.lblStartArgs.Name = "lblStartArgs";
            this.lblStartArgs.Size = new System.Drawing.Size(156, 15);
            this.lblStartArgs.TabIndex = 19;
            this.lblStartArgs.Text = "Argumentos de inicialização";
            // 
            // btnFindDir
            // 
            this.btnFindDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindDir.Location = new System.Drawing.Point(275, 26);
            this.btnFindDir.Name = "btnFindDir";
            this.btnFindDir.Size = new System.Drawing.Size(75, 23);
            this.btnFindDir.TabIndex = 18;
            this.btnFindDir.Text = "Procurar";
            this.btnFindDir.UseVisualStyleBackColor = true;
            this.btnFindDir.Click += new System.EventHandler(this.btnFindDir_Click);
            // 
            // txtWorkdir
            // 
            this.txtWorkdir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkdir.Location = new System.Drawing.Point(5, 27);
            this.txtWorkdir.Name = "txtWorkdir";
            this.txtWorkdir.Size = new System.Drawing.Size(264, 23);
            this.txtWorkdir.TabIndex = 17;
            // 
            // lblWorkDir
            // 
            this.lblWorkDir.AutoSize = true;
            this.lblWorkDir.Location = new System.Drawing.Point(5, 9);
            this.lblWorkDir.Name = "lblWorkDir";
            this.lblWorkDir.Size = new System.Drawing.Size(136, 15);
            this.lblWorkDir.TabIndex = 16;
            this.lblWorkDir.Text = "Pasta de funcionamento";
            // 
            // btnFindApp
            // 
            this.btnFindApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindApp.Location = new System.Drawing.Point(272, 74);
            this.btnFindApp.Name = "btnFindApp";
            this.btnFindApp.Size = new System.Drawing.Size(75, 23);
            this.btnFindApp.TabIndex = 5;
            this.btnFindApp.Text = "Procurar";
            this.btnFindApp.UseVisualStyleBackColor = true;
            this.btnFindApp.Click += new System.EventHandler(this.btnFindApp_Click);
            // 
            // txtProgramPath
            // 
            this.txtProgramPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProgramPath.Location = new System.Drawing.Point(5, 75);
            this.txtProgramPath.Name = "txtProgramPath";
            this.txtProgramPath.Size = new System.Drawing.Size(261, 23);
            this.txtProgramPath.TabIndex = 4;
            // 
            // lblProgramPath
            // 
            this.lblProgramPath.AutoSize = true;
            this.lblProgramPath.Location = new System.Drawing.Point(5, 57);
            this.lblProgramPath.Name = "lblProgramPath";
            this.lblProgramPath.Size = new System.Drawing.Size(128, 15);
            this.lblProgramPath.TabIndex = 3;
            this.lblProgramPath.Text = "Caminho do programa";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(5, 28);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(338, 23);
            this.txtName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(40, 15);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Nome";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tpApp);
            this.tabControl.Controls.Add(this.tpWorking);
            this.tabControl.Controls.Add(this.tpAdvanced);
            this.tabControl.Controls.Add(this.tpIgnored);
            this.tabControl.Location = new System.Drawing.Point(3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(360, 206);
            this.tabControl.TabIndex = 0;
            // 
            // tpApp
            // 
            this.tpApp.Controls.Add(this.lblActive);
            this.tpApp.Controls.Add(this.cbActive);
            this.tpApp.Controls.Add(this.txtName);
            this.tpApp.Controls.Add(this.lblName);
            this.tpApp.Controls.Add(this.txtProgramPath);
            this.tpApp.Controls.Add(this.lblProgramPath);
            this.tpApp.Controls.Add(this.btnFindApp);
            this.tpApp.Location = new System.Drawing.Point(4, 24);
            this.tpApp.Name = "tpApp";
            this.tpApp.Padding = new System.Windows.Forms.Padding(3);
            this.tpApp.Size = new System.Drawing.Size(352, 178);
            this.tpApp.TabIndex = 0;
            this.tpApp.Text = "Aplicativo";
            this.tpApp.UseVisualStyleBackColor = true;
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Location = new System.Drawing.Point(5, 111);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(35, 15);
            this.lblActive.TabIndex = 6;
            this.lblActive.Text = "Ativo";
            // 
            // cbActive
            // 
            this.cbActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActive.FormattingEnabled = true;
            this.cbActive.Items.AddRange(new object[] {
            "Não",
            "Sim"});
            this.cbActive.Location = new System.Drawing.Point(5, 129);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(63, 23);
            this.cbActive.TabIndex = 7;
            // 
            // tpWorking
            // 
            this.tpWorking.Controls.Add(this.groupBox1);
            this.tpWorking.Controls.Add(this.lblDaysOfWeek);
            this.tpWorking.Controls.Add(this.clbDays);
            this.tpWorking.Location = new System.Drawing.Point(4, 24);
            this.tpWorking.Name = "tpWorking";
            this.tpWorking.Padding = new System.Windows.Forms.Padding(3);
            this.tpWorking.Size = new System.Drawing.Size(352, 178);
            this.tpWorking.TabIndex = 1;
            this.tpWorking.Text = "Horários";
            this.tpWorking.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbAutoClose);
            this.groupBox1.Controls.Add(this.dtpStartAt);
            this.groupBox1.Controls.Add(this.dtpEntAt);
            this.groupBox1.Controls.Add(this.lblStartTime);
            this.groupBox1.Controls.Add(this.lblEndTime);
            this.groupBox1.Location = new System.Drawing.Point(163, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 164);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Horários";
            // 
            // cbAutoClose
            // 
            this.cbAutoClose.AutoSize = true;
            this.cbAutoClose.Location = new System.Drawing.Point(12, 137);
            this.cbAutoClose.Name = "cbAutoClose";
            this.cbAutoClose.Size = new System.Drawing.Size(166, 19);
            this.cbAutoClose.TabIndex = 15;
            this.cbAutoClose.Text = "Encerrar automáticamente";
            this.cbAutoClose.UseVisualStyleBackColor = true;
            // 
            // lblDaysOfWeek
            // 
            this.lblDaysOfWeek.AutoSize = true;
            this.lblDaysOfWeek.Location = new System.Drawing.Point(5, 6);
            this.lblDaysOfWeek.Name = "lblDaysOfWeek";
            this.lblDaysOfWeek.Size = new System.Drawing.Size(89, 15);
            this.lblDaysOfWeek.TabIndex = 8;
            this.lblDaysOfWeek.Text = "Dias da semana";
            // 
            // tpAdvanced
            // 
            this.tpAdvanced.Controls.Add(this.txtAppArgs);
            this.tpAdvanced.Controls.Add(this.lblWorkDir);
            this.tpAdvanced.Controls.Add(this.txtWorkdir);
            this.tpAdvanced.Controls.Add(this.btnFindDir);
            this.tpAdvanced.Controls.Add(this.lblStartArgs);
            this.tpAdvanced.Location = new System.Drawing.Point(4, 24);
            this.tpAdvanced.Name = "tpAdvanced";
            this.tpAdvanced.Size = new System.Drawing.Size(352, 178);
            this.tpAdvanced.TabIndex = 2;
            this.tpAdvanced.Text = "Avançado";
            this.tpAdvanced.UseVisualStyleBackColor = true;
            // 
            // tpIgnored
            // 
            this.tpIgnored.Controls.Add(this.llRemove);
            this.tpIgnored.Controls.Add(this.llAdd);
            this.tpIgnored.Controls.Add(this.lbIgnoredDays);
            this.tpIgnored.Location = new System.Drawing.Point(4, 24);
            this.tpIgnored.Name = "tpIgnored";
            this.tpIgnored.Size = new System.Drawing.Size(352, 178);
            this.tpIgnored.TabIndex = 3;
            this.tpIgnored.Text = "Dias ignorados";
            this.tpIgnored.UseVisualStyleBackColor = true;
            // 
            // llRemove
            // 
            this.llRemove.AutoSize = true;
            this.llRemove.Location = new System.Drawing.Point(69, 158);
            this.llRemove.Name = "llRemove";
            this.llRemove.Size = new System.Drawing.Size(54, 15);
            this.llRemove.TabIndex = 23;
            this.llRemove.TabStop = true;
            this.llRemove.Text = "Remover";
            this.llRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRemove_LinkClicked);
            // 
            // llAdd
            // 
            this.llAdd.AutoSize = true;
            this.llAdd.Location = new System.Drawing.Point(5, 158);
            this.llAdd.Name = "llAdd";
            this.llAdd.Size = new System.Drawing.Size(58, 15);
            this.llAdd.TabIndex = 22;
            this.llAdd.TabStop = true;
            this.llAdd.Text = "Adicionar";
            this.llAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAdd_LinkClicked);
            // 
            // lbIgnoredDays
            // 
            this.lbIgnoredDays.FormattingEnabled = true;
            this.lbIgnoredDays.ItemHeight = 15;
            this.lbIgnoredDays.Location = new System.Drawing.Point(5, 3);
            this.lbIgnoredDays.Name = "lbIgnoredDays";
            this.lbIgnoredDays.Size = new System.Drawing.Size(340, 154);
            this.lbIgnoredDays.TabIndex = 21;
            this.lbIgnoredDays.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbIgnoredDays_KeyUp);
            // 
            // ProgramDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 241);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProgramDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adicionar ou modificar uma programa";
            this.tabControl.ResumeLayout(false);
            this.tpApp.ResumeLayout(false);
            this.tpApp.PerformLayout();
            this.tpWorking.ResumeLayout(false);
            this.tpWorking.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpAdvanced.ResumeLayout(false);
            this.tpAdvanced.PerformLayout();
            this.tpIgnored.ResumeLayout(false);
            this.tpIgnored.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.DateTimePicker dtpEntAt;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.DateTimePicker dtpStartAt;
        private System.Windows.Forms.CheckedListBox clbDays;
        private System.Windows.Forms.TextBox txtAppArgs;
        private System.Windows.Forms.Label lblStartArgs;
        private System.Windows.Forms.Button btnFindDir;
        private System.Windows.Forms.TextBox txtWorkdir;
        private System.Windows.Forms.Label lblWorkDir;
        private System.Windows.Forms.Button btnFindApp;
        private System.Windows.Forms.TextBox txtProgramPath;
        private System.Windows.Forms.Label lblProgramPath;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpApp;
        private System.Windows.Forms.TabPage tpWorking;
        private System.Windows.Forms.TabPage tpAdvanced;
        private System.Windows.Forms.Label lblDaysOfWeek;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.ComboBox cbActive;
        private System.Windows.Forms.CheckBox cbAutoClose;
        private System.Windows.Forms.TabPage tpIgnored;
        private System.Windows.Forms.LinkLabel llAdd;
        private System.Windows.Forms.ListBox lbIgnoredDays;
        private System.Windows.Forms.LinkLabel llRemove;
    }
}