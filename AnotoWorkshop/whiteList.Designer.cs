namespace AnotoWorkshop
{
    partial class whiteList
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.dgBlackList = new System.Windows.Forms.DataGridView();
            this.lstWhiteList = new System.Windows.Forms.ListView();
            this.btnSaveList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgBlackList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(221, 65);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "<<<";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(221, 141);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(57, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = ">>>";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // dgBlackList
            // 
            this.dgBlackList.AllowUserToAddRows = false;
            this.dgBlackList.AllowUserToDeleteRows = false;
            this.dgBlackList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBlackList.Location = new System.Drawing.Point(284, 12);
            this.dgBlackList.Name = "dgBlackList";
            this.dgBlackList.ReadOnly = true;
            this.dgBlackList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBlackList.Size = new System.Drawing.Size(400, 376);
            this.dgBlackList.TabIndex = 2;
            // 
            // lstWhiteList
            // 
            this.lstWhiteList.Location = new System.Drawing.Point(13, 13);
            this.lstWhiteList.Name = "lstWhiteList";
            this.lstWhiteList.Size = new System.Drawing.Size(202, 375);
            this.lstWhiteList.TabIndex = 3;
            this.lstWhiteList.UseCompatibleStateImageBehavior = false;
            this.lstWhiteList.View = System.Windows.Forms.View.List;
            // 
            // btnSaveList
            // 
            this.btnSaveList.Location = new System.Drawing.Point(221, 365);
            this.btnSaveList.Name = "btnSaveList";
            this.btnSaveList.Size = new System.Drawing.Size(57, 23);
            this.btnSaveList.TabIndex = 4;
            this.btnSaveList.Text = "Save";
            this.btnSaveList.UseVisualStyleBackColor = true;
            this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
            // 
            // whiteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 400);
            this.Controls.Add(this.btnSaveList);
            this.Controls.Add(this.lstWhiteList);
            this.Controls.Add(this.dgBlackList);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Name = "whiteList";
            this.Text = "Whitelist";
            this.Load += new System.EventHandler(this.whiteList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgBlackList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridView dgBlackList;
        private System.Windows.Forms.ListView lstWhiteList;
        private System.Windows.Forms.Button btnSaveList;
    }
}