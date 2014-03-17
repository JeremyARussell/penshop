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
        private Point _topCrossPoint;       //The four points of the cross-hairs that show to help you place fields.
        private Point _bottomCrossPoint;    //...
        private Point _leftCrossPoint;      //...
        private Point _rightCrossPoint;     //...
        private Rectangle _selectionRect;      //This is the Rectangle used to actively track where the user is "selecting"
        private Rectangle _groupSelectionRect; //This Rectangle is the group selection rectangle used to encompass a group of things that have been selected.
        //private Rectangle _resRect;            //This is the little blue resizing widget you grab to resize the width of the fields.

        private double _zoomLevel = 1.00;      //Used to zoom in and out of pages.
        private bool _shouldZoom = true;       //Used to flag if we should allow zooming or not.

        private readonly Pen _selectionPen = new Pen(Color.Gray);            //The pen used for the _selectionRect
        private readonly Pen _groupSelectionPen = new Pen(Color.Blue, 2.0f); //The pen used for the _groupSelectionRect

        private _mode _globalMode = new _mode(); //global mode instance

        public class _mode {
            private MouseMode _internalMode = MouseMode.None; //The MouseMode is used to keep track of what we should be doing with the mouse during the misc mouse events

// ReSharper disable InconsistentNaming
            public bool None = true;
            public bool TextEditing = false;
            public bool Selecting = false;
            public bool Selected = false;
            public bool Resizing = false;
            public bool Adding = false;
// ReSharper restore InconsistentNaming
            public MouseMode mode() {
                return _internalMode; // Internal mode keeper
            }

            private void reset() {
                None = false;
                TextEditing = false;
                Selecting = false;
                Selected = false;
                Resizing = false;
                Adding = false;
            }

            public void setMode(MouseMode m){
                reset(); // Reset Bools
                switch (m){
                    case MouseMode.None:
                        None = true;
                        _internalMode = m;
                        break;

                    case MouseMode.Adding:
                        Adding = true;
                        _internalMode = m;
                        break;

                    case MouseMode.Resizing:
                        Resizing = true;
                        _internalMode = m;
                        break;

                    case MouseMode.Selected:
                        Selected = true;
                        _internalMode = m;
                        break;

                    case MouseMode.Selecting:
                        Selecting = true;
                        _internalMode = m;
                        break;

                    case MouseMode.TextEditing:
                        TextEditing = true;
                        _internalMode = m;
                        break;

                    default:
                        None = true;
                        _internalMode = MouseMode.None; // If the mousemode is nothing known (errors) apply No mousemode
                        break;
                }
            }
        }

        public enum MouseMode {                   //The MouseMode enum, pretty basic here but I'll run through each mode.
            None,                                 //This is a placeholder for switching the mode to when we want the mouse to act as it would normally act.
            TextEditing,                          //This is the mode for editing text labels and rich text label inline.
            Selecting,                            //This mode is used to let the paint event know to draw the selection rectangle, etc, as well as highlight fields and such.
            Selected,                             //When you are done selecting, if you have selected something this is the mode you'll be in, so that things like moving stuff can happen.
            Resizing,                             //This mode is activated when resizing, so that the movement of the mouse can be used for that instead of anything else.
            Adding                                //Mode used when adding new fields, lets the adding box get painted and the mouse events get tailored for adding the new field.
        }

        #endregion Variables



        #region Initializers
        /// <summary>
        /// Initialize new instance of the Designer Form. The only argument it takes is an existing form.
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

                cntxtFormatSets.Items.Add(fSet.Key);//Building the FormatSet context menu's items.
            }

            Text = _currentForm.FormName;       //Put the forms name as the screens title
        }

        #endregion Initializers



        #region The Designer

        private void designer_Paint(object sender, PaintEventArgs e) {//The paint event handler for when the designer area gets redrawn. - Franklin, look for zoomLevel
            lblCurrentPage.Text = Convert.ToString(_currentPageNumber + 1); //For displaying which we we are on...
            lblTotalpages.Text = _currentForm.totalPages().ToString();      //...out of the total pages on this form.

            //For Debugging//
            lblDebugMMode.Text = _globalMode.mode().ToString();
            //For Debugging//

            if (_currentForm.totalPages() > 0) { //Being safe about what we paint, without this we get exceptions when have empty pages.
                Rectangle pageRectangle = new Rectangle {       //This rectangle is for drawing the white "page" that all the fields are on
                                X = _xOffset,                   //Offsetting the page by the X and Y...
                                Y = _yOffset,                   //...offsets to facilitate "moving" the page.
                                Height = (int) (792*_zoomLevel),//Height and Width get their dimensions multiplied by the...
                                Width = (int) (612*_zoomLevel)  //...value of _zoomLevel to allow for enlarging and shrinking.
                };
                e.Graphics.FillRectangle(new SolidBrush(Color.White), pageRectangle);//Finally, we actually draw the page to the panel.   

                switch (_globalMode.mode()) {
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
                        //e.Graphics.DrawRectangle(_groupSelectionPen,                                    //Same idea here...
                        //    new Rectangle((new Point(_resRect.X + _xOffset, _resRect.Y + _yOffset)),    //...except using the resising rectangle instead.
                        //    _resRect.Size));
                        break;

                    case MouseMode.Adding:
                        Pen pt = new Pen(Color.Pink, 2); //When adding the rectangle is pink (for now) to...
                        e.Graphics.DrawRectangle(pt, _selectionRect); //help the user know they aren't selecting things...
                        if (_fieldToAdd.type != Type.LineDraw) { //The crosshairs get in the way of seeing the line the user is drawing, so we only draw the crosshairs for everything except the LineDraw fields
                            e.Graphics.DrawLine(pt, _topCrossPoint,     //Starting to draw the vertical crosshair, the first point being the top...
                                                _bottomCrossPoint);     //...and the last point being the bottom.
                            e.Graphics.DrawLine(pt, _leftCrossPoint,    //Same principle here, but the horizontal crosshair instead...
                                                _rightCrossPoint);      //...
                        }
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
                                e.Graphics.DrawString(fi.text, fi.formatSet.font(_zoomLevel), flPen.Brush, 
                                    new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));//The rectangle here works to give word wrapping to this drawString overload.
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
                                double zhmod = 4 * _zoomLevel; //This is to give a slight offset to the y position for the string we draw for checkboxes, but also make it match with the _zoomLevel.
                                e.Graphics.DrawString(fi.text, fi.formatSet.font(_zoomLevel), p3.Brush, new Point(                      //This draw strings starts off pretty standard with the text, font and pen...
                                       fi.zx + _xOffset +    fi.zwidth                                                                  //...starts getting a little fancy by offsetting the x position to account for the checkboxes width...
                                    , (int)(fi.zy + _yOffset + (((fi.zheight - fi.formatSet.font(_zoomLevel).Size) / 2)) - zhmod)));    //...but here we get really fancy, in order to center along the height and account for different sizes of checkbox...
                                break;                                                                                                  //...and different levels of zoom and font size, etc.

                            case Type.OptionsGroup:
                                Pen p4 = new Pen(Color.Red);
                                e.Graphics.DrawRectangle(p4, new Rectangle((new Point(fi.zx + _xOffset, fi.zy + _yOffset)), fi.rect().Size));
                                break;
                        }
                    }
                    if (fi.selected) {                          //drawing the dashed outline to indicate something is selected //TODO - Redesign this to be more of a selection glow or something.
                        Rectangle selectedRect = fi.rect();     //Initialize new rectangle based of field's position and size.
                        selectedRect.X = fi.zx - 1 + _xOffset;  //Added "padding" for the individual selected rectangle...
                        selectedRect.Y = fi.zy - 1 + _yOffset;  //...(otherwise you wouldn't see it due to overlap)
                        selectedRect.Width = fi.zwidth + 2;     
                        selectedRect.Height = fi.zheight + 2;
                        e.Graphics.DrawRectangle(_selectionPen, selectedRect);

                        e.Graphics.DrawRectangle(_groupSelectionPen, new Rectangle( //Paint the selected fields resizer(s)
                            (new Point(fi.resizer().Location.X + _xOffset,
                            fi.resizer().Location.Y + _yOffset)), 
                            fi.resizer().Size));
                    }
                }
            }
        }

        //++Mouse Variables
        //+Left Click

        //+Middle Click
        private int _oldXOffset = 35;        //The old X and Y offset values are needed due to the fact that during the move event the offsets are changing constantly as the...
        private int _oldYOffset = 35;        //...mouse moves, you get some weird exponential math problems that made the page jump off the screen soon after the mouse is moved.

        private int _xOffset = 35;           //The variables used to ultimately track how...
        private int _yOffset = 35;           //...much the user has moved the page around.

        //+Right Click
        //Nothing yet

        private void designer_MouseDown(object sender, MouseEventArgs e) {//Controls the events that occur when the mouse is down. (not a click though, which is an down and up)
            if (_currentForm.totalPages() > 0) {//Empty page protection.
                _startPoint.X = e.X;    //Assigning the start point as the location the mouse is clicked down on...
                _startPoint.Y = e.Y;    //used generally for moving and resizing fields, but also for adding, etc.

                #region Left Click Down
                if (e.Button == MouseButtons.Left) {
                    if (_globalMode.TextEditing) {
                        stopEditing();
                    }
                    if(_globalMode.Selected) {   //To prevent the click down from being able to trigger the resize without being able to see the resizer. //TODO - BUG - We can still have this issue if we click the invisible resizers when we are resizing another seperate field.
                        foreach(Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            if(fi.resizer().Contains(e.X - _xOffset, e.Y - _yOffset)) { //If the field's resizer Rectangle contains the point that was clicked...  
                                fi.resizing = true;                                     //...the field is flagged as resizing...
                                _globalMode.setMode(MouseMode.Resizing);                             //...and the mode is set to resizing (for proper mouseMove handling)
                            }
                        }
                    }
                    //if (_resRect.Contains(e.X - _xOffset, e.Y - _yOffset)) { //If the resizing rectangle is what is clicked. Accounting for a moved field.
                    //    _mode = MouseMode.Resizing;
                    //}
                    
                    if (!_groupSelectionRect.Contains(e.X - _xOffset, e.Y - _yOffset)           //Not within the group box
                    && !_globalMode.Resizing                                              //or if just just went into resizing mode
                    && !_globalMode.Adding) {                                             //or we just clicked one of the adding buttons.
                        int notSelectedCount = 0;                                                   //this is used to track how many fields are selected
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Iterating through all the fields on the page.
                            if (fi.isInside(e.X - _xOffset, e.Y - _yOffset)) {                  //If where we are clicking is inside the field.
                                if(ModifierKeys.HasFlag(Keys.Control)) {                        //AND CTRL is being held down.
                                    fi.selected = !fi.selected;                                 //the clicked field is toggled as selected or not
                                } else {                                                        //if...but no CTRL
                                    fi.selected = true;                                         //field is selected
                                }
                                _globalMode.setMode(MouseMode.Selected);                                     //mode become selected
                            } else {                                                            //if we aren't clicking inside a field
                                if(!ModifierKeys.HasFlag(Keys.Control)) {                       //And we are holding down CTRL
                                    fi.selected = false;                                        //field is unselected
                                    ++notSelectedCount;                                             //Increment the notSelectedCount integer by one
                                    if (notSelectedCount ==                                         //Here we check if the notSelectedCount integer is...
                                        _currentForm.page(_currentPageNumber).Fields.Count)         //...equal to the total amount of fields on this page...
                                        _globalMode.setMode(MouseMode.Selecting);                                //...
                                   _groupSelectionRect = new Rectangle();
                                }                               
                            }
                        }
                        
                    } else {                                                                    //For when we are clicking within the selected box.
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    
                            if (fi.isInside(e.X - _xOffset, e.Y - _yOffset)) {          //Clicking within a field, within the selection box.
                                if(ModifierKeys.HasFlag(Keys.Control)) {                //and CTRL is held down.
                                    fi.selected = !fi.selected;                         //toggle if it's selected.
                                }
                            }
                        }
                    }

                    switch (_globalMode.mode()) {
                        case MouseMode.None:
                            break;

                        case MouseMode.Selecting:
                            break;

                        case MouseMode.Selected:
                            if (_shouldZoom) _shouldZoom = false;
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Foreach of the page fields...
                                fi.moveStart = new Point(fi.zx, fi.zy);                             //...grab the current position to use for moving around during mouseMove
                            }
                            break;

                        case MouseMode.Resizing:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Foreach of the page fields...
                                fi.resizeStart = new Size(fi.zwidth, fi.zheight);                   //...grab the current size to use for resizing during mouseMove
                            }
                            break;

                        case MouseMode.Adding:
                            switch (_fieldToAdd.type) {                         //Using a switch in case we need it later, it could be an if statement now though.
                                case Type.Label:                                                    //When the left mouse button clicks down, and we're adding a label.
                                    _fieldToAdd.zx = e.X - _xOffset;                                                                                    //Setting the fields positions...
                                    _fieldToAdd.zy = e.Y - _yOffset;                                                                                    //...
                                    _fieldToAdd.zwidth  = TextRenderer.MeasureText(_fieldToAdd.text, _fieldToAdd.formatSet.font(_zoomLevel)).Width;     //A Label's outer box size is based off the string it displays...
                                    _fieldToAdd.zheight = TextRenderer.MeasureText(_fieldToAdd.text, _fieldToAdd.formatSet.font(_zoomLevel)).Height;    //...here we use TextRenderer.MeasureText to get this information.
                                  
                                    _currentForm.page(_currentPageNumber).Fields.Add(_fieldToAdd);  //Add the field...
                                    _fieldToAdd = null;                                             //...effectively empty the field, to prepare for a new addition.
                                    _globalMode.setMode(MouseMode.None);     //To prevent null _fieldToAdd during mouseMove and mouseUp errors I was having.
                                    needToSave();
                                    break;
                            }
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
            if (_currentForm.totalPages() > 0) {    //
                if (e.Button == MouseButtons.Middle) {
                    _xOffset = e.X - _startPoint.X + _oldXOffset;
                    _yOffset = e.Y - _startPoint.Y + _oldYOffset;
                }

                if (_globalMode.Adding)
                {
                    _topCrossPoint = new Point(e.X, 0);
                    _bottomCrossPoint = new Point(e.X, 1000);
                    _leftCrossPoint = new Point(0, e.Y);
                    _rightCrossPoint = new Point(1000, e.Y); 
                }

                if (e.Button == MouseButtons.Left && !ModifierKeys.HasFlag(Keys.Control)) {
                    switch (_globalMode.mode()) {
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

                            needToSave(); 
                            calculateSfBox();
                            break;

                        case MouseMode.Resizing:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Go through the fields...
                                if (fi.resizing) {                                              //...and find the one we are specifically resizing.
                                    if(fi.type == Type.TextField) {
                                        fi.zwidth = fi.resizeStart.Width - (_startPoint.X - e.X);//We only resize width for TextFields

                                        if(fi.width < 5) fi.width = 5;                           //Don't let it get smaller then 5 points wide.
                                    }
                                    if(fi.type == Type.Checkbox) {
                                        fi.zwidth = fi.resizeStart.Width - (_startPoint.X - e.X);       //Here we equally resize width and height...
                                        fi.zheight = fi.resizeStart.Height - (_startPoint.X - e.X);     //...based off the horizontal motion of the mouse.

                                        if(fi.width < 2) fi.width = 2;                                  //Without letting it get smaller then 2 points in size.
                                        if(fi.height < 2) fi.height = 2;
                                    }
                                    if(fi.type == Type.RectangleDraw || fi.type == Type.FancyLabel) {
                                        fi.zwidth = fi.resizeStart.Width - (_startPoint.X - e.X);  //For the Rectangles and Rich Labels we freely resize width...
                                        fi.zheight = fi.resizeStart.Height - (_startPoint.Y - e.Y);//...and height

                                        if(fi.width < 5) fi.width = 5;                             //Here we limit them from getting smaller then 5 points for width...
                                        if(fi.height < 5) fi.height = 5;                           //...and height
                                        
                                    }
                                }
                            }
                            needToSave();
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
                                    //Nothing special, no constraints
                                    break;
                  
                                case Type.FancyLabel:
                                    //Nothing special, no constraints
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

                switch (_globalMode.mode()) {
                    case MouseMode.None:
                        break;

                    case MouseMode.Selecting:
                        bool toSelect = false;
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            if (fi.selected) toSelect = true;
                        } if (toSelect) {
                            _globalMode.setMode(MouseMode.Selected);
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
                        foreach(Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            fi.resizing = false;//When we were just resizing, if the mouse is lifted we want to go through our fields and make sure none are resizing (since we stopped)
                        }
                        calculateSfBox();

                        _globalMode.setMode(MouseMode.Selected);
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

                                case Type.Label: //Nothing for MouseUp concerning the Label
                                    break;
                               
                                case Type.FancyLabel:
                                    _fieldToAdd.zx = _selectionRect.X - _xOffset;
                                    _fieldToAdd.zy = _selectionRect.Y - _yOffset;
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
                            if (_fieldToAdd.type != Type.Label) {
                                _fieldToAdd.zwidth = _selectionRect.Width;
                                _fieldToAdd.zheight = _selectionRect.Height; 
                            }

                            needToSave();

                            if (_fieldToAdd.type != Type.Label) {   //We don't add a field during MouseUp for labels since that is handled during MouseDown.
                                _currentForm.page(_currentPageNumber).Fields.Add(_fieldToAdd);
                            }
                            buildFieldTree();
                            refreshProperties(_fieldToAdd);
                            _fieldToAdd = null;
                        }
                        _globalMode.setMode(MouseMode.None);
                        break;
                }

                if (e.Button == MouseButtons.Right) {
                    int conPostX = e.X + designPanel.PointToScreen(designPanel.Location).X;
                    int conPostY = e.Y + designPanel.PointToScreen(designPanel.Location).Y;

                    cntxtFieldControls.Show(conPostX, conPostY);
                }

                _shouldZoom = true;//flag for deciding on if we should allow zooming at the moment. Enabled to allow zooming.

                _startPoint = new Point();
                _endPoint = new Point();
                _selectionRect = new Rectangle();

                highlightHeirarchy();
                designPanel.Invalidate();
            }
        }

        /// <summary>
        /// This function goes through and makes sure the blue group selection box is the right size based off the fields that are selected.
        /// </summary>
        private void calculateSfBox() {
            Point sfBoxPosition = new Point(99999, 99999);//Position starts way off screen since below we decrease it based off the positions in the selected fields. This keeps the top left spot, 0, 0. From always being the smallest possible locations.
            Size sfBoxSize = new Size();

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Figuring out where to place the top left corner of the sfBox.
                if (fi.selected) {                                              //This will loop through the selected fields to grab whichever is the lowest...
                    if (fi.zx < sfBoxPosition.X) {                              //...value of the X position...
                        sfBoxPosition.X = fi.zx - 6;
                    }
                    if (fi.zy < sfBoxPosition.Y) {                              //...as well as the lowest value for the Y position.
                        sfBoxPosition.Y = fi.zy - 6;
                    }
                }
            }
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Figuring out what size to make the sfBox.
                if (fi.selected) {
                    if (fi.zx + fi.zwidth - sfBoxPosition.X > sfBoxSize.Width) {    //Same principle, except to get size we add width to x position to get the rightmost...
                        sfBoxSize.Width = fi.zx + 6 + fi.zwidth - sfBoxPosition.X;  //...position of the selected fields...
                    }
                    if (fi.zy + fi.zheight - sfBoxPosition.Y > sfBoxSize.Height) {
                        sfBoxSize.Height = fi.zy + 6 + fi.zheight - sfBoxPosition.Y;//...as well as the bottom most position for height.
                    }
                }
            }

            _groupSelectionRect = new Rectangle(sfBoxPosition, sfBoxSize);      //Build a Rectangle out of the calculated position and size.
            //_resRect = new Rectangle(new Point(sfBoxPosition.X + sfBoxSize.Width + 7, sfBoxPosition.Y), new Size(2, sfBoxSize.Height)); //This is the resizing rectangle. TODO - Which will be later reworked to only show for individual items. Or something more streamlined.
        }

        public void mouseWheel(object sender, MouseEventArgs e) {
            if (e.Delta > 0)//Scrolling up
                zoomIn();
            else {          //Scrolling down
                zoomOut();
            }
        }

        private void zoomIn() {
            if (_shouldZoom) {  //Using this bool to prevent zooming if we are doing a few specific things (like editing labels, or moving fields)
                if (_zoomLevel < 4.00) {//4.00 acts as an upper bounds for zooming
                    _zoomLevel += 0.25;
                    foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Apply the new zoomLevel to all the pages fields.
                        fi.zoomLevel = _zoomLevel;
                    }
                }
            }

            calculateLabelSizes();          //Some visual cleanup
            calculateSfBox();
            designPanel.Invalidate();
        }

        private void zoomOut() {
            if (_shouldZoom) {
                if (_zoomLevel > 0.26) {//0.26 is the lower bounds.
                    _zoomLevel -= 0.25;
                    foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                        fi.zoomLevel = _zoomLevel;
                    }
                }
            }

            calculateLabelSizes();
            calculateSfBox();
            designPanel.Invalidate();
        }


        #region Designer Focus

        private void designPanel_MouseEnter(object sender, EventArgs e) {
            if (!_globalMode.TextEditing) {
                ((Panel)sender).Focus();
            }
        }

        private void designPanel_MouseLeave(object sender, EventArgs e) {

        }

        #endregion Designer Focus


        #endregion The Designer



        #region Form Controls


        #region Cut, Copy, and Paste.

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) {
            cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            paste();
        }

        /// <summary>
        /// The Designer's cut function, it takes the fields and places them in a list, then removes them from the form to prepare for a paste.
        /// </summary>
        private void cut() {
            int shouldClearFieldsToCopy = 0;    //This integer is used to track how many fields are selected in order to solve an issue with the fields to copy being overwritten even if nothing was selected.
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Iterate through the fields on the current page...
                if (fi.selected) {                                                  //...and if they are selected...
                    shouldClearFieldsToCopy++;  //Incremenent integer for tracking selected fields.
                    if (shouldClearFieldsToCopy == 1) 
                        _fieldsToCopy = new List<Field>();    //Create new blank _fieldsToCopy list of Fields
                    _fieldsToCopy.Add(fi);                              //add the actual field and not a copy, so that we can remove them from the fields to perform the cutting portion this function.
                }
            }
            foreach (Field fi2 in _fieldsToCopy) {
                _currentForm.page(_currentPageNumber).Fields.Remove(fi2);//In order to actually remove them, we use the actual field instead of a copy.
            }

            needToSave();
            deselectAll();              //Nothing should be selected, this is ran to be double sure.
            buildFieldTree();            
            designPanel.Invalidate();
        }

        /// <summary>
        /// The Designer's copy function. Takes the field or fields selected and places a copy in memory to later paste.
        /// </summary>
        private void copy() {
            int shouldClearFieldsToCopy = 0;    //See cut() for breakdown of this variable.
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Iterate through the fields on the current page...
                if (fi.selected) {                                                  //...and if they are selected...
                    shouldClearFieldsToCopy++;
                    if (shouldClearFieldsToCopy == 1) _fieldsToCopy = new List<Field>();
                    _fieldsToCopy.Add(returnCopy(fi));                              //...add a copy of them (this is done due to the nature of C# references acting sort of like pointers) to the _fieldsToCopy list
                }
            }

            designPanel.Invalidate();
        }

        private RichTextBox _labelEditBox = new RichTextBox();
        private Field _currentEditField;

        /// <summary>
        /// The Designer's edit function. Takes the field or fields selected and places a copy in memory to later paste.
        /// </summary>

        private void startEditing() {
            _shouldZoom = false;//Toggling zoom off while editing

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Iterate through the fields on the current page...
                if (fi.selected) {
                    if (fi.type == Type.FancyLabel || fi.type == Type.Label) {
                        if (fi.type == Type.FancyLabel) {
                            _labelEditBox.Multiline = true; //Fancy label with multiline
                        } else {
                            _labelEditBox.Multiline = false; //Not a fancy label so no multiline
                        }
                        _currentEditField = fi; //Assigning the field that will be assigned text later
                        _labelEditBox.ScrollBars = RichTextBoxScrollBars.None; //No Scrollbar in RichTextEdit
                        _labelEditBox.BorderStyle = BorderStyle.None;

                        _labelEditBox.Font = fi.formatSet.font(_zoomLevel);
                        _labelEditBox.Text = fi.text;
                        _labelEditBox.RightMargin = fi.zwidth - 10;
                        _labelEditBox.Location = new Point(fi.zx + _xOffset, fi.zy + _oldYOffset);
                        _labelEditBox.Size = new Size(fi.zwidth + 1, fi.zheight + 1);
                        designPanel.Controls.Add(_labelEditBox);
                        _labelEditBox.Show();

                        _globalMode.setMode(MouseMode.TextEditing);

                        break;//Stopping the loop from checking past the first label or RichLabel.
                    }
                }
            }

            designPanel.Invalidate();
        }

        public void stopEditing() {
            _currentEditField.text = _labelEditBox.Text;
            _labelEditBox.Hide();
            _currentEditField = null;

            _shouldZoom = true;//Toggling zoom back on
            calculateLabelSizes();
        }

        /// <summary>
        /// The Designer's pasting function. Pastes all the fields that have been cut or copied.
        /// </summary>
        private void paste() {                                                      
            deselectAll();//We run this here to prevent an issue with what's being copied remaining selected.

            foreach (Field fi in _fieldsToCopy) {                                   
                //fi.x = fi.x + 15; //TODO - Added pasting in different positions depensing on if it's a right click and paste or a CTRL-V paste.
                fi.y = fi.y + 18;          //Offsetting so the pasted object(s) sits below the copied object(s).
                fi.selected = true;                 //Selecting them before they are finally pasted. So that they will be when they are added. This helps with possible overlap issues, etc.
                fi.zoomLevel = _zoomLevel;                    //Setting the zoom level to counter if someone copies, then zooms out, then pastes. etc.
                _currentForm.page(_currentPageNumber).Fields.Add(returnCopy(fi));   //Using returnCopy to be able to make position, selected, and zoomlevel changes before the fields are pasted...
                                                                                    //...It also works to prevent continuosly repasting the same fields over and over, effectively just shimmying them down repeatedly.
            }

            _globalMode.setMode(MouseMode.Selected);
            needToSave();
            buildFieldTree();
            calculateSfBox();                                                       
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
            tempField.formatSetName = fi.formatSet.name;                                    //formatSet copying is done in two stages, first is the setting of the tempField's formatSet's name as...
            if (tempField.formatSetName != null) {          //make sure formatSetName for the tempField isn't null, to prevent a null access bug with the _settings call within this if statement.
                tempField.formatSet = _settings.getFormatSetByName(tempField.formatSetName);//...the field to copies formatSet's name. Then back again through the settings singleton. //TODO - There's probably a better way to do this.
            }    
            tempField.text = fi.text;
            tempField.selected = fi.selected;
            tempField.zoomLevel = fi.zoomLevel;
            needToSave();
            return tempField;
            
        }

        #endregion Cut, Copy, and Paste.


        #region Field Deletion

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
            needToSave();
            deselectAll();
            buildFieldTree();
        }

        #endregion Field Deletion


        #region Field Addition

        private void btn_AddField_Click(object sender, EventArgs e) {
            deselectAll();

            _globalMode.setMode(MouseMode.Adding);

            //Testing Grounds///
            string fieldName = "";

            //using (var form = new fieldSelection(_currentForm.formTemplates, "Text")) {//FT1C
            //    var result = form.ShowDialog();
            //    if (result == DialogResult.OK) {
            //        string val = form.name;
            //
             //       fieldName = val;
            //    }
            //}

            //Testing Grounds End///

            
            _fieldToAdd = new Field(fieldName, Type.TextField);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        private void btnAddCheckBox_Click(object sender, EventArgs e) {
            deselectAll();

            _globalMode.setMode(MouseMode.Adding);

            string fieldName = "";

            /*using (var form = new fieldSelection(_currentForm.formTemplates, "Text")) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    string val = form.name;

                    fieldName = val;
                }
            }*/

            _fieldToAdd = new Field(fieldName, Type.Checkbox);
            _fieldToAdd.x = 10;
            _fieldToAdd.y = 10;
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        private void btnAddLabel_Click(object sender, EventArgs e) {
            deselectAll();

            _globalMode.setMode(MouseMode.Adding);
            _fieldToAdd = new Field("Label", Type.Label);
            _fieldToAdd.text = "Placeholder Text";                 //To give it text, and therefore dimensions in the display.
            _fieldToAdd.zoomLevel = _zoomLevel;

            cntxtFormatSets.Show(MousePosition.X, MousePosition.Y);//Setting this up for picking the formatSet instead of the below.
        }

        private void btnAddRichLabel_Click(object sender, EventArgs e) {
            deselectAll();

            _globalMode.setMode(MouseMode.Adding);
            _fieldToAdd = new Field("Rich Label", Type.FancyLabel);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        private void btnAddRectangle_Click(object sender, EventArgs e) {
            deselectAll();

            _globalMode.setMode(MouseMode.Adding);
            _fieldToAdd = new Field("Rectangle", Type.RectangleDraw);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        private void btnAddLine_Click(object sender, EventArgs e) {
            deselectAll();

            _globalMode.setMode(MouseMode.Adding);
            _fieldToAdd = new Field("Line", Type.LineDraw);
            _fieldToAdd.zoomLevel = _zoomLevel;
        }

        #endregion Field Addition


        #region Global Control Functions

        private void calculateLabelSizes() { //TODO - Refactor
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.type == Type.Label) {
                    fi.zwidth = TextRenderer.MeasureText(fi.text, fi.formatSet.font(_zoomLevel)).Width;
                    fi.zheight = TextRenderer.MeasureText(fi.text, fi.formatSet.font(_zoomLevel)).Height;
                }
            }
        }
        
        /// <summary>
        /// Short and sweet, when we make a change we have to run needToSave(); It's for safety when closing forms
        /// as well as a very standard and expected feature of just about any program that allows you to create something.
        /// </summary>
        private void needToSave() {
            if (!_needToSaveForm) {
                Text = _currentForm.FormName + "*";
                _needToSaveForm = true;
            }
        }

        private void deselectAll() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                fi.selected = false;
            }

            _topCrossPoint = new Point();
            _bottomCrossPoint = new Point();
            _leftCrossPoint = new Point();
            _rightCrossPoint = new Point();
            //_resRect = new Rectangle();
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
            _currentForm.page(_currentPageNumber).Fields.Remove(_fieldInProperties);

            _fieldInProperties.name = txtPropName.Text;
            _fieldInProperties.x = Convert.ToInt32(txtPropX.Text);
            _fieldInProperties.y = Convert.ToInt32(txtPropY.Text);
            _fieldInProperties.width = Convert.ToInt32(txtPropWidth.Text);
            _fieldInProperties.height = Convert.ToInt32(txtPropHeight.Text);
            _fieldInProperties.hidden = chkPropHidden.Checked;
            _fieldInProperties.readOnly = chkPropReadOnly.Checked;
            _fieldInProperties.text = txtPropText.Text;
            if (cmbFormatSetNames.SelectedItem.ToString() != "") {
                _fieldInProperties.formatSet = _settings.getFormatSetByName(cmbFormatSetNames.SelectedItem.ToString());
            }

            _currentForm.page(_currentPageNumber).Fields.Add(_fieldInProperties);
            designPanel.Invalidate();
        }

        #endregion Field Properties



        #region Keyboard Bindings

        #region Universal Methods

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {//Steals the KeyEvents away from everything, for better control over what happens during key presses.
            //Delete
            //if (keyData == Keys.Delete) OnKeyPress(new KeyPressEventArgs((Char)Keys.Delete));
            //Mouse Arrow Moving
            if (keyData == Keys.Up) {
                if (designPanel.Focused) {
                    OnKeyPress(new KeyPressEventArgs((Char)Keys.Up));
                    return true;                                        //return true if we don't want the key combo to be sent for processing by the other controls.
                }
            }
            if (keyData == Keys.Down) {
                if (designPanel.Focused) {
                    OnKeyPress(new KeyPressEventArgs((Char)Keys.Down));
                    return true;
                }
            }
            if (keyData == Keys.Left) {
                if (designPanel.Focused) {
                    OnKeyPress(new KeyPressEventArgs((Char)Keys.Left));
                    return true;
                }
            }
            if (keyData == Keys.Right) {
                if (designPanel.Focused) {
                    OnKeyPress(new KeyPressEventArgs((Char)Keys.Right));
                    return true;
                }
            }
            //Form Control Keybindings
            //if (keyData == Keys.ControlKey) OnKeyPress(new KeyPressEventArgs((Char)Keys.ControlKey));
            //if (keyData == Keys.C) OnKeyPress(new KeyPressEventArgs((Char)Keys.C));
            //if (keyData == Keys.X) OnKeyPress(new KeyPressEventArgs((Char)Keys.X));
            //if (keyData == Keys.V) OnKeyPress(new KeyPressEventArgs((Char)Keys.V));

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (Char)Keys.Up) moveFieldsUp();
            if (e.KeyChar == (Char)Keys.Down) moveFieldsDown();
            if (e.KeyChar == (Char)Keys.Left) moveFieldsLeft();
            if (e.KeyChar == (Char)Keys.Right) moveFieldsRight();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) { deleteFields(); return; }

            if (e.Control && e.KeyCode == Keys.X) { cut(); return; }
            if (e.Control && e.KeyCode == Keys.C) { copy(); return; }
            if (e.Control && e.KeyCode == Keys.V) { paste(); return; }
            if (e.KeyCode == Keys.Space && !_globalMode.TextEditing) { startEditing(); return; }//Only enters if TextEditing is false
        }

        #endregion Universal Methods

        #region Arrow Key Moving - Mostly Finished, gradiant speed up would be intersting.

        public void moveFieldsUp() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.y--;
                }
            }
            needToSave();
            designPanel.Invalidate();
            calculateSfBox();
        }

        public void moveFieldsDown() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.y++;
                }
            }
            needToSave();
            designPanel.Invalidate();
            calculateSfBox();
        }

        public void moveFieldsLeft() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.x--;
                }
            }
            needToSave();
            designPanel.Invalidate();
            calculateSfBox();
        }

        public void moveFieldsRight() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.selected) {
                    fi.x++;
                }
            }
            needToSave();
            designPanel.Invalidate();
            calculateSfBox();
        }

        #endregion Arrow Key Moving - Mostly Finished, gradiant speed up would be intersting.

        #endregion Keyboard Bindings




        #region Field Tree Management

        private void btnRefreshFieldTree_Click(object sender, EventArgs e) {
            buildFieldTree();
        }

        /// <summary>
        /// Refreshes the fields listed in the field hierarchy tree view.
        /// </summary>
        private void buildFieldTree() {//TODO - Give this a good Refactor
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

            _globalMode.setMode(MouseMode.Selected);
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

            using (var form = new templateSelection(_currentForm.FormTemplates)) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    List<int> val = form.templates;

                    _currentForm.FormTemplates = val;
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

        private void cntxtFormatSets_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            _fieldToAdd.formatSet = _settings.getFormatSetByName(e.ClickedItem.Text);//Assign the field a formatSet
        }
    }
}