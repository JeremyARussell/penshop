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
            this.txtText = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.lstvFormatSets = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFontWeight = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFontSizes = new System.Windows.Forms.ComboBox();
            this.cmbFontList = new System.Windows.Forms.ComboBox();
            this.btnSaveSetName = new System.Windows.Forms.Button();
            this.txtSetName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(416, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(106, 52);
            this.txtText.Margin = new System.Windows.Forms.Padding(2);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(158, 20);
            this.txtText.TabIndex = 27;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(326, 12);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(84, 23);
            this.btnSaveFile.TabIndex = 29;
            this.btnSaveFile.Text = "Save Settings";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // lstvFormatSets
            // 
            this.lstvFormatSets.Location = new System.Drawing.Point(12, 310);
            this.lstvFormatSets.Name = "lstvFormatSets";
            this.lstvFormatSets.Size = new System.Drawing.Size(336, 177);
            this.lstvFormatSets.TabIndex = 30;
            this.lstvFormatSets.UseCompatibleStateImageBehavior = false;
            this.lstvFormatSets.View = System.Windows.Forms.View.List;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 76);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 20);
            this.textBox1.TabIndex = 31;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(106, 100);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(158, 20);
            this.textBox2.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Format Set Weight";
            this.label4.Visible = false;
            // 
            // cmbFontWeight
            // 
            this.cmbFontWeight.FormattingEnabled = true;
            this.cmbFontWeight.Items.AddRange(new object[] {
            "normal",
            "bold",
            "italic"});
            this.cmbFontWeight.Location = new System.Drawing.Point(289, 270);
            this.cmbFontWeight.Name = "cmbFontWeight";
            this.cmbFontWeight.Size = new System.Drawing.Size(121, 21);
            this.cmbFontWeight.TabIndex = 40;
            this.cmbFontWeight.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Format Set Font";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Format Set Size";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Format Set Name";
            this.label1.Visible = false;
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
            this.cmbFontSizes.Location = new System.Drawing.Point(289, 216);
            this.cmbFontSizes.Name = "cmbFontSizes";
            this.cmbFontSizes.Size = new System.Drawing.Size(121, 21);
            this.cmbFontSizes.TabIndex = 36;
            this.cmbFontSizes.Visible = false;
            // 
            // cmbFontList
            // 
            this.cmbFontList.FormattingEnabled = true;
            this.cmbFontList.Location = new System.Drawing.Point(289, 246);
            this.cmbFontList.Name = "cmbFontList";
            this.cmbFontList.Size = new System.Drawing.Size(121, 21);
            this.cmbFontList.TabIndex = 35;
            this.cmbFontList.Visible = false;
            // 
            // btnSaveSetName
            // 
            this.btnSaveSetName.Location = new System.Drawing.Point(367, 296);
            this.btnSaveSetName.Name = "btnSaveSetName";
            this.btnSaveSetName.Size = new System.Drawing.Size(43, 23);
            this.btnSaveSetName.TabIndex = 34;
            this.btnSaveSetName.Text = "Save";
            this.btnSaveSetName.UseVisualStyleBackColor = true;
            this.btnSaveSetName.Visible = false;
            // 
            // txtSetName
            // 
            this.txtSetName.Location = new System.Drawing.Point(289, 191);
            this.txtSetName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSetName.Name = "txtSetName";
            this.txtSetName.Size = new System.Drawing.Size(122, 20);
            this.txtSetName.TabIndex = 33;
            this.txtSetName.Visible = false;
            // 
            // settingsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 545);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbFontWeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFontSizes);
            this.Controls.Add(this.cmbFontList);
            this.Controls.Add(this.btnSaveSetName);
            this.Controls.Add(this.txtSetName);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lstvFormatSets);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtText);
            this.Name = "settingsScreen";
            this.Text = "settingsScreen";
            this.Load += new System.EventHandler(this.settingsScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.ListView lstvFormatSets;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFontWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFontSizes;
        private System.Windows.Forms.ComboBox cmbFontList;
        private System.Windows.Forms.Button btnSaveSetName;
        private System.Windows.Forms.TextBox txtSetName;
    }
}