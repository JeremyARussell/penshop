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
            this.button1 = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.lstvFormatSets = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Load Prop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(36, 54);
            this.txtText.Margin = new System.Windows.Forms.Padding(2);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(158, 20);
            this.txtText.TabIndex = 27;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(199, 54);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(64, 23);
            this.btnTest.TabIndex = 26;
            this.btnTest.Text = "Save Prop";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(303, 510);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(182, 23);
            this.btnSaveFile.TabIndex = 29;
            this.btnSaveFile.Text = "Save Settings File";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // lstvFormatSets
            // 
            this.lstvFormatSets.Location = new System.Drawing.Point(36, 92);
            this.lstvFormatSets.Name = "lstvFormatSets";
            this.lstvFormatSets.Size = new System.Drawing.Size(336, 225);
            this.lstvFormatSets.TabIndex = 30;
            this.lstvFormatSets.UseCompatibleStateImageBehavior = false;
            this.lstvFormatSets.View = System.Windows.Forms.View.List;
            // 
            // settingsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 545);
            this.Controls.Add(this.lstvFormatSets);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.btnTest);
            this.Name = "settingsScreen";
            this.Text = "settingsScreen";
            this.Load += new System.EventHandler(this.settingsScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.ListView lstvFormatSets;
    }
}