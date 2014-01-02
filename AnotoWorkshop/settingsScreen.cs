using System;
using System.Windows.Forms;

namespace AnotoWorkshop {

    public partial class settingsScreen : Form {

        #region Variables - Not Started

        private Settings _settings;

        #endregion Variables - Not Started

        #region Initializers

        public settingsScreen() {
            InitializeComponent();
        }

        private void settingsScreen_Load(object sender, EventArgs e) {
            _settings = Settings.instance;
        }

        #endregion Initializers

        #region Testing Stuff

        private void btnTest_Click(object sender, EventArgs e) {
            _settings.setString(txtText.Text);
        }

        private void button1_Click(object sender, EventArgs e) {
            txtText.Text = _settings.getString();
        }

        #endregion Testing Stuff

        private void btnSaveFile_Click(object sender, EventArgs e) {
            _settings.saveToFile();
        }
    }
}