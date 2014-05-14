﻿using System.Drawing;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class FontEditorMenu : UserControl {
        public FontEditorMenu() {
            InitializeComponent();

            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //BackColor = Color.Transparent;

        }

        public bool needToStayOpen = true;

        private void fontEditorMenu_Load(object sender, System.EventArgs e) {
            foreach (FontFamily font in FontFamily.Families) {//Populate the font list with system fonts
                cmbFontList.Items.Add(font.Name);
            }
        }

    }
}
