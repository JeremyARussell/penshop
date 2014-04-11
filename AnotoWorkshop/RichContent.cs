using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AnotoWorkshop {

    /// <summary>
    /// This mini-class is used to represent an individual word.
    /// </summary>
    public class Word {
        public Font font;           //The font used for this word
        public string pString;      //The string of the word itself
        public float horizontalPos;   //The horizontalPos is the measured width of the word - Note: may change this later to be the measured width of the previous word
    }

    /// <summary>
    /// This mini-class represents individual lines.
    /// </summary>
    public class Line {
        public Dictionary<int, Word> words = new Dictionary<int, Word>();   //The individuals words that make up this line, ordered by their placement number.
        public int baselineDrop = 0;                                        //The baseline height for the line, based off the tallest word.
    }

    public class RichContent {
        public Dictionary<int, Font> usedFormatSets = new Dictionary<int, Font>();//An ordered set of FormatSets...
        public Dictionary<int, char> usedCharacters = new Dictionary<int, char>();//                            ...and the characters that line up with them

        //a calculated returned width - I think I meant a running calculated width, not sure right now.

        private Size _size = new Size();//A max width and height for the wordwrapping.

        public Dictionary<int, Line> lines = new Dictionary<int, Line>();//This RichContent instances order dictionary of lines that make it up.

        private RichTextBox _box = new RichTextBox();//The working RichTextBox for this RichContent instance.

        //Constructed out of a richTextBox that get's passed along from the startEditing and endEditing parts of the main code.
        public RichContent (RichTextBox box, Size size) {
            _box = box;
            _size = size;

            parseIntoDictionaries();
            parseOutLines();
        }

        //TODO - Will need a constructor to handle loading this class from the xml .penform file.
        
        /*A method/algorithm that goes through each character and font of the richTextBox and calculates the width,
         * the baseline height, the words and lines that use the height. Using the max width of the entire 
         * RichLabel/FancyLabel.
         */

        private void parseIntoDictionaries() {
            if (_box.TextLength > 0) {
                for (int i = 0; i < _box.TextLength; i++) {
                    _box.SelectionStart = i;
                    _box.SelectionLength = 1;

                    usedCharacters.Add(i, Convert.ToChar(_box.SelectedText));
                    usedFormatSets.Add(i, _box.SelectionFont);
                }
            }
        }



        private void parseOutLines() {
            lines.Clear();

            Word workingWord = null;
            int currentWordNumber = 0;

            Line workingLine = new Line();
            int currentLineNumber = 0;

            float runningWidth = 0;

            StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

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
                             workingWord.horizontalPos = g.MeasureString(stringToAdd, setToAdd, new Point(0, 0), sf).Width + runningWidth;
                        }

                        runningWidth = workingWord.horizontalPos;


                        if (runningWidth > _size.Width) {

                            lines.Add(currentLineNumber, workingLine);

                            currentLineNumber++;

                            workingLine = new Line();
                            currentWordNumber = 0;
                            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) {
                                 runningWidth = g.MeasureString(workingWord.pString, workingWord.font, new Point(0, 0), sf).Width;
                            }

                            workingWord.horizontalPos = runningWidth;
                            workingLine.words.Add(currentWordNumber, workingWord);
                            currentWordNumber++;

                        } else {
                            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) {
                                if (workingLine.baselineDrop < g.MeasureString(workingWord.pString, workingWord.font, new Point(0, 0), sf).Height) 
                                    workingLine.baselineDrop = (int) g.MeasureString(workingWord.pString, workingWord.font, new Point(0, 0), sf).Height;
                                }
                            workingLine.words.Add(currentWordNumber, workingWord);
                            currentWordNumber++;
                        }


                        //If the workingWord plus the prior words total widths add up to less than the wrapping width the workingWord
                        //...is added to the currentLine, if it's not we first increment the currentLine and add it to this latest line.

                        workingString = "";

                    } else {
                        workingString += usedCharacters[i];
                    }

                    if (i == usedCharacters.Count - 1) {
                        lines.Add(currentLineNumber, workingLine);
                    }
 
                }



            }



        }


        /*
         * private void parseIntoWords(){
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
        }*/

        /*A method which returns a richTextBox, with the .Text and .Size properties preset.
         */


    }
}
