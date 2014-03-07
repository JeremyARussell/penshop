namespace AnotoWorkshop
{
    partial class labelCreator
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
            this.txtText = new System.Windows.Forms.TextBox();
            this.cmbFormatSetNames = new System.Windows.Forms.ComboBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(12, 12);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(336, 20);
            this.txtText.TabIndex = 0;
            // 
            // cmbFormatSetNames
            // 
            this.cmbFormatSetNames.FormattingEnabled = true;
            this.cmbFormatSetNames.Location = new System.Drawing.Point(202, 39);
            this.cmbFormatSetNames.Name = "cmbFormatSetNames";
            this.cmbFormatSetNames.Size = new System.Drawing.Size(147, 21);
            this.cmbFormatSetNames.TabIndex = 25;
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDone.Location = new System.Drawing.Point(12, 37);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 26;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // labelCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 72);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.cmbFormatSetNames);
            this.Controls.Add(this.txtText);
            this.Name = "labelCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Label";
            this.Load += new System.EventHandler(this.labelCreator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.ComboBox cmbFormatSetNames;
        private System.Windows.Forms.Button btnDone;
    }
}