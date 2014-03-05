namespace AnotoWorkshop
{
    partial class ImportWizard
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
            this.lstvForms = new System.Windows.Forms.ListView();
            this.lstvFormatSets = new System.Windows.Forms.ListView();
            this.btnFinish = new System.Windows.Forms.Button();
            this.lblTestFormat = new System.Windows.Forms.Label();
            this.txtSetName = new System.Windows.Forms.TextBox();
            this.btnSaveSetName = new System.Windows.Forms.Button();
            this.cmbFontList = new System.Windows.Forms.ComboBox();
            this.cmbFontSizes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFontWeight = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveFormName = new System.Windows.Forms.Button();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstvForms
            // 
            this.lstvForms.Location = new System.Drawing.Point(10, 11);
            this.lstvForms.Name = "lstvForms";
            this.lstvForms.Size = new System.Drawing.Size(221, 281);
            this.lstvForms.TabIndex = 20;
            this.lstvForms.UseCompatibleStateImageBehavior = false;
            this.lstvForms.View = System.Windows.Forms.View.List;
            this.lstvForms.Click += new System.EventHandler(this.lstvForms_Click);
            this.lstvForms.DoubleClick += new System.EventHandler(this.lstvForms_DoubleClick);
            // 
            // lstvFormatSets
            // 
            this.lstvFormatSets.Location = new System.Drawing.Point(507, 74);
            this.lstvFormatSets.Name = "lstvFormatSets";
            this.lstvFormatSets.Size = new System.Drawing.Size(221, 281);
            this.lstvFormatSets.TabIndex = 21;
            this.lstvFormatSets.UseCompatibleStateImageBehavior = false;
            this.lstvFormatSets.View = System.Windows.Forms.View.List;
            this.lstvFormatSets.Click += new System.EventHandler(this.lstvFormatSets_DoubleClick);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(361, 269);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 22;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lblTestFormat
            // 
            this.lblTestFormat.AutoSize = true;
            this.lblTestFormat.Location = new System.Drawing.Point(236, 28);
            this.lblTestFormat.Name = "lblTestFormat";
            this.lblTestFormat.Size = new System.Drawing.Size(63, 13);
            this.lblTestFormat.TabIndex = 23;
            this.lblTestFormat.Text = "Format Test";
            this.lblTestFormat.Visible = false;
            // 
            // txtSetName
            // 
            this.txtSetName.Location = new System.Drawing.Point(379, 89);
            this.txtSetName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSetName.Name = "txtSetName";
            this.txtSetName.Size = new System.Drawing.Size(122, 20);
            this.txtSetName.TabIndex = 24;
            this.txtSetName.Visible = false;
            // 
            // btnSaveSetName
            // 
            this.btnSaveSetName.Location = new System.Drawing.Point(457, 194);
            this.btnSaveSetName.Name = "btnSaveSetName";
            this.btnSaveSetName.Size = new System.Drawing.Size(43, 23);
            this.btnSaveSetName.TabIndex = 25;
            this.btnSaveSetName.Text = "Save";
            this.btnSaveSetName.UseVisualStyleBackColor = true;
            this.btnSaveSetName.Visible = false;
            this.btnSaveSetName.Click += new System.EventHandler(this.btnSaveSetName_Click);
            // 
            // cmbFontList
            // 
            this.cmbFontList.FormattingEnabled = true;
            this.cmbFontList.Location = new System.Drawing.Point(379, 144);
            this.cmbFontList.Name = "cmbFontList";
            this.cmbFontList.Size = new System.Drawing.Size(121, 21);
            this.cmbFontList.TabIndex = 26;
            this.cmbFontList.Visible = false;
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
            this.cmbFontSizes.Location = new System.Drawing.Point(379, 114);
            this.cmbFontSizes.Name = "cmbFontSizes";
            this.cmbFontSizes.Size = new System.Drawing.Size(121, 21);
            this.cmbFontSizes.TabIndex = 27;
            this.cmbFontSizes.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Format Set Name";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Format Set Size";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Format Set Font";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 32;
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
            this.cmbFontWeight.Location = new System.Drawing.Point(379, 168);
            this.cmbFontWeight.Name = "cmbFontWeight";
            this.cmbFontWeight.Size = new System.Drawing.Size(121, 21);
            this.cmbFontWeight.TabIndex = 31;
            this.cmbFontWeight.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Form Name";
            this.label5.Visible = false;
            // 
            // btnSaveFormName
            // 
            this.btnSaveFormName.Location = new System.Drawing.Point(458, 230);
            this.btnSaveFormName.Name = "btnSaveFormName";
            this.btnSaveFormName.Size = new System.Drawing.Size(43, 20);
            this.btnSaveFormName.TabIndex = 34;
            this.btnSaveFormName.Text = "Save";
            this.btnSaveFormName.UseVisualStyleBackColor = true;
            this.btnSaveFormName.Visible = false;
            this.btnSaveFormName.Click += new System.EventHandler(this.btnSaveFormName_Click);
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(347, 230);
            this.txtFormName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(107, 20);
            this.txtFormName.TabIndex = 33;
            this.txtFormName.Visible = false;
            // 
            // importWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 369);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSaveFormName);
            this.Controls.Add(this.txtFormName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbFontWeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFontSizes);
            this.Controls.Add(this.cmbFontList);
            this.Controls.Add(this.btnSaveSetName);
            this.Controls.Add(this.txtSetName);
            this.Controls.Add(this.lblTestFormat);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.lstvFormatSets);
            this.Controls.Add(this.lstvForms);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ImportWizard";
            this.Text = "Import Wizard";
            this.Load += new System.EventHandler(this.importWizard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvForms;
        private System.Windows.Forms.ListView lstvFormatSets;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Label lblTestFormat;
        private System.Windows.Forms.TextBox txtSetName;
        private System.Windows.Forms.Button btnSaveSetName;
        private System.Windows.Forms.ComboBox cmbFontList;
        private System.Windows.Forms.ComboBox cmbFontSizes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFontWeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveFormName;
        private System.Windows.Forms.TextBox txtFormName;
    }
}