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
            this.btnLoadTemplates = new System.Windows.Forms.Button();
            this.btnOkay = new System.Windows.Forms.Button();
            this.chklstTemplates = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnLoadTemplates
            // 
            this.btnLoadTemplates.Location = new System.Drawing.Point(13, 13);
            this.btnLoadTemplates.Name = "btnLoadTemplates";
            this.btnLoadTemplates.Size = new System.Drawing.Size(75, 23);
            this.btnLoadTemplates.TabIndex = 0;
            this.btnLoadTemplates.Text = "TestLoad";
            this.btnLoadTemplates.UseVisualStyleBackColor = true;
            this.btnLoadTemplates.Click += new System.EventHandler(this.btnLoadTemplates_Click);
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
            this.chklstTemplates.Location = new System.Drawing.Point(13, 42);
            this.chklstTemplates.Name = "chklstTemplates";
            this.chklstTemplates.Size = new System.Drawing.Size(242, 289);
            this.chklstTemplates.TabIndex = 3;
            // 
            // templateSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 374);
            this.Controls.Add(this.chklstTemplates);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.btnLoadTemplates);
            this.Name = "templateSelection";
            this.Text = "Select Templates";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadTemplates;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.CheckedListBox chklstTemplates;
    }
}