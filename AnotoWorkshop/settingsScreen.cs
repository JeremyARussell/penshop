using System;
using System.Drawing;

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

            foreach (FontFamily font in FontFamily.Families) {//Populate the font list with system fonts
                cmbFontList.Items.Add(font.Name);
            }

            txtFormsFolder.Text = _settings.formsFolderLocation;
            txtExportFolder.Text = _settings.exportFolder;

            refreshList();

        }

        #endregion Initializers

        private void btnSaveFile_Click(object sender, EventArgs e) {
            _settings.saveToFile();
        }

        #region Format Sets

        private string _activeFormatSet;

        private void refreshList() {
            lstvFormatSets.Clear();
            foreach (var fSet in _settings.globalFormatSet) {
                lstvFormatSets.Items.Add(fSet.Key);
            }

        }
        
        FormatSet workingFormatSet = new FormatSet();

        private void lstvFormatSets_Click(object sender, EventArgs e) {
            if (((ListView)sender).SelectedItems.Count > 0) {
                workingFormatSet = new FormatSet();

                _activeFormatSet = ((ListView)sender).SelectedItems[0].Text;
                
                //TODO - Need to make a temp FormatSet to work with, then save that one over the old one 
                //once the button is clicked instead of having to save and click to load the new changes 
                //or try something out without saving the format set.

                workingFormatSet.name = _settings.globalFormatSet[_activeFormatSet].name;
                workingFormatSet.fontSize = _settings.globalFormatSet[_activeFormatSet].fontSize;
                workingFormatSet.fontTypeface = _settings.globalFormatSet[_activeFormatSet].fontTypeface;
                workingFormatSet.fontWeight = _settings.globalFormatSet[_activeFormatSet].fontWeight;

                refreshFontExample();

                //txtSetName.Focus();

                //AcceptButton = btnSaveSetName;
            }
        }

        private void refreshFontExample() {
                lblTestFormat.Text = workingFormatSet.fontSizeString + " - " +
                                     workingFormatSet.fontTypeface + " - " +
                                     workingFormatSet.fontWeight;

                lblTestFormat.Font = workingFormatSet.font();

                txtSetName.Text = workingFormatSet.name;
                cmbFontSizes.SelectedItem = workingFormatSet.fontSize.ToString();
                cmbFontList.SelectedItem = workingFormatSet.fontTypeface;
                cmbFontWeight.SelectedItem = workingFormatSet.fontWeight;

        }

        private void createNewFormatSet() {//Preparing for later
            /*
            //Not handling Format Set renaming except for during import and creation of a new one.
             * Commenting out for now.
            bool needToBreak = false;

            foreach (var fSet in _settings.globalFormatSet) {

            }

            for (int i = 0; i < _formatSets.Count; i++) {
                if (needToBreak) break;
                if (txtSetName.Text == _formatSets[i].name) {
                    if (i == _activeFormatSet) break;
                    MessageBox.Show("The Format Set name " + txtSetName.Text + " is already taken. Please choose another."
                        , "Same name error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    needToBreak = true;
                }
            }

            if (needToBreak) return;
           
            _formatSets[_activeFormatSet].name = txtSetName.Text;
             */
        }

        private void btnSaveSetName_Click(object sender, EventArgs e) {
            _settings.globalFormatSet[_activeFormatSet].fontSize = Convert.ToInt32(cmbFontSizes.SelectedItem.ToString());
            _settings.globalFormatSet[_activeFormatSet].fontTypeface = cmbFontList.SelectedItem.ToString();
            _settings.globalFormatSet[_activeFormatSet].fontWeight = cmbFontWeight.SelectedItem.ToString();

            refreshList();

        }

        private void cmbFontSizes_SelectedValueChanged(object sender, EventArgs e) {
            workingFormatSet.fontSize = Convert.ToInt32(cmbFontSizes.SelectedItem.ToString());
            refreshFontExample();
        }

        private void cmbFontList_SelectedValueChanged(object sender, EventArgs e) {
            workingFormatSet.fontTypeface = cmbFontList.SelectedItem.ToString();
            refreshFontExample();
        }

        private void cmbFontWeight_SelectedValueChanged(object sender, EventArgs e) {
            workingFormatSet.fontWeight = cmbFontWeight.SelectedItem.ToString();
            refreshFontExample();
        }
        #endregion Format Sets

        private void btnSaveFormsFolders_Click(object sender, EventArgs e) {
            _settings.formsFolderLocation = txtFormsFolder.Text;
            _settings.exportFolder = txtExportFolder.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Close();
        }



    }
}