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

        private string _fusionPrintFilePath;

        #region Loading FusionPrintConfig

        //Exactly what it sounds like.
        public string TheQuery;

        public List<Alias> TheAliases;

        private void loadPrintConfig() {
            string printConfigText = File.ReadAllText(_fusionPrintFilePath);//Loads the printconfig file into this string

            int startPos = printConfigText.IndexOf("ScheduleQuery");//Find the start of the ScheduleQuery string that contains the variables we need when a nextPen/Anoto form is printed.
            int queryStart = printConfigText.IndexOf("\"", startPos);//The starting position of the ScheduleQuery, which is a " mark
            int queryEnd = printConfigText.IndexOf("\"", queryStart + 1);//The ending position of the ScheduleQuery, also a " mark, the + 1 is an offset to get the right spot. Kinda forgot exactly what for. TODO - Will test later

            TheQuery = printConfigText.Substring(queryStart + 1, queryEnd - queryStart - 1);//The offets are for things like an equal sign between "ScheduleQuery" and the first " mark.
            txt_varQuery.Text = TheQuery;//Don't need this as a placeholder for the text anymore, but it's usefull right now and I'll probably use it to for the time being to edit the query itself.

            TheAliases = new List<Alias>();//Initializing

            int posAlongQuery = 7;
            foreach (Match m in Regex.Matches(TheQuery, @"\sAS\s([^\s]+)")) {//Matches AS statements
                if (!isInCast(m.Index)) { //If the index of this " AS " is NOT in a CAST/CONVERT*
                    //Grabbing the name
                    int nameLength = m.Value.Length - 4;
                    string tempName = m.Value.Substring(4, nameLength);
                    //Grabbing the local start and end(length)
                    int tempStartPos = 0 + posAlongQuery;
                    int queryLength = m.Index + nameLength + 4;
                    //Grabbing the query based off the start and length
                    string tempQuery = TheQuery.Substring(tempStartPos, queryLength - tempStartPos);

                    Alias tempAlias = new Alias(tempName, tempStartPos, queryLength - tempStartPos + tempStartPos, tempQuery);//Turn it into a class that includes name, string, startPos and endPos
                    TheAliases.Add(tempAlias);//Add the instance
                    lstv_Variables.Items.Add(tempAlias.name);

                    posAlongQuery = queryLength + 1;//Incrementing to keep it going.
                }//This is the alias grabber specific stuff. Could be turned into a method later but it's fine where it's at right now.
            }

            Match test = Regex.Match(TheQuery, @"WHERE", RegexOptions.IgnoreCase);
            txt_QueryFrom.Text = TheQuery.Substring(TheAliases[TheAliases.Count - 1].endPos, test.Index - TheAliases[TheAliases.Count - 1].endPos);//Snags the FROM section.

            txt_QueryWhere.Text = TheQuery.Substring(TheAliases[TheAliases.Count - 1].endPos + test.Index - TheAliases[TheAliases.Count - 1].endPos);//And the WHERE section. With the conditions. TODO - create where grabber for the conditions.
        }

        private bool isInCast(int i) {//Pretty straight forward, this take an index and checks if the words matched is within a CAST or CONVERT statement, returns true or false so appropriate actions can be taken.
            foreach (Match m in Regex.Matches(TheQuery, @"CAST\(([^\)]+)\)|convert\(([^\)]+)\)|CONVERT\(([^\)]+)\)")) {//loop through search material for regex patterns that match convert and CAST statements
                if (i > m.Index && i < m.Index + m.Length) {//If an AS is inside one of those statements the function returns true
                    return true;
                }
            }
            return false;
        }

        private void lstv_Variables_DoubleClick(object sender, EventArgs e) {//Loads the double clicked item's query string into a textfield
            int index;
            index = lstv_Variables.SelectedItems[0].Index;
            txt_QueryVars.Text = TheAliases[index].aQuery;
        }

        #endregion Loading FusionPrintConfig

        private void variablesBuilder_Load(object sender, EventArgs e) {
            _fusionPrintFilePath = Application.StartupPath + @"\FusionPrintConfig.xml";//TODO - move into settings file
            loadPrintConfig();//Better method, faster and more effecient. No need to cleanup, etc.
        }
    }
}