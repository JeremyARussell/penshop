using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnotoWorkshop {
    class PrintConfig {
        //Variables
        private Dictionary<int, Section> _sectionList;
        private string _configFilePath;
        private string _scheduleQuery;

        //private string _placeStringWhere;
        //private string _placeStringFrom;

        

        public PrintConfig(string path) {
            _sectionList = new Dictionary<int, Section>();

            _configFilePath = path;
            extractQuery();
        }

        public string scheduleQuery {
            get { return _scheduleQuery; }
            set { _scheduleQuery = value; }
        }

        public Dictionary<int, Section> sectionList {
            get { return _sectionList; }
        }


        private void extractQuery() {
            string printConfigText = File.ReadAllText(_configFilePath);//Loads the printconfig file into this string

            int startPos = printConfigText.IndexOf("ScheduleQuery");//Find the start of the ScheduleQuery string that contains the variables we need when a nextPen/Anoto form is printed.
            int queryStart = printConfigText.IndexOf("\"", startPos);//The starting position of the ScheduleQuery, which is a " mark
            int queryEnd = printConfigText.IndexOf("\"", queryStart + 1);//The ending position of the ScheduleQuery, also a " mark, the + 1 is an offset to get the right spot. Kinda forgot exactly what for. TODO - Will test later

            _scheduleQuery = printConfigText.Substring(queryStart + 1, queryEnd - queryStart - 1);//The offets are for things like an equal sign between "ScheduleQuery" and the first " mark.

            int posAlongQuery = 0;
            int sectionIndex = 1;

            Match selMatch = Regex.Match(_scheduleQuery, @"select ", RegexOptions.IgnoreCase);

            string selectContents = _scheduleQuery.Substring(0, selMatch.Length);
            sectionList.Add(sectionIndex, new Section(selectContents));
            sectionIndex++;
            posAlongQuery = selMatch.Length;

            foreach(Match m in Regex.Matches(_scheduleQuery, @",\s")) {//Capturing the comma seperated columns and aliases
                if(!isInQuote(m.Index) && !isInParens(m.Index) && !afterWhere(m.Index)) {
                    int tempStartPos = 0 + posAlongQuery;
                    int queryLength = m.Index + m.Value.Length;
                    //Grabbing the query based off the start and length
                    string workingQueryPart = _scheduleQuery.Substring(tempStartPos, queryLength - tempStartPos);

                    sectionList.Add(sectionIndex, new Section(workingQueryPart));

                    posAlongQuery = queryLength;//Incrementing to keep it going.
                    sectionIndex++;
                }
            }

            //From spot
            foreach(Match m in Regex.Matches(_scheduleQuery, @"from|((left(\s+outer)?|inner)\s+)?join\b")) {
                if(!isInQuote(m.Index) && !isInParens(m.Index) && !afterWhere(m.Index)) {
                    int tempStartPos = 0 + posAlongQuery;
                    int queryLength = m.Index;// +m.Value.Length; - Seriously, programming is craycray sometimes.

                    string workingQueryPart = _scheduleQuery.Substring(tempStartPos, queryLength - tempStartPos);

                    sectionList.Add(sectionIndex, new Section(workingQueryPart));

                    posAlongQuery = queryLength;
                    sectionIndex++;
                }
            }

            
            
            
            
            //Where spot
            //Match whereMatch = Regex.Match(_scheduleQuery, @"where", RegexOptions.IgnoreCase);

                       





            //txt_varQuery.Text = _scheduleQuery/*TheQuery*/;//Don't need this as a placeholder for the text anymore, but it's usefull right now and I'll probably use it to for the time being to edit the query itself.
            // _aliasList - TheAliases = new List<Alias>();//Initializing
            /*
            int posAlongQuery = 7;
            foreach (Match m in Regex.Matches(_scheduleQuery, @"\sAS\s([^\s]+)", RegexOptions.IgnoreCase)) {//Matches AS statements
                if (!isInCast(m.Index)) { //If the index of this " AS " is NOT in a CAST/CONVERT*
                    //Grabbing the name
                    int nameLength = m.Value.Length - 4;
                    string tempName = m.Value.Substring(4, nameLength);
                    //Grabbing the local start and end(length)
                    int tempStartPos = 0 + posAlongQuery;
                    int queryLength = m.Index + nameLength + 4;
                    //Grabbing the query based off the start and length
                    string workingQueryPart = _scheduleQuery.Substring(tempStartPos, queryLength - tempStartPos);
                    //Alias tempAlias = new Alias(tempName,                                  //Name            --turning to dictionary key
                    //                            tempStartPos,                              //Start Int       --Incorporate into this 
                    //                            queryLength - tempStartPos + tempStartPos, //End Int         --function.
                    //                            workingQueryPart);                         //Complete Query for alias. -- still the query
                    /*_aliasList - TheAliases.Add(tempAlias);*
                    // Switch to happen in the form itself - lstv_Variables.Items.Add(tempAlias.name);
                    posAlongQuery = queryLength + 1;//Incrementing to keep it going.
                }
            }
            */
            //This below needs to be in a sort of parrelel managed string that represents the WHERE and FROM statements in the nextpen's
            //form. 
            
            //Match test = Regex.Match(_scheduleQuery, @"WHERE", RegexOptions.IgnoreCase);
            //_placeStringFrom = _scheduleQuery.Substring(TheAliases[TheAliases.Count - 1].endPos, test.Index - TheAliases[TheAliases.Count - 1].endPos);//Snags the FROM section.

            //_placeStringWhere = _scheduleQuery.Substring(TheAliases[TheAliases.Count - 1].endPos + test.Index - TheAliases[TheAliases.Count - 1].endPos);//And the WHERE section. With the conditions. TODO - create where grabber for the conditions.
        }

        private bool afterWhere(int i) {
            Match m = Regex.Match(_scheduleQuery, @"where", RegexOptions.IgnoreCase);
            if (i > m.Index) {
                return true;
                
                }
            return false;
        }

        private bool isInCast(int i) {
            foreach (Match m in Regex.Matches(_scheduleQuery, @"CAST\(([^\)]+)\)|CONVERT\(([^\)]+)\)", RegexOptions.IgnoreCase)) {
                if (i > m.Index && i < m.Index + m.Length) {
                    return true;
                }
            }
            return false;
        }

        private bool isInQuote(int i) {
            foreach (Match m in Regex.Matches(_scheduleQuery, @"', '", RegexOptions.IgnoreCase)) {
                if (i > m.Index && i < m.Index + m.Length) {
                    return true;
                }
            }
            return false;
        }

        private bool isInParens(int i) {
            foreach (Match m in Regex.Matches(_scheduleQuery, @"\([^()]*((?<paren>\()[^()]*|(?<close-paren>\))[^()]*)*(?(paren)(?!))\)")) {
                if (i > m.Index && i < m.Index + m.Length) {
                    return true;
                }
            }
            return false;
        }

    }

    public enum SectionType {
        Select,
        Column,
        Alias,
        From,
        Join,
        Where

    }

    public class Section {
        private SectionType _type;
        private string _contents;
        private string _name;

        public Section(string contents) {
            _contents = contents;


            //Do some analysis on the contents to figure out 

            //Name is sometimes empty, sometimes, maybe never though.

        }

        public string contents {
            get { return _contents; }
        }
    }
}
