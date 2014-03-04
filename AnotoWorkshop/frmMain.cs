using System.Threading;
using ImageMagick;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class frmMain : Form {
        #region Variables
        private bool needToSaveForm = false;//Used to track if we need to show the save or don't save prompt on form close.
        private Settings _settings = Settings.instance;//The Settings Singleton I use in the code for everything meant to be globalally accessible.
        private int _currentPageNumber;//Used to track the current page number.

        private PenForm _currentForm;

        private Point _startPoint;
        private Point _endPoint;
        private Rectangle _selectionRect;
        private Field _fieldToAdd;
        private Rectangle _sfBoxRect;
        private Rectangle _resRect;

        private Point _sfBoxMoveStart;
        private double _zoomLevel = 1.00;//Franklin

        private readonly Pen _selectionPen = new Pen(Color.Gray);
        private readonly Pen _groupSelectionPen = new Pen(Color.Blue, 2.0f);

        private MouseMode _mode = MouseMode.None;

        public enum MouseMode {
            None,
            Selecting,
            Selected,
            Resizing,
            Adding
        }



        #endregion Variables

        #region Initializers
        /// <summary>
        /// Initialize new instance of the frmMain Form. The only argument it takes is an existing form.
        /// </summary>
        /// <param name="form"></param>
        public frmMain(PenForm form) {
            _currentForm = form;

            InitializeComponent();

            MouseWheel += mouseWheel;
        }

        private void frmMain_Load(object sender, EventArgs e) {
            designerLoadStuff();//Misc things to initialize for the designer stuff
            //-Franklin - Refresh list of objects on load
            /*if (selectedFields == null) {
                selectedFields = new List<Field>();
                lblCurrentProp.Text = "1";
                lblTotalProp.Text = currentForm.page(_currentPageNumber).Fields.Count.ToString();
                backC = trvFieldList.BackColor;
                foreC = trvFieldList.ForeColor;
            }*/
            buildFieldTree();

            this.Text = _currentForm.FormName;
            
        }
        #endregion Initializers

        #region The Designer

        #region Variables
        #endregion Variables

        private void designerLoadStuff() {//Some initilization stuff
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance |
                BindingFlags.NonPublic, null, designPanel, new object[] { true });

            lblVersionNumber.Text = _currentForm.versionNumber.ToString();

            foreach (var fSet in _settings.globalFormatSet) {
                cmbFormatSetNames.Items.Add(fSet.Key);
            }
        }

        private void designer_Paint(object sender, PaintEventArgs e) {//The paint event handler for when the designer area gets redrawn. - Franklin, look for zoomLevel
            lblCurrentPage.Text = Convert.ToString(_currentPageNumber + 1);
            lblTotalpages.Text = _currentForm.totalPages().ToString();

            if (_currentForm.totalPages() > 0) {
                _selectionPen.DashStyle = DashStyle.Dash;

                Rectangle pageRectangle = new Rectangle();
                pageRectangle.X = _xOffset;
                pageRectangle.Y = _yOffset;
                pageRectangle.Height = (int)(792 * _zoomLevel);
                pageRectangle.Width = (int)(612 * _zoomLevel);

                e.Graphics.FillRectangle(new SolidBrush(Color.White), pageRectangle);

                switch (_mode) {
                    case MouseMode.None://No reason to paint anything when there is no selection mode.
                        break;

                    case MouseMode.Selecting:
                        e.Graphics.DrawRectangle(_selectionPen, _selectionRect);
                        break;
                    case MouseMode.Selected://The selection box and editing widgets
                    case MouseMode.Resizing:
                        if (_sfBoxRect.Height > 0) {
                            e.Graphics.DrawRectangle(_groupSelectionPen, new Rectangle((new Point(_sfBoxRect.X + _xOffset, _sfBoxRect.Y + _yOffset)), _sfBoxRect.Size));
                            e.Graphics.DrawRectangle(_groupSelectionPen, new Rectangle((new Point(_resRect.X + _xOffset, _resRect.Y + _yOffset)), _resRect.Size));
                        }
                        break;

                    case MouseMode.Adding:
                        Pen pt = new Pen(Color.Pink, 2);//TODO - This color can change for each item.
                        e.Graphics.DrawRectangle(pt, _selectionRect);
                        break;
                }

                foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                    if (!fi.hidden) {
                        switch (fi.type) {
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

                                //e.Graphics.DrawString(

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
                    if (fi.selected) {//Turn into a create selection box given a Rectangle
                        Rectangle selectedRect = fi.rect();//Initialize new rectangle based of field's position and size.
                        selectedRect.X = fi.zx - 1 + _xOffset;//Added "padding" for the individual selection rectangle
                        selectedRect.Y = fi.zy - 1 + _yOffset;
                        selectedRect.Width = fi.zwidth + 2;
                        selectedRect.Height = fi.zheight + 2;
                        e.Graphics.DrawRectangle(_selectionPen, selectedRect);
                        //e.Graphics.DrawRectangle(_groupSelectionPen, new Rectangle((new Point(selectedRect.X, selectedRect.Y)), selectedRect.Size));
                    }
                }
            }
        }

        #region Mouse Related Variables

        #region Left Click



        #endregion Left Click

        #region Middle Click

        private int _xOffset;
        private int _yOffset;

        private int _oldXOffset;
        private int _oldYOffset;

        private Point _offsetMoveStart;

        #endregion Middle Click

        #region Right Click

        #endregion Right Click

        #endregion Mouse Related Variables


        private float _resChange;

        private void designer_MouseDown(object sender, MouseEventArgs e) {//Controls the events that occur when the mouse is down. (not a click though, which is an down and up)
            if (_currentForm.totalPages() > 0) {
                _startPoint.X = e.X;
                _startPoint.Y = e.Y;

                #region Left Click Down - Kind Done

                if (e.Button == MouseButtons.Left) {
                    if (_resRect.Contains(e.X - _xOffset, e.Y - _yOffset) && _mode != MouseMode.Adding) {
                        _mode = MouseMode.Resizing;

                    }
                    
                    if (!_sfBoxRect.Contains(e.X - _xOffset, e.Y - _yOffset) && _mode != MouseMode.Adding && _mode != MouseMode.Resizing) {//Neither Adding or clicking within the group selection box
                        int notSelectedCount = 0;
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            if (fi.isInside(e.X - _xOffset, e.Y - _yOffset)) {
                                if(ModifierKeys.HasFlag(Keys.Control)) {
                                    fi.selected = !fi.selected;
                                } else {
                                    fi.selected = true;
                                }
                                _mode = MouseMode.Selected;
                            } else {
                                if(ModifierKeys.HasFlag(Keys.Control)) {
                                    fi.selected = fi.selected;
                                } else {
                                    fi.selected = false;
                                }
                                ++notSelectedCount;
                                if (notSelectedCount == _currentForm.page(_currentPageNumber).Fields.Count) _mode = MouseMode.Selecting;
                                _sfBoxRect = new Rectangle();
                            }
                        }
                    }

                    switch (_mode) {
                        case MouseMode.None:
                            break;

                        case MouseMode.Selecting:
                            break;

                        case MouseMode.Selected:
                            if (e.Button == MouseButtons.Left) {//Preparing for moving fields/groups around.
                                foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                                    fi.moveStart = new Point(fi.zx, fi.zy);
                                }
                                _sfBoxMoveStart = new Point(_sfBoxRect.X, _sfBoxRect.Y);
                            }
                            break;
                        case MouseMode.Resizing:
                            
                            if (e.Button == MouseButtons.Left) {//Preparing for moving fields/groups around.
                                foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                                    fi.resizeStart = new Size(fi.zwidth, fi.zheight);
                                }
                                _resChange = 0.0f;
                            }
                            break;
                        case MouseMode.Adding:
                            //_fieldToAdd.zx = _startPoint.X;
                            // _fieldToAdd.zy = _startPoint.Y;
                            break;
                    }
                }

                #endregion Left Click Down - Kind Done

                #region Middle Click Down - Kinda Started

                if (e.Button == MouseButtons.Middle) {
                    _xOffset = _oldXOffset;
                    _yOffset = _oldYOffset;

                    _offsetMoveStart = _startPoint;
                }

                #endregion Middle Click Down - Kinda Started

                #region Right Click Down - Not Started Yet

                #endregion Right Click Down - Not Started Yet
            }
        }

        private void designer_MouseMove(object sender, MouseEventArgs e) {//Handling what happens when the mouse is moving.
            if (_currentForm.totalPages() > 0) {
                if (e.Button == MouseButtons.Middle) {
                    _xOffset = e.X - _offsetMoveStart.X + _oldXOffset;
                    _yOffset = e.Y - _offsetMoveStart.Y + _oldYOffset;
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
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields)
                            {
                                if (fi.selected)
                                {//Moving stuff around code.
                                    fi.zx = fi.moveStart.X - (_startPoint.X - e.X);
                                    fi.zy = fi.moveStart.Y - (_startPoint.Y - e.Y);
                                }
                            }
                            //_sfBoxRect.X = _sfBoxMoveStart.X - (_startPoint.X - e.X);//Moving the blue selected items
                            //_sfBoxRect.Y = _sfBoxMoveStart.Y - (_startPoint.Y - e.Y);

                            if (!needToSaveForm) {
                                this.Text = _currentForm.FormName + "*";
                                needToSaveForm = true;
                            } 
                            calculateSfBox();
                            break;
                        case MouseMode.Resizing:
                            _resChange = _startPoint.X - e.X;//Isn't used yet.
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                                if (fi.selected) {
                                    fi.zwidth = fi.resizeStart.Width - (_startPoint.X - e.X);

                                }
                            }
                            if (!needToSaveForm) {
                                this.Text = _currentForm.FormName + "*";
                                needToSaveForm = true;
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
                            switch (_fieldToAdd.type) {//Need to put in stuff that edits only length of control, etc, etc.
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

                            if (!needToSaveForm) {
                                this.Text = _currentForm.FormName + "*";
                                needToSaveForm = true;
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

            _sfBoxRect = new Rectangle(sfBoxPosition, sfBoxSize);
            _resRect = new Rectangle(new Point(sfBoxPosition.X + sfBoxSize.Width + 7, sfBoxPosition.Y), new Size(2, sfBoxSize.Height));
        }

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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
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



            //_fieldToAdd.text = Interaction.InputBox("Text of Label");
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

            _sfBoxRect = new Rectangle();
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
            this.Text = _currentForm.FormName;
            needToSaveForm = false;         
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
            this.Text = _currentForm.FormName;
            needToSaveForm = false;
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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
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
            if (!needToSaveForm) {
                this.Text = _currentForm.FormName + "*";
                needToSaveForm = true;
            }
            designPanel.Invalidate();
            calculateSfBox();
        }

        #endregion Arrow Key Moving - Mostly Finished, gradiant speed up would be intersting.

        #endregion Keyboard Bindings

        #region Camera Controls

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

        #endregion Camera Controls

        #region Designer Focus

        private void designPanel_MouseEnter(object sender, EventArgs e) {
            ((Panel)sender).Focus();
        }

        private void designPanel_MouseLeave(object sender, EventArgs e) {

        }

        #endregion Designer Focus

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

            if (ModifierKeys.HasFlag(Keys.Control)) {
                _currentForm.page(_currentPageNumber).Fields[e.Node.Index].selected = !_currentForm.page(_currentPageNumber).Fields[e.Node.Index].selected;
            } else {
                deselectAll();
                _currentForm.page(_currentPageNumber).Fields[e.Node.Index].selected = true;
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
            if(needToSaveForm) {//Used to intercept the form closing, in order to see if the user wants to save or not.
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
                newY = newY/yPositions.Count;
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
    }
}