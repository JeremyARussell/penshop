using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace AnotoWorkshop {

    internal class Settings {

        #region Variables
        public string formsFolderLocation = @"";
        public string exportFolder = @"";

        #region Tutorial Flags
        public bool visitedLoadingScreen;
        public bool visitedImportWizard;
        public bool visitedSettingsScreen;
        //public bool visitedAliasBuilder;
        //public bool visitedImportWizard;
        //public bool visitedImportWizard;
        //public bool visitedImportWizard;
        //public bool visitedImportWizard;
        //public bool visitedImportWizard;
        //public bool visitedImportWizard;
        #endregion Tutorial Flags

        private string _saveDirectory = System.Windows.Forms.Application.StartupPath;
        private Dictionary<string, FormatSet> _globalFormatSet;
        private Dictionary<string, Alias> _globalAliases;

        private static Settings _instance;


        #endregion Variables

        #region Instance Management

        public static Settings instance {
            get {
                if (_instance == null) {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

        private Settings() {
            _globalFormatSet = new Dictionary<string, FormatSet>();
            _globalAliases = new Dictionary<string, Alias>();

            loadFromFile();

        }

        #endregion Instance Management

        #region Create New File

        private void createNewFile() {
            formsFolderLocation = @"C:\PenForms\";//TODO - Turn into a message box like the adding textfield
            saveToFile();
        }

        #endregion Create New File

        #region File Loading
        private readonly XmlDocument _dom = new XmlDocument();

        public void loadFromFile() {
            try {
                _dom.Load(_saveDirectory + @"\settings.xml");

                XmlReader node = XmlReader.Create(new StringReader(_dom.DocumentElement.OuterXml));

                while (node.Read()) {

                    #region Load General Settings
                    if (node.Name == @"General") {//Loading the misc FormatSets
                        formsFolderLocation = node["formsFolderLocation"].ToString();
                        exportFolder = node["exportFolder"].ToString();
                    }
                    #endregion Load General Settings

                    #region Load FormatSets
                    if (node.Name == @"FormatSet") {//Loading the misc FormatSets
                        string tempKey = node["name"].ToString();
                        string tempFont = node["fontType"].ToString();
                        int tempFontSize = Convert.ToInt32(node["fontSize"].ToString());
                        string tempFontWeight = node["fontWeight"].ToString();

                        FormatSet tempSet = new FormatSet();

                        tempSet.fontTypeface = tempFont;
                        tempSet.fontSize = tempFontSize;
                        tempSet.fontWeight = tempFontWeight;
                        tempSet.name = tempKey;

                        _globalFormatSet.Add(tempKey, tempSet);
                    }
                    #endregion Load FormatSets

                    #region Load Visisted Flags

                    if (node.Name == @"Flags") {
                        bool.TryParse(node["visitedLoadingScreen"], out visitedLoadingScreen);
                        bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        bool.TryParse(node["visitedSettingsScreen"], out visitedSettingsScreen);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        //bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                    }

                    #endregion Load Visited Flags

                }
            }

            catch (Exception) {
                MessageBox.Show("Something weird happened loading the settings file, either it wasn't "
                + "there or it didn't contain the correct information. -- TODO: Create backup and rewrite file."
                    , "Settings File Load Error", MessageBoxButtons.OK);
                createNewFile();
                //TODO - Handle backup, upgrades, etc.
                //throw; - This was preventing the settings from instantiating and the loading screen 
                //from loading all the way
            }
        }
        #endregion File Loading

        #region File Saving
        public void saveToFile() {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
            using (XmlWriter writer = XmlWriter.Create(_saveDirectory + @"\settings.xml", settings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");

                #region Save General Settings
                writer.WriteStartElement("General");
                writer.WriteAttributeString("formsFolderLocation", formsFolderLocation);
                writer.WriteAttributeString("exportFolder", exportFolder);

                writer.WriteEndElement();//General
                #endregion Save General Settings

                #region Saving Format Sets
                writer.WriteStartElement("FormatSets");

                foreach (var set in _globalFormatSet) {
                    writer.WriteStartElement("FormatSet");
                    writer.WriteAttributeString("name", set.Key);

                    writer.WriteAttributeString("fontType", set.Value.fontTypeface);
                    writer.WriteAttributeString("fontSize", set.Value.fontSize.ToString());
                    writer.WriteAttributeString("fontWeight", set.Value.fontWeight);

                    writer.WriteEndElement();//FormatSet
                }

                writer.WriteEndElement();//FormatSet
                #endregion Saving Format Sets

                #region First Time Flags

                writer.WriteStartElement("Flags");
                writer.WriteAttributeString("visitedLoadingScreen", visitedLoadingScreen.ToString());
                writer.WriteAttributeString("visitedImportWizard", visitedImportWizard.ToString());
                writer.WriteAttributeString("visitedSettingsScreen", visitedSettingsScreen.ToString());

                writer.WriteEndElement();//Flags

                #endregion First Time Flags

                writer.WriteEndElement();//Settings
                writer.WriteEndDocument();
            }
        }
        #endregion File Saving

        #region Upgrading

        private void upgradeSettingsFile() {//Just in case an upgrade requires careful handling of the settings file itself
            //Load current settings -> Create new version of settings file -> rename old settings file for roll-back purposes->
            //Save new settings file -> confirm settings are fine and reopen program.
        }

        #endregion Upgrading

        #region Aliases

        public Dictionary<string, Alias> globalAliases {
            get { return _globalAliases; }
            set { _globalAliases = value; }
        }

        #endregion Aliases

        #region FormatSets

        public Dictionary<string, FormatSet> globalFormatSet {
            get { return _globalFormatSet; }
            set { _globalFormatSet = value; }
        }

        public string saveDirectory {
            get { return _saveDirectory; }
            set { _saveDirectory = value; }
        }

        public FormatSet getFormatSetByName(string name) {
            return _globalFormatSet[name];
        }

        #endregion FormatSets

    }
}