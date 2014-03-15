using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace AnotoWorkshop {

    public partial class LoadingScreen : Form {

        #region Variables

        private Settings _settings;

        #endregion Variables

        #region Initializers - Kind of Started

        public LoadingScreen() {
            InitializeComponent();
        }

        private void loadingScreen_Load(object sender, EventArgs e) {
            _settings = Settings.instance;
            checkForForms();

            if (!_settings.visitedLoadingScreen) {
                if (MessageBox.Show("Hello, welcome to the Pen Studio would you like to watch the video?", "Loading Screen - Tutorial",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    _settings.visitedLoadingScreen = true;
                    _settings.saveToFile();
                    //Watch Video
                }
            }
        }

        #endregion Initializers - Kind of Started

        #region Form Loading

        public void checkForForms() {
            lstvForms.Clear();

            try {
                if (_settings.formsFolderLocation != null) {
                    if (Directory.Exists(_settings.formsFolderLocation) != true) { // Added a check and prompt for when the settings exist but the folder does not.
                        FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();

                        openFileDialog1.SelectedPath = Application.StartupPath;
                        DialogResult result = openFileDialog1.ShowDialog();
                        if (result == DialogResult.OK) {
                            string folderPath = openFileDialog1.SelectedPath;
                            _settings.formsFolderLocation = folderPath;
                            _settings.saveToFile();
                        }
                        if (result == DialogResult.Cancel) {
                            MessageBox.Show("A working folder for your forms is required", "Loading Screen - Settings Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            return;
                        }
                    }

                    IEnumerable<string> fileList = Directory.EnumerateFiles(_settings.formsFolderLocation, "*.penform");
                    foreach (string file in fileList) {
                        PenForm tempForm = _form = new PenForm(file);
                        string formName = tempForm.FormName;

                        ListViewItem workingListViewItem = new ListViewItem(formName);
                        workingListViewItem.Tag = file;
                        lstvForms.Items.Add(workingListViewItem);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        PenForm _form;

        private void lstvForms_DoubleClick(object sender, EventArgs e) {
            //-Franklin - Added check to make sure items are really selected.
            if (lstvForms.SelectedItems.Count > 0) {
                string filePath = lstvForms.SelectedItems[0].Tag.ToString();
                _form = new PenForm(filePath);

                //Revamped this to allow for OLE interaction with the designer form.
                //Specifically the is blank handling for the save and export folders.
                Thread designerThread = new Thread((openForm));
                designerThread.SetApartmentState(ApartmentState.STA);
                designerThread.Start();     
            }
        }

        void openForm() {
            Designer dlg = new Designer(_form);
            dlg.ShowDialog();
        }
        #endregion Form Loading

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
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    _settings.visitedImportWizard = true;
                    _settings.saveToFile();
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
                        workingForm.ThisFormsPath = file;//TODO - internal file path for forms
                        _formsToSave.Add(workingForm);
                    }
                } catch (Exception ex) {
                    throw ex;
                }

                new ImportWizard(_formsToSave, _formatSets, this).ShowDialog();
            }
        }

        #endregion Import Forms

        private void btnNewForm_Click(object sender, EventArgs e) {
            PenForm newPenform = new PenForm();
            newPenform.FormName = Interaction.InputBox("Name of form?");
            newPenform.saveForm();

            checkForForms();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new whiteList().ShowDialog();
        }
    }
}