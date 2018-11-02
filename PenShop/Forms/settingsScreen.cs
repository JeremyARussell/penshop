using System;
using System.Drawing;

using System.Windows.Forms;

namespace PenShop {

    public partial class settingsScreen : Form {

        #region Variables

        private Settings _settings;
        private bool _needToSaveSettings = false;

        #endregion Variables

        #region Initializers

        public settingsScreen() {
            InitializeComponent();
        }

        private void settingsScreen_Load(object sender, EventArgs e) {
            _settings = Settings.instance;
            _settings.loadFromFile();

/* Disable for now, once tutorial situation is figured out can reenable.
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
 */

            //foreach (FontFamily font in FontFamily.Families) {//Populate the font list with system fonts
            //    cmbFontList.Items.Add(font.Name);
            //}
            //Populate folder paths
            txtFormsFolder.Text = _settings.formsFolderLocation;
            txtExportFolder.Text = _settings.exportFolder;
            //Populate dv connection settings
            txtDbUser.Text = _settings.dbcUser;
            txtDbPass.Text = _settings.dbcPassword;
            txtDbServer.Text = _settings.dbcServerName;
            cmbDbTrusted.Text = _settings.dbcTrusted;
            txtDbName.Text = _settings.dbcDatabaseName;
            nmrTimeout.Value = _settings.dbcTimeout;

            //refreshList();

        }

        #endregion Initializers

        private void btnSaveFile_Click(object sender, EventArgs e) {
            Text = "Settings";        
            _needToSaveSettings = false;
            
            _settings.saveToFile();
        }

        #region Folder Paths

        #endregion

        #region Db Connection Settings
        private void btnSaveDb_Click(object sender, EventArgs e)
        {
            _settings.dbcUser = txtDbUser.Text;
            _settings.dbcPassword = txtDbPass.Text;
            _settings.dbcServerName = txtDbServer.Text;
            _settings.dbcTrusted = cmbDbTrusted.Text;
            _settings.dbcDatabaseName = txtDbName.Text;
            _settings.dbcTimeout = (int)nmrTimeout.Value;
            Text = "Settings*";
            _needToSaveSettings = true;
        }
        #endregion Db Connection Settings

        #region Format Sets
        /*
        //private string _activeFormatSet;

        //private void refreshList() {
        //    lstvFormatSets.Clear();
        //    foreach (var fSet in _settings.globalFormatSet) {
        //        lstvFormatSets.Items.Add(fSet.Key);
        //   }
        //
        //}
        
        //FormatSet _workingFormatSet = new FormatSet();

        private void lstvFormatSets_Click(object sender, EventArgs e) {
        //    if (((ListView)sender).SelectedItems.Count > 0) {
        //        _workingFormatSet = new FormatSet();

        //        _activeFormatSet = ((ListView)sender).SelectedItems[0].Text;
                
        //       _workingFormatSet.name = _settings.globalFormatSet[_activeFormatSet].name;
        //        _workingFormatSet.fontSize = _settings.globalFormatSet[_activeFormatSet].fontSize;
        //        _workingFormatSet.fontTypeface = _settings.globalFormatSet[_activeFormatSet].fontTypeface;
        //        _workingFormatSet.fontWeight = _settings.globalFormatSet[_activeFormatSet].fontWeight;

        //        refreshFontExample();

        //    }
        }

        private void refreshFontExample() {
                lblTestFormat.Text = _workingFormatSet.fontSizeString + " - " +
                                     _workingFormatSet.fontTypeface + " - " +
                                     _workingFormatSet.fontWeight;

                lblTestFormat.Font = _workingFormatSet.font();

                txtSetName.Text = _workingFormatSet.name;
                cmbFontSizes.SelectedItem = _workingFormatSet.fontSize.ToString();
                cmbFontList.SelectedItem = _workingFormatSet.fontTypeface;
                cmbFontWeight.SelectedItem = _workingFormatSet.fontWeight;

        }

        private bool _savingNewSet;

        private void createNewFormatSet() {
            _workingFormatSet = new FormatSet
                                {name = "New...", fontSize = 12, fontTypeface = "Arial", fontWeight = "normal"};

            lblTestFormat.Text = "";

            refreshFontExample();

            _savingNewSet = true;
        }

        private void btnSaveSetName_Click(object sender, EventArgs e) {
            if(_savingNewSet) {

                _workingFormatSet.name = txtSetName.Text;

                bool needToBreak = false;

                foreach (var fSet in _settings.globalFormatSet) {
                    if (needToBreak) break;

                    if (txtSetName.Text == fSet.Key) {
                        //if (workingFormatSet.name == _activeFormatSet) break;
                        MessageBox.Show("The Format Set name " + txtSetName.Text + " is already taken. Please choose another."
                            , "Same name error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        needToBreak = true;
                    }

                }

                if (needToBreak) return;
                Text = "Settings*";
                _needToSaveSettings = true;
                _settings.globalFormatSet.Add(_workingFormatSet.name, _workingFormatSet);
                _savingNewSet = false;

                refreshList();
                return;
            }
            Text = "Settings*";
            _needToSaveSettings = true;

            _settings.globalFormatSet[_activeFormatSet].fontSize = Convert.ToInt32(cmbFontSizes.SelectedItem.ToString());
            _settings.globalFormatSet[_activeFormatSet].fontTypeface = cmbFontList.SelectedItem.ToString();
            _settings.globalFormatSet[_activeFormatSet].fontWeight = cmbFontWeight.SelectedItem.ToString();

            refreshList();

        }

        private void cmbFontSizes_SelectedValueChanged(object sender, EventArgs e) {
            _workingFormatSet.fontSize = Convert.ToInt32(cmbFontSizes.SelectedItem.ToString());
            refreshFontExample();
        }

        private void cmbFontList_SelectedValueChanged(object sender, EventArgs e) {
            _workingFormatSet.fontTypeface = cmbFontList.SelectedItem.ToString();
            refreshFontExample();
        }

        private void cmbFontWeight_SelectedValueChanged(object sender, EventArgs e) {
            _workingFormatSet.fontWeight = cmbFontWeight.SelectedItem.ToString();
            refreshFontExample();
        }
        */
        #endregion Format Sets

        private void settingsScreen_FormClosing(object sender, FormClosingEventArgs e) {
            if(_needToSaveSettings) {
                
                DialogResult dRes = MessageBox.Show("You've made changes to your settings, would you like to save before you close?", "Do you want to save?",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);  


                    if (dRes == DialogResult.Yes) {
                        _settings.saveToFile();
                        Text = "Settings";
                        _needToSaveSettings = false;
                    }
                    if (dRes == DialogResult.No) {
                        Text = "Settings";
                        _needToSaveSettings = false;
                    }
                    if (dRes == DialogResult.Cancel) {

                        e.Cancel = true;
                    }               
              }    
        }

        private void btnFormsFolderBrowser_Click(object sender, EventArgs e) {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.SelectedPath = txtFormsFolder.Text;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) {
                txtFormsFolder.Text = openFileDialog.SelectedPath;
                _settings.formsFolderLocation = txtFormsFolder.Text;
                Text = "Settings*";
                _needToSaveSettings = true;
            }
        }

        private void btnExportFolderBrowser_Click(object sender, EventArgs e) {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.SelectedPath = txtExportFolder.Text;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) {
                txtExportFolder.Text = openFileDialog.SelectedPath;
                _settings.exportFolder = txtExportFolder.Text;
                Text = "Settings*";
                _needToSaveSettings = true;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            Text = "Settings";
            _needToSaveSettings = false;
            Close();
        }
    }
}