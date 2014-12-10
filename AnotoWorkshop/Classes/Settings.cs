using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace AnotoWorkshop {

    internal class Settings {

        #region Variables
        #region File Paths
        public string formsFolderLocation = @"";
        public string exportFolder = @"";
        #endregion File Paths
        #region Network Settings
        private string _user;              //The user name used to access the database connection.
        private string _password;          //The users password TODO - Encrypt this sucka.
        private string _serverName;        //The name of the server on the network this connection is for.
        private string _trusted;           //Whether or not we're using the windows truested method, or SQL.
        private string _databaseName;      //The name of the database we'll be connecting to.
        private int _timeout;              //How long we'll set the timeout for this connection.
        #endregion Network Settings
        #region Tutorial Flags
        public bool visitedLoadingScreen;
        public bool visitedImportWizard;
        public bool visitedSettingsScreen;
        #endregion Tutorial Flags
        #region Private Variables
        private string _saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PenShop\";
        //private Dictionary<string, FormatSet> _globalFormatSet;
        private Dictionary<string, Section> _globalAliases;
        private List<int> _whiteList; 

        private static Settings _instance;
        #endregion Private Variables
        #endregion Variables

        #region Instance Management
        public static Settings instance {
            get { return _instance ?? (_instance = new Settings()); }
        }

        private Settings() {
            loadFromFile();
        }
        #endregion Instance Management

        #region Create New File
        //private void createNewFile() {
            //formsFolderLocation = @"C:\PenForms\";//TODO - See if I need to either turn this into a mbox or if it is and I just need to make this blank
            //saveToFile();
        //}
        #endregion Create New File

        #region File Loading
        private readonly XmlDocument _dom = new XmlDocument();

        /// <summary>
        /// Load the global settings with the information from a settings.xml file.
        /// </summary>
        public void loadFromFile() {
            //_globalFormatSet = new Dictionary<string, FormatSet>();
            _globalAliases = new Dictionary<string, Section>();
            _whiteList = new List<int>();
            
            try {
                _dom.Load(_saveDirectory + @"\settings.xml");
            } catch(Exception) {

                Form pf = new Form();

                MessageBox.Show(pf, "Welcome to the PenShop, let's take a moment to set things up a bit."
                    , "First run", MessageBoxButtons.OK);

                MessageBox.Show(pf, "The first thing to do is select the folder you would like to save PenShop© forms to."
                    , "Choose forms folder", MessageBoxButtons.OK);

                FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
                DialogResult result1 = openFileDialog1.ShowDialog(pf);
                if (result1 == DialogResult.OK) {
                    formsFolderLocation = openFileDialog1.SelectedPath;
                }

                MessageBox.Show(pf, "Next is choosing your NextPen© forms location, this is where your forms will be exported to so that " +
                                "they can be printed by the NextPen© Print program."
                    , "Choose export folder", MessageBoxButtons.OK);

                FolderBrowserDialog openFileDialog2 = new FolderBrowserDialog();
                DialogResult result2 = openFileDialog2.ShowDialog(pf);
                if (result2 == DialogResult.OK) {
                    exportFolder = openFileDialog2.SelectedPath;
                }

                saveToFile();

                MessageBox.Show(pf, "First time setup is complete, enjoy using PenShop"
                    , "All done", MessageBoxButtons.OK);

            }

            try {
                _dom.Load(_saveDirectory + @"\settings.xml");

                XmlReader node = XmlReader.Create(new StringReader(_dom.DocumentElement.OuterXml));

                while (node.Read()) {

                    #region Load General Settings
                    //Loading the filepaths, etc.
                    if (node.Name == @"General") {
                        formsFolderLocation = node["formsFolderLocation"].ToString();
                        exportFolder = node["exportFolder"].ToString();
                    }
                    #endregion Load General Settings

                    #region Load FormatSets
                    //Loading the misc FormatSets
                    /*if (node.Name == @"FormatSet") {
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
                    }*/
                    #endregion Load FormatSets

                    #region Load Visisted Flags
                    if (node.Name == @"Flags") {
                        bool.TryParse(node["visitedLoadingScreen"], out visitedLoadingScreen);
                        bool.TryParse(node["visitedImportWizard"], out visitedImportWizard);
                        bool.TryParse(node["visitedSettingsScreen"], out visitedSettingsScreen);
                    }
                    #endregion Load Visited Flags

                    #region Database Connection Information
                    //Loading the database connection information
                    if (node.Name == @"Connection") {
                        _user = node["user"].ToString();
                        _password = node["password"].ToString();
                        _serverName = node["serverName"].ToString();
                        _trusted = node["trusted"].ToString();
                        _databaseName = node["databaseName"].ToString();
                        _timeout = Convert.ToInt32(node["timeout"].ToString());
                    }
                     #endregion Database Connection Information

                    #region White List
                    //Loading the White List of allowed templates. - FT2D

                    if(node.Name == "WhiteList") {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(node.ReadOuterXml());
                        XmlNode newnode = doc.DocumentElement;


                        processTemplatesList(newnode);
                    }

                    #endregion White List

                }
            }

            catch (Exception) {
                MessageBox.Show("Something weird happened loading the settings file, either it wasn't "
                + "there or it didn't contain the correct information. -- TODO: Create backup and rewrite file."
                    , "Settings File Load Error", MessageBoxButtons.OK);
                //createNewFile();
                //TODO - Handle backup, upgrades, etc.
                //throw; - This was preventing the settings from instantiating and the loading screen 
                //from loading all the way
            }
        }
        
        private void processTemplatesList(XmlNode node) {
            foreach(XmlNode nd in node) {
                if(nd.Name == "Item") {
                    _whiteList.Add(Convert.ToInt32(nd.InnerText));
                }
            }
        }

        #endregion File Loading

        #region File Saving
        public void saveToFile() {

            bool isExists = System.IO.Directory.Exists(_saveDirectory);
            if(!isExists) System.IO.Directory.CreateDirectory(_saveDirectory);

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
                /*writer.WriteStartElement("FormatSets");

                foreach (var set in _globalFormatSet) {
                    writer.WriteStartElement("FormatSet");
                    writer.WriteAttributeString("name", set.Key);

                    writer.WriteAttributeString("fontType", set.Value.fontTypeface);
                    writer.WriteAttributeString("fontSize", set.Value.fontSize.ToString());
                    writer.WriteAttributeString("fontWeight", set.Value.fontWeight);

                    writer.WriteEndElement();//FormatSet
                }

                writer.WriteEndElement();//FormatSets
                 */
                #endregion Saving Format Sets

                #region First Time Flags
                writer.WriteStartElement("Flags");
                writer.WriteAttributeString("visitedLoadingScreen", visitedLoadingScreen.ToString());
                writer.WriteAttributeString("visitedImportWizard", visitedImportWizard.ToString());
                writer.WriteAttributeString("visitedSettingsScreen", visitedSettingsScreen.ToString());

                writer.WriteEndElement();//Flags
                #endregion First Time Flags

                #region Database Connection Information
                writer.WriteStartElement("Connection");
                writer.WriteAttributeString("user", _user);
                writer.WriteAttributeString("password", _password);
                writer.WriteAttributeString("serverName", _serverName);
                writer.WriteAttributeString("trusted", _trusted);
                writer.WriteAttributeString("databaseName", _databaseName);
                writer.WriteAttributeString("timeout", _timeout.ToString());

                writer.WriteEndElement();//Connection
                #endregion Database Connection Information

                #region White List
                //Saving the White List.
                writer.WriteStartElement("WhiteList");//Saving the template association list
                foreach(int template in whiteList) {
                    writer.WriteStartElement("Item");
                    writer.WriteString(template.ToString());
                    writer.WriteEndElement();//Item                   
                }
                writer.WriteEndElement();//WhiteList
                #endregion White List

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
        public Dictionary<string, Section> globalAliases {
            get { return _globalAliases; }
            set { _globalAliases = value; }
        }
        #endregion Aliases

        #region FormatSets
        //public Dictionary<string, FormatSet> globalFormatSet {
        //    get { return _globalFormatSet; }
        //    set { _globalFormatSet = value; }
        //}

        public string saveDirectory {
            get { return _saveDirectory; }
            set { _saveDirectory = value; }
        }

        //public FormatSet getFormatSetByName(string name) {
        //    return _globalFormatSet[name];//TODO - BUG - 11AG
        //}
        #endregion FormatSets

        #region Database Connection String
        public string dbConnectionString {
            get { 
      
                string workingString = "user id=" + _user + ";" +
                                   "password=" + _password + ";" + 
                                   "server=" + _serverName + ";" + 
                                   "Trusted_Connection=" + _trusted + ";" +
                                   "database=" + _databaseName + "; " + 
                                   "connection timeout=" + _timeout.ToString() +"";

                
                return workingString; }

            //set { There is no setter for this property }
        }

        public string dbcUser {
            get { return _user; }
            set { _user = value; }
        }
        public string dbcPassword {
            get { return _password; }
            set { _password = value; }
        }
        public string dbcServerName {
            get { return _serverName; }
            set { _serverName = value; }
        }
        public string dbcTrusted {
            get { return _trusted; }
            set { _trusted = value; }
        }
        public string dbcDatabaseName {
            get { return _databaseName; }
            set { _databaseName = value; }
        }
        public int dbcTimeout {
            get { return _timeout; }
            set { _timeout = value; }
        }

        #endregion Database Connection String

        #region White List


        public List<int> whiteList {
            get { return _whiteList; }
        }

        public void addToWhiteList(int toAdd) {
            if(!whiteList.Contains(toAdd)) whiteList.Add(toAdd);
        }

        public void removeFromWhiteList(int toRemove) {
            if(whiteList.Contains(toRemove)) whiteList.Remove(toRemove);
        }


        #endregion White List

        public settingsScreen screen = new settingsScreen();
    }
}