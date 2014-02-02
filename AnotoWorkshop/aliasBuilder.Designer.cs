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
            this.label3 = new System.Windows.Forms.Label();
            this.txt_QueryFrom = new System.Windows.Forms.TextBox();
            this.txt_QueryWhere = new System.Windows.Forms.TextBox();
            this.txt_QueryVars = new System.Windows.Forms.TextBox();
            this.btn_VarEdit = new System.Windows.Forms.Button();
            this.btn_VarAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_VarRemove = new System.Windows.Forms.Button();
            this.btnVarSave = new System.Windows.Forms.Button();
            this.lstv_Variables = new System.Windows.Forms.ListView();
            this.Aliases = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txt_varQuery = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Where";
            // 
            // txt_QueryFrom
            // 
            this.txt_QueryFrom.Location = new System.Drawing.Point(442, 241);
            this.txt_QueryFrom.Multiline = true;
            this.txt_QueryFrom.Name = "txt_QueryFrom";
            this.txt_QueryFrom.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_QueryFrom.Size = new System.Drawing.Size(775, 178);
            this.txt_QueryFrom.TabIndex = 23;
            this.txt_QueryFrom.WordWrap = false;
            // 
            // txt_QueryWhere
            // 
            this.txt_QueryWhere.Location = new System.Drawing.Point(442, 453);
            this.txt_QueryWhere.Multiline = true;
            this.txt_QueryWhere.Name = "txt_QueryWhere";
            this.txt_QueryWhere.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_QueryWhere.Size = new System.Drawing.Size(775, 218);
            this.txt_QueryWhere.TabIndex = 19;
            this.txt_QueryWhere.WordWrap = false;
            // 
            // txt_QueryVars
            // 
            this.txt_QueryVars.Location = new System.Drawing.Point(666, 11);
            this.txt_QueryVars.Multiline = true;
            this.txt_QueryVars.Name = "txt_QueryVars";
            this.txt_QueryVars.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_QueryVars.Size = new System.Drawing.Size(551, 178);
            this.txt_QueryVars.TabIndex = 16;
            this.txt_QueryVars.WordWrap = false;
            // 
            // btn_VarEdit
            // 
            this.btn_VarEdit.Location = new System.Drawing.Point(523, 194);
            this.btn_VarEdit.Name = "btn_VarEdit";
            this.btn_VarEdit.Size = new System.Drawing.Size(61, 23);
            this.btn_VarEdit.TabIndex = 22;
            this.btn_VarEdit.Text = "Edit";
            this.btn_VarEdit.UseVisualStyleBackColor = true;
            // 
            // btn_VarAdd
            // 
            this.btn_VarAdd.Location = new System.Drawing.Point(442, 194);
            this.btn_VarAdd.Name = "btn_VarAdd";
            this.btn_VarAdd.Size = new System.Drawing.Size(61, 23);
            this.btn_VarAdd.TabIndex = 21;
            this.btn_VarAdd.Text = "Add";
            this.btn_VarAdd.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "From";
            // 
            // btn_VarRemove
            // 
            this.btn_VarRemove.Location = new System.Drawing.Point(598, 194);
            this.btn_VarRemove.Name = "btn_VarRemove";
            this.btn_VarRemove.Size = new System.Drawing.Size(61, 23);
            this.btn_VarRemove.TabIndex = 17;
            this.btn_VarRemove.Text = "Remove";
            this.btn_VarRemove.UseVisualStyleBackColor = true;
            // 
            // btnVarSave
            // 
            this.btnVarSave.Location = new System.Drawing.Point(10, 648);
            this.btnVarSave.Name = "btnVarSave";
            this.btnVarSave.Size = new System.Drawing.Size(105, 23);
            this.btnVarSave.TabIndex = 18;
            this.btnVarSave.Text = "Save";
            this.btnVarSave.UseVisualStyleBackColor = true;
            // 
            // lstv_Variables
            // 
            this.lstv_Variables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Aliases});
            this.lstv_Variables.Location = new System.Drawing.Point(442, 11);
            this.lstv_Variables.Name = "lstv_Variables";
            this.lstv_Variables.Size = new System.Drawing.Size(218, 178);
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
            // txt_varQuery
            // 
            this.txt_varQuery.AcceptsReturn = true;
            this.txt_varQuery.Location = new System.Drawing.Point(10, 53);
            this.txt_varQuery.Multiline = true;
            this.txt_varQuery.Name = "txt_varQuery";
            this.txt_varQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_varQuery.Size = new System.Drawing.Size(423, 589);
            this.txt_varQuery.TabIndex = 14;
            this.txt_varQuery.WordWrap = false;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(164, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(61, 23);
            this.btnTest.TabIndex = 25;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // aliasBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 694);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_QueryFrom);
            this.Controls.Add(this.txt_QueryWhere);
            this.Controls.Add(this.txt_QueryVars);
            this.Controls.Add(this.btn_VarEdit);
            this.Controls.Add(this.btn_VarAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_VarRemove);
            this.Controls.Add(this.btnVarSave);
            this.Controls.Add(this.lstv_Variables);
            this.Controls.Add(this.txt_varQuery);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "aliasBuilder";
            this.Text = "Alias Builder";
            this.Load += new System.EventHandler(this.variablesBuilder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_QueryFrom;
        private System.Windows.Forms.TextBox txt_QueryWhere;
        private System.Windows.Forms.TextBox txt_QueryVars;
        private System.Windows.Forms.Button btn_VarEdit;
        private System.Windows.Forms.Button btn_VarAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_VarRemove;
        private System.Windows.Forms.Button btnVarSave;
        private System.Windows.Forms.ListView lstv_Variables;
        private System.Windows.Forms.ColumnHeader Aliases;
        private System.Windows.Forms.TextBox txt_varQuery;
        private System.Windows.Forms.Button btnTest;
    }
}