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
            this.chkBoldToggle = new System.Windows.Forms.CheckBox();
            this.chkUnderlinedToggle = new System.Windows.Forms.CheckBox();
            this.chkItalicToggle = new System.Windows.Forms.CheckBox();
            this.chkStrikethroughToggle = new System.Windows.Forms.CheckBox();
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
            this.cmbFontList.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbFontList.DropDownHeight = 222;
            this.cmbFontList.FormattingEnabled = true;
            this.cmbFontList.IntegralHeight = false;
            this.cmbFontList.Location = new System.Drawing.Point(3, 3);
            this.cmbFontList.Name = "cmbFontList";
            this.cmbFontList.Size = new System.Drawing.Size(142, 21);
            this.cmbFontList.TabIndex = 37;
            // 
            // chkBoldToggle
            // 
            this.chkBoldToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBoldToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoldToggle.Location = new System.Drawing.Point(211, 2);
            this.chkBoldToggle.Name = "chkBoldToggle";
            this.chkBoldToggle.Size = new System.Drawing.Size(23, 23);
            this.chkBoldToggle.TabIndex = 42;
            this.chkBoldToggle.Text = "B";
            this.chkBoldToggle.UseVisualStyleBackColor = true;
            // 
            // chkUnderlinedToggle
            // 
            this.chkUnderlinedToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUnderlinedToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUnderlinedToggle.Location = new System.Drawing.Point(263, 2);
            this.chkUnderlinedToggle.Name = "chkUnderlinedToggle";
            this.chkUnderlinedToggle.Size = new System.Drawing.Size(23, 23);
            this.chkUnderlinedToggle.TabIndex = 43;
            this.chkUnderlinedToggle.Text = "U";
            this.chkUnderlinedToggle.UseVisualStyleBackColor = true;
            // 
            // chkItalicToggle
            // 
            this.chkItalicToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkItalicToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkItalicToggle.Location = new System.Drawing.Point(237, 2);
            this.chkItalicToggle.Name = "chkItalicToggle";
            this.chkItalicToggle.Size = new System.Drawing.Size(23, 23);
            this.chkItalicToggle.TabIndex = 44;
            this.chkItalicToggle.Text = "I";
            this.chkItalicToggle.UseVisualStyleBackColor = true;
            // 
            // chkStrikethroughToggle
            // 
            this.chkStrikethroughToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkStrikethroughToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStrikethroughToggle.Location = new System.Drawing.Point(289, 2);
            this.chkStrikethroughToggle.Name = "chkStrikethroughToggle";
            this.chkStrikethroughToggle.Size = new System.Drawing.Size(23, 23);
            this.chkStrikethroughToggle.TabIndex = 45;
            this.chkStrikethroughToggle.Text = "S";
            this.chkStrikethroughToggle.UseVisualStyleBackColor = true;
            // 
            // FontEditorMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.chkStrikethroughToggle);
            this.Controls.Add(this.chkItalicToggle);
            this.Controls.Add(this.chkUnderlinedToggle);
            this.Controls.Add(this.chkBoldToggle);
            this.Controls.Add(this.cmbFontSizes);
            this.Controls.Add(this.cmbFontList);
            this.Name = "FontEditorMenu";
            this.Size = new System.Drawing.Size(315, 28);
            this.Load += new System.EventHandler(this.fontEditorMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cmbFontList;
        public System.Windows.Forms.ComboBox cmbFontSizes;
        public System.Windows.Forms.CheckBox chkBoldToggle;
        public System.Windows.Forms.CheckBox chkUnderlinedToggle;
        public System.Windows.Forms.CheckBox chkItalicToggle;
        public System.Windows.Forms.CheckBox chkStrikethroughToggle;
    }
}
