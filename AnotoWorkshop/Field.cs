using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AnotoWorkshop {

    #region Field Type Enum

    public enum Type {//The types of fields you can have in an anoto/nextpen form.
        None,
        TextField,//an input text field that recognizes handwriting and turns it into digital alphanums.
        Checkbox,//check box, attached to an appropriate checkbox field in nextgen/other EMR stuff.
        RichLabel,
        Label,//basically static labels and such.
        OptionsGroup,//The main thing to watch for here is the naming scheme with the XML version.
        LineDraw,
        RectangleDraw
    }

    #endregion Field Type Enum

    //public class textSetPair { //Being replaced by internal RichContent.
    //    public string text { get; set; }
    //    public Font set { get; set; }
    //}

    public class Field {
        #region Variables

        public string name;
        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public Type type;

        public string text;
        public string rtc;

        public FormatSet formatSet = new FormatSet();
        public string formatSetName;

        public int group;
        public bool hidden;
        public bool readOnly;
        public int listIndex;

        public Point moveStart;
        public Size resizeStart;

        public bool selected;

        private double _zoomLevel = 1.00;

        //public List<FormatSet> textTypes;
        //public Dictionary<int, textSetPair> texts;
        //public RichContent richContents = null;
        private RichTextBox _richBox = new RichTextBox();//The working RichTextBox for this RichContent instance.

        
        //calculate, only found in readOnly (AKA Variable info from database/(ei, NextGen)) OR the script stuff.

        #endregion Variables

        #region Initializers

        public Field() {
            name = "blank";
        }

        public Field(string name, Type type) {
            this.name = name;
            this.type = type;
        }

        public Field(string name, int x, int y, int width, int height, Type type) {
            this.name = name;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            this.type = type;
        }

        #endregion Initializers

        #region Universal Properties

        public int zx {
            get { return (int)(_x * _zoomLevel); }
            set { _x = value / _zoomLevel; }
        }

        public int zy {
            get { return (int)(_y * _zoomLevel); }
            set { _y = value / _zoomLevel; }
        }

        public int zwidth {
            get { return (int)(_width * _zoomLevel); }
            set { _width = value / _zoomLevel; }
        }

        public int zheight {
            get { return (int)(_height * _zoomLevel); }
            set { _height = value / _zoomLevel; }
        }

        public int x {
            get { return (int)(_x); }
            set { _x = value; }
        }

        public int y {
            get { return (int)(_y); }
            set { _y = value; }
        }

        public int width {
            get { return (int)(_width); }
            set { _width = value; }
        }

        public int height {
            get { return (int)(_height); }
            set { _height = value; }
        }

        public double zoomLevel {
            get { return _zoomLevel; }
            set { _zoomLevel = value; }
        }

        public RichTextBox richBox {
            get { return _richBox; }
            set { _richBox = value; }
        }

        #endregion Universal Properties

        #region Class Functions

        public bool resizing;   //This bool is used to flag if this field is being resized, this is so that we can find which resizer was clicked with a foreach loop. 
        //TODO - One day after undo/redo we'll have indexed fields, when that happens we should be able to refer to the specific field clicked.

        /// <summary>
        /// This function works out a specific resizer Rectangle depending on what the field's type is.
        /// </summary>
        /// <returns>The Rectangle representing where to click to begin resizing.</returns>
        public Rectangle resizer() {
            Rectangle retRect = new Rectangle();

            switch (type) {
                case Type.TextField:
                    retRect = new Rectangle(
                        (int)((_x + _width + 4) * _zoomLevel),
                        (int)((_y + 6) * _zoomLevel),
                        (int)(4 * _zoomLevel),
                        (int)(4 * _zoomLevel));
                    break;
                case Type.Checkbox:
                    retRect = new Rectangle(
                        (int)((_x + ((_width - 4) / 2)) * _zoomLevel),
                        (int)((_y + ((_height - 4) / 2)) * _zoomLevel),
                        (int)(4 * _zoomLevel),
                        (int)(4 * _zoomLevel));
                    break;
                case Type.RichLabel:
                case Type.RectangleDraw:
                    retRect = new Rectangle(
                        (int)((_x + _width + 4) * _zoomLevel),
                        (int)((_y + _height + 4) * _zoomLevel),
                        (int)(4 * _zoomLevel),
                        (int)(4 * _zoomLevel));
                    break;

            }

            return retRect;
        }

        public bool isInside(int locx, int locy) {
            if (locx > (int)(_x * _zoomLevel) && locy > (int)(_y * _zoomLevel) &&
                locx < (int)(_x * _zoomLevel) + (int)(_width * _zoomLevel) &&
                locy < (int)(_y * _zoomLevel) + (int)(_height * _zoomLevel)) {
                //selected = true;
                return true;
            }
            //selected = false;
            return false;
        }

        public Rectangle rect() {
            Rectangle thisRect = new Rectangle((int)(_x * _zoomLevel), (int)(_y * _zoomLevel), (int)(_width * _zoomLevel), (int)(_height * _zoomLevel));

            return thisRect;
        }

        #endregion Class Functions

        #region Options Group Specific - Not Started

        #endregion Options Group Specific - Not Started

        #region Scripting - Not Started

        #endregion Scripting - Not Started
    }
}