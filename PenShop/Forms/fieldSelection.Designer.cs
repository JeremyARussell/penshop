namespace PenShop
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuickSearch = new System.Windows.Forms.TextBox();
            this.btnSelectName = new System.Windows.Forms.Button();
            this.dgFields = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fields to select from";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Quick Search";
            // 
            // txtQuickSearch
            // 
            this.txtQuickSearch.Location = new System.Drawing.Point(59, 21);
            this.txtQuickSearch.Name = "txtQuickSearch";
            this.txtQuickSearch.Size = new System.Drawing.Size(223, 20);
            this.txtQuickSearch.TabIndex = 6;
            this.txtQuickSearch.TextChanged += new System.EventHandler(this.txtQuickSearch_TextChanged);
            // 
            // btnSelectName
            // 
            this.btnSelectName.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelectName.Location = new System.Drawing.Point(131, 428);
            this.btnSelectName.Name = "btnSelectName";
            this.btnSelectName.Size = new System.Drawing.Size(76, 23);
            this.btnSelectName.TabIndex = 8;
            this.btnSelectName.Text = "Okay";
            this.btnSelectName.UseVisualStyleBackColor = true;
            this.btnSelectName.Click += new System.EventHandler(this.btnSelectName_Click);
            // 
            // dgFields
            // 
            this.dgFields.AllowUserToAddRows = false;
            this.dgFields.AllowUserToDeleteRows = false;
            this.dgFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFields.Location = new System.Drawing.Point(12, 60);
            this.dgFields.Name = "dgFields";
            this.dgFields.ReadOnly = true;
            this.dgFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFields.Size = new System.Drawing.Size(316, 362);
            this.dgFields.TabIndex = 9;
            // 
            // fieldSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 453);
            this.Controls.Add(this.dgFields);
            this.Controls.Add(this.btnSelectName);
            this.Controls.Add(this.txtQuickSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "fieldSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "fieldSelection";
            this.Load += new System.EventHandler(this.fieldSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuickSearch;
        private System.Windows.Forms.Button btnSelectName;
        private System.Windows.Forms.DataGridView dgFields;
    }
}