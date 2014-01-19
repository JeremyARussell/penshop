namespace AnotoWorkshop
{
    partial class fieldSelection
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnQueryDB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lstNames = new System.Windows.Forms.ListBox();
            this.btnSelectName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fields to select from";
            // 
            // btnQueryDB
            // 
            this.btnQueryDB.Location = new System.Drawing.Point(12, 12);
            this.btnQueryDB.Name = "btnQueryDB";
            this.btnQueryDB.Size = new System.Drawing.Size(124, 23);
            this.btnQueryDB.TabIndex = 4;
            this.btnQueryDB.Text = "chng to on_load";
            this.btnQueryDB.UseVisualStyleBackColor = true;
            this.btnQueryDB.Click += new System.EventHandler(this.btnQueryDB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Future search bar";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(151, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(223, 20);
            this.textBox1.TabIndex = 6;
            // 
            // lstNames
            // 
            this.lstNames.FormattingEnabled = true;
            this.lstNames.Location = new System.Drawing.Point(151, 110);
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(223, 147);
            this.lstNames.TabIndex = 7;
            // 
            // btnSelectName
            // 
            this.btnSelectName.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelectName.Location = new System.Drawing.Point(420, 224);
            this.btnSelectName.Name = "btnSelectName";
            this.btnSelectName.Size = new System.Drawing.Size(76, 23);
            this.btnSelectName.TabIndex = 8;
            this.btnSelectName.Text = "Okay";
            this.btnSelectName.UseVisualStyleBackColor = true;
            this.btnSelectName.Click += new System.EventHandler(this.btnSelectName_Click);
            // 
            // fieldSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 282);
            this.Controls.Add(this.btnSelectName);
            this.Controls.Add(this.lstNames);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnQueryDB);
            this.Controls.Add(this.label1);
            this.Name = "fieldSelection";
            this.Text = "fieldSelection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQueryDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox lstNames;
        private System.Windows.Forms.Button btnSelectName;
    }
}