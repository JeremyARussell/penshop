using System;
using System.Drawing;

using System.Windows.Forms;

namespace AnotoWorkshop {

    public partial class settingsScreen : Form {

        #region Variables - Not Started

        private Settings _settings;
        private bool needToSaveSettings = false;

        #endregion Variables - Not Started

        #region Initializers

        public settingsScreen() {
            InitializeComponent();
        }

        private void settingsScreen_Load(object sender, EventArgs e) {
            _settings = Settings.instance;
            _settings.loadFromFile();

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

            refreshList();

        }

        #endregion Initializers

        private void btnSaveFile_Click(object sender, EventArgs e) {
            this.Text = "Settings";        
            needToSaveSettings = false;
            
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
            this.Text = "Settings*";
            needToSaveSettings = true;
        }
        #endregion Db Connection Settings

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
                
                workingFormatSet.name = _settings.globalFormatSet[_activeFormatSet].name;
                workingFormatSet.fontSize = _settings.globalFormatSet[_activeFormatSet].fontSize;
                workingFormatSet.fontTypeface = _settings.globalFormatSet[_activeFormatSet].fontTypeface;
                workingFormatSet.fontWeight = _settings.globalFormatSet[_activeFormatSet].fontWeight;

                refreshFontExample();

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

        private bool savingNewSet;

        private void createNewFormatSet() {
            workingFormatSet = new FormatSet();
            
            workingFormatSet.name = "New...";
            workingFormatSet.fontSize = 12;
            workingFormatSet.fontTypeface = "Arial";
            workingFormatSet.fontWeight = "normal";

            lblTestFormat.Text = "";

            refreshFontExample();

            savingNewSet = true;
        }

        private void btnSaveSetName_Click(object sender, EventArgs e) {
            if(savingNewSet) {

                workingFormatSet.name = txtSetName.Text;

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
                this.Text = "Settings*";
                needToSaveSettings = true;
                _settings.globalFormatSet.Add(workingFormatSet.name, workingFormatSet);
                savingNewSet = false;

                refreshList();
                return;
            }
            this.Text = "Settings*";
            needToSaveSettings = true;

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
            this.Text = "Settings*";
            needToSaveSettings = true;
        }

        private void btnCancel_Click(object sender, EventArgs e) {

        }

        private void btnNewFormatSet_Click(object sender, EventArgs e) {
            createNewFormatSet();
        }

        private void txtSetName_TextChanged(object sender, EventArgs e) {
            if(savingNewSet) {
                workingFormatSet.name = txtSetName.Text;
            }
        }

        private void settingsScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(needToSaveSettings) {
                
                DialogResult dRes = MessageBox.Show("You've made changes to your settings, would you like to save before you close?", "Do you want to save?",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);  


                    if (dRes == DialogResult.Yes) {
                        _settings.saveToFile();
                    }
                    if (dRes == DialogResult.No) {
                        
                    }
                    if (dRes == DialogResult.Cancel) {

                        e.Cancel = true;
                    }               
              }    
        }




    }
}