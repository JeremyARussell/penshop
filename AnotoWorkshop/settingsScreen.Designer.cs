namespace AnotoWorkshop
{
    partial class settingsScreen
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
            this.txtFormsFolder = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.txtExportFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveFormsFolders = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDbUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDbPass = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDbServer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.nmrTimeout = new System.Windows.Forms.NumericUpDown();
            this.btnSaveDb = new System.Windows.Forms.Button();
            this.cmbDbTrusted = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nmrTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFormsFolder
            // 
            this.txtFormsFolder.Location = new System.Drawing.Point(85, 15);
            this.txtFormsFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtFormsFolder.Name = "txtFormsFolder";
            this.txtFormsFolder.Size = new System.Drawing.Size(158, 20);
            this.txtFormsFolder.TabIndex = 27;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(410, 3);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(84, 23);
            this.btnSaveFile.TabIndex = 29;
            this.btnSaveFile.Text = "Save Settings";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // txtExportFolder
            // 
            this.txtExportFolder.Location = new System.Drawing.Point(85, 39);
            this.txtExportFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtExportFolder.Name = "txtExportFolder";
            this.txtExportFolder.Size = new System.Drawing.Size(158, 20);
            this.txtExportFolder.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Forms Folder";
            // 
            // btnSaveFormsFolders
            // 
            this.btnSaveFormsFolders.Location = new System.Drawing.Point(267, 35);
            this.btnSaveFormsFolders.Name = "btnSaveFormsFolders";
            this.btnSaveFormsFolders.Size = new System.Drawing.Size(42, 20);
            this.btnSaveFormsFolders.TabIndex = 44;
            this.btnSaveFormsFolders.Text = "Save";
            this.btnSaveFormsFolders.UseVisualStyleBackColor = true;
            this.btnSaveFormsFolders.Click += new System.EventHandler(this.btnSaveFormsFolders_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Export Folder";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(497, 538);
            this.shapeContainer1.TabIndex = 47;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 33;
            this.lineShape2.X2 = 462;
            this.lineShape2.Y1 = 258;
            this.lineShape2.Y2 = 258;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 33;
            this.lineShape1.X2 = 462;
            this.lineShape1.Y1 = 75;
            this.lineShape1.Y2 = 75;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "User";
            // 
            // txtDbUser
            // 
            this.txtDbUser.Location = new System.Drawing.Point(92, 95);
            this.txtDbUser.Margin = new System.Windows.Forms.Padding(2);
            this.txtDbUser.Name = "txtDbUser";
            this.txtDbUser.Size = new System.Drawing.Size(158, 20);
            this.txtDbUser.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "Password";
            // 
            // txtDbPass
            // 
            this.txtDbPass.Location = new System.Drawing.Point(92, 119);
            this.txtDbPass.Margin = new System.Windows.Forms.Padding(2);
            this.txtDbPass.Name = "txtDbPass";
            this.txtDbPass.PasswordChar = '*';
            this.txtDbPass.Size = new System.Drawing.Size(158, 20);
            this.txtDbPass.TabIndex = 50;
            this.txtDbPass.UseSystemPasswordChar = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 53;
            this.label9.Text = "Server";
            // 
            // txtDbServer
            // 
            this.txtDbServer.Location = new System.Drawing.Point(92, 143);
            this.txtDbServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtDbServer.Name = "txtDbServer";
            this.txtDbServer.Size = new System.Drawing.Size(158, 20);
            this.txtDbServer.TabIndex = 52;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 170);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "Trusted?";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 194);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 57;
            this.label11.Tag = "";
            this.label11.Text = "DB Name";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(92, 191);
            this.txtDbName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(158, 20);
            this.txtDbName.TabIndex = 56;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 218);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 59;
            this.label12.Text = "Timeout";
            // 
            // nmrTimeout
            // 
            this.nmrTimeout.Location = new System.Drawing.Point(94, 217);
            this.nmrTimeout.Name = "nmrTimeout";
            this.nmrTimeout.Size = new System.Drawing.Size(156, 20);
            this.nmrTimeout.TabIndex = 60;
            // 
            // btnSaveDb
            // 
            this.btnSaveDb.Location = new System.Drawing.Point(267, 218);
            this.btnSaveDb.Name = "btnSaveDb";
            this.btnSaveDb.Size = new System.Drawing.Size(43, 23);
            this.btnSaveDb.TabIndex = 61;
            this.btnSaveDb.Text = "Save";
            this.btnSaveDb.UseVisualStyleBackColor = true;
            this.btnSaveDb.Click += new System.EventHandler(this.btnSaveDb_Click);
            // 
            // cmbDbTrusted
            // 
            this.cmbDbTrusted.FormattingEnabled = true;
            this.cmbDbTrusted.Items.AddRange(new object[] {
            "yes",
            "no"});
            this.cmbDbTrusted.Location = new System.Drawing.Point(94, 165);
            this.cmbDbTrusted.Name = "cmbDbTrusted";
            this.cmbDbTrusted.Size = new System.Drawing.Size(156, 21);
            this.cmbDbTrusted.TabIndex = 62;
            // 
            // settingsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 538);
            this.Controls.Add(this.cmbDbTrusted);
            this.Controls.Add(this.btnSaveDb);
            this.Controls.Add(this.nmrTimeout);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDbName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDbServer);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDbPass);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDbUser);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSaveFormsFolders);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtExportFolder);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.txtFormsFolder);
            this.Controls.Add(this.shapeContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "settingsScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.settingsScreen_FormClosing);
            this.Load += new System.EventHandler(this.settingsScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmrTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFormsFolder;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.TextBox txtExportFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveFormsFolders;
        private System.Windows.Forms.Label label6;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDbUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDbPass;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDbServer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nmrTimeout;
        private System.Windows.Forms.Button btnSaveDb;
        private System.Windows.Forms.ComboBox cmbDbTrusted;
    }
}