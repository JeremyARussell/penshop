namespace AnotoWorkshop
{
    partial class FontEditorMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbFontSizes = new System.Windows.Forms.ComboBox();
            this.cmbFontList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.cmbFontSizes.Location = new System.Drawing.Point(151, 3);
            this.cmbFontSizes.Name = "cmbFontSizes";
            this.cmbFontSizes.Size = new System.Drawing.Size(55, 21);
            this.cmbFontSizes.TabIndex = 38;
            // 
            // cmbFontList
            // 
            this.cmbFontList.DropDownHeight = 222;
            this.cmbFontList.FormattingEnabled = true;
            this.cmbFontList.IntegralHeight = false;
            this.cmbFontList.Location = new System.Drawing.Point(3, 3);
            this.cmbFontList.Name = "cmbFontList";
            this.cmbFontList.Size = new System.Drawing.Size(142, 21);
            this.cmbFontList.TabIndex = 37;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(212, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 39;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(243, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 23);
            this.button2.TabIndex = 40;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(274, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 23);
            this.button3.TabIndex = 41;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // FontEditorMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbFontSizes);
            this.Controls.Add(this.cmbFontList);
            this.Name = "FontEditorMenu";
            this.Size = new System.Drawing.Size(317, 28);
            this.Load += new System.EventHandler(this.fontEditorMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFontSizes;
        private System.Windows.Forms.ComboBox cmbFontList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
