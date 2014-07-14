namespace AnotoWorkshop
{
    partial class OptionsEditorMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRemoveSubitem = new System.Windows.Forms.Button();
            this.lstvSubitems = new System.Windows.Forms.ListView();
            this.txtSubitemValue = new System.Windows.Forms.TextBox();
            this.btnAddSubitem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nmColumns = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubitemLabel = new System.Windows.Forms.TextBox();
            this.btnMoveSubitemUp = new System.Windows.Forms.Button();
            this.btnMoveSubitemDown = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRemoveSubitem
            // 
            this.btnRemoveSubitem.Location = new System.Drawing.Point(242, 126);
            this.btnRemoveSubitem.Name = "btnRemoveSubitem";
            this.btnRemoveSubitem.Size = new System.Drawing.Size(61, 23);
            this.btnRemoveSubitem.TabIndex = 11;
            this.btnRemoveSubitem.Text = "Remove";
            this.btnRemoveSubitem.UseVisualStyleBackColor = true;
            // 
            // lstvSubitems
            // 
            this.lstvSubitems.Location = new System.Drawing.Point(63, 11);
            this.lstvSubitems.Name = "lstvSubitems";
            this.lstvSubitems.Size = new System.Drawing.Size(173, 165);
            this.lstvSubitems.TabIndex = 10;
            this.lstvSubitems.UseCompatibleStateImageBehavior = false;
            this.lstvSubitems.View = System.Windows.Forms.View.List;
            // 
            // txtSubitemValue
            // 
            this.txtSubitemValue.Location = new System.Drawing.Point(10, 193);
            this.txtSubitemValue.Name = "txtSubitemValue";
            this.txtSubitemValue.Size = new System.Drawing.Size(289, 20);
            this.txtSubitemValue.TabIndex = 9;
            // 
            // btnAddSubitem
            // 
            this.btnAddSubitem.Location = new System.Drawing.Point(250, 219);
            this.btnAddSubitem.Name = "btnAddSubitem";
            this.btnAddSubitem.Size = new System.Drawing.Size(49, 23);
            this.btnAddSubitem.TabIndex = 8;
            this.btnAddSubitem.Text = "Add";
            this.btnAddSubitem.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Columns";
            // 
            // nmColumns
            // 
            this.nmColumns.Location = new System.Drawing.Point(10, 14);
            this.nmColumns.Name = "nmColumns";
            this.nmColumns.Size = new System.Drawing.Size(44, 20);
            this.nmColumns.TabIndex = 6;
            this.nmColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Label Text";
            // 
            // txtSubitemLabel
            // 
            this.txtSubitemLabel.Location = new System.Drawing.Point(10, 248);
            this.txtSubitemLabel.Name = "txtSubitemLabel";
            this.txtSubitemLabel.Size = new System.Drawing.Size(289, 20);
            this.txtSubitemLabel.TabIndex = 14;
            // 
            // btnMoveSubitemUp
            // 
            this.btnMoveSubitemUp.Location = new System.Drawing.Point(242, 56);
            this.btnMoveSubitemUp.Name = "btnMoveSubitemUp";
            this.btnMoveSubitemUp.Size = new System.Drawing.Size(49, 23);
            this.btnMoveSubitemUp.TabIndex = 15;
            this.btnMoveSubitemUp.Text = "Up";
            this.btnMoveSubitemUp.UseVisualStyleBackColor = true;
            // 
            // btnMoveSubitemDown
            // 
            this.btnMoveSubitemDown.Location = new System.Drawing.Point(242, 79);
            this.btnMoveSubitemDown.Name = "btnMoveSubitemDown";
            this.btnMoveSubitemDown.Size = new System.Drawing.Size(49, 23);
            this.btnMoveSubitemDown.TabIndex = 16;
            this.btnMoveSubitemDown.Text = "Down";
            this.btnMoveSubitemDown.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Move";
            // 
            // OptionsEditorMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnMoveSubitemDown);
            this.Controls.Add(this.btnMoveSubitemUp);
            this.Controls.Add(this.txtSubitemLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemoveSubitem);
            this.Controls.Add(this.lstvSubitems);
            this.Controls.Add(this.txtSubitemValue);
            this.Controls.Add(this.btnAddSubitem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmColumns);
            this.Name = "OptionsEditorMenu";
            this.Size = new System.Drawing.Size(312, 282);
            this.Load += new System.EventHandler(this.fontEditorMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnRemoveSubitem;
        public System.Windows.Forms.ListView lstvSubitems;
        public System.Windows.Forms.TextBox txtSubitemValue;
        public System.Windows.Forms.Button btnAddSubitem;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown nmColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtSubitemLabel;
        public System.Windows.Forms.Button btnMoveSubitemUp;
        public System.Windows.Forms.Button btnMoveSubitemDown;
        private System.Windows.Forms.Label label4;

    }
}
