namespace AnotoWorkshop
{
    partial class templateSelection
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
            this.btnOkay = new System.Windows.Forms.Button();
            this.chklstTemplates = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnOkay
            // 
            this.btnOkay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOkay.Location = new System.Drawing.Point(95, 339);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(75, 23);
            this.btnOkay.TabIndex = 1;
            this.btnOkay.Text = "Okay";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
            // 
            // chklstTemplates
            // 
            this.chklstTemplates.FormattingEnabled = true;
            this.chklstTemplates.Location = new System.Drawing.Point(12, 12);
            this.chklstTemplates.Name = "chklstTemplates";
            this.chklstTemplates.Size = new System.Drawing.Size(243, 319);
            this.chklstTemplates.TabIndex = 3;
            // 
            // templateSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 374);
            this.Controls.Add(this.chklstTemplates);
            this.Controls.Add(this.btnOkay);
            this.Name = "templateSelection";
            this.Text = "Select Templates";
            this.Load += new System.EventHandler(this.templateSelection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.CheckedListBox chklstTemplates;
    }
}