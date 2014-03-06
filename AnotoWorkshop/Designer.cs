using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class Designer : Form {
        #region Variables
        private PenForm _currentForm;

        private bool _needToSaveForm = false;           //Used to track if we need to show the save or don't save prompt on form close.
        private Settings _settings = Settings.instance; //The Settings Singleton I use in the code for everything meant to be globalally accessible.
        private int _currentPageNumber;                 //Used to track the current page number.
        private Field _fieldToAdd;                      //This field is used when adding fields,

        private Point _startPoint;             //This Point tracks when the mouse is clicked down.
        private Point _endPoint;               //And this Point tracks when the click is released back up.
        private Rectangle _selectionRect;      //This is the Rectangle used to actively track where the user is "selecting"
        private Rectangle _groupSelectionRect; //This Rectangle is the group selection rectangle used to encompass a group of things that have been selected.
        private Rectangle _resRect;            //This is the little blue resizing widget you grab to resize the width of the fields.

        private double _zoomLevel = 1.00;      //Used to zoom in and out of pages.

        private readonly Pen _selectionPen = new Pen(Color.Gray);            //The pen used for the _selectionRect
        private readonly Pen _groupSelectionPen = new Pen(Color.Blue, 2.0f); //The pen used for the _groupSelectionRect

        private MouseMode _mode = MouseMode.None; //The MouseMode is used to keep track of what we should be doing with the mouse during the misc mouse events

        public enum MouseMode {                   //The MouseMode enum, pretty basic here but I'll run through each mode.
            None,                                 //This is a placeholder for switching the mode to when we want the mouse to act as it would normally act.
            Selecting,                            //This mode is used to let the paint event know to draw the selection rectangle, etc, as well as highlight fields and such.
            Selected,                             //When you are done selecting, if you have selected something this is the mode you'll be in, so that things like moving stuff can happen.
            Resizing,                             //This mode is activated when resizing, so that the movement of the mouse can be used for that instead of anything else.
            Adding                                //Mode used when adding new fields, lets the adding box get painted and the mouse events get tailored for adding the new field.
        }

        #endregion Variables



        #region Initializers
        /// <summary>
        /// Initialize new instance of the frmMain Form. The only argument it takes is an existing form.
        /// </summary>
        /// <param name="form">The form that will be used when designing.</param>
        public Designer(PenForm form) {//Only constructor for loading up the designer.
            _currentForm = form;      

            InitializeComponent();    

            MouseWheel += mouseWheel; //To override the built in MouseWheel event with my own.
        }

        private void frmMain_Load(object sender, EventArgs e) {
            designerLoadStuff();            //Misc things to initialize for the designer stuff
            buildFieldTree();               //Function for building the Field Heirarchy 

        }

        /// <summary>
        /// Just a void function for doing some on load prep work.
        /// </summary>
        private void designerLoadStuff() {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty |     //Needed to get the designer panel to not have artifacts, etc.
                BindingFlags.Instance | BindingFlags.NonPublic, null, designPanel, new object[] { true });

            _selectionPen.DashStyle = DashStyle.Dash;  //The selection box will be drawn with dashes lines.

            lblVersionNumber.Text = _currentForm.versionNumber.ToString();              //Setting the version number label

            foreach (var fSet in _settings.globalFormatSet) {   //Iterate through the globalFormatSets...
                cmbFormatSetNames.Items.Add(fSet.Key);          //... to add each one to the formatSet combobox
            }

            Text = _currentForm.FormName;       //Put the forms name as the screens title
        }

        #endregion Initializers



        #region The Designer

        private void designer_Paint(object sender, PaintEventArgs e) {//The paint event handler for when the designer area gets redrawn. - Franklin, look for zoomLevel
            lblCurrentPage.Text = Convert.ToString(_currentPageNumber + 1); //For displaying which we we are on...
            lblTotalpages.Text = _currentForm.totalPages().ToString();      //...out of the total pages on this form.

            if (_currentForm.totalPages() > 0) { //Being safe about what we paint, without this we get exceptions when have empty pages.
                Rectangle pageRectangle = new Rectangle {       //This rectangle is for drawing the white "page" that all the fields are on
                                X = _xOffset,                   //Offsetting the page by the X and Y...
                                Y = _yOffset,                   //...offsets to facilitate "moving" the page.
                                Height = (int) (792*_zoomLevel),//Height and Width get their dimensions multiplied by the...
                                Width = (int) (612*_zoomLevel)  //...value of _zoomLevel to allow for enlarging and shrinking.
                };
                e.Graphics.FillRectangle(new SolidBrush(Color.White), pageRectangle);//Finally, we actually draw the page to the panel.   

                switch (_mode) {
                    case MouseMode.None://No reason to paint anything special when there is no selection mode.
                        break;

                    case MouseMode.Selecting:
                        e.Graphics.DrawRectangle(_selectionPen, _selectionRect);//This is that selection box being painted on screen.
                        break;

                    case MouseMode.Selected://Things to draw while the mode is selected or resizing.
                    case MouseMode.Resizing:
                        e.Graphics.DrawRectangle(_groupSelectionPen, //Drawing the group selection box, using the blue group selection pen.
                            new Rectangle((new Point(_groupSelectionRect.X + _xOffset, _groupSelectionRect.Y + _yOffset)), //Using the location of the group selection box combined with the page offset...
                            _groupSelectionRect.Size));                                                                    //...as well as the same rectangles size.
                        e.Graphics.DrawRectangle(_groupSelectionPen,                                    //Same idea here...
                            new Rectangle((new Point(_resRect.X + _xOffset, _resRect.Y + _yOffset)),    //...except using the resising rectangle instead.
                            _resRect.Size));
                        break;

                    case MouseMode.Adding:
                        Pen pt = new Pen(Color.Pink, 2);                //When adding the rectangle is pink (for now) to...
                        e.Graphics.DrawRectangle(pt, _selectionRect);   //help the user know they aren't selecting things...
                        break;
                }

                foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {  //After the mode specific stuff is drawn, this loop draws out all the fields on the current page.
                    if (!fi.hidden) {   //If the field isn't a hidden field.
                        switch (fi.type) { //Switch statement to draw different things for each type of field.
                            case Type.TextField:
                                Pen p1 = new Pen(Color.DarkGray);
                                e.Graphics.DrawRectangle(p1, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));
                                p1.Color = Color.LightGray;
                                e.Graphics.DrawLine(p1, fi.zx + _xOffset + 1, fi.zy + _yOffset - 1 + fi.zheight, //Creates the shadow line for the text box.
                                                        fi.zx + _xOffset - 1 + fi.zwidth, fi.zy + _yOffset - 1 + fi.zheight);
                                break;

                            case Type.Label:
                                Pen p2 = new Pen(Color.LightBlue);
                                e.Graphics.DrawRectangle(p2, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));

                                p2.Color = Color.Black;
                                e.Graphics.DrawString(fi.text, fi.formatSet.font(_zoomLevel), p2.Brush, new Point(fi.zx + _xOffset, fi.zy + _yOffset));
                                break;

                            case Type.FancyLabel:
                                Pen flPen = new Pen(Color.LightBlue);
                                e.Graphics.DrawRectangle(flPen, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));

                                flPen.Color = Color.Black;
                                e.Graphics.DrawString(fi.text, Font, flPen.Brush, new Point(fi.zx + _xOffset, fi.zy + _yOffset));
                                break;

                            case Type.RectangleDraw:
                                Pen recPen = new Pen(Color.LightGreen);
                                e.Graphics.DrawRectangle(recPen, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));
                                break;

                            case Type.LineDraw:
                                Pen linePen = new Pen(Color.LightGreen);
                                e.Graphics.DrawRectangle(linePen, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));
                                break;

                            case Type.Checkbox:
                                Pen p3 = new Pen(Color.Green);
                                e.Graphics.DrawRectangle(p3, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));
                                p3.Color = Color.Black;
                                e.Graphics.DrawString(fi.text, Font, p3.Brush, new Point(fi.zx + _xOffset, fi.zy + _yOffset + 3));
                                break;

                            case Type.OptionsGroup:
                                Pen p4 = new Pen(Color.Red);
                                e.Graphics.DrawRectangle(p4, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));
                                break;
                        }
                    }
                    if (fi.selected) {                          //drawing the dashed outline to indicate something is selected
                        Rectangle selectedRect = fi.rect();     //Initialize new rectangle based of field's position and size.
                        selectedRect.X = fi.zx - 1 + _xOffset;  //Added "padding" for the individual selected rectangle...
                        selectedRect.Y = fi.zy - 1 + _yOffset;  //...(otherwise you wouldn't see it due to overlap)
                        selectedRect.Width = fi.zwidth + 2;     
                        selectedRect.Height = fi.zheight + 2;
                        e.Graphics.DrawRectangle(_selectionPen, selectedRect);
                    }
                }
            }
        }

        //++Mouse Variables
        //+Left Click

        //+Middle Click
        private int _oldXOffset;        //The old X and Y offset values are needed due to the fact that during the move event the offsets are changing constantly as the...
        private int _oldYOffset;        //...mouse moves, you get some weird exponential math problems that made the page jump off the screen soon after the mouse is moved.

        private int _xOffset;           //The variables used to ultimately track how...
        private int _yOffset;           //...much the user has moved the page around.

        //+Right Click
        //Nothing yet

        private void designer_MouseDown(object sender, MouseEventArgs e) {//Controls the events that occur when the mouse is down. (not a click though, which is an down and up)
            if (_currentForm.totalPages() > 0) {//Empty page protection.
                _startPoint.X = e.X;    //Assigning the start point as the location the mouse is clicked down on...
                _startPoint.Y = e.Y;    //used generally for moving and resizing fields, but also for adding, etc.

                #region Left Click Down
                if (e.Button == MouseButtons.Left) {//All the code being ran during the left mouse click.
                    if (_resRect.Contains(e.X - _xOffset, e.Y - _yOffset)) { //If the resizing rectangle is what is clicked. Accounting for a moved field.
                        _mode = MouseMode.Resizing;
                    }
                    
                    if (!_groupSelectionRect.Contains(e.X - _xOffset, e.Y - _yOffset)           //Not within the group box
                    && _mode != MouseMode.Resizing
                    && _mode != MouseMode.Adding) {                                           //or if just just went into resizing mode
                        int notSelectedCount = 0;                                               //this is used to track how many fields are selected
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Iterating through all the fields on the page.
                            if (fi.isInside(e.X - _xOffset, e.Y - _yOffset)) {                  //If where we are clicking is inside the field.
                                if(ModifierKeys.HasFlag(Keys.Control)) {                        //AND CTRL is being held down.
                                    fi.selected = !fi.selected;                                 //the clicked field is toggled as selected or not
                                } else {                                                        //if...but no CTRL
                                    fi.selected = true;                                         //field is selected
                                }
                                _mode = MouseMode.Selected;                                     //mode become selected
                            } else {                                                            //if we aren't clicking inside a field
                                if(ModifierKeys.HasFlag(Keys.Control)) {                        //And we are holding down CTRL
                                    //fi.selected = fi.selected;                                // eveidently this did absolutely nothing. Not sure why it was here 
                                } else {                                                        //Not holding CTRL down
                                    fi.selected = false;                                        //field is unselected
                                }
                                ++notSelectedCount;                                             
                                if (notSelectedCount == _currentForm.page(_currentPageNumber).Fields.Count) _mode = MouseMode.Selecting;
                                _groupSelectionRect = new Rectangle();
                            }
                        }
                    }

                    switch (_mode) {
                        case MouseMode.None:
                            break;

                        case MouseMode.Selecting:
                            break;

                        case MouseMode.Selected:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                                fi.moveStart = new Point(fi.zx, fi.zy);
                            }
                            break;

                        case MouseMode.Resizing:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                                fi.resizeStart = new Size(fi.zwidth, fi.zheight);
                            }
                            break;

                        case MouseMode.Adding:
                            //TODO - some stuff for the labels and junk, whatever fields react to just the click and not any sizing or what not.
                            break;
                    }
                }
                #endregion Left Click Down

                #region Middle Click Down
                if (e.Button == MouseButtons.Middle) {
                    _xOffset = _oldXOffset;
                    _yOffset = _oldYOffset;
                }
                #endregion Middle Click Down

                #region Right Click Down
                #endregion Right Click Down
            }
        }

        private void designer_MouseMove(object sender, MouseEventArgs e) {//Handling what happens when the mouse is moving.
            if (_currentForm.totalPages() > 0) {
                if (e.Button == MouseButtons.Middle) {
                    _xOffset = e.X - _startPoint.X + _oldXOffset;
                    _yOffset = e.Y - _startPoint.Y + _oldYOffset;
                }

                if (e.Button == MouseButtons.Left) {
                    switch (_mode) {
                        case MouseMode.None:
                            break;

                        case MouseMode.Selecting://Figuring out the position and size of the selection rectangle.
                            if (e.X > _startPoint.X) {
                                _selectionRect.X = _startPoint.X;
                                _selectionRect.Width = e.X - _startPoint.X;
                            } else {
                                _selectionRect.X = e.X;
                                _selectionRect.Width = _startPoint.X - e.X;
                            }
                            if (e.Y > _startPoint.Y) {
                                _selectionRect.Y = _startPoint.Y;
                                _selectionRect.Height = e.Y - _startPoint.Y;
                            } else {
                                _selectionRect.Y = e.Y;
                                _selectionRect.Height = _startPoint.Y - e.Y;
                            }

                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Checking if fields are within the selection rectangle.
                                if (fi.zx > _selectionRect.X - _xOffset && fi.zx < _selectionRect.Width + _selectionRect.X - _xOffset &&
                                    fi.zy > _selectionRect.Y - _yOffset && fi.zy < _selectionRect.Height + _selectionRect.Y - _yOffset &&

                                    fi.zwidth + fi.zx < _selectionRect.Width + _selectionRect.X - _xOffset &&
                                    fi.zheight + fi.zy < _selectionRect.Height + _selectionRect.Y - _yOffset
                                    )
                                {
                                    fi.selected = true;
                                } else {
                                    fi.selected = false;
                                }
                            }
                            break;

                        case MouseMode.Selected:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                                if (fi.selected) {//Moving stuff around code.
                                    fi.zx = fi.moveStart.X - (_startPoint.X - e.X);
                                    fi.zy = fi.moveStart.Y - (_startPoint.Y - e.Y);
                                }
                            }

                            if (!_needToSaveForm) {
                                Text = _currentForm.FormName + "*";
                                _needToSaveForm = true;
                            } 
                            calculateSfBox();
                            break;

                        case MouseMode.Resizing:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                                if (fi.selected) {
                                    fi.zwidth = fi.resizeStart.Width - (_startPoint.X - e.X);

                                }
                            }
                            if (!_needToSaveForm) {
                                Text = _currentForm.FormName + "*";
                                _needToSaveForm = true;
                            }
                            calculateSfBox();
                            break;

                        case MouseMode.Adding:
                            if (e.X > _startPoint.X) {
                                _selectionRect.X = _startPoint.X;
                                _selectionRect.Width = e.X - _startPoint.X;
                            } else {
                                _selectionRect.X = e.X;
                                _selectionRect.Width = _startPoint.X - e.X;
                            }

                            if (e.Y > _startPoint.Y) {
                                _selectionRect.Y = _startPoint.Y;
                                _selectionRect.Height = e.Y - _startPoint.Y;
                            } else {
                                _selectionRect.Y = e.Y;
                                _selectionRect.Height = _startPoint.Y - e.Y;
                            }
                            switch (_fieldToAdd.type) {
                                case Type.TextField:
                                    _selectionRect.Height = (int)(16 * _zoomLevel);
                                    break;

                                case Type.OptionsGroup:
                                    _selectionRect.Height = (int)(16 * _zoomLevel);
                                    break;

                                case Type.Checkbox:
                                    if (_selectionRect.Height < _selectionRect.Width) _selectionRect.Height = _selectionRect.Width;
                                    else _selectionRect.Width = _selectionRect.Height;
                                    break;

                                case Type.Label:
                                    //_selectionRect.Height = (int)(16 * _zoomLevel);
                                    break;

                                case Type.RectangleDraw:
                                    //Nothing special, no constraints
                                    break;

                                case Type.LineDraw:
                                    if (_selectionRect.Height < _selectionRect.Width) _selectionRect.Height = (int)(2 * _zoomLevel);
                                    else _selectionRect.Width = (int)(2 * _zoomLevel);
                                    break;
                            }
                            break;
                    }
                }
                designPanel.Invalidate();
            }
        }

        private void designer_MouseUp(object sender, MouseEventArgs e) {//Handles what happene when you release the botton.
            if (_currentForm.totalPages() > 0) {
                _endPoint.X = e.X;
                _endPoint.Y = e.Y;

                _oldXOffset = _xOffset;
                _oldYOffset = _yOffset;

                switch (_mode) {
                    case MouseMode.None:
                        break;

                    case MouseMode.Selecting:
                        bool toSelect = false;
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            if (fi.selected) toSelect = true;
                        } if (toSelect) {
                            _mode = MouseMode.Selected;
                            goto case MouseMode.Selected;
                        }
                        break;

                    case MouseMode.Selected:
                        calculateSfBox();

                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            if (fi.selected) {
                                refreshProperties(fi);
                            }
                        }

                        break;
                    case MouseMode.Resizing:
                        calculateSfBox();

                        _mode = MouseMode.Selected;
                        break;
                    case MouseMode.Adding:
                        if (_selectionRect.Width > 0 && _selectionRect.Height > 0) {
                            switch (_fieldToAdd.type) {
                                case Type.TextField:
                                    _fieldToAdd.zx = _selectionRect.X - _xOffset;
                                    _fieldToAdd.zy = _selectionRect.Y - _yOffset;
                                    break;

                                case Type.OptionsGroup:
                                    break;

                                case Type.Checkbox:
                                    _fieldToAdd.zx = _selectionRect.X - _xOffset;
                                    _fieldToAdd.zy = _selectionRect.Y - _yOffset;

                                    break;

                                case Type.Label:
                                    _fieldToAdd.zx = _selectionRect.X - _xOffset;
                                    _fieldToAdd.zy = _selectionRect.Y - _yOffset;
                                    _fieldToAdd.zwidth = TextRenderer.MeasureText(_fieldToAdd.text, _fieldToAdd.formatSet.font(_zoomLevel)).Width;
                                    _fieldToAdd.zheight = TextRenderer.MeasureText(_fieldToAdd.text, _fieldToAdd.formatSet.font(_zoomLevel)).Height;
                                    break;

                                case Type.RectangleDraw:
                                    _fieldToAdd.zx = _selectionRect.X - _xOffset;
                                    _fieldToAdd.zy = _selectionRect.Y - _yOffset;
                                    break;

                                case Type.LineDraw:
                                    _fieldToAdd.zx = _selectionRect.X - _xOffset;
                                    _fieldToAdd.zy = _selectionRect.Y - _yOffset;
                                    break;
                            }
                            if (_fieldToAdd.type != Type.Label && _fieldToAdd.type != Type.FancyLabel) {
                                _fieldToAdd.zwidth = _selectionRect.Width;
                                _fieldToAdd.zheight = _selectionRect.Height; 
                            }

                            if (!_needToSaveForm) {
                                Text = _currentForm.FormName + "*";
                                _needToSaveForm = true;
                            }
                            _currentForm.page(_currentPageNumber).Fields.Add(_fieldToAdd);
                            refreshProperties(_fieldToAdd);
                            _fieldToAdd = null;
                        }
                        _mode = MouseMode.None;
                        break;
                }

                if (e.Button == MouseButtons.Right) {
                    int conPostX = e.X + designPanel.PointToScreen(designPanel.Location).X;
                    int conPostY = e.Y + designPanel.PointToScreen(designPanel.Location).Y;

                    cntxtFieldControls.Show(conPostX, conPostY);
                }

                _startPoint = new Point();
                _endPoint = new Point();
                _selectionRect = new Rectangle();

                highlightHeirarchy();
                designPanel.Invalidate();
            }
        }

        private void calculateSfBox() {
            Point sfBoxPosition = new Point(99999, 99999);
            Size sfBoxSize = new Size();

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Figuring out where to place the TL corner of the sfBox.
                if (fi.selected) {
                    if (fi.zx < sfBoxPosition.X) {
                        sfBoxPosition.X = fi.zx - 6;
                    }
                    if (fi.zy < sfBoxPosition.Y) {
                        sfBoxPosition.Y = fi.zy - 6;
                    }
                }
            }
            foreach (Field pi in _currentForm.page(_currentPageNumber).Fields) {//Figuring out what size to make the sfBox.
                if (pi.selected) {
                    if (pi.zx + pi.zwidth - sfBoxPosition.X > sfBoxSize.Width) {
                        sfBoxSize.Width = pi.zx + 6 + pi.zwidth - sfBoxPosition.X;
                    }
                    if (pi.zy + pi.zheight - sfBoxPosition.Y > sfBoxSize.Height) {
                        sfBoxSize.Height = pi.zy + 6 + pi.zheight - sfBoxPosition.Y;
                    }
                }
            }

            _groupSelectionRect = new Rectangle(sfBoxPosition, sfBoxSize);
            _resRect = new Rectangle(new Point(sfBoxPosition.X + sfBoxSize.Width + 7, sfBoxPosition.Y), new Size(2, sfBoxSize.Height));
        }

        public void mouseWheel(object sender, MouseEventArgs e) {
            if (e.Delta > 0)
                zoomIn();
            else
                zoomOut();
        }

        private void zoomIn() {
            _zoomLevel += 0.25;
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                fi.zoomLevel = _zoomLevel;
            }

            calculateSfBox();
            designPanel.Invalidate();
        }

        private void zoomOut() {
            if (_zoomLevel > 0.26) {
                _zoomLevel -= 0.25;
            }
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                fi.zoomLevel = _zoomLevel;
            }

            calculateSfBox();
            designPanel.Invalidate();
        }


        #region Designer Focus

        private void designPanel_MouseEnter(object sender, EventArgs e) {
            ((Panel)sender).Focus();
        }

        private void designPanel_MouseLeave(object sender, EventArgs e) {

        }

        #endregion Designer Focus


        #endregion The Designer



        #region Form Controls


        #region Cut, Copy, and Paste.

        private void btnCut_Click(object sender, EventArgs e) {
            cut();
        }

        private void btnCopy_Click(object sender, EventArgs e) {
            copy();
        }

        private void btnPaste_Click(object sender, EventArgs e) {
            paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
            cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            paste();
        }

        private void cut() {
            _fieldsToCopy = new List<Field>();

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    _fieldsToCopy.Add(fi);
                }
            }
            foreach (Field fi2 in _fieldsToCopy) {
                _currentForm.page(_currentPageNumber).Fields.Remove(fi2);
            }
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            designPanel.Invalidate();
        }

        private void copy() {
            _fieldsToCopy = new List<Field>();

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    _fieldsToCopy.Add(returnCopy(fi));
                }
            }
            designPanel.Invalidate();
        }

        private void paste() {
            foreach (Field fi in _fieldsToCopy) {
                fi.x = fi.x + 15;
                fi.y = fi.y + 15;

                _currentForm.page(_currentPageNumber).addField(returnCopy(fi));
            }
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            designPanel.Invalidate();
        }

        private List<Field> _fieldsToCopy = new List<Field>();

        private Field returnCopy(Field fi) {
            Field tempField = new Field();

            tempField.name = fi.name;
            tempField.x = fi.x;
            tempField.y = fi.y;
            tempField.width = fi.width;
            tempField.height = fi.height;
            tempField.hidden = fi.hidden;
            tempField.readOnly = fi.readOnly;
            tempField.type = fi.type;
            tempField.formatSet.fontTypeface = fi.formatSet.fontTypeface;//TODO - just needs to associate this fields textType with an instance.
            tempField.formatSet.fontSizeString = fi.formatSet.fontSizeString;
            tempField.formatSet.fontWeight = fi.formatSet.fontWeight;
            tempField.text = fi.text;
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            return tempField;
            
        }

        #endregion Cut, Copy, and Paste.


        #region Field Deletion

        private void btnDeleteField_Click(object sender, EventArgs e) {
            deleteFields();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            deleteFields();
        }

        private void deleteFields() {
            List<Field> fieldsToDelete = new List<Field>();
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fieldsToDelete.Add(fi);
                }
            }
            foreach (Field fi in fieldsToDelete) {
                _currentForm.page(_currentPageNumber).Fields.Remove(fi);
            }
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            deselectAll();

            designPanel.Invalidate();
        }

        #endregion Field Deletion


        #region Field Addition

        private void btn_AddField_Click(object sender, EventArgs e) {
            deselectAll();

            _mode = MouseMode.Adding;

            //Testing Grounds///
            string fieldName = "";

            using (var form = new fieldSelection(_currentForm.formTemplates, "Text")) {//FT1C
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    string val = form.name;

                    fieldName = val;
                }
            }

            //Testing Grounds End///

            _fieldToAdd = new Field(fieldName, Type.TextField);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        private void btnAddCheckBox_Click(object sender, EventArgs e) {
            deselectAll();

            _mode = MouseMode.Adding;

            string fieldName = "";

            using (var form = new fieldSelection(_currentForm.formTemplates, "Text")) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    string val = form.name;

                    fieldName = val;
                }
            }

            _fieldToAdd = new Field(fieldName, Type.Checkbox);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        private void btnAddLabel_Click(object sender, EventArgs e) {
            deselectAll();

            _mode = MouseMode.Adding;
            _fieldToAdd = new Field("Label", Type.Label);
            _fieldToAdd.zoomLevel = _zoomLevel;

            using (var form = new labelCreator()) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    string val = form.text;
                    string val2 = form.type;

                    _fieldToAdd.text = val;
                    _fieldToAdd.formatSet = _settings.getFormatSetByName(val2);

                }
            }
        }

        private void btnAddRectangle_Click(object sender, EventArgs e) {
            deselectAll();

            _mode = MouseMode.Adding;
            _fieldToAdd = new Field("Rectangle", Type.RectangleDraw);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        private void btnAddLine_Click(object sender, EventArgs e) {
            deselectAll();

            _mode = MouseMode.Adding;
            _fieldToAdd = new Field("Line", Type.LineDraw);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        #endregion Field Addition


        #region Global Control Functions

        private void deselectAll() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                fi.selected = false;
            }

            _groupSelectionRect = new Rectangle();
            designPanel.Invalidate();
        }

        #endregion Global Control Functions


        #region Page Navigation/Switching

        private void btnPreviousPage_Click(object sender, EventArgs e) {
            if (_currentPageNumber > 0) {
                _currentPageNumber--;
                btnNextPage.Enabled = true;
            }

            if (_currentPageNumber == 0) {
                btnPreviousPage.Enabled = false;
            }

            if (_currentForm.page(_currentPageNumber).Fields[0] != null) _zoomLevel = _currentForm.page(_currentPageNumber).Fields[0].zoomLevel;

            deselectAll();
            buildFieldTree();
            designPanel.Invalidate();
        }

        private void btnNextPage_Click(object sender, EventArgs e) {
            if (_currentPageNumber < _currentForm.totalPages() - 1) {
                _currentPageNumber++;
                btnPreviousPage.Enabled = true;
            }

            if (_currentPageNumber == _currentForm.totalPages() - 1) {
                btnNextPage.Enabled = false;
            }

            if (_currentForm.page(_currentPageNumber).Fields.Capacity != 0) _zoomLevel = _currentForm.page(_currentPageNumber).Fields[0].zoomLevel;

            deselectAll();
            buildFieldTree();
            designPanel.Invalidate();
        }

        #endregion Page Navigation/Switching


        #region New Page

        private void btnNewPage_Click(object sender, EventArgs e) {
            _currentForm.addNewBlankPage();
        }

        #endregion New Page


        #region Save Form Button
        private void btnSaveForm_Click(object sender, EventArgs e) {

            if (_settings.formsFolderLocation == @"") {
                FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) {
                    _settings.formsFolderLocation = openFileDialog1.SelectedPath;
                }
                if (result == DialogResult.Cancel) {
                    MessageBox.Show("Without a folder to save to, I'll just cancel this process.", "No Save Path",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            Text = _currentForm.FormName;
            _needToSaveForm = false;         
            _currentForm.saveForm();
        }
        #endregion Save Form Button


        #region Export Form Button

        private void btnExportForm_Click(object sender, EventArgs e) {
            
            if (_settings.exportFolder == @"") {
                FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) {
                    _settings.exportFolder = openFileDialog1.SelectedPath;
                }
                if (result == DialogResult.Cancel) {
                    MessageBox.Show("Without a folder to export to, I'll just cancel this process.", "No Export Path",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            Text = _currentForm.FormName;
            _needToSaveForm = false;
            _currentForm.versionNumber++;
            _currentForm.exportXDP();
            _currentForm.exportEPS();
            //currentForm.exportPNG();

            _currentForm.saveForm();

            lblVersionNumber.Text = _currentForm.versionNumber.ToString();
        }

        #endregion Export Form Button


        #region Settings Screen Button
        private void btnLoadSettingsScreen_Click(object sender, EventArgs e) {
            new settingsScreen().ShowDialog();
        }
        #endregion Settings Screen Button


        #region Allignment
        private void alignHorizontallyToolStripMenuItem_Click(object sender, EventArgs e) {
            List<int> yPositions = new List<int>();

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    yPositions.Add(fi.y);
                }
            }

            int newY = 0;
            foreach(int i in yPositions) {
                newY = newY + i;
            }

            if (yPositions.Count != 0) {
                newY = newY / yPositions.Count;
            }

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.y = newY;
                }
            }

            designPanel.Invalidate();
        }

        private void alignVerticallyToolStripMenuItem_Click(object sender, EventArgs e) {
            List<int> xPositions = new List<int>();

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    xPositions.Add(fi.x);
                }
            }

            int newX = 0;
            foreach(int i in xPositions) {
                newX = newX + i;
            }

            if (xPositions.Count != 0) {
                newX = newX / xPositions.Count;
            }

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.x = newX;
                }
            }

            designPanel.Invalidate();
         }
        #endregion Allignment


        #endregion Form Controls



        #region Field Properties

        private Field _fieldInProperties;

        private void refreshProperties(Field fi) {
            txtPropName.Text = fi.name;
            txtPropX.Text = fi.x.ToString();
            txtPropY.Text = fi.y.ToString();
            txtPropWidth.Text = fi.width.ToString();
            txtPropHeight.Text = fi.height.ToString();
            chkPropHidden.Checked = fi.hidden;
            chkPropReadOnly.Checked = fi.readOnly;
            txtPropFieldType.Text = fi.type.ToString();
            txtPropText.Text = fi.text;
            cmbFormatSetNames.SelectedItem = fi.formatSet.name;

            _fieldInProperties = fi;
        }

        private void btnPropSave_Click(object sender, EventArgs e) {
            _currentForm.page(_currentPageNumber).removeField(_fieldInProperties);

            _fieldInProperties.name = txtPropName.Text;
            _fieldInProperties.x = Convert.ToInt32(txtPropX.Text);
            _fieldInProperties.y = Convert.ToInt32(txtPropY.Text);
            _fieldInProperties.width = Convert.ToInt32(txtPropWidth.Text);
            _fieldInProperties.height = Convert.ToInt32(txtPropHeight.Text);
            _fieldInProperties.hidden = chkPropHidden.Checked;
            _fieldInProperties.readOnly = chkPropReadOnly.Checked;
            _fieldInProperties.text = txtPropText.Text;
            _fieldInProperties.formatSet = _settings.getFormatSetByName(cmbFormatSetNames.SelectedItem.ToString());

            _currentForm.page(_currentPageNumber).addField(_fieldInProperties);
            designPanel.Invalidate();
        }

        #endregion Field Properties



        #region Keyboard Bindings

        #region Universal Methods

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            //Delete
            if (keyData == Keys.Delete) OnKeyPress(new KeyPressEventArgs((Char)Keys.Delete));
            //Mouse Arrow Moving
            if (keyData == Keys.Up) OnKeyPress(new KeyPressEventArgs((Char)Keys.Up));
            if (keyData == Keys.Down) OnKeyPress(new KeyPressEventArgs((Char)Keys.Down));
            if (keyData == Keys.Left) OnKeyPress(new KeyPressEventArgs((Char)Keys.Left));
            if (keyData == Keys.Right) OnKeyPress(new KeyPressEventArgs((Char)Keys.Right));
            //Form Control Keybindings
            if (keyData == Keys.ControlKey) OnKeyPress(new KeyPressEventArgs((Char)Keys.ControlKey));
            if (keyData == Keys.C) OnKeyPress(new KeyPressEventArgs((Char)Keys.C));
            if (keyData == Keys.X) OnKeyPress(new KeyPressEventArgs((Char)Keys.X));
            if (keyData == Keys.V) OnKeyPress(new KeyPressEventArgs((Char)Keys.V));

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (Char)Keys.Up) moveFieldsUp();
            if (e.KeyChar == (Char)Keys.Down) moveFieldsDown();
            if (e.KeyChar == (Char)Keys.Left) moveFieldsLeft();
            if (e.KeyChar == (Char)Keys.Right) moveFieldsRight();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) deleteFields();

            if (e.Control && e.KeyCode == Keys.X) cut();
            if (e.Control && e.KeyCode == Keys.C) copy();
            if (e.Control && e.KeyCode == Keys.V) paste();
        }

        #endregion Universal Methods

        #region Arrow Key Moving - Mostly Finished, gradiant speed up would be intersting.

        public void moveFieldsUp() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.y--;
                }
            }
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            designPanel.Invalidate();
            calculateSfBox();
        }

        public void moveFieldsDown() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.y++;
                }
            }
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            designPanel.Invalidate();
            calculateSfBox();
        }

        public void moveFieldsLeft() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.x--;
                }
            }
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            designPanel.Invalidate();
            calculateSfBox();
        }

        public void moveFieldsRight() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.x++;
                }
            }
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
            designPanel.Invalidate();
            calculateSfBox();
        }

        #endregion Arrow Key Moving - Mostly Finished, gradiant speed up would be intersting.

        #endregion Keyboard Bindings




        #region Field Tree Management

        private void btnRefreshFieldTree_Click(object sender, EventArgs e) {
            buildFieldTree();
        }

        private void buildFieldTree()
        {
            trvFieldList.Nodes.Clear();
            int c = 0;
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                fi.listIndex = c;

                TreeNode workingNode = new TreeNode(fi.name);
                if(fi.selected) {
                    workingNode.BackColor = SystemColors.Highlight;
                    workingNode.ForeColor = SystemColors.HighlightText;
                }
                workingNode.Tag = c;
                trvFieldList.Nodes.Add(workingNode);
                c++;
            }
        }

        private void heirarchyViewClick(object sender, TreeNodeMouseClickEventArgs e) {

            if (ModifierKeys.HasFlag(Keys.Control)) { //If CTRL is being held during the click.
                _currentForm.page(_currentPageNumber).Fields[e.Node.Index].selected = //The field associated with the item clicked equals the opposite...
               !_currentForm.page(_currentPageNumber).Fields[e.Node.Index].selected;  //...of what it was before it was clicked. To "toggle" if the item is selected.
            } else { //CTRL isn't being held down.
                deselectAll();
                _currentForm.page(_currentPageNumber).Fields[e.Node.Index].selected = true; //Selects the one field 
            }

            highlightHeirarchy();

            _mode = MouseMode.Selected;
            calculateSfBox();
            designPanel.Invalidate();


        }

        private void highlightHeirarchy() {
            
            foreach(Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if(fi.selected) {
                    foreach (TreeNode tr in trvFieldList.Nodes) {
                        if((int)tr.Tag == fi.listIndex) {
                            tr.BackColor = SystemColors.Highlight;
                            tr.ForeColor = SystemColors.HighlightText;
                        }
                    }
                } else {
                    foreach (TreeNode tr in trvFieldList.Nodes) {
                        if((int)tr.Tag == fi.listIndex) {
                            tr.BackColor = new TreeNode().BackColor;
                            tr.ForeColor = new TreeNode().ForeColor;
                        }
                    }
                }
            }

        }

        private void trvFieldList_MouseEnter(object sender, EventArgs e) {
            ((TreeView)sender).Focus();
        }

        #endregion Field Tree Management

        private void btnTemplatesList_Click(object sender, EventArgs e) {

            using (var form = new templateSelection(_currentForm.formTemplates)) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    List<int> val = form.templates;

                    _currentForm.formTemplates = val;
                }
            }
        }

        private void trvFieldList_AfterSelect(object sender, TreeViewEventArgs e) {
            trvFieldList.SelectedNode = null;
            trvFieldList.Invalidate();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if(_needToSaveForm) {//Used to intercept the form closing, in order to see if the user wants to save or not.
                DialogResult dRes = MessageBox.Show("You've made changes to your form, would you like to save before you close?", "Do you want to save?",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);  
                if (dRes == DialogResult.Yes) {
                    _currentForm.saveForm();
                }
                if (dRes == DialogResult.No) {
                }
                if (dRes == DialogResult.Cancel) {
                    e.Cancel = true;
                }               
            }    
        }
    }
}