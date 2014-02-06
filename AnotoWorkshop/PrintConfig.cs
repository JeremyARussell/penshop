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
                if (!isInQuote(m.Index, _scheduleQuery) &&
                    !isInParens(m.Index, _scheduleQuery) &&
                    !afterWhere(m.Index, _scheduleQuery)) 
                {
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
                //if(!isInQuote(m.Index, _scheduleQuery) && !isInParens(m.Index, _scheduleQuery) && !afterWhere(m.Index, _scheduleQuery)) {
                    int tempStartPos = 0 + posAlongQuery;
                    int queryLength = m.Index;// +m.Value.Length; - Seriously, programming is craycray sometimes.

                    string workingQueryPart = _scheduleQuery.Substring(tempStartPos, queryLength - tempStartPos);

                    sectionList.Add(sectionIndex, new Section(workingQueryPart));

                    posAlongQuery = queryLength;
                    sectionIndex++;
               // }
            }

            //Where spot
            Match whereMatch = Regex.Match(_scheduleQuery, @"where", RegexOptions.IgnoreCase);

            string whereContents = _scheduleQuery.Substring(whereMatch.Index);
            sectionList.Add(sectionIndex, new Section(whereContents));
        }

        private bool afterWhere(int i, string checkAgainst) {
            Match m = Regex.Match(checkAgainst, @"where", RegexOptions.IgnoreCase);
            if (i > m.Index) {
                return true;
            }
            return false;
        }

        private bool isInCast(int i, string checkAgainst) {
            foreach (Match m in Regex.Matches(checkAgainst, @"CAST\(([^\)]+)\)|CONVERT\(([^\)]+)\)", RegexOptions.IgnoreCase)) {
                if (i > m.Index && i < m.Index + m.Length) {
                    return true;
                }
            }
            return false;
        }

        private bool isInQuote(int i, string checkAgainst) {
            foreach (Match m in Regex.Matches(checkAgainst, @"', '", RegexOptions.IgnoreCase)) {
                if (i > m.Index && i < m.Index + m.Length) {
                    return true;
                }
            }
            return false;
        }

        private bool isInParens(int i, string checkAgainst) {
            foreach (Match m in Regex.Matches(checkAgainst, @"\([^()]*((?<paren>\()[^()]*|(?<close-paren>\))[^()]*)*(?(paren)(?!))\)")) {
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

            processSection();
            //Do some analysis on the contents to figure out 

            //Name is sometimes empty, sometimes, maybe never though.

        }

        private void processSection() {
            Match selectMatch = Regex.Match(_contents, @"select ", RegexOptions.IgnoreCase);
            Match columnMatch = Regex.Match(_contents, @"[aA-zZ]+\.[aA-zZ]+", RegexOptions.IgnoreCase);
            Match aliasMatch = Regex.Match(_contents, @"\sas\s", RegexOptions.IgnoreCase);
            Match fromMatch = Regex.Match(_contents, @"from", RegexOptions.IgnoreCase);
            Match joinMatch = Regex.Match(_contents, @"((left(\s+outer)?|inner)\s+)?join\b", RegexOptions.IgnoreCase);
            Match whereMatch = Regex.Match(_contents, @"where", RegexOptions.IgnoreCase);

            if (selectMatch.Success) {
                _type = SectionType.Select;
            }

            if (columnMatch.Success) {
                _type = SectionType.Column;
            }

            if (aliasMatch.Success) {
                _type = SectionType.Alias;
            }

            if (fromMatch.Success) {
                _type = SectionType.From;
            }

            if (joinMatch.Success) {
                _type = SectionType.Join;
            }

            if (whereMatch.Success) {
                _type = SectionType.Where;
            }
        }

        public string type() {

            return _type.ToString();
            
        }

        public string contents {
            get { return _contents; }
            set { _contents = value; }
        }
    }
}
