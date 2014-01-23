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
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtFormsFolder = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.lstvFormatSets = new System.Windows.Forms.ListView();
            this.txtExportFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFontWeight = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFontSizes = new System.Windows.Forms.ComboBox();
            this.cmbFontList = new System.Windows.Forms.ComboBox();
            this.btnSaveSetName = new System.Windows.Forms.Button();
            this.txtSetName = new System.Windows.Forms.TextBox();
            this.lblTestFormat = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveFormsFolders = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNewFormatSet = new System.Windows.Forms.Button();
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
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(429, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnSaveFile.Location = new System.Drawing.Point(339, 12);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(84, 23);
            this.btnSaveFile.TabIndex = 29;
            this.btnSaveFile.Text = "Save Settings";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // lstvFormatSets
            // 
            this.lstvFormatSets.Location = new System.Drawing.Point(12, 356);
            this.lstvFormatSets.Name = "lstvFormatSets";
            this.lstvFormatSets.Size = new System.Drawing.Size(238, 177);
            this.lstvFormatSets.TabIndex = 30;
            this.lstvFormatSets.UseCompatibleStateImageBehavior = false;
            this.lstvFormatSets.View = System.Windows.Forms.View.List;
            this.lstvFormatSets.Click += new System.EventHandler(this.lstvFormatSets_Click);
            // 
            // txtExportFolder
            // 
            this.txtExportFolder.Location = new System.Drawing.Point(85, 39);
            this.txtExportFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtExportFolder.Name = "txtExportFolder";
            this.txtExportFolder.Size = new System.Drawing.Size(158, 20);
            this.txtExportFolder.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 443);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Format Set Weight";
            // 
            // cmbFontWeight
            // 
            this.cmbFontWeight.FormattingEnabled = true;
            this.cmbFontWeight.Items.AddRange(new object[] {
            "normal",
            "bold",
            "italic"});
            this.cmbFontWeight.Location = new System.Drawing.Point(364, 441);
            this.cmbFontWeight.Name = "cmbFontWeight";
            this.cmbFontWeight.Size = new System.Drawing.Size(121, 21);
            this.cmbFontWeight.TabIndex = 40;
            this.cmbFontWeight.SelectedValueChanged += new System.EventHandler(this.cmbFontWeight_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 416);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Format Set Font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Format Set Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Format Set Name";
            // 
            // cmbFontSizes
            // 
            this.cmbFontSizes.FormattingEnabled = true;
            this.cmbFontSizes.Items.AddRange(new object[] {
            "6",
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.cmbFontSizes.Location = new System.Drawing.Point(364, 387);
            this.cmbFontSizes.Name = "cmbFontSizes";
            this.cmbFontSizes.Size = new System.Drawing.Size(121, 21);
            this.cmbFontSizes.TabIndex = 36;
            this.cmbFontSizes.SelectedValueChanged += new System.EventHandler(this.cmbFontSizes_SelectedValueChanged);
            // 
            // cmbFontList
            // 
            this.cmbFontList.FormattingEnabled = true;
            this.cmbFontList.Location = new System.Drawing.Point(364, 417);
            this.cmbFontList.Name = "cmbFontList";
            this.cmbFontList.Size = new System.Drawing.Size(121, 21);
            this.cmbFontList.TabIndex = 35;
            this.cmbFontList.SelectedValueChanged += new System.EventHandler(this.cmbFontList_SelectedValueChanged);
            // 
            // btnSaveSetName
            // 
            this.btnSaveSetName.Location = new System.Drawing.Point(442, 467);
            this.btnSaveSetName.Name = "btnSaveSetName";
            this.btnSaveSetName.Size = new System.Drawing.Size(43, 23);
            this.btnSaveSetName.TabIndex = 34;
            this.btnSaveSetName.Text = "Save";
            this.btnSaveSetName.UseVisualStyleBackColor = true;
            this.btnSaveSetName.Click += new System.EventHandler(this.btnSaveSetName_Click);
            // 
            // txtSetName
            // 
            this.txtSetName.Location = new System.Drawing.Point(364, 362);
            this.txtSetName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSetName.Name = "txtSetName";
            this.txtSetName.Size = new System.Drawing.Size(122, 20);
            this.txtSetName.TabIndex = 33;
            this.txtSetName.TextChanged += new System.EventHandler(this.txtSetName_TextChanged);
            // 
            // lblTestFormat
            // 
            this.lblTestFormat.AutoSize = true;
            this.lblTestFormat.Location = new System.Drawing.Point(12, 278);
            this.lblTestFormat.Name = "lblTestFormat";
            this.lblTestFormat.Size = new System.Drawing.Size(63, 13);
            this.lblTestFormat.TabIndex = 42;
            this.lblTestFormat.Text = "Format Test";
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
            // btnNewFormatSet
            // 
            this.btnNewFormatSet.Location = new System.Drawing.Point(256, 510);
            this.btnNewFormatSet.Name = "btnNewFormatSet";
            this.btnNewFormatSet.Size = new System.Drawing.Size(46, 23);
            this.btnNewFormatSet.TabIndex = 46;
            this.btnNewFormatSet.Text = "New...";
            this.btnNewFormatSet.UseVisualStyleBackColor = true;
            this.btnNewFormatSet.Click += new System.EventHandler(this.btnNewFormatSet_Click);
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
            this.txtDbPass.Size = new System.Drawing.Size(158, 20);
            this.txtDbPass.TabIndex = 50;
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
            this.Controls.Add(this.btnNewFormatSet);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSaveFormsFolders);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTestFormat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbFontWeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFontSizes);
            this.Controls.Add(this.cmbFontList);
            this.Controls.Add(this.btnSaveSetName);
            this.Controls.Add(this.txtSetName);
            this.Controls.Add(this.txtExportFolder);
            this.Controls.Add(this.lstvFormatSets);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtFormsFolder);
            this.Controls.Add(this.shapeContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "settingsScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.settingsScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmrTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtFormsFolder;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.ListView lstvFormatSets;
        private System.Windows.Forms.TextBox txtExportFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFontWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFontSizes;
        private System.Windows.Forms.ComboBox cmbFontList;
        private System.Windows.Forms.Button btnSaveSetName;
        private System.Windows.Forms.TextBox txtSetName;
        private System.Windows.Forms.Label lblTestFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveFormsFolders;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnNewFormatSet;
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