using System;
using System.Collections.Generic;
using System.Drawing;//May not need
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnotoWorkshop {

    public class Word {
        public Font font;
        public string pString;
        public int horizontalPos;
    }

    public class Line {
        public List<Word> words = new List<Word>() ;
        public int baselineDrop = 0;
    }

    public class RichContent {
        ////Dictionary sets matched using common index.
        //a font set
        public Dictionary<int, Font> usedFormatSets = new Dictionary<int, Font>();
        //a character set
        public Dictionary<int, char> usedCharacters = new Dictionary<int, char>(); 
        //a calculated returned width - I think I meant a running calculated width, not sure right now.

        
        //A max width and height for the wordwrapping.
        private Size _size = new Size();

        //words that represent grouped characters with common formats
        //returned "lines" and there calculated, respective content and (sizes) - ()for the fact that they cannot be wider than the max width
        public Dictionary<int, Line> lines = new Dictionary<int, Line>(); 

        private RichTextBox _box = new RichTextBox();//The working RichTextBox for this RichContent instance.

        //private Graphics _g;

        //Constructed out of a richTextBox that get's passed along from the startEditing and endEditing parts of the main code.
        public RichContent (RichTextBox box, Size size) {
            _box = box;
            _size = size;

            parseIntoDictionaries();

            parseOutLines();
        }
        

        /*A method/algorithm that goes through each character and font of the richTextBox and calculates the width,
         * the baseline height, the words and lines that use the height. Using the max width of the entire 
         * RichLabel/FancyLabel.
         */

        private void parseIntoDictionaries() {
            if (_box.TextLength > 0) {
                for (int i = 0; i < _box.TextLength; i++) {
                    _box.SelectionStart = i;
                    _box.SelectionLength = 1;

                    string stringToAdd = _box.SelectedText;
                    Font fontToAdd = _box.SelectionFont;

                    usedCharacters.Add(i, Convert.ToChar(stringToAdd));
                    usedFormatSets.Add(i, fontToAdd);
                }
            }
        }



        private void parseOutLines() {
            lines.Clear();

            Word workingWord = null;
            //Font workingFont = null;

            Line workingLine = new Line();
            int currentLineNumber = 0;

            int runningWidth = 0;

            if (usedCharacters.Count > 0) {
                
                string workingString = null;
                Font prevFormat = null;

                for (int i = 0; i < usedCharacters.Count; i++) {

                    if (i == 0) {
                        prevFormat = usedFormatSets[i];
                        workingString = "";
                    }

                    if (!prevFormat.Equals(usedFormatSets[i]) || //A change in Font causes us to make a new word from the prevtText
                        usedCharacters[i].ToString() == " " || //A space causes us to make a new word out of the prevText
                        i == usedCharacters.Count - 1) {

                        workingWord = new Word();

                        workingString = workingString + usedCharacters[i];

                        string stringToAdd = workingString;
                        Font setToAdd = prevFormat;

                        prevFormat = usedFormatSets[i];

                        workingWord.pString = stringToAdd;
                        workingWord.font    = setToAdd;

                        using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) {
                             workingWord.horizontalPos = (int)g.MeasureString(stringToAdd, setToAdd).Width + runningWidth;
                        }

                        runningWidth = workingWord.horizontalPos;


                        if (runningWidth > _size.Width) {

                            //BASELINE CHECK BITCHES


                            lines.Add(currentLineNumber, workingLine);

                            currentLineNumber++;

                            workingLine = new Line();
                            runningWidth = 0;

                            workingLine.words.Add(workingWord);


                        } else {
                            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) {
                                if (workingLine.baselineDrop < (int)g.MeasureString(workingWord.pString, workingWord.font).Height) 
                                    workingLine.baselineDrop = (int)g.MeasureString(workingWord.pString, workingWord.font).Height;
                                }
                            workingLine.words.Add(workingWord);
                        }


                        //If the workingWord plus the prior words total widths add up to less than the wrapping width the workingWord
                        //...is added to the currentLine, if it's not we first increment the currentLine and add it to this latest line.

                        workingString = "";

                    } else {
                        workingString = workingString + usedCharacters[i];
                    }

                    if (i == usedCharacters.Count - 1) {
                        lines.Add(currentLineNumber, workingLine);
                    }
 
                }



            }



        }


        private void parseIntoWords(){
            var tempFont = new Dictionary<int, Font>();
            var tempStringSet = new Dictionary<int, string>();

            if (_box.TextLength > 0) {
                string prevText = null;
                Font prevFormat = null;

                for (int i = 0; i < _box.TextLength; i++) {
                    _box.SelectionStart = i;
                    _box.SelectionLength = 1;

                    if (i == 0) {
                        prevFormat = _box.SelectionFont;
                    }

                    if(!prevFormat.Equals(_box.SelectionFont)) {
                        Font setToAdd = null;

                        string stringToAdd = prevText;
                        setToAdd = prevFormat;

                        prevFormat = _box.SelectionFont;
                        tempStringSet.Add(tempStringSet.Count, stringToAdd);
                        tempFont.Add(tempFont.Count, setToAdd);
                        prevText = _box.SelectedText;
                    } else {
                        prevText = prevText + _box.SelectedText;
                    }
                    if (i == _box.TextLength - 1) {
                        Font setToAdd = null;

                        string stringToAdd = prevText;

                        setToAdd = prevFormat;
                        prevFormat = _box.SelectionFont;
                        tempStringSet.Add(tempStringSet.Count, stringToAdd);
                        tempFont.Add(tempFont.Count, setToAdd);
                    }
                }
            }
        }

        /*A method which returns a richTextBox, with the .Text and .Size properties preset.
         */


    }
}
