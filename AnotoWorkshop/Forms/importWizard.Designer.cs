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
            this.btnFinish = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveFormName = new System.Windows.Forms.Button();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstvForms
            // 
            this.lstvForms.Location = new System.Drawing.Point(12, 11);
            this.lstvForms.Name = "lstvForms";
            this.lstvForms.Size = new System.Drawing.Size(221, 281);
            this.lstvForms.TabIndex = 21;
            this.lstvForms.UseCompatibleStateImageBehavior = false;
            this.lstvForms.View = System.Windows.Forms.View.List;
            this.lstvForms.Click += new System.EventHandler(this.lstvForms_DoubleClick);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(88, 334);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 22;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Form Name";
            this.label5.Visible = false;
            // 
            // btnSaveFormName
            // 
            this.btnSaveFormName.Location = new System.Drawing.Point(185, 295);
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
            this.txtFormName.Location = new System.Drawing.Point(74, 295);
            this.txtFormName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(107, 20);
            this.txtFormName.TabIndex = 33;
            this.txtFormName.Visible = false;
            // 
            // ImportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 372);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSaveFormName);
            this.Controls.Add(this.txtFormName);
            this.Controls.Add(this.btnFinish);
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
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveFormName;
        private System.Windows.Forms.TextBox txtFormName;
    }
}