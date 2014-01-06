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

            if (!_settings.visitedSettingsScreen) {
                if (MessageBox.Show("Hello, welcome to the Settings screen would you like to watch the video describing what the settings are?", "Settings Screen - Tutorial",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    _settings.visitedSettingsScreen = true;
                    _settings.saveToFile();
                    //Watch Video
                }
            }

        }

        #endregion Initializers

        private void btnSaveFile_Click(object sender, EventArgs e) {
            _settings.saveToFile();
        }
    }
}