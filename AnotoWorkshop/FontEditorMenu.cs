using System.Drawing;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class FontEditorMenu : UserControl {
        public FontEditorMenu() {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

        }

protected override CreateParams CreateParams {  
  get {  
    CreateParams cp = base.CreateParams;  
    cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT  
    return cp;  
  }  
}

        private void fontEditorMenu_Load(object sender, System.EventArgs e) {
            foreach (FontFamily font in FontFamily.Families) {//Populate the font list with system fonts
                cmbFontList.Items.Add(font.Name);
            }
        }
    }
}
