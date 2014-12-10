using System;
using System.Drawing;

namespace AnotoWorkshop {

    public class OBS_FormatSet {

        #region Variables

        public string fontTypeface { get; set; }//typeface - "Arial"
        public string fontWeight { get; set; }//weight - bold
        public string name { get; set; }

        private float _fontSize;//size - 14pt, 12pt

        //baselineShift - 0pt - Todo - Won't do this one right now
        //node for fill color - Todo - was on colored text, won't worry about right now

        //A Brief history of margins - They suck, we aren't going to do them. They are either blank or 0, 0, 0, 0 in value anyways...
        //para, for draw or labels it's a 0, 0, 0, 0, 0 pattern. fields(checkbox) is vAlign = "middle"

        //////////////////////////////Framework//////////////////////////////

        //Color - (int, int, int)

        /////////////////////////////////END/////////////////////////////////

        #endregion Variables

        #region Initializers

        public OBS_FormatSet() {
            //_name = "empty";
            _fontSize = 10;
            fontTypeface = "Arial";
            fontWeight = "normal";
        }

        #endregion Initializers

        #region Properties

        public float fontSize {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        public string fontSizeString {
            get { return _fontSize.ToString() + "pt"; }
            set { _fontSize = Convert.ToInt32(value.Substring(0, value.IndexOf("pt"))); }
        }

        //Todo - extrapolate font Style from string. Probably a search against the possible types, bold italic etc. (ENUM PARSING)
        public Font font() {
            FontStyle theFontStyle;
            switch (fontWeight) {
                case "bold":
                    theFontStyle = FontStyle.Bold;
                    break;

                case "italic":
                    theFontStyle = FontStyle.Italic;
                    break;

                case "normal":
                    theFontStyle = FontStyle.Regular;
                    break;

                default:
                    goto case "normal";
            }

            FontFamily theFontFamily = new FontFamily(fontTypeface);

            Font retFont = new Font(theFontFamily, _fontSize, theFontStyle);
            return retFont;
        }

        #endregion Properties
    }
}