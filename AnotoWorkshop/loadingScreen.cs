using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AnotoWorkshop {

    public partial class loadingScreen : Form {

        #region Variables

        private Settings _settings;

        #endregion Variables

        #region Initializers - Kind of Started

        public loadingScreen() {
            InitializeComponent();
        }

        private void loadingScreen_Load(object sender, EventArgs e) {
            _settings = Settings.instance;
            checkForForms();
        }

        #endregion Initializers - Kind of Started

        #region Form Loading

        public void checkForForms() {
            try {
                if (_settings.formsFolderLocation != null) {
                    IEnumerable<string> fileList = Directory.EnumerateFiles(_settings.formsFolderLocation, "*.penform");
                    foreach (string file in fileList) {
                        string formName = file.Remove(0, _settings.formsFolderLocation.Length + 1);

                        ListViewItem workingListViewItem = new ListViewItem(formName);
                        workingListViewItem.Tag = file;
                        lstvForms.Items.Add(workingListViewItem);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void lstvForms_DoubleClick(object sender, EventArgs e) {
            //-Franklin - Added check to make sure items are really selected.
            if (lstvForms.SelectedItems.Count > 0) {
                string filePath = lstvForms.SelectedItems[0].Tag.ToString();
                PenForm form = new PenForm(filePath);//TODO - Culprit
                new Thread(() => new frmMain(form).ShowDialog()).Start();
            }
        }

        #endregion Form Loading

        #region SQL Loader

        private void btnOpenSQLform_Click(object sender, EventArgs e) {
            new Thread(() => new fieldSelection().ShowDialog()).Start();
        }

        #endregion SQL Loader

        #region Variable Builder

        private void btlLoadAliasBuilder_Click(object sender, EventArgs e) {
            new Thread(() => new aliasBuilder().ShowDialog()).Start();
        }

        #endregion Variable Builder

        #region Load Settings

        private void btnLoadSettings_Click(object sender, EventArgs e) {
            new settingsScreen().ShowDialog();
        }

        #endregion Load Settings

        #region Import Forms

        private List<PenForm> _formsToSave = new List<PenForm>();
        private Dictionary<int, FormatSet> _formatSets = new Dictionary<int, FormatSet>();

        private void btnImportForms_Click(object sender, EventArgs e) {
            if(!_settings.visitedImportWizard) {
                if (MessageBox.Show("Hello, this is the first time you've visited the import wizard, would you like to watch the video?", "Import Wizard - Tutorial",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    _settings.visitedImportWizard = true;
                    //Watch Video
                }
            }
            
            PenForm workingForm = new PenForm();

            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();

            openFileDialog1.SelectedPath = Application.StartupPath;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string folderPath = openFileDialog1.SelectedPath;

                try {
                    IEnumerable<string> fileList = Directory.EnumerateFiles(folderPath, "*.xdp");
                    foreach (string file in fileList) {
                        workingForm = new PenForm(file, _formatSets);
                        workingForm.thisFormsPath = file;//TODO - internal file path for forms
                        _formsToSave.Add(workingForm);
                    }
                } catch (Exception ex) {
                    throw ex;
                }

                new importWizard(_formsToSave, _formatSets, this).ShowDialog();
            }
        }

        #endregion Import Forms
    }
}