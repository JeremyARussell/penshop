using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AnotoWorkshop {

    public partial class aliasBuilder : Form {

        public aliasBuilder() {
            InitializeComponent();
        }

        //private string _fusionPrintFilePath;

        #region Loading FusionPrintConfig

        //Exactly what it sounds like.
        //public string TheQuery;

        //public List<Alias> TheAliases;


        private void lstv_Variables_DoubleClick(object sender, EventArgs e) {//Loads the double clicked item's query string into a textfield
            int index;
            index = lstv_Variables.SelectedItems[0].Index + 1;
            txt_QueryVars.Text = test.sectionList[index].contents;
        }

        #endregion Loading FusionPrintConfig

        private void variablesBuilder_Load(object sender, EventArgs e) {
            //_fusionPrintFilePath = Application.StartupPath + @"\FusionPrintConfig.xml";//TODO - move into settings file
            //loadPrintConfig();//Better method, faster and more effecient. No need to cleanup, etc.
        }

        PrintConfig test;

        private void btnTest_Click(object sender, EventArgs e)
        {
            test = new PrintConfig(Application.StartupPath + @"\FusionPrintConfig.xml");

            foreach (var testvar in test.sectionList) {
                lstv_Variables.Items.Add(testvar.Key.ToString());
            }
        }
    }
}