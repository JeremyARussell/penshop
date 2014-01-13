namespace AnotoWorkshop
{
    partial class pngExporter
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.designPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.designPanel);
            this.splitContainer1.Size = new System.Drawing.Size(719, 740);
            this.splitContainer1.SplitterDistance = 686;
            this.splitContainer1.TabIndex = 0;
            // 
            // designPanel
            // 
            this.designPanel.AutoSize = true;
            this.designPanel.BackColor = System.Drawing.Color.Silver;
            this.designPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.designPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designPanel.Location = new System.Drawing.Point(0, 0);
            this.designPanel.Name = "designPanel";
            this.designPanel.Size = new System.Drawing.Size(719, 686);
            this.designPanel.TabIndex = 2;
            this.designPanel.Click += new System.EventHandler(this.designPanel_Click);
            this.designPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.designPanel_Paint);
            // 
            // pngExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 740);
            this.Controls.Add(this.splitContainer1);
            this.Name = "pngExporter";
            this.Text = "pngExporter";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.Panel designPanel;
    }
}