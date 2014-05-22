using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

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


    [DefaultPropertyAttribute("name")]
    public class Field {
        #region Variables

        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public string rtc;

        public int exWidth;
        public int exHeight;

        public string fontTypeface = "Arial";
        public FontStyle fontStyle = FontStyle.Regular;
        public int fontSize = 12;

        public Font font() {

            FontFamily theFontFamily = new FontFamily(fontTypeface);

            Font retFont = new Font(theFontFamily, fontSize, fontStyle);
            return retFont;
        }


        //public int group;
        public int listIndex;

        public Point moveStart;
        public Size resizeStart;

        public bool selected;

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

        [Browsable(true)]
        [ReadOnly(false)]//Subclassing will let us apply read only differently for different field types.
        [Description("The X coordinate of the selected field.")]             
        [Category("Position")]
        [DisplayName("X")]
        public int x {
            get { return (int)(_x); }
            set { _x = value; }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("The Y coordinate of the selected field.")]             
        [Category("Position")]
        [DisplayName("Y")]
        public int y {
            get { return (int)(_y); }
            set { _y = value; }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("The width of the selected field.")]             
        [Category("Size")]
        [DisplayName("Width")]
        public int width {
            get { return (int)(_width); }
            set { _width = value; }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("The height of the selected field.")]             
        [Category("Size")]
        [DisplayName("Height")]
        public int height {
            get { return (int)(_height); }
            set { _height = value; }
        }

        [Browsable(false)]
        public RichTextBox richBox {
            get { return _richBox; }
            set { _richBox = value; }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("The name of this field, used to link with tables and columns.")]
        [Category("")]
        [DisplayName("Name")]
        public string name { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("This flag shows if we are using this Text Field for handwriting recognition(false) or displaying patient information(true).")]             
        [Category("Flags")]
        [DisplayName("pt Output")]//TODO - This will not be a showing property once the subclassing is setup, except for the textField
        public bool readOnly { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("This flag is used to toggle between showing and hiding a field.")]             
        [Category("Flags")]
        [DisplayName("Hidden?")]
        public bool hidden { get; set; }

        [Browsable(true)]
        [ReadOnly(true)]
        [Description("The text that belongs to this field.")]             
        [Category("")]
        [DisplayName("")]
        public string text { get; set; }

        [Browsable(true)]
        [ReadOnly(true)]
        [Description("The type of field we have selected.")]             
        [Category("")]
        [DisplayName("Type")]
        public Type type { get; set; }

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
                        (int)(_x + _width + 4),
                        (int)(_y + 6),
                        4,
                        4);
                    break;

                case Type.Checkbox:
                    retRect = new Rectangle(
                        (int)((_x + (_width - 4) / 2)),
                        (int)((_y + (_height - 4) / 2)),
                        4,
                        4);
                    break;

                case Type.RichLabel:
                case Type.RectangleDraw:
                    retRect = new Rectangle(
                        (int)(_x + _width + 4),
                        (int)(_y + _height + 4),
                        4,
                        4);
                    break;

            }

            return retRect;
        }

        public bool isInside(int locx, int locy) {
            if (type == Type.Checkbox) {
                if (locx > (int)(_x) && 
                    locy > (int)(_y) &&
                    locx < (int)(_x) + (int)(_width) + exWidth &&
                    locy < (int)(_y) + (int)(_height) + exHeight) {
                    return true;
                }
            } else {
                if (locx > (int)(_x) && 
                    locy > (int)(_y) &&
                    locx < (int)(_x) + (int)(_width) &&
                    locy < (int)(_y) + (int)(_height)) {
                    return true;
                }
            }
            return false;
        }

        public Rectangle rect() {
            Rectangle thisRect = new Rectangle((int)(_x), (int)(_y), (int)(_width), (int)(_height));

            return thisRect;
        }

        #endregion Class Functions

        #region Options Group Specific - Not Started

        #endregion Options Group Specific - Not Started

        #region Scripting - Not Started

        #endregion Scripting - Not Started
    }
}