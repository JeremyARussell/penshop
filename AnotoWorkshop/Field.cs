using System.Collections.Generic;
using System.Drawing;

namespace AnotoWorkshop {

    #region Field Type Enum

    public enum Type {//The types of fields you can have in an anoto/nextpen form.
        None,
        TextField,//an input text field that recognizes handwriting and turns it into digital alphanums.
        Checkbox,//check box, attached to an appropriate checkbox field in nextgen/other EMR stuff.
        FancyLabel,
        Label,//basically static labels and such.
        OptionsGroup,//The main thing to watch for here is the naming scheme with the XML version.
        LineDraw,
        RectangleDraw
    }

    #endregion Field Type Enum

    public class Field //Franklin
    {//Advanced type stuff is filled when the add button is clicked for each guy.
        #region Variables

        public string name;
        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public Type type;

        public string text;

        public FormatSet formatSet = new FormatSet();
        public string formatSetName;

        public int group;
        public bool hidden;
        public bool readOnly;

        public Point moveStart;

        public bool selected;

        private double _zoomLevel = 1.00;

        public List<FormatSet> textTypes;
        public List<string> texts;

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

        #endregion Universal Properties

        #region Class Functions

        public bool isInside(int locx, int locy) {
            if (locx > (int)(_x * _zoomLevel) && locy > (int)(_y * _zoomLevel) &&
                locx < (int)(_x * _zoomLevel) + (int)(_width * _zoomLevel) &&
                locy < (int)(_y * _zoomLevel) + (int)(_height * _zoomLevel)) {
                selected = true;
                return true;
            }
            selected = false;
            return false;
        }

        public Rectangle rect() {
            Rectangle thisRect = new Rectangle((int)(_x * _zoomLevel), (int)(_y * _zoomLevel), (int)(_width * _zoomLevel), (int)(_height * _zoomLevel));

            return thisRect;
        }

        #endregion Class Functions

        #region Fancy Field Specific - Not Started

        #endregion Fancy Field Specific - Not Started

        #region Options Group Specific - Not Started

        #endregion Options Group Specific - Not Started

        #region Scripting - Not Started

        #endregion Scripting - Not Started
    }
}