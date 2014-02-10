namespace AnotoWorkshop
{
    partial class loadingScreen
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
            this.btnLoadSettings = new System.Windows.Forms.Button();
            this.btlLoadAliasBuilder = new System.Windows.Forms.Button();
            this.btnOpenSQLform = new System.Windows.Forms.Button();
            this.btnImportForms = new System.Windows.Forms.Button();
            this.btnNewForm = new System.Windows.Forms.Button();
            this.btnRemoveForm = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstvForms
            // 
            this.lstvForms.Location = new System.Drawing.Point(10, 11);
            this.lstvForms.Name = "lstvForms";
            this.lstvForms.Size = new System.Drawing.Size(221, 281);
            this.lstvForms.TabIndex = 19;
            this.lstvForms.UseCompatibleStateImageBehavior = false;
            this.lstvForms.View = System.Windows.Forms.View.List;
            this.lstvForms.DoubleClick += new System.EventHandler(this.lstvForms_DoubleClick);
            // 
            // btnLoadSettings
            // 
            this.btnLoadSettings.Image = global::AnotoWorkshop.Properties.Resources.cog_24x24;
            this.btnLoadSettings.Location = new System.Drawing.Point(470, 11);
            this.btnLoadSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadSettings.Name = "btnLoadSettings";
            this.btnLoadSettings.Size = new System.Drawing.Size(32, 32);
            this.btnLoadSettings.TabIndex = 20;
            this.btnLoadSettings.UseVisualStyleBackColor = true;
            this.btnLoadSettings.Click += new System.EventHandler(this.btnLoadSettings_Click);
            // 
            // btlLoadAliasBuilder
            // 
            this.btlLoadAliasBuilder.Location = new System.Drawing.Point(428, 267);
            this.btlLoadAliasBuilder.Margin = new System.Windows.Forms.Padding(2);
            this.btlLoadAliasBuilder.Name = "btlLoadAliasBuilder";
            this.btlLoadAliasBuilder.Size = new System.Drawing.Size(74, 24);
            this.btlLoadAliasBuilder.TabIndex = 21;
            this.btlLoadAliasBuilder.Text = "Variables";
            this.btlLoadAliasBuilder.UseVisualStyleBackColor = true;
            this.btlLoadAliasBuilder.Click += new System.EventHandler(this.btlLoadAliasBuilder_Click);
            // 
            // btnOpenSQLform
            // 
            this.btnOpenSQLform.Location = new System.Drawing.Point(428, 239);
            this.btnOpenSQLform.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenSQLform.Name = "btnOpenSQLform";
            this.btnOpenSQLform.Size = new System.Drawing.Size(74, 24);
            this.btnOpenSQLform.TabIndex = 22;
            this.btnOpenSQLform.Text = "SQL Query";
            this.btnOpenSQLform.UseVisualStyleBackColor = true;
            this.btnOpenSQLform.Click += new System.EventHandler(this.btnOpenSQLform_Click);
            // 
            // btnImportForms
            // 
            this.btnImportForms.Location = new System.Drawing.Point(428, 211);
            this.btnImportForms.Name = "btnImportForms";
            this.btnImportForms.Size = new System.Drawing.Size(74, 23);
            this.btnImportForms.TabIndex = 26;
            this.btnImportForms.Text = "Import...";
            this.btnImportForms.UseVisualStyleBackColor = true;
            this.btnImportForms.Click += new System.EventHandler(this.btnImportForms_Click);
            // 
            // btnNewForm
            // 
            this.btnNewForm.Location = new System.Drawing.Point(237, 12);
            this.btnNewForm.Name = "btnNewForm";
            this.btnNewForm.Size = new System.Drawing.Size(60, 23);
            this.btnNewForm.TabIndex = 27;
            this.btnNewForm.Text = "New";
            this.btnNewForm.UseVisualStyleBackColor = true;
            this.btnNewForm.Click += new System.EventHandler(this.btnNewForm_Click);
            // 
            // btnRemoveForm
            // 
            this.btnRemoveForm.Location = new System.Drawing.Point(237, 41);
            this.btnRemoveForm.Name = "btnRemoveForm";
            this.btnRemoveForm.Size = new System.Drawing.Size(60, 23);
            this.btnRemoveForm.TabIndex = 28;
            this.btnRemoveForm.Text = "Remove";
            this.btnRemoveForm.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(256, 211);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 24);
            this.button1.TabIndex = 29;
            this.button1.Text = "Variables";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // loadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 301);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRemoveForm);
            this.Controls.Add(this.btnNewForm);
            this.Controls.Add(this.btnImportForms);
            this.Controls.Add(this.btnOpenSQLform);
            this.Controls.Add(this.btlLoadAliasBuilder);
            this.Controls.Add(this.btnLoadSettings);
            this.Controls.Add(this.lstvForms);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "loadingScreen";
            this.Text = "Loading Screen";
            this.Load += new System.EventHandler(this.loadingScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstvForms;
        private System.Windows.Forms.Button btnLoadSettings;
        private System.Windows.Forms.Button btlLoadAliasBuilder;
        private System.Windows.Forms.Button btnOpenSQLform;
        private System.Windows.Forms.Button btnImportForms;
        private System.Windows.Forms.Button btnNewForm;
        private System.Windows.Forms.Button btnRemoveForm;
        private System.Windows.Forms.Button button1;
    }
}