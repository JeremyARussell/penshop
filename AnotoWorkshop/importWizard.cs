using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AnotoWorkshop {

    public partial class ImportWizard : Form {

        #region Variables

        private Settings _settings;
        private List<PenForm> _formsToSave = new List<PenForm>();
        private Dictionary<int, FormatSet> _formatSets = new Dictionary<int, FormatSet>();
        private readonly LoadingScreen _loadingScreen;

        //TODO - Add add the network path for the forms as a prefix. Save in settings file.
        private int _activeFormatSet;

        #endregion Variables

        #region Initailizer

        public ImportWizard(List<PenForm> formsToSave, Dictionary<int, FormatSet> formatSetsToCleanup, LoadingScreen loadingScreen) {
            _settings = Settings.instance;
            _formsToSave = formsToSave;
            _formatSets = formatSetsToCleanup;
            _loadingScreen = loadingScreen;

            InitializeComponent();
        }

        private void importWizard_Load(object sender, EventArgs e) {
            refreshFormsList();
            refreshList();

            foreach (FontFamily font in FontFamily.Families) {//Populate the font list with system fonts
                cmbFontList.Items.Add(font.Name);
            }

            _loadingScreen.Hide();
        }

        #endregion Initailizer

        #region Format Sets

        private void lstvFormatSets_DoubleClick(object sender, EventArgs e) {
            //Show Format Set related fields.
            lblTestFormat.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            txtSetName.Visible = true;
            cmbFontList.Visible = true;
            cmbFontSizes.Visible = true;
            cmbFontWeight.Visible = true;
            btnSaveSetName.Visible = true;
            //Hide Form Saving fields.
            txtFormName.Visible = false;
            btnSaveFormName.Visible = false;
            label5.Visible = false;
            //Fix issue again for if something wasn't clicked and using sender for list variables
            if (((ListView)sender).SelectedItems.Count > 0) {
                _activeFormatSet = ((ListView)sender).SelectedItems[0].Index;
                lblTestFormat.Text = _formatSets[_activeFormatSet].fontSizeString + " - " +
                                     _formatSets[_activeFormatSet].fontTypeface + " - " +
                                     _formatSets[_activeFormatSet].fontWeight;

                lblTestFormat.Font = _formatSets[_activeFormatSet].font();

                txtSetName.Text = _formatSets[_activeFormatSet].name;
                cmbFontSizes.SelectedItem = _formatSets[_activeFormatSet].fontSize.ToString();
                cmbFontList.SelectedItem = _formatSets[_activeFormatSet].fontTypeface;
                cmbFontWeight.SelectedItem = _formatSets[_activeFormatSet].fontWeight;

                txtSetName.Focus();

                AcceptButton = btnSaveSetName;
            }
        }

        private void btnSaveSetName_Click(object sender, EventArgs e) {
            bool needToBreak = false;

            for (int i = 0; i < _formatSets.Count; i++) {
                if (needToBreak) break;//run through all the format sets to make sure there are no duplicate names, displays a message if there is.
                if (txtSetName.Text == _formatSets[i].name) {
                    if (i == _activeFormatSet) break;
                    MessageBox.Show("The Format Set name " + txtSetName.Text + " is already taken. Please choose another."
                        , "Same name error", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    needToBreak = true;
                }
            }

            if (needToBreak) return;

            _formatSets[_activeFormatSet].name = txtSetName.Text;
            _formatSets[_activeFormatSet].fontSize = Convert.ToInt32(cmbFontSizes.SelectedItem.ToString());
            _formatSets[_activeFormatSet].fontTypeface = cmbFontList.SelectedItem.ToString();
            _formatSets[_activeFormatSet].fontWeight = cmbFontWeight.SelectedItem.ToString();

            refreshList();
        }

        private void refreshList() {
            lstvFormatSets.Clear();
            for (int i = 0; i < _formatSets.Count; i++) {//populating the test formatsets to cleanup list
                lstvFormatSets.Items.Add(_formatSets[i].fontSizeString + " - " + _formatSets[i].fontTypeface + " - " +
                                             _formatSets[i].fontWeight + " - " + _formatSets[i].name);
            }
        }

        #endregion Format Sets

        #region Forms To Import

        private void lstvForms_Click(object sender, EventArgs e) {
            //Show Form Saving fields.
            if (((ListView)sender).SelectedItems.Count > 0) {
                txtFormName.Visible = true;
                btnSaveFormName.Visible = true;
                label5.Visible = true;
                //Hide Format Set related fields.
                lblTestFormat.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                txtSetName.Visible = false;
                cmbFontList.Visible = false;
                cmbFontSizes.Visible = false;
                cmbFontWeight.Visible = false;
                btnSaveSetName.Visible = false;

                txtFormName.Focus();
                txtFormName.Text = _formsToSave[((ListView)sender).SelectedItems[0].Index].FormName;

                AcceptButton = btnSaveFormName;
            }
        }

        private void lstvForms_DoubleClick(object sender, EventArgs e) {
            if (((ListView)sender).SelectedItems.Count > 0) {
                int tint = ((ListView)sender).SelectedItems[0].Index;

                PenForm form = new PenForm(_formsToSave[tint].ThisFormsPath, _formatSets);
                //////
                _formsToSave[tint] = form;//Do this again for all of them before importing.
                //////
                //The issue is that when opening the designer form, the form handed off doesn't have te updated formatSet
                //to work with.

                //Todo - Should heavily consider creating an alternative way to load the designerForm to use for importing.
                new Thread(() => new Designer(form).ShowDialog()).Start();
            }
        }

        private void btnSaveFormName_Click(object sender, EventArgs e) {
            try {
                _formsToSave[((ListView)sender).SelectedItems[0].Index].FormName = txtFormName.Text;
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }

            refreshFormsList();
        }

        private void refreshFormsList() {
            lstvForms.Clear();
            foreach (PenForm form in _formsToSave) {//Populating the test form list
                lstvForms.Items.Add(form.FormName);
            }
        }

        #endregion Forms To Import

        #region Finish Importing

        private void btnFinish_Click(object sender, EventArgs e) {
            Directory.CreateDirectory(_settings.formsFolderLocation);

            for (int i = 0; i < _formsToSave.Count; i++) {
                string nameHolder = _formsToSave[i].FormName;
                PenForm form = new PenForm(_formsToSave[i].ThisFormsPath, _formatSets, nameHolder);
                _formsToSave[i] = form;
                _formsToSave[i].saveForm();
            }

            for (int i = 0; i < _formatSets.Count; i++) {//populating the test formatsets to cleanup list
                //add format Auto Naming
                if (_formatSets[i].name == null) {
                    _formatSets[i].name = _formatSets[i].fontSize.ToString() + _formatSets[i].fontTypeface +"_"+ _formatSets[i].fontWeight[0];
                }
                _settings.globalFormatSet.Add(_formatSets[i].name, _formatSets[i]);
            }

            //

            _loadingScreen.Visible = true;
            _loadingScreen.checkForForms();
            Close();
        }

        #endregion Finish Importing
    }
}