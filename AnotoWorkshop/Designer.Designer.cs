namespace AnotoWorkshop
{
    partial class Designer
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
            this.components = new System.ComponentModel.Container();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabDesigner = new System.Windows.Forms.TabPage();
            this.lblDebugMMode = new System.Windows.Forms.Label();
            this.btnTemplatesList = new System.Windows.Forms.Button();
            this.btnLoadSettingsScreen = new System.Windows.Forms.Button();
            this.lblVersionNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.designPanel = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.grpAddingFields = new System.Windows.Forms.GroupBox();
            this.btnAddRichLabel = new System.Windows.Forms.Button();
            this.btnAddLine = new System.Windows.Forms.Button();
            this.btnAddRectangle = new System.Windows.Forms.Button();
            this.btnAddOptionGroup = new System.Windows.Forms.Button();
            this.btnAddLabel = new System.Windows.Forms.Button();
            this.btnAddCheckBox = new System.Windows.Forms.Button();
            this.btnAddTextField = new System.Windows.Forms.Button();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.lblTotalProp = new System.Windows.Forms.Label();
            this.lblCurrentProp = new System.Windows.Forms.Label();
            this.cmbFormatSetNames = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNextProp = new System.Windows.Forms.Button();
            this.btnPrevProp = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPropText = new System.Windows.Forms.TextBox();
            this.btnPropSave = new System.Windows.Forms.Button();
            this.chkPropReadOnly = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPropFieldType = new System.Windows.Forms.TextBox();
            this.chkPropHidden = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPropHeight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPropWidth = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPropY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPropX = new System.Windows.Forms.TextBox();
            this.txtPropName = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btnRefreshFieldTree = new System.Windows.Forms.Button();
            this.trvFieldList = new System.Windows.Forms.TreeView();
            this.btnSaveForm = new System.Windows.Forms.Button();
            this.btnNewPage = new System.Windows.Forms.Button();
            this.lblTotalpages = new System.Windows.Forms.Label();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExportForm = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.cntxtFieldControls = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.alignHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxtFormatSets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabs.SuspendLayout();
            this.tabDesigner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grpAddingFields.SuspendLayout();
            this.grpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.cntxtFieldControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabDesigner);
            this.tabs.Controls.Add(this.tabHistory);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Margin = new System.Windows.Forms.Padding(0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1317, 740);
            this.tabs.TabIndex = 1;
            // 
            // tabDesigner
            // 
            this.tabDesigner.Controls.Add(this.lblDebugMMode);
            this.tabDesigner.Controls.Add(this.btnTemplatesList);
            this.tabDesigner.Controls.Add(this.btnLoadSettingsScreen);
            this.tabDesigner.Controls.Add(this.lblVersionNumber);
            this.tabDesigner.Controls.Add(this.label2);
            this.tabDesigner.Controls.Add(this.splitContainer1);
            this.tabDesigner.Controls.Add(this.btnSaveForm);
            this.tabDesigner.Controls.Add(this.btnNewPage);
            this.tabDesigner.Controls.Add(this.lblTotalpages);
            this.tabDesigner.Controls.Add(this.lblCurrentPage);
            this.tabDesigner.Controls.Add(this.label9);
            this.tabDesigner.Controls.Add(this.btnExportForm);
            this.tabDesigner.Controls.Add(this.btnNextPage);
            this.tabDesigner.Controls.Add(this.btnPreviousPage);
            this.tabDesigner.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabDesigner.Location = new System.Drawing.Point(4, 22);
            this.tabDesigner.Name = "tabDesigner";
            this.tabDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tabDesigner.Size = new System.Drawing.Size(1309, 714);
            this.tabDesigner.TabIndex = 1;
            this.tabDesigner.Text = "Designer";
            this.tabDesigner.UseVisualStyleBackColor = true;
            // 
            // lblDebugMMode
            // 
            this.lblDebugMMode.AutoSize = true;
            this.lblDebugMMode.Location = new System.Drawing.Point(310, 8);
            this.lblDebugMMode.Name = "lblDebugMMode";
            this.lblDebugMMode.Size = new System.Drawing.Size(42, 13);
            this.lblDebugMMode.TabIndex = 35;
            this.lblDebugMMode.Text = "Debug:";
            // 
            // btnTemplatesList
            // 
            this.btnTemplatesList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplatesList.Location = new System.Drawing.Point(1193, 7);
            this.btnTemplatesList.Name = "btnTemplatesList";
            this.btnTemplatesList.Size = new System.Drawing.Size(75, 23);
            this.btnTemplatesList.TabIndex = 34;
            this.btnTemplatesList.Text = "Templates";
            this.btnTemplatesList.UseVisualStyleBackColor = true;
            this.btnTemplatesList.Click += new System.EventHandler(this.btnTemplatesList_Click);
            // 
            // btnLoadSettingsScreen
            // 
            this.btnLoadSettingsScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadSettingsScreen.Image = global::AnotoWorkshop.Properties.Resources.cog_24x24;
            this.btnLoadSettingsScreen.Location = new System.Drawing.Point(1274, 3);
            this.btnLoadSettingsScreen.Name = "btnLoadSettingsScreen";
            this.btnLoadSettingsScreen.Size = new System.Drawing.Size(32, 32);
            this.btnLoadSettingsScreen.TabIndex = 32;
            this.btnLoadSettingsScreen.UseVisualStyleBackColor = true;
            this.btnLoadSettingsScreen.Click += new System.EventHandler(this.btnLoadSettingsScreen_Click);
            // 
            // lblVersionNumber
            // 
            this.lblVersionNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersionNumber.AutoSize = true;
            this.lblVersionNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionNumber.Location = new System.Drawing.Point(868, 11);
            this.lblVersionNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersionNumber.Name = "lblVersionNumber";
            this.lblVersionNumber.Size = new System.Drawing.Size(0, 17);
            this.lblVersionNumber.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(796, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 30;
            this.label2.Text = "Version #";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(6, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.designPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1295, 670);
            this.splitContainer1.SplitterDistance = 835;
            this.splitContainer1.TabIndex = 29;
            // 
            // designPanel
            // 
            this.designPanel.AutoSize = true;
            this.designPanel.BackColor = System.Drawing.Color.Silver;
            this.designPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.designPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designPanel.Location = new System.Drawing.Point(0, 0);
            this.designPanel.Name = "designPanel";
            this.designPanel.Size = new System.Drawing.Size(833, 668);
            this.designPanel.TabIndex = 1;
            this.designPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.designer_Paint);
            this.designPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.designer_MouseDown);
            this.designPanel.MouseEnter += new System.EventHandler(this.designPanel_MouseEnter);
            this.designPanel.MouseLeave += new System.EventHandler(this.designPanel_MouseLeave);
            this.designPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.designer_MouseMove);
            this.designPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.designer_MouseUp);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(456, 670);
            this.splitContainer2.SplitterDistance = 321;
            this.splitContainer2.TabIndex = 30;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.grpAddingFields);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.grpProperties);
            this.splitContainer3.Size = new System.Drawing.Size(454, 319);
            this.splitContainer3.SplitterDistance = 159;
            this.splitContainer3.TabIndex = 20;
            // 
            // grpAddingFields
            // 
            this.grpAddingFields.Controls.Add(this.btnAddRichLabel);
            this.grpAddingFields.Controls.Add(this.btnAddLine);
            this.grpAddingFields.Controls.Add(this.btnAddRectangle);
            this.grpAddingFields.Controls.Add(this.btnAddOptionGroup);
            this.grpAddingFields.Controls.Add(this.btnAddLabel);
            this.grpAddingFields.Controls.Add(this.btnAddCheckBox);
            this.grpAddingFields.Controls.Add(this.btnAddTextField);
            this.grpAddingFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAddingFields.Location = new System.Drawing.Point(0, 0);
            this.grpAddingFields.Margin = new System.Windows.Forms.Padding(2);
            this.grpAddingFields.Name = "grpAddingFields";
            this.grpAddingFields.Padding = new System.Windows.Forms.Padding(2);
            this.grpAddingFields.Size = new System.Drawing.Size(159, 319);
            this.grpAddingFields.TabIndex = 5;
            this.grpAddingFields.TabStop = false;
            this.grpAddingFields.Text = "Add Menu";
            // 
            // btnAddRichLabel
            // 
            this.btnAddRichLabel.Location = new System.Drawing.Point(5, 92);
            this.btnAddRichLabel.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddRichLabel.Name = "btnAddRichLabel";
            this.btnAddRichLabel.Size = new System.Drawing.Size(72, 23);
            this.btnAddRichLabel.TabIndex = 9;
            this.btnAddRichLabel.Text = "Rich Label";
            this.btnAddRichLabel.UseVisualStyleBackColor = true;
            this.btnAddRichLabel.Click += new System.EventHandler(this.btnAddRichLabel_Click);
            // 
            // btnAddLine
            // 
            this.btnAddLine.Location = new System.Drawing.Point(5, 208);
            this.btnAddLine.Name = "btnAddLine";
            this.btnAddLine.Size = new System.Drawing.Size(65, 23);
            this.btnAddLine.TabIndex = 8;
            this.btnAddLine.Text = "Line";
            this.btnAddLine.UseVisualStyleBackColor = true;
            this.btnAddLine.Click += new System.EventHandler(this.btnAddLine_Click);
            // 
            // btnAddRectangle
            // 
            this.btnAddRectangle.Location = new System.Drawing.Point(5, 181);
            this.btnAddRectangle.Name = "btnAddRectangle";
            this.btnAddRectangle.Size = new System.Drawing.Size(65, 23);
            this.btnAddRectangle.TabIndex = 7;
            this.btnAddRectangle.Text = "Rectangle";
            this.btnAddRectangle.UseVisualStyleBackColor = true;
            this.btnAddRectangle.Click += new System.EventHandler(this.btnAddRectangle_Click);
            // 
            // btnAddOptionGroup
            // 
            this.btnAddOptionGroup.Location = new System.Drawing.Point(5, 291);
            this.btnAddOptionGroup.Name = "btnAddOptionGroup";
            this.btnAddOptionGroup.Size = new System.Drawing.Size(82, 23);
            this.btnAddOptionGroup.TabIndex = 5;
            this.btnAddOptionGroup.Text = "Option Group";
            this.btnAddOptionGroup.UseVisualStyleBackColor = true;
            // 
            // btnAddLabel
            // 
            this.btnAddLabel.Location = new System.Drawing.Point(5, 67);
            this.btnAddLabel.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddLabel.Name = "btnAddLabel";
            this.btnAddLabel.Size = new System.Drawing.Size(72, 23);
            this.btnAddLabel.TabIndex = 4;
            this.btnAddLabel.Text = "Label";
            this.btnAddLabel.UseVisualStyleBackColor = true;
            this.btnAddLabel.Click += new System.EventHandler(this.btnAddLabel_Click);
            // 
            // btnAddCheckBox
            // 
            this.btnAddCheckBox.Location = new System.Drawing.Point(5, 42);
            this.btnAddCheckBox.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddCheckBox.Name = "btnAddCheckBox";
            this.btnAddCheckBox.Size = new System.Drawing.Size(72, 23);
            this.btnAddCheckBox.TabIndex = 3;
            this.btnAddCheckBox.Text = "Check Box";
            this.btnAddCheckBox.UseVisualStyleBackColor = true;
            this.btnAddCheckBox.Click += new System.EventHandler(this.btnAddCheckBox_Click);
            // 
            // btnAddTextField
            // 
            this.btnAddTextField.Location = new System.Drawing.Point(5, 17);
            this.btnAddTextField.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddTextField.Name = "btnAddTextField";
            this.btnAddTextField.Size = new System.Drawing.Size(72, 23);
            this.btnAddTextField.TabIndex = 2;
            this.btnAddTextField.Text = "Text Field";
            this.btnAddTextField.UseVisualStyleBackColor = true;
            this.btnAddTextField.Click += new System.EventHandler(this.btn_AddField_Click);
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.lblTotalProp);
            this.grpProperties.Controls.Add(this.lblCurrentProp);
            this.grpProperties.Controls.Add(this.cmbFormatSetNames);
            this.grpProperties.Controls.Add(this.label16);
            this.grpProperties.Controls.Add(this.label1);
            this.grpProperties.Controls.Add(this.btnNextProp);
            this.grpProperties.Controls.Add(this.btnPrevProp);
            this.grpProperties.Controls.Add(this.label14);
            this.grpProperties.Controls.Add(this.txtPropText);
            this.grpProperties.Controls.Add(this.btnPropSave);
            this.grpProperties.Controls.Add(this.chkPropReadOnly);
            this.grpProperties.Controls.Add(this.label10);
            this.grpProperties.Controls.Add(this.txtPropFieldType);
            this.grpProperties.Controls.Add(this.chkPropHidden);
            this.grpProperties.Controls.Add(this.label7);
            this.grpProperties.Controls.Add(this.txtPropHeight);
            this.grpProperties.Controls.Add(this.label8);
            this.grpProperties.Controls.Add(this.txtPropWidth);
            this.grpProperties.Controls.Add(this.label6);
            this.grpProperties.Controls.Add(this.txtPropY);
            this.grpProperties.Controls.Add(this.label5);
            this.grpProperties.Controls.Add(this.label4);
            this.grpProperties.Controls.Add(this.txtPropX);
            this.grpProperties.Controls.Add(this.txtPropName);
            this.grpProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProperties.Location = new System.Drawing.Point(0, 0);
            this.grpProperties.Margin = new System.Windows.Forms.Padding(2);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Padding = new System.Windows.Forms.Padding(2);
            this.grpProperties.Size = new System.Drawing.Size(291, 319);
            this.grpProperties.TabIndex = 19;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Properties";
            // 
            // lblTotalProp
            // 
            this.lblTotalProp.AutoSize = true;
            this.lblTotalProp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProp.Location = new System.Drawing.Point(235, 13);
            this.lblTotalProp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalProp.Name = "lblTotalProp";
            this.lblTotalProp.Size = new System.Drawing.Size(16, 17);
            this.lblTotalProp.TabIndex = 35;
            this.lblTotalProp.Text = "9";
            // 
            // lblCurrentProp
            // 
            this.lblCurrentProp.AutoSize = true;
            this.lblCurrentProp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentProp.Location = new System.Drawing.Point(209, 13);
            this.lblCurrentProp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrentProp.Name = "lblCurrentProp";
            this.lblCurrentProp.Size = new System.Drawing.Size(16, 17);
            this.lblCurrentProp.TabIndex = 36;
            this.lblCurrentProp.Text = "1";
            // 
            // cmbFormatSetNames
            // 
            this.cmbFormatSetNames.FormattingEnabled = true;
            this.cmbFormatSetNames.Location = new System.Drawing.Point(81, 248);
            this.cmbFormatSetNames.Name = "cmbFormatSetNames";
            this.cmbFormatSetNames.Size = new System.Drawing.Size(111, 21);
            this.cmbFormatSetNames.TabIndex = 24;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(223, 13);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(12, 17);
            this.label16.TabIndex = 37;
            this.label16.Text = "/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 248);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Format Set";
            // 
            // btnNextProp
            // 
            this.btnNextProp.Location = new System.Drawing.Point(257, 10);
            this.btnNextProp.Name = "btnNextProp";
            this.btnNextProp.Size = new System.Drawing.Size(28, 23);
            this.btnNextProp.TabIndex = 33;
            this.btnNextProp.Text = ">>";
            this.btnNextProp.UseVisualStyleBackColor = true;
            // 
            // btnPrevProp
            // 
            this.btnPrevProp.Location = new System.Drawing.Point(176, 10);
            this.btnPrevProp.Name = "btnPrevProp";
            this.btnPrevProp.Size = new System.Drawing.Size(28, 23);
            this.btnPrevProp.TabIndex = 34;
            this.btnPrevProp.Text = "<<";
            this.btnPrevProp.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 165);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "Text:";
            // 
            // txtPropText
            // 
            this.txtPropText.Location = new System.Drawing.Point(10, 181);
            this.txtPropText.Margin = new System.Windows.Forms.Padding(2);
            this.txtPropText.Name = "txtPropText";
            this.txtPropText.Size = new System.Drawing.Size(158, 20);
            this.txtPropText.TabIndex = 21;
            // 
            // btnPropSave
            // 
            this.btnPropSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPropSave.Location = new System.Drawing.Point(194, 291);
            this.btnPropSave.Name = "btnPropSave";
            this.btnPropSave.Size = new System.Drawing.Size(94, 23);
            this.btnPropSave.TabIndex = 20;
            this.btnPropSave.Text = "Save Properties";
            this.btnPropSave.UseVisualStyleBackColor = true;
            this.btnPropSave.Click += new System.EventHandler(this.btnPropSave_Click);
            // 
            // chkPropReadOnly
            // 
            this.chkPropReadOnly.AutoSize = true;
            this.chkPropReadOnly.Location = new System.Drawing.Point(94, 214);
            this.chkPropReadOnly.Margin = new System.Windows.Forms.Padding(2);
            this.chkPropReadOnly.Name = "chkPropReadOnly";
            this.chkPropReadOnly.Size = new System.Drawing.Size(73, 17);
            this.chkPropReadOnly.TabIndex = 19;
            this.chkPropReadOnly.Text = "ReadOnly";
            this.chkPropReadOnly.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 127);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Type:";
            // 
            // txtPropFieldType
            // 
            this.txtPropFieldType.Location = new System.Drawing.Point(42, 125);
            this.txtPropFieldType.Margin = new System.Windows.Forms.Padding(2);
            this.txtPropFieldType.Name = "txtPropFieldType";
            this.txtPropFieldType.Size = new System.Drawing.Size(98, 20);
            this.txtPropFieldType.TabIndex = 11;
            // 
            // chkPropHidden
            // 
            this.chkPropHidden.AutoSize = true;
            this.chkPropHidden.Location = new System.Drawing.Point(31, 214);
            this.chkPropHidden.Margin = new System.Windows.Forms.Padding(2);
            this.chkPropHidden.Name = "chkPropHidden";
            this.chkPropHidden.Size = new System.Drawing.Size(60, 17);
            this.chkPropHidden.TabIndex = 10;
            this.chkPropHidden.Text = "Hidden";
            this.chkPropHidden.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 89);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Height";
            // 
            // txtPropHeight
            // 
            this.txtPropHeight.Location = new System.Drawing.Point(120, 89);
            this.txtPropHeight.Margin = new System.Windows.Forms.Padding(2);
            this.txtPropHeight.Name = "txtPropHeight";
            this.txtPropHeight.Size = new System.Drawing.Size(30, 20);
            this.txtPropHeight.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 89);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Width";
            // 
            // txtPropWidth
            // 
            this.txtPropWidth.Location = new System.Drawing.Point(42, 87);
            this.txtPropWidth.Margin = new System.Windows.Forms.Padding(2);
            this.txtPropWidth.Name = "txtPropWidth";
            this.txtPropWidth.Size = new System.Drawing.Size(34, 20);
            this.txtPropWidth.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 63);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Y:";
            // 
            // txtPropY
            // 
            this.txtPropY.Location = new System.Drawing.Point(63, 63);
            this.txtPropY.Margin = new System.Windows.Forms.Padding(2);
            this.txtPropY.Name = "txtPropY";
            this.txtPropY.Size = new System.Drawing.Size(25, 20);
            this.txtPropY.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-2, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name:";
            // 
            // txtPropX
            // 
            this.txtPropX.Location = new System.Drawing.Point(16, 63);
            this.txtPropX.Margin = new System.Windows.Forms.Padding(2);
            this.txtPropX.Name = "txtPropX";
            this.txtPropX.Size = new System.Drawing.Size(25, 20);
            this.txtPropX.TabIndex = 1;
            // 
            // txtPropName
            // 
            this.txtPropName.Location = new System.Drawing.Point(11, 36);
            this.txtPropName.Margin = new System.Windows.Forms.Padding(2);
            this.txtPropName.Name = "txtPropName";
            this.txtPropName.Size = new System.Drawing.Size(158, 20);
            this.txtPropName.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btnRefreshFieldTree);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.trvFieldList);
            this.splitContainer4.Size = new System.Drawing.Size(454, 343);
            this.splitContainer4.SplitterDistance = 32;
            this.splitContainer4.TabIndex = 27;
            // 
            // btnRefreshFieldTree
            // 
            this.btnRefreshFieldTree.Location = new System.Drawing.Point(2, 2);
            this.btnRefreshFieldTree.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefreshFieldTree.Name = "btnRefreshFieldTree";
            this.btnRefreshFieldTree.Size = new System.Drawing.Size(75, 24);
            this.btnRefreshFieldTree.TabIndex = 21;
            this.btnRefreshFieldTree.Text = "Refresh Tree";
            this.btnRefreshFieldTree.UseVisualStyleBackColor = true;
            this.btnRefreshFieldTree.Click += new System.EventHandler(this.btnRefreshFieldTree_Click);
            // 
            // trvFieldList
            // 
            this.trvFieldList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvFieldList.Location = new System.Drawing.Point(0, 0);
            this.trvFieldList.Margin = new System.Windows.Forms.Padding(2);
            this.trvFieldList.Name = "trvFieldList";
            this.trvFieldList.Size = new System.Drawing.Size(454, 307);
            this.trvFieldList.TabIndex = 20;
            this.trvFieldList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvFieldList_AfterSelect);
            this.trvFieldList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.heirarchyViewClick);
            this.trvFieldList.MouseEnter += new System.EventHandler(this.trvFieldList_MouseEnter);
            // 
            // btnSaveForm
            // 
            this.btnSaveForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveForm.Location = new System.Drawing.Point(653, 8);
            this.btnSaveForm.Name = "btnSaveForm";
            this.btnSaveForm.Size = new System.Drawing.Size(66, 23);
            this.btnSaveForm.TabIndex = 28;
            this.btnSaveForm.Text = "Save";
            this.btnSaveForm.UseVisualStyleBackColor = true;
            this.btnSaveForm.Click += new System.EventHandler(this.btnSaveForm_Click);
            // 
            // btnNewPage
            // 
            this.btnNewPage.Location = new System.Drawing.Point(4, 4);
            this.btnNewPage.Name = "btnNewPage";
            this.btnNewPage.Size = new System.Drawing.Size(42, 23);
            this.btnNewPage.TabIndex = 12;
            this.btnNewPage.Text = "New";
            this.btnNewPage.UseVisualStyleBackColor = true;
            this.btnNewPage.Click += new System.EventHandler(this.btnNewPage_Click);
            // 
            // lblTotalpages
            // 
            this.lblTotalpages.AutoSize = true;
            this.lblTotalpages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalpages.Location = new System.Drawing.Point(232, 7);
            this.lblTotalpages.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalpages.Name = "lblTotalpages";
            this.lblTotalpages.Size = new System.Drawing.Size(16, 17);
            this.lblTotalpages.TabIndex = 10;
            this.lblTotalpages.Text = "9";
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.AutoSize = true;
            this.lblCurrentPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPage.Location = new System.Drawing.Point(206, 7);
            this.lblCurrentPage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(16, 17);
            this.lblCurrentPage.TabIndex = 11;
            this.lblCurrentPage.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(220, 7);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "/";
            // 
            // btnExportForm
            // 
            this.btnExportForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportForm.Location = new System.Drawing.Point(725, 8);
            this.btnExportForm.Name = "btnExportForm";
            this.btnExportForm.Size = new System.Drawing.Size(66, 23);
            this.btnExportForm.TabIndex = 7;
            this.btnExportForm.Text = "Export Form";
            this.btnExportForm.UseVisualStyleBackColor = true;
            this.btnExportForm.Click += new System.EventHandler(this.btnExportForm_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(129, 4);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 4;
            this.btnNextPage.Text = ">>";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(48, 4);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(75, 23);
            this.btnPreviousPage.TabIndex = 4;
            this.btnPreviousPage.Text = "<<";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // tabHistory
            // 
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(1309, 714);
            this.tabHistory.TabIndex = 3;
            this.tabHistory.Text = "...";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // cntxtFieldControls
            // 
            this.cntxtFieldControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.alignHorizontallyToolStripMenuItem,
            this.alignVerticallyToolStripMenuItem});
            this.cntxtFieldControls.Name = "cntxtFieldControls";
            this.cntxtFieldControls.Size = new System.Drawing.Size(170, 180);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem2.Text = "-------";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem3.Text = "----------";
            // 
            // alignHorizontallyToolStripMenuItem
            // 
            this.alignHorizontallyToolStripMenuItem.Name = "alignHorizontallyToolStripMenuItem";
            this.alignHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.alignHorizontallyToolStripMenuItem.Text = "Align Horizontally";
            this.alignHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.alignHorizontallyToolStripMenuItem_Click);
            // 
            // alignVerticallyToolStripMenuItem
            // 
            this.alignVerticallyToolStripMenuItem.Name = "alignVerticallyToolStripMenuItem";
            this.alignVerticallyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.alignVerticallyToolStripMenuItem.Text = "Align Vertically";
            this.alignVerticallyToolStripMenuItem.Click += new System.EventHandler(this.alignVerticallyToolStripMenuItem_Click);
            // 
            // cntxtFormatSets
            // 
            this.cntxtFormatSets.Name = "cntxtFormatSets";
            this.cntxtFormatSets.Size = new System.Drawing.Size(153, 26);
            this.cntxtFormatSets.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cntxtFormatSets_ItemClicked);
            // 
            // Designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 740);
            this.Controls.Add(this.tabs);
            this.KeyPreview = true;
            this.Name = "Designer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pen Workshop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            this.tabs.ResumeLayout(false);
            this.tabDesigner.ResumeLayout(false);
            this.tabDesigner.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.grpAddingFields.ResumeLayout(false);
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.cntxtFieldControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabDesigner;
        private System.Windows.Forms.Button btnAddTextField;
        public System.Windows.Forms.Panel designPanel;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.GroupBox grpAddingFields;
        private System.Windows.Forms.Button btnAddLabel;
        private System.Windows.Forms.Button btnAddCheckBox;
        private System.Windows.Forms.Button btnAddOptionGroup;
        private System.Windows.Forms.Button btnExportForm;
        private System.Windows.Forms.Label lblTotalpages;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnNewPage;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.Button btnPropSave;
        private System.Windows.Forms.CheckBox chkPropReadOnly;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPropFieldType;
        private System.Windows.Forms.CheckBox chkPropHidden;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPropHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPropWidth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPropY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPropX;
        private System.Windows.Forms.TextBox txtPropName;
        private System.Windows.Forms.Button btnRefreshFieldTree;
        private System.Windows.Forms.TreeView trvFieldList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPropText;
        private System.Windows.Forms.Button btnAddRectangle;
        private System.Windows.Forms.Button btnAddLine;
        private System.Windows.Forms.ContextMenuStrip cntxtFieldControls;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFormatSetNames;
        private System.Windows.Forms.Button btnSaveForm;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lblVersionNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadSettingsScreen;
        private System.Windows.Forms.Label lblTotalProp;
        private System.Windows.Forms.Label lblCurrentProp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnNextProp;
        private System.Windows.Forms.Button btnPrevProp;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button btnTemplatesList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem alignHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alignVerticallyToolStripMenuItem;
        private System.Windows.Forms.Label lblDebugMMode;
        private System.Windows.Forms.Button btnAddRichLabel;
        private System.Windows.Forms.ContextMenuStrip cntxtFormatSets;
    }
}

