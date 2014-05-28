namespace AnotoWorkshop
{
    partial class aliasBuilder
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
            this.txt_QueryVars = new System.Windows.Forms.TextBox();
            this.btn_VarEdit = new System.Windows.Forms.Button();
            this.btn_VarAdd = new System.Windows.Forms.Button();
            this.btn_VarRemove = new System.Windows.Forms.Button();
            this.btnVarSave = new System.Windows.Forms.Button();
            this.lstv_Variables = new System.Windows.Forms.ListView();
            this.Aliases = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_QueryVars
            // 
            this.txt_QueryVars.Location = new System.Drawing.Point(268, 41);
            this.txt_QueryVars.Multiline = true;
            this.txt_QueryVars.Name = "txt_QueryVars";
            this.txt_QueryVars.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_QueryVars.Size = new System.Drawing.Size(669, 171);
            this.txt_QueryVars.TabIndex = 16;
            this.txt_QueryVars.WordWrap = false;
            // 
            // btn_VarEdit
            // 
            this.btn_VarEdit.Location = new System.Drawing.Point(126, 309);
            this.btn_VarEdit.Name = "btn_VarEdit";
            this.btn_VarEdit.Size = new System.Drawing.Size(61, 23);
            this.btn_VarEdit.TabIndex = 22;
            this.btn_VarEdit.Text = "Edit";
            this.btn_VarEdit.UseVisualStyleBackColor = true;
            // 
            // btn_VarAdd
            // 
            this.btn_VarAdd.Location = new System.Drawing.Point(45, 309);
            this.btn_VarAdd.Name = "btn_VarAdd";
            this.btn_VarAdd.Size = new System.Drawing.Size(61, 23);
            this.btn_VarAdd.TabIndex = 21;
            this.btn_VarAdd.Text = "Add";
            this.btn_VarAdd.UseVisualStyleBackColor = true;
            // 
            // btn_VarRemove
            // 
            this.btn_VarRemove.Location = new System.Drawing.Point(201, 309);
            this.btn_VarRemove.Name = "btn_VarRemove";
            this.btn_VarRemove.Size = new System.Drawing.Size(61, 23);
            this.btn_VarRemove.TabIndex = 17;
            this.btn_VarRemove.Text = "Remove";
            this.btn_VarRemove.UseVisualStyleBackColor = true;
            // 
            // btnVarSave
            // 
            this.btnVarSave.Location = new System.Drawing.Point(832, 218);
            this.btnVarSave.Name = "btnVarSave";
            this.btnVarSave.Size = new System.Drawing.Size(105, 23);
            this.btnVarSave.TabIndex = 18;
            this.btnVarSave.Text = "Save";
            this.btnVarSave.UseVisualStyleBackColor = true;
            this.btnVarSave.Click += new System.EventHandler(this.btnVarSave_Click);
            // 
            // lstv_Variables
            // 
            this.lstv_Variables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Aliases});
            this.lstv_Variables.Location = new System.Drawing.Point(12, 41);
            this.lstv_Variables.Name = "lstv_Variables";
            this.lstv_Variables.Size = new System.Drawing.Size(250, 262);
            this.lstv_Variables.TabIndex = 15;
            this.lstv_Variables.UseCompatibleStateImageBehavior = false;
            this.lstv_Variables.View = System.Windows.Forms.View.Details;
            this.lstv_Variables.DoubleClick += new System.EventHandler(this.lstv_Variables_DoubleClick);
            // 
            // Aliases
            // 
            this.Aliases.Text = "Aliases";
            this.Aliases.Width = 979;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(25, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(61, 23);
            this.btnTest.TabIndex = 25;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(832, 247);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(105, 23);
            this.btnSaveFile.TabIndex = 26;
            this.btnSaveFile.Text = "Save File";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(425, 228);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 29);
            this.lblName.TabIndex = 27;
            this.lblName.Text = "Name";
            // 
            // aliasBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 496);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txt_QueryVars);
            this.Controls.Add(this.btn_VarEdit);
            this.Controls.Add(this.btn_VarAdd);
            this.Controls.Add(this.btn_VarRemove);
            this.Controls.Add(this.btnVarSave);
            this.Controls.Add(this.lstv_Variables);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "aliasBuilder";
            this.Text = "Alias Builder";
            this.Load += new System.EventHandler(this.variablesBuilder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_QueryVars;
        private System.Windows.Forms.Button btn_VarEdit;
        private System.Windows.Forms.Button btn_VarAdd;
        private System.Windows.Forms.Button btn_VarRemove;
        private System.Windows.Forms.Button btnVarSave;
        private System.Windows.Forms.ListView lstv_Variables;
        private System.Windows.Forms.ColumnHeader Aliases;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Label lblName;
    }
}