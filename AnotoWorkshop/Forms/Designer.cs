﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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

        private int _oldXOffset = 35;        //The old X and Y offset values are needed due to the fact that during the move event the offsets are changing constantly as the...
        private int _oldYOffset = 35;        //...mouse moves, you get some weird exponential math problems that made the page jump off the screen soon after the mouse is moved.

        private int _xOffset = 35;           //The variables used to ultimately track how...
        private int _yOffset = 35;           //...much the user has moved the page around.

        //variables needed for editing the labels
        private RichTextBox _activeTextEditBox = new RichTextBox();
        private Field _activeEditField;


        private float _zoomLevel = 1.00f;      //Used to zoom in and out of pages.
        private bool _shouldZoom = true;       //Used to flag if we should allow zooming or not.
        private bool _shouldDelete = true;     //Used to flag if we should allow deleting functionality, implemented to prevent deletion of fields while editing.
        private bool _shouldMove = true;     //

        private readonly Pen _selectionPen = new Pen(Color.Gray);            //The pen used for the _selectionRect
        private readonly Pen _groupSelectionPen = new Pen(Color.Blue, 2.0f); //The pen used for the _groupSelectionRect

        private Mode _globalMode = new Mode(); //global mode instance


        private bool _needFontMenu = false;
        Field _changingFontField = null;

        FontEditorMenu _mFontMenu;
        FontPopupContainer _mFontMenuContainer;


        private bool _needOGEditor = false;
        Field _activeOGField = null;
        OptionsEditorMenu _OGEditor;
        OptionsPopupContainer _OGEditorContainer;


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

        #region Font Editor
        //Names
        private void fontNameCmbValueChanged (object sender, EventArgs a) {
            _changingFontField.fontTypeface = _mFontMenu.cmbFontList.Text;
            calculateLabelSizes();
            designPanel.Invalidate();
        }        
        
        private void fontNameCmbDropOpened (object sender, EventArgs a) {
            _mFontMenuContainer.AutoClose = false;
        }
        
        private void fontNameCmbDropClosed (object sender, EventArgs a) {
            _mFontMenuContainer.AutoClose = true;
        }

        //Size
        private void fontSizeCmbValueChanged (object sender, EventArgs a) {
            int.TryParse(_mFontMenu.cmbFontSizes.Text, out _changingFontField.fontSize);
            calculateLabelSizes();
            designPanel.Invalidate();
        }        
        
        private void fontSizeCmbDropOpened (object sender, EventArgs a) {
            _mFontMenuContainer.AutoClose = false;
        }
        
        private void fontSizeCmbDropClosed (object sender, EventArgs a) {
            _mFontMenuContainer.AutoClose = true;
        }

        //Styles
        private void chkFontBold (object sender, EventArgs a) {
            if(_mFontMenu.chkBoldToggle.Checked) {
                _changingFontField.fontStyle = _changingFontField.fontStyle | FontStyle.Bold;
                designPanel.Invalidate();
            } else {
                _changingFontField.fontStyle = _changingFontField.fontStyle & ~FontStyle.Bold;
                designPanel.Invalidate();
            }
            calculateLabelSizes();
        }        
 
        private void chkFontItalic (object sender, EventArgs a) {
            if(_mFontMenu.chkItalicToggle.Checked) {
                _changingFontField.fontStyle = _changingFontField.fontStyle | FontStyle.Italic;
                designPanel.Invalidate();
            } else {
                _changingFontField.fontStyle = _changingFontField.fontStyle & ~FontStyle.Italic;
                designPanel.Invalidate();
            }
            calculateLabelSizes();
        }        
 
        private void chkFontUnderline (object sender, EventArgs a) {
            if(_mFontMenu.chkUnderlineToggle.Checked) {
                _changingFontField.fontStyle = _changingFontField.fontStyle | FontStyle.Underline;
                designPanel.Invalidate();
            } else {
                _changingFontField.fontStyle = _changingFontField.fontStyle & ~FontStyle.Underline;
                designPanel.Invalidate();
            }
            calculateLabelSizes();
        }        
         
        #endregion Font Editor

        #region Option Group Editor

        private void addSubitem(object sender, EventArgs a) {
            if(isDupSubitem(_activeOGField.items ,_OGEditor.txtSubitemValue.Text)) {
                return;
            }

            _OGEditor.lstvSubitems.Items.Add(_OGEditor.txtSubitemValue.Text + " - " +_OGEditor.txtSubitemLabel.Text);

            int items = _activeOGField.items.Count;

            SubItem subItem = new SubItem();
            subItem.value = _OGEditor.txtSubitemValue.Text;
            if (_OGEditor.txtSubitemLabel.Text != "") {
                subItem.text = _OGEditor.txtSubitemLabel.Text;
                subItem.width = TextRenderer.MeasureText(subItem.text, Font).Width;
                subItem.height = TextRenderer.MeasureText(subItem.text, Font).Height;
            } else {
                subItem.text = _OGEditor.txtSubitemValue.Text;
                subItem.width = TextRenderer.MeasureText(subItem.text, Font).Width;
                subItem.height = TextRenderer.MeasureText(subItem.text, Font).Height;
            }
            _activeOGField.items.Add(items + 1, subItem);

            setupActiveOptionsGroup();
            needToSave();

            designPanel.Invalidate();

            _OGEditor.txtSubitemValue.Clear();
            _OGEditor.txtSubitemLabel.Clear();

        }

        private void removeSubitem(object sender, EventArgs a) {
            if (_OGEditor.lstvSubitems.SelectedItems.Count == 0) return;
            string valToRemove = _OGEditor.lstvSubitems.SelectedItems[0].Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];

            int toRemove = 0;

            foreach (var subItem in _activeOGField.items) {
                if (subItem.Value.value == valToRemove) toRemove = subItem.Key;
            }

            List<int> toDecrease = new List<int>();

            if (toRemove != 0) {
                _activeOGField.items.Remove(toRemove);
                for (int i = toRemove; i <= _activeOGField.items.Count; i++) {
                    toDecrease.Add(i + 1);
                }
                foreach (var i in toDecrease) {
                    SubItem newItem = new SubItem();

                    newItem.value = _activeOGField.items[i].value;
                    newItem.text = _activeOGField.items[i].text;

                    _activeOGField.items.Remove(i);
                    _activeOGField.items.Add(i - 1, newItem);
                }
                toDecrease.Clear();
            }

            _OGEditor.lstvSubitems.Clear();

            for (int i = 1; i <= _activeOGField.items.Count; i++) {
                _OGEditor.lstvSubitems.Items.Add(_activeOGField.items[i].value + " - " + _activeOGField.items[i].text);
            }

            setupActiveOptionsGroup();
            needToSave();
            designPanel.Invalidate();
        }

        private bool _ignoreOGChange = false;
        private void changeOGColumns(object sender, EventArgs a) {
            if(!_ignoreOGChange) {
                _activeOGField.columns = (int)_OGEditor.nmColumns.Value;
                setupActiveOptionsGroup();
                needToSave();
                designPanel.Invalidate();
            }
        }

        private void moveSubitemUp(object sender, EventArgs a) {
            if (_OGEditor.lstvSubitems.SelectedItems.Count == 0) return;

            string valToRaise = _OGEditor.lstvSubitems.SelectedItems[0].Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];

            int toRaise = 0;

            foreach (var subItem in _activeOGField.items) {
                if (subItem.Value.value == valToRaise) toRaise = subItem.Key;
            }

            if (toRaise == 1) return;

            if (toRaise != 0) {

                SubItem oldItem = new SubItem();
                oldItem.value = _activeOGField.items[toRaise].value;
                oldItem.text = _activeOGField.items[toRaise].text;

                _activeOGField.items.Remove(toRaise);

                SubItem newItem = new SubItem();
                newItem.value = _activeOGField.items[toRaise - 1].value;
                newItem.text = _activeOGField.items[toRaise - 1].text;

                _activeOGField.items.Remove(toRaise - 1);

                _activeOGField.items.Add(toRaise, newItem);

                _activeOGField.items.Add(toRaise - 1, oldItem);
            }

            _OGEditor.lstvSubitems.Clear();
            for (int i = 1; i <= _activeOGField.items.Count; i++) {
                _OGEditor.lstvSubitems.Items.Add(_activeOGField.items[i].value + " - " + _activeOGField.items[i].text);
            }
            _OGEditor.lstvSubitems.Items[toRaise - 2].Selected = true;
            _OGEditor.lstvSubitems.Focus();

            setupActiveOptionsGroup();
            needToSave();
            designPanel.Invalidate();
        }

        private void moveSubitemDown(object sender, EventArgs a) {
            if (_OGEditor.lstvSubitems.SelectedItems.Count == 0) return;

            string valToLower = _OGEditor.lstvSubitems.SelectedItems[0].Text.Split(new string[] { " - " }, StringSplitOptions.None)[0];

            int toLower = 0;

            foreach (var subItem in _activeOGField.items) {
                if (subItem.Value.value == valToLower) toLower = subItem.Key;
            }

            if (toLower == _activeOGField.items.Count) return;

            if (toLower != 0) {

                SubItem oldItem = new SubItem();
                oldItem.value = _activeOGField.items[toLower].value;
                oldItem.text = _activeOGField.items[toLower].text;

                _activeOGField.items.Remove(toLower);

                SubItem newItem = new SubItem();
                newItem.value = _activeOGField.items[toLower + 1].value;
                newItem.text = _activeOGField.items[toLower + 1].text;

                _activeOGField.items.Remove(toLower + 1);

                _activeOGField.items.Add(toLower, newItem);

                _activeOGField.items.Add(toLower + 1, oldItem);
            }

            _OGEditor.lstvSubitems.Clear();
            for (int i = 1; i <= _activeOGField.items.Count; i++) {
                _OGEditor.lstvSubitems.Items.Add(_activeOGField.items[i].value + " - " + _activeOGField.items[i].text);
            }
            _OGEditor.lstvSubitems.Items[toLower].Selected = true;
            _OGEditor.lstvSubitems.Focus();

            setupActiveOptionsGroup();
            needToSave();
            designPanel.Invalidate();

        }

        private void setupActiveOptionsGroup() {
            int columns     = _activeOGField.columns;
            int qtyOptions  = _activeOGField.items.Count;
            double width    = _activeOGField.width;
            double height   = _activeOGField.height;
            int rows        = (int)Math.Ceiling(( (double)qtyOptions / columns));
            double xSpacing = width / columns;
            double ySpacing = height / rows;

            int curColumn = 1;
            int curRow    = 1;
            int curItem;

            for (curItem = 1; curItem <= qtyOptions; curItem++) {
                if (curColumn < columns) {
                    _activeOGField.items[curItem].x = (int)((curColumn - 1) * xSpacing);
                    _activeOGField.items[curItem].y = (int)((curRow - 1) * ySpacing);

                    curColumn++;
                } else if (curColumn == columns) {
                    _activeOGField.items[curItem].x = (int)((curColumn - 1) * xSpacing);
                    _activeOGField.items[curItem].y = (int)((curRow - 1) * ySpacing);

                    curRow++;
                    curColumn = 1;
                }
            }                
        }

        private bool isDupSubitem(Dictionary<int, SubItem> toCheckAgainst, string toCheck) {
            for (int i = 1; i <= toCheckAgainst.Count; i++) {
                if (toCheck == toCheckAgainst[i].value) {
                    return true;
                }
            }
            return false;
        }

        #endregion Option Group Editor

        void loadQueryFields() {
            try {
                PrintConfig test = new PrintConfig(_settings.exportFolder + @"\FusionPrintConfig.xml");
                List<string> testList = new List<string>();

                foreach (var testvar in test.sectionList) {
                    if (testvar.Value.type == SectionType.Column || testvar.Value.type == SectionType.Alias) {
                        testList.Add(testvar.Value.name);
                    }
                }

                List<string> fieldsToCreate = new List<string>();

                foreach (string ts in testList) {
                    if (ts != null) {
                        fieldsToCreate.Add(ts);
                    }
                }

                foreach (string ts in testList) {
                    foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                        if (ts == fi.name) {
                            fieldsToCreate.Remove(fi.name);
                        }
                    }
                }

                foreach (string nameToAdd in fieldsToCreate) {
                    Field testField = new Field(nameToAdd, Type.TextField);
                    testField.readOnly = true;
                    testField.hidden = true;
                    testField.x = 1;
                    testField.y = 1;
                    testField.width = 2;
                    testField.height = 16;

                    _currentForm.page(_currentPageNumber).Fields.Add(testField);
                }
            }
            catch (Exception) {
                MessageBox.Show("The export folder is missing the FusionPrintConfig.xml file, please copy it to your export folder or choose the correct folder to export to.", "File Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                throw;
            }     
        }

        private void frmMain_Load(object sender, EventArgs e) {

            loadQueryFields();

            #region Font Editor
            _mFontMenu = new FontEditorMenu();
            _mFontMenuContainer = new FontPopupContainer(_mFontMenu, this);

            //Names
            _mFontMenu.cmbFontList.SelectedValueChanged += fontNameCmbValueChanged;
            _mFontMenu.cmbFontList.DropDown += fontNameCmbDropOpened;
            _mFontMenu.cmbFontList.DropDownClosed += fontNameCmbDropClosed;
            //Size
            _mFontMenu.cmbFontSizes.SelectedValueChanged += fontSizeCmbValueChanged;
            _mFontMenu.cmbFontSizes.DropDown += fontSizeCmbDropOpened;
            _mFontMenu.cmbFontSizes.DropDownClosed += fontSizeCmbDropClosed;
            //Styles
            _mFontMenu.chkBoldToggle.CheckedChanged += chkFontBold;
            _mFontMenu.chkItalicToggle.CheckedChanged += chkFontItalic;
            _mFontMenu.chkUnderlineToggle.CheckedChanged += chkFontUnderline;
            #endregion Font Editor

            #region Option Group Editor
            _OGEditor = new OptionsEditorMenu();
            _OGEditorContainer = new OptionsPopupContainer(_OGEditor);
            
            _OGEditor.btnAddSubitem.Click += addSubitem;
            _OGEditor.btnRemoveSubitem.Click += removeSubitem;
            _OGEditor.nmColumns.ValueChanged += changeOGColumns;
            _OGEditor.btnMoveSubitemUp.Click += moveSubitemUp;
            _OGEditor.btnMoveSubitemDown.Click += moveSubitemDown;
            #endregion Option Group Editor

            splitContainer2.Panel1MinSize = _OGEditor.Width;//So the hierarchy view will always fit the OGediter

            //Icon stuff more or less
            //btnUndo.Text = "\uE10D";
            //btnRedo.Text = "\uE10E";
            //btnSaveForm.Text = "\uE105";
            //btnExportForm.Text = "\uE126";
            //btnLoadSettingsScreen.Text = "\uE115";
            //btnNewPage.Text = "\uE160";
            //btnNextPage.Text = "\uE111";
            //btnPreviousPage.Text = "\uE112";
            //chkAddWritten.Text = "\uE18F";
            //chkAddCheckbox.Text = "\uE0A2";
            //chkAddLabel.Text = "\uE185";
            //btnAddRichLabel.Text = "\uE185";
            //chkAddRectangle.Text = "\uE2B3";
            //chkAddLine.Text = "\uE108";

            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Loading the RichTextBox's that go with the RichLabel's
                if(fi.type == Type.RichLabel) {
                    fi.richBox = new RichTextBox();
                    fi.richBox.Rtf = fi.rtc;
                }
                if(fi.type == Type.OptionGroup) {
                    _activeOGField = fi;
                    setupActiveOptionsGroup();
                    _activeOGField = null;
                }
            }

            //Consistent activeTextEdit
            _activeTextEditBox.ScrollBars = RichTextBoxScrollBars.None; //No Scrollbar in RichTextEdit
            _activeTextEditBox.BorderStyle = BorderStyle.None;
            _activeTextEditBox.MouseUp += activeRichTextEditBox_MouseUp;
            _activeTextEditBox.Hide();
            designPanel.Controls.Add(_activeTextEditBox);


            designerLoadStuff();            //Misc things to initialize for the designer stuff
            refreshHierarchyView();               //Function for building the Field Heirarchy 

            calculateLabelSizes();

        }

        private void ResizeDescriptionArea(ref PropertyGrid grid, int nNumLines) {
            try {
                PropertyInfo pi = grid.GetType().GetProperty("Controls");

                foreach(Control c in grid.Controls) {
                    System.Type ct = c.GetType();
                    string sName = ct.Name;
       
                    if(sName == "DocComment") {
                        pi = ct.GetProperty("Lines");
                        pi.SetValue(c, nNumLines, null);

                        FieldInfo fi = ct.BaseType.GetField("userSized",
                        BindingFlags.Instance | BindingFlags.NonPublic);
 
                        fi.SetValue(c, true);
                        }
                    }
                } catch(Exception error) {
                #if(DEBUG)
                MessageBox.Show(error.Message, "ResizeDescriptionArea()");
                #endif

                return;
            }
        }
        
        /// <summary>
        /// Just a void function for doing some on load prep work.
        /// </summary>
        private void designerLoadStuff() {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty |     //Needed to get the designer panel to not have artifacts, etc.
                BindingFlags.Instance | BindingFlags.NonPublic, null, designPanel, new object[] { true });

            _selectionPen.DashStyle = DashStyle.Dash;  //The selection box will be drawn with dashes lines.

            lblVersionNumber.Text = _currentForm.versionNumber.ToString();              //Setting the version number label

            //foreach (var fSet in _settings.globalFormatSet) {   //Iterate through the globalFormatSets...
            //    cmbFormatSetNames.Items.Add(fSet.Key);          //... to add each one to the formatSet combobox
            //    cntxtFormatSets.Items.Add(fSet.Key);//Building the FormatSet context menu's items.
            //}

            ResizeDescriptionArea(ref propertyGrid, 6);

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

            e.Graphics.ScaleTransform(_zoomLevel, _zoomLevel);

            if (_currentForm.totalPages() > 0) { //Being safe about what we paint, without this we get exceptions when have empty pages.
                Rectangle pageRectangle = new Rectangle {       //This rectangle is for drawing the white "page" that all the fields are on
                                X = _xOffset,                   //Offsetting the page by the X and Y...
                                Y = _yOffset,                   //...offsets to facilitate "moving" the page.
                                Height = 792,//Height and Width get their dimensions multiplied by the...
                                Width  = 612  //...value of _zoomLevel to allow for enlarging and shrinking.
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
                                e.Graphics.DrawRectangle(p1, new Rectangle((new Point(fi.x + _xOffset, fi.y + _yOffset)), fi.rect().Size));
                                p1.Color = Color.LightGray;
                                e.Graphics.DrawLine(p1, fi.x + _xOffset + 1, fi.y + _yOffset - 1 + fi.height, //Creates the shadow line for the text box.
                                                        fi.x + _xOffset - 1 + fi.width, fi.y + _yOffset - 1 + fi.height);
                                break;

                            case Type.Label:
                                Pen p2 = new Pen(Color.LightBlue);
                                e.Graphics.DrawRectangle(p2, new Rectangle((new Point(fi.x + _xOffset, fi.y + _yOffset)), fi.rect().Size));

                                p2.Color = Color.Black;
                                e.Graphics.DrawString(fi.text, fi.font(), p2.Brush, new Point(fi.x + _xOffset, fi.y + _yOffset));
                                break;

                            case Type.RichLabel:
                                Pen flPen = new Pen(Color.LightBlue);
                                e.Graphics.DrawRectangle(flPen, new Rectangle((new Point(fi.x + _xOffset, fi.y + _yOffset)), fi.rect().Size));
                     
                                if(fi.richBox != null) e.Graphics.DrawRtfText(fi.richBox.Rtf, fi.rect(), new Point(_xOffset, _yOffset));

                                break;

                            case Type.RectangleDraw:
                                Pen recPen = new Pen(Color.LightGreen);
                                e.Graphics.DrawRectangle(recPen, new Rectangle((new Point(fi.x + _xOffset, fi.y + _yOffset)), fi.rect().Size));
                                break;

                            case Type.LineDraw:
                                Pen linePen = new Pen(Color.LightGreen);
                                e.Graphics.DrawRectangle(linePen, new Rectangle((new Point(fi.x + _xOffset, fi.y + _yOffset)), fi.rect().Size));
                                break;

                            case Type.Checkbox:
                                Pen p3 = new Pen(Color.Green);
                                e.Graphics.DrawRectangle(p3, new Rectangle((new Point(fi.x + _xOffset, fi.y + _yOffset)), fi.rect().Size));
                                p3.Color = Color.Black;
                                double zhmod = 4; //This is to give a slight offset to the y position for the string we draw for checkboxes, but also make it match with the _zoomLevel.
                                e.Graphics.DrawString(fi.text, fi.font(), p3.Brush, new Point(                      //This draw strings starts off pretty standard with the text, font and pen...
                                       fi.x + _xOffset +    fi.width                                                                  //...starts getting a little fancy by offsetting the x position to account for the checkboxes width...
                                    , (int)(fi.y + _yOffset + (((fi.height - fi.font().Size) / 2)) - zhmod)));    //...but here we get really fancy, in order to center along the height and account for different sizes of checkbox...
                                break;                                                                                                  //...and different levels of zoom and font size, etc.

                            case Type.OptionGroup:
                                Pen p4 = new Pen(Color.Red);
                                e.Graphics.DrawRectangle(p4, new Rectangle((new Point(fi.x + _xOffset, fi.y + _yOffset)), fi.rect().Size));

                                foreach (var item in fi.items) {
                                    e.Graphics.DrawRectangle(p4, new Rectangle(item.Value.x + fi.x + _xOffset + 2, item.Value.y +  fi.y + _yOffset + 2, 12, 12));
                                    
                                    e.Graphics.DrawString(item.Value.text, Font, p4.Brush, item.Value.x + fi.x + _xOffset + 14, item.Value.y +  fi.y + _yOffset);
                                }
                                break;
                        }
                    }
                    if (fi.selected) {                          //drawing the dashed outline to indicate something is selected //TODO - Redesign this to be more of a selection glow or something.
                        Rectangle selectedRect = fi.rect();     //Initialize new rectangle based of field's position and size.
                        selectedRect.X = fi.x - 1 + _xOffset;  //Added "padding" for the individual selected rectangle...
                        selectedRect.Y = fi.y - 1 + _yOffset;  //...(otherwise you wouldn't see it due to overlap)
                        selectedRect.Width = fi.width + 2;     
                        selectedRect.Height = fi.height + 2;
                        e.Graphics.DrawRectangle(_selectionPen, selectedRect);

                        e.Graphics.DrawRectangle(_groupSelectionPen, new Rectangle( //Paint the selected fields resizer(s)
                            (new Point(fi.resizer().Location.X + _xOffset,
                            fi.resizer().Location.Y + _yOffset)), 
                            fi.resizer().Size));
                    }
                }
            }
        }


        //+Right Click
        //Nothing yet

        private void designer_MouseDown(object sender, MouseEventArgs e) {//Controls the events that occur when the mouse is down. (not a click though, which is an down and up)
            if (_currentForm.totalPages() > 0) {//Empty page protection.

                int zx = (int)(e.X / _zoomLevel);
                int zy = (int)(e.Y / _zoomLevel);


                _startPoint.X = zx;    //Assigning the start point as the location the mouse is clicked down on...
                _startPoint.Y = zy;    //used generally for moving and resizing fields, but also for adding, etc.

                #region Left Click Down
                if (e.Button == MouseButtons.Left) {
                    if (_globalMode.TextEditing) {
                        stopEditing();
                    }
                    if(_globalMode.Selected) {   //To prevent the click down from being able to trigger the resize without being able to see the resizer. //TODO - BUG - We can still have this issue if we click the invisible resizers when we are resizing another seperate field.
                        foreach(Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            if(fi.resizer().Contains(zx - _xOffset, zy - _yOffset)) { //If the field's resizer Rectangle contains the point that was clicked...  
                                fi.resizing = true;                                     //...the field is flagged as resizing...
                                _globalMode.setMode(MouseMode.Resizing);                             //...and the mode is set to resizing (for proper mouseMove handling)
                            }
                        }
                    }
                    //if (_resRect.Contains(zx - _xOffset, zy - _yOffset)) { //If the resizing rectangle is what is clicked. Accounting for a moved field.
                    //    _mode = MouseMode.Resizing;
                    //}
                    
                    if (!_groupSelectionRect.Contains(zx - _xOffset, zy - _yOffset)           //Not within the group box
                    && !_globalMode.Resizing                                              //or if just just went into resizing mode
                    && !_globalMode.Adding) {                                             //or we just clicked one of the adding buttons.
                        int notSelectedCount = 0;                                                   //this is used to track how many fields are selected
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Iterating through all the fields on the page.
                            if (fi.isInside(zx - _xOffset, zy - _yOffset)) {                  //If where we are clicking is inside the field.
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
                            if (fi.isInside(zx - _xOffset, zy - _yOffset)) {          //Clicking within a field, within the selection box.
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
                                fi.moveStart = new Point(fi.x, fi.y);                             //...grab the current position to use for moving around during mouseMove
                            }
                            break;

                        case MouseMode.Resizing:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Foreach of the page fields...
                                fi.resizeStart = new Size(fi.width, fi.height);                   //...grab the current size to use for resizing during mouseMove
                            }
                            break;

                        case MouseMode.Adding:
                            switch (_fieldToAdd.type) {                         //Using a switch in case we need it later, it could be an if statement now though.
                                case Type.Label:                                                    //When the left mouse button clicks down, and we're adding a label.
                                    _fieldToAdd.x = zx - _xOffset;                                                                                    //Setting the fields positions...
                                    _fieldToAdd.y = zy - _yOffset;                                                                                    //...
                                    _fieldToAdd.width = TextRenderer.MeasureText(_fieldToAdd.text, _fieldToAdd.font()).Width;      //A Label's outer box size is based off the string it displays...
                                    _fieldToAdd.height = TextRenderer.MeasureText(_fieldToAdd.text, _fieldToAdd.font()).Height;    //...here we use TextRenderer.MeasureText to get this information.
                                    
                                    _currentForm.page(_currentPageNumber).Fields.Add(_fieldToAdd);  //Add the field...
                                    _fieldToAdd = null;                                             //...effectively empty the field, to prepare for a new addition.
                                    disableOtherAdders(new CheckBox());
                                    _globalMode.setMode(MouseMode.None);     //To prevent null _fieldToAdd during mouseMove and mouseUp errors I was having.
                                    
                                    refreshHierarchyView();
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
                if (e.Button == MouseButtons.Right) {
                    _needFontMenu = false;
                }
                #endregion Right Click Down
            }
        }

        private void designer_MouseMove(object sender, MouseEventArgs e) {//Handling what happens when the mouse is moving.
            if (_currentForm.totalPages() > 0) {    //

                int zx = (int)(e.X / _zoomLevel);
                int zy = (int)(e.Y / _zoomLevel);

                if (e.Button == MouseButtons.Middle) {
                    _xOffset = zx - _startPoint.X + _oldXOffset;
                    _yOffset = zy - _startPoint.Y + _oldYOffset;
                }

                if (_globalMode.Adding)
                {
                    _topCrossPoint = new Point(zx, 0);
                    _bottomCrossPoint = new Point(zx, 1000);
                    _leftCrossPoint = new Point(0, zy);
                    _rightCrossPoint = new Point(1000, zy); 
                }

                if (e.Button == MouseButtons.Left && !ModifierKeys.HasFlag(Keys.Control)) {
                    switch (_globalMode.mode()) {
                        case MouseMode.None:
                            break;

                        case MouseMode.Selecting://Figuring out the position and size of the selection rectangle.
                            if (zx > _startPoint.X) {
                                _selectionRect.X = _startPoint.X;
                                _selectionRect.Width = zx - _startPoint.X;
                            } else {
                                _selectionRect.X = zx;
                                _selectionRect.Width = _startPoint.X - zx;
                            }
                            if (zy > _startPoint.Y) {
                                _selectionRect.Y = _startPoint.Y;
                                _selectionRect.Height = zy - _startPoint.Y;
                            } else {
                                _selectionRect.Y = zy;
                                _selectionRect.Height = _startPoint.Y - zy;
                            }

                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Checking if fields are within the selection rectangle.
                                if (fi.x > _selectionRect.X - _xOffset && fi.x < _selectionRect.Width + _selectionRect.X - _xOffset &&
                                    fi.y > _selectionRect.Y - _yOffset && fi.y < _selectionRect.Height + _selectionRect.Y - _yOffset &&

                                    fi.width + fi.x < _selectionRect.Width + _selectionRect.X - _xOffset &&
                                    fi.height + fi.y < _selectionRect.Height + _selectionRect.Y - _yOffset
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
                                    fi.x = fi.moveStart.X - (_startPoint.X - zx);
                                    fi.y = fi.moveStart.Y - (_startPoint.Y - zy);
                                    if (_startPoint.X - zx != 0 && _startPoint.Y - zy != 0) needToSave(); //Only trigger if it actually changes, fixing a weird error when a menu is open and you click on a field
                                }
                            }
                            
                            calculateSfBox();
                            break;

                        case MouseMode.Resizing:
                            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {//Go through the fields...
                                if (fi.resizing) {                                              //...and find the one we are specifically resizing.
                                    if(fi.type == Type.TextField) {
                                        fi.width = fi.resizeStart.Width - (_startPoint.X - zx);//We only resize width for TextFields

                                        if(fi.width < 5) fi.width = 5;                           //Don't let it get smaller then 5 points wide.
                                    }
                                    if(fi.type == Type.Checkbox) {
                                        fi.width = fi.resizeStart.Width - (_startPoint.X - zx);       //Here we equally resize width and height...
                                        fi.height = fi.resizeStart.Height - (_startPoint.X - zx);     //...based off the horizontal motion of the mouse.

                                        if(fi.width < 2) fi.width = 2;                                  //Without letting it get smaller then 2 points in size.
                                        if(fi.height < 2) fi.height = 2;
                                    }
                                    if(fi.type == Type.RectangleDraw || fi.type == Type.RichLabel || fi.type == Type.OptionGroup) {
                                        fi.width = fi.resizeStart.Width - (_startPoint.X - zx);  //For the Rectangles and Rich Labels we freely resize width...
                                        fi.height = fi.resizeStart.Height - (_startPoint.Y - zy);//...and height

                                        if(fi.width < 5) fi.width = 5;                             //Here we limit them from getting smaller then 5 points for width...
                                        if(fi.height < 5) fi.height = 5;                           //...and height
                                        
                                        if(fi.type == Type.OptionGroup && _activeOGField != null) {
                                            setupActiveOptionsGroup();
                                        }

                                    }
                                }
                            }
                            needToSave();
                            calculateSfBox();
                            break;

                        case MouseMode.Adding:
                            if (zx > _startPoint.X) {
                                _selectionRect.X = _startPoint.X;
                                _selectionRect.Width = zx - _startPoint.X;
                            } else {
                                _selectionRect.X = zx;
                                _selectionRect.Width = _startPoint.X - zx;
                            }

                            if (zy > _startPoint.Y) {
                                _selectionRect.Y = _startPoint.Y;
                                _selectionRect.Height = zy - _startPoint.Y;
                            } else {
                                _selectionRect.Y = zy;
                                _selectionRect.Height = _startPoint.Y - zy;
                            }

                            switch (_fieldToAdd.type) {
                                case Type.TextField:
                                    _selectionRect.Height = 16;
                                    break;

                                case Type.OptionGroup:
                                    //Nothing special, no constraints
                                    break;

                                case Type.Checkbox:
                                    if (_selectionRect.Height < _selectionRect.Width) _selectionRect.Height = _selectionRect.Width;
                                    else _selectionRect.Width = _selectionRect.Height;
                                    break;

                                case Type.Label:
                                    //Nothing special, no constraints
                                    break;
                  
                                case Type.RichLabel:
                                    //Nothing special, no constraints
                                    break;
                            
                                case Type.RectangleDraw:
                                    //Nothing special, no constraints
                                    break;

                                case Type.LineDraw:
                                    if (_selectionRect.Height < _selectionRect.Width) _selectionRect.Height = 1;
                                    else _selectionRect.Width = 1;
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

                        int fieldsCount = 0;//For tracking how many fields get selected, so that only one field will display in the properties
                        foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                            if (fi.selected) {
                                fieldsCount++;
                                if (fieldsCount > 1) {
                                    refreshProperties(null);
                                } else {
                                    refreshProperties(fi);
                                }

                                if(fi.type == Type.Label || fi.type == Type.Checkbox) {
                                    _needFontMenu = true;
                                    _changingFontField = fi;
                                } else {
                                    _needFontMenu = false;
                                    _changingFontField = null;
                                }

                                if(fi.type == Type.OptionGroup) {
                                    _needOGEditor = true;
                                    _activeOGField = fi;
                                } else {
                                    _needOGEditor = false;
                                    _activeOGField = null;
                                }

                            }
                        }

                    if(_needFontMenu && _changingFontField != null) {
                        int fontMenuPosX = designPanel.PointToScreen(new Point((int)((_changingFontField.x + _xOffset) * _zoomLevel), (int)((_changingFontField.y + _yOffset) * _zoomLevel))).X - (int)(10 * _zoomLevel);
                        int fontMenuPosY = designPanel.PointToScreen(new Point((int)((_changingFontField.x + _xOffset) * _zoomLevel), (int)((_changingFontField.y + _yOffset) * _zoomLevel))).Y - (int)(28 + (8 * _zoomLevel));

                        _mFontMenu.cmbFontList.Text = _changingFontField.fontTypeface;
                        _mFontMenu.cmbFontSizes.Text = _changingFontField.fontSize.ToString();

                        if (_changingFontField.fontStyle.HasFlag(FontStyle.Bold)) {
                            _mFontMenu.chkBoldToggle.Checked = true;
                        } else {
                            _mFontMenu.chkBoldToggle.Checked = false;
                        }

                        if (_changingFontField.fontStyle.HasFlag(FontStyle.Italic)) {
                            _mFontMenu.chkItalicToggle.Checked = true;
                        } else {
                            _mFontMenu.chkItalicToggle.Checked = false;
                        }

                        if (_changingFontField.fontStyle.HasFlag(FontStyle.Underline)) {
                            _mFontMenu.chkUnderlineToggle.Checked = true;
                        } else {
                            _mFontMenu.chkUnderlineToggle.Checked = false;
                        }

                        _mFontMenuContainer.Show(fontMenuPosX, fontMenuPosY);
                    }

                    if(_needOGEditor && _activeOGField != null) {


                        _OGEditor.lstvSubitems.Clear();
                        for (int i = 1; i <= _activeOGField.items.Count; i++) {
                            _OGEditor.lstvSubitems.Items.Add(_activeOGField.items[i].value + " - " + _activeOGField.items[i].text);
                        }

                        _ignoreOGChange = true;
                        _OGEditor.nmColumns.Value = _activeOGField.columns;
                        _ignoreOGChange = false;
                       
                        _OGEditor.txtSubitemValue.Clear();
                        _OGEditor.txtSubitemLabel.Clear();

                        Point position = trvFieldList.PointToScreen(new Point(0, 0));
                        int fontMenuPosX = position.X;
                        int fontMenuPosY = position.Y;


                        _OGEditorContainer.Show(fontMenuPosX, fontMenuPosY);
                        _OGEditor.nmColumns.Focus();

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
                                    _fieldToAdd.x = _selectionRect.X - _xOffset;
                                    _fieldToAdd.y = _selectionRect.Y - _yOffset;
                                    break;

                                case Type.OptionGroup:
                                    _fieldToAdd.x = _selectionRect.X - _xOffset;
                                    _fieldToAdd.y = _selectionRect.Y - _yOffset;
                                    break;

                                case Type.Checkbox:
                                    _fieldToAdd.x = _selectionRect.X - _xOffset;
                                    _fieldToAdd.y = _selectionRect.Y - _yOffset;

                                    break;

                                case Type.Label: //Nothing for MouseUp concerning the Label
                                    break;
                               
                                case Type.RichLabel:
                                    _fieldToAdd.x = _selectionRect.X - _xOffset;
                                    _fieldToAdd.y = _selectionRect.Y - _yOffset;
                                    break;

                                case Type.RectangleDraw:
                                    _fieldToAdd.x = _selectionRect.X - _xOffset;
                                    _fieldToAdd.y = _selectionRect.Y - _yOffset;
                                    break;

                                case Type.LineDraw:
                                    _fieldToAdd.x = _selectionRect.X - _xOffset;
                                    _fieldToAdd.y = _selectionRect.Y - _yOffset;
                                    break;
                            }
                            if (_fieldToAdd.type != Type.Label) {
                                _fieldToAdd.width = _selectionRect.Width;
                                _fieldToAdd.height = _selectionRect.Height; 
                            }

                            needToSave();

                            if (_fieldToAdd.type != Type.Label) {   //We don't add a field during MouseUp for labels since that is handled during MouseDown.
                                _currentForm.page(_currentPageNumber).Fields.Add(_fieldToAdd);
                            }
                            refreshHierarchyView();
                            refreshProperties(_fieldToAdd);

                            disableOtherAdders(new CheckBox());
                            _fieldToAdd = null;
                        }


                        _globalMode.setMode(MouseMode.None);
                        break;
                }

                if (e.Button == MouseButtons.Right) {
                    cntxtFieldControls.Show(MousePosition.X, MousePosition.Y);
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
                    if (fi.x < sfBoxPosition.X) {                              //...value of the X position...
                        sfBoxPosition.X = fi.x - 6;
                    }
                    if (fi.y < sfBoxPosition.Y) {                              //...as well as the lowest value for the Y position.
                        sfBoxPosition.Y = fi.y - 6;
                    }
                }
            }
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Figuring out what size to make the sfBox.
                if (fi.selected) {
                    if (fi.x + fi.width - sfBoxPosition.X > sfBoxSize.Width) {    //Same principle, except to get size we add width to x position to get the rightmost...
                        sfBoxSize.Width = fi.x + 6 + fi.width - sfBoxPosition.X;  //...position of the selected fields...
                    }
                    if (fi.y + fi.height - sfBoxPosition.Y > sfBoxSize.Height) {
                        sfBoxSize.Height = fi.y + 6 + fi.height - sfBoxPosition.Y;//...as well as the bottom most position for height.
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
                    _zoomLevel += 0.25f;
                }
            }

            calculateLabelSizes();          //Some visual cleanup
            calculateSfBox();
            designPanel.Invalidate();
        }

        private void zoomOut() {
            if (_shouldZoom) {
                if (_zoomLevel > 0.26) {//0.26 is the lower bounds.
                    _zoomLevel -= 0.25f;
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
                _shouldZoom = true;
            }
        }

        private void designPanel_MouseLeave(object sender, EventArgs e) {
            _shouldZoom = false;
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
            deselectAllFields();              //Nothing should be selected, this is ran to be double sure.
            refreshHierarchyView();            
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

        /// <summary>
        /// The Designer's pasting function. Pastes all the fields that have been cut or copied.
        /// </summary>
        private void paste() {                                                      
            deselectAllFields();//We run this here to prevent an issue with what's being copied remaining selected.

            foreach (Field fi in _fieldsToCopy) {                                   
                //fi.x = fi.x + 15; //TODO - Added pasting in different positions depensing on if it's a right click and paste or a CTRL-V paste.
                fi.y = fi.y + 18;          //Offsetting so the pasted object(s) sits below the copied object(s).
                fi.selected = true;                 //Selecting them before they are finally pasted. So that they will be when they are added. This helps with possible overlap issues, etc.
                _currentForm.page(_currentPageNumber).Fields.Add(returnCopy(fi));   //Using returnCopy to be able to make position, selected, and zoomlevel changes before the fields are pasted...
                                                                                    //...It also works to prevent continuosly repasting the same fields over and over, effectively just shimmying them down repeatedly.
            }

            _globalMode.setMode(MouseMode.Selected);
            needToSave();
            refreshHierarchyView();
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
            //tempField.formatSetName = fi.formatSet.name;                                    //formatSet copying is done in two stages, first is the setting of the tempField's formatSet's name as...
            //if (tempField.formatSetName != null) {          //make sure formatSetName for the tempField isn't null, to prevent a null access bug with the _settings call within this if statement.
            //    tempField.formatSet = _settings.getFormatSetByName(tempField.formatSetName);//...the field to copies formatSet's name. Then back again through the settings singleton. //TODO - There's probably a better way to do this.
            //}    
            tempField.fontTypeface = fi.fontTypeface;
            tempField.fontSize = fi.fontSize;
            tempField.fontStyle = fi.fontStyle;

            tempField.text = fi.text;
            tempField.selected = fi.selected;
            needToSave();
            return tempField;
            
        }

        #endregion Cut, Copy, and Paste.


        #region Field Deletion

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            deleteFields();
        }

        private void deleteFields() {
            if (_shouldDelete) {
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
                deselectAllFields();
                refreshHierarchyView();
            }
        }

        #endregion Field Deletion


        #region Field Addition

        /// <summary>
        /// This method is used to deactivate all but the CheckBox that is passed to it. New Adding checkboxes 
        /// will need to be implemented as we go.
        /// </summary>
        /// <param name="me">The CheckBox to remain checked.</param>
        private void disableOtherAdders(CheckBox me) {
            if (me != chkAddCheckbox) chkAddCheckbox.Checked = false;
            if (me != chkAddLabel) chkAddLabel.Checked = false;
            if (me != chkAddLine) chkAddLine.Checked = false;
            if (me != chkAddRectangle) chkAddRectangle.Checked = false;
            if (me != chkAddWritten) chkAddWritten.Checked = false;
            if (me != chkAddOptionGroup) chkAddOptionGroup.Checked = false;
        }

        //These methods are attached to the adder checkboxes click event. They each set things up 
        //for their respective fields to be added to the page.
        private void addWritten(object sender, EventArgs e) {
            disableOtherAdders((CheckBox)sender);
            deselectAllFields();

            _globalMode.setMode(MouseMode.Adding);
            if(!chkAddWritten.Checked) _globalMode.setMode(MouseMode.None);

            string fieldName = "";

            /*using (var form = new fieldSelection(_currentForm.formTemplates, "Text")) {//FT1C
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    string val = form.name;
                    fieldName = val;
                }
            //}*/
            
            _fieldToAdd = new Field(fieldName, Type.TextField);
        }

        private void addCheckbox(object sender, EventArgs e) {
            disableOtherAdders((CheckBox)sender);
            deselectAllFields();

            _globalMode.setMode(MouseMode.Adding);
            if(!chkAddCheckbox.Checked) _globalMode.setMode(MouseMode.None);

            string fieldName = "";
            _fieldToAdd = new Field(fieldName, Type.Checkbox);
            _fieldToAdd.x = 10;
            _fieldToAdd.y = 10;
        }

        private void addLabel(object sender, EventArgs e) {
            disableOtherAdders((CheckBox)sender);
            deselectAllFields();

            _globalMode.setMode(MouseMode.Adding);
            if(!chkAddLabel.Checked) _globalMode.setMode(MouseMode.None);

            _fieldToAdd = new Field("Label", Type.Label);
            _fieldToAdd.text = "Placeholder Text";                 //To give it text, and therefore dimensions in the display.

        }

        private void addRectangle(object sender, EventArgs e) {
            disableOtherAdders((CheckBox)sender);
            deselectAllFields();

            _globalMode.setMode(MouseMode.Adding);
            if(!chkAddRectangle.Checked) _globalMode.setMode(MouseMode.None);

            _fieldToAdd = new Field("Rectangle", Type.RectangleDraw);
        }

        private void addLine(object sender, EventArgs e) {
            disableOtherAdders((CheckBox)sender);
            deselectAllFields();

            _globalMode.setMode(MouseMode.Adding);
            if(!chkAddLine.Checked) _globalMode.setMode(MouseMode.None);

            _fieldToAdd = new Field("Line", Type.LineDraw);
        }

        private void addOptionGroup(object sender, EventArgs e) {
            disableOtherAdders((CheckBox)sender);
            deselectAllFields();

            _globalMode.setMode(MouseMode.Adding);
            if(!chkAddOptionGroup.Checked) _globalMode.setMode(MouseMode.None);

            _fieldToAdd = new Field("Line", Type.OptionGroup);
        }

        //Not using now
        private void addRichLabel(object sender, EventArgs e) {

            deselectAllFields();

            _globalMode.setMode(MouseMode.Adding);
            _fieldToAdd = new Field("Rich Label", Type.RichLabel);

        }
        #endregion Field Addition


        #region Global Control Functions

        private void calculateLabelSizes() { //TODO - Refactor
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                if (fi.type == Type.Label) {
                    fi.width = TextRenderer.MeasureText(fi.text, fi.font()).Width;
                    fi.height = TextRenderer.MeasureText(fi.text, fi.font()).Height;
                }
                if (fi.type == Type.Checkbox) {
                    fi.exWidth = TextRenderer.MeasureText(fi.text, fi.font()).Width;
                    fi.exHeight = TextRenderer.MeasureText(fi.text, fi.font()).Height;
                }
                if (fi.type == Type.OptionGroup) {
                    foreach(var sub in fi.items) {
                        sub.Value.width = TextRenderer.MeasureText(sub.Value.text, Font).Width;
                        sub.Value.height = TextRenderer.MeasureText(sub.Value.text, Font).Height;
                    }
                }

            }
            calculateSfBox();
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

        private void deselectAllFields() {
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                fi.selected = false;
            }

            _topCrossPoint = new Point();
            _bottomCrossPoint = new Point();
            _leftCrossPoint = new Point();
            _rightCrossPoint = new Point();
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

            //TODO - Page zoom level, offset position changing

            deselectAllFields();
            refreshHierarchyView();
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

            //TODO - Zoom and offset per page
            deselectAllFields();
            refreshHierarchyView();
            designPanel.Invalidate();
        }

        #endregion Page Navigation/Switching


        #region New Page

        private void btnNewPage_Click(object sender, EventArgs e) {
            btnNextPage.Enabled = true;
            _currentForm.addNewBlankPage();
            needToSave();
            designPanel.Invalidate();
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
            calculateLabelSizes();
            
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
            _currentForm.exportPNG();

            _currentForm.saveForm();

            lblVersionNumber.Text = _currentForm.versionNumber.ToString();
        }

        #endregion Export Form Button


        #region Settings Screen Button
        private void btnLoadSettingsScreen_Click(object sender, EventArgs e) {
            if (_settings.screen.Visible) {
                if (_settings.screen.InvokeRequired) {
                    _settings.screen.Invoke(new MethodInvoker(_settings.screen.Activate));
                } else {
                    _settings.screen.BringToFront();
                }
            } else {
                _settings.screen.ShowDialog();
            }
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

        private void refreshProperties(Field fi) {

            propertyGrid.SelectedObject = fi;

        }


        #endregion Field Properties



        #region Keyboard Bindings

        #region Universal Methods

        public void ProcessCmdKeyPassthrough(ref Message msg, Keys keyData) {
            ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {//Steals the KeyEvents away from everything, for better control over what happens during key presses.
            //stop Editing Text
            if (keyData == Keys.Enter) {
                if (_globalMode.mode() == MouseMode.TextEditing) {
                    if (_activeEditField.type == Type.Checkbox ||
                        _activeEditField.type == Type.Label) {
                            stopEditing();
                            return true;
                    }
                }
            }   
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

            if (keyData == Keys.Space) {
                if (designPanel.Focused || _mFontMenuContainer.Visible) {
                    _mFontMenuContainer.Close();
                    OnKeyPress(new KeyPressEventArgs((Char)Keys.Space));
                    return true;
                }
                if (_activeTextEditBox.Focused) {
                    return base.ProcessCmdKey(ref msg, keyData);//If the _labelEditBox is focused we let the Spacebar act as it normally would.
                }
            }

            if (keyData == Keys.Delete) {//Doing here as well in case we came from
                _mFontMenuContainer.Close();
                deleteFields();
                return true;
            }

            //Form Control Keybindings
            //if (keyData == Keys.ControlKey) OnKeyPress(new KeyPressEventArgs((Char)Keys.ControlKey));
            //if (keyData == Keys.C) OnKeyPress(new KeyPressEventArgs((Char)Keys.C));
            //if (keyData == Keys.X) OnKeyPress(new KeyPressEventArgs((Char)Keys.X));
            //if (keyData == Keys.V) OnKeyPress(new KeyPressEventArgs((Char)Keys.V));

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e) {
            if (_shouldMove) {
                if (e.KeyChar == (Char) Keys.Up) moveFieldsUp();
                if (e.KeyChar == (Char) Keys.Down) moveFieldsDown();
                if (e.KeyChar == (Char) Keys.Left) moveFieldsLeft();
                if (e.KeyChar == (Char) Keys.Right) moveFieldsRight();
            }
            if (e.KeyChar == (Char)Keys.Space && !_globalMode.TextEditing) { startEditing(); return; }//Only enters if TextEditing is false
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e) {
            if (_globalMode.TextEditing) return;

            if (e.KeyCode == Keys.Delete) { deleteFields(); return; }

            if (e.Control && e.KeyCode == Keys.X) { cut(); return; }
            if (e.Control && e.KeyCode == Keys.C) { copy(); return; }
            if (e.Control && e.KeyCode == Keys.V) { paste(); return; }
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

        /// <summary>
        /// Refreshes the fields listed in the field hierarchy tree view.
        /// </summary>
        private void refreshHierarchyView() {//TODO - Give this a good Refactor
            trvFieldList.Nodes.Clear();
            int c = 0;
            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {
                fi.listIndex = c;
                TreeNode workingNode;
                if (fi.type == Type.Checkbox || fi.type == Type.TextField) {
                    if (fi.name == "") {
                        workingNode = new TreeNode("blank - " + fi.type);
                    } else {
                        workingNode = new TreeNode(fi.name + " - " + fi.type);
                    }
                } else {
                    workingNode = new TreeNode(fi.name);
                }
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
                refreshProperties(null);
            } else { //CTRL isn't being held down.
                deselectAllFields();
                _currentForm.page(_currentPageNumber).Fields[e.Node.Index].selected = true; //Selects the one field 
                refreshProperties(_currentForm.page(_currentPageNumber).Fields[e.Node.Index]);
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



        #region Editing Labels

        void activeRichTextEditBox_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                //TODO - handle rich text SelectedText Font changing
                
            }
        }

        /// <summary>
        /// The Designer's edit function. Takes the field or fields selected and places a copy in memory to later paste.
        /// </summary>
        private void startEditing() {
            _shouldZoom = false;//Toggling zoom off while editing
            _shouldDelete = false;
            _shouldMove = false;

            _activeTextEditBox.ZoomFactor = _zoomLevel;
            _activeTextEditBox.ContentsResized += activeTextEditBox_ContentsResized;


            foreach (Field fi in _currentForm.page(_currentPageNumber).Fields) {    //Iterate through the fields on the current page...
                if (fi.selected) { 
                    if (fi.type == Type.Label) {
                        _globalMode.setMode(MouseMode.TextEditing);
                        _activeEditField = fi;

                        _activeTextEditBox.Multiline = false;

                        _activeTextEditBox.Font = fi.font();
                        _activeTextEditBox.Text = fi.text;
                        _activeTextEditBox.Select(_activeTextEditBox.TextLength, 0);
                        _activeTextEditBox.Location = new Point((int)((fi.x + _xOffset) * _zoomLevel) + 1, (int)((fi.y + _yOffset) * _zoomLevel));
                        _activeTextEditBox.Size = new Size((int)(fi.width * _zoomLevel) + 1, (int)(fi.height * _zoomLevel) + 1);


                        _activeTextEditBox.Show();
                        _activeTextEditBox.Focus();

                        
                        break;//Stopping the loop from checking past the first label or RichLabel.

                    }

                    if (fi.type == Type.RichLabel) {
                        _globalMode.setMode(MouseMode.TextEditing);
                        _activeEditField = fi;
                        
                        _activeTextEditBox.Multiline = true;
                    
                        _activeTextEditBox.Rtf = fi.richBox.Rtf;
                        _activeTextEditBox.Select(_activeTextEditBox.TextLength, 0);
                        _activeTextEditBox.Location = new Point((int)((fi.x + _xOffset) * _zoomLevel) + 1, (int)((fi.y + _yOffset) * _zoomLevel));
                        _activeTextEditBox.Size = new Size((int)(fi.width * _zoomLevel) + 1, (int)(fi.height * _zoomLevel) + 1);


                        _activeTextEditBox.Show();
                        _activeTextEditBox.Focus();

                        break;
                    }

                    if (fi.type == Type.Checkbox) {
                        _globalMode.setMode(MouseMode.TextEditing);
                        _activeEditField = fi;

                        _activeTextEditBox.Multiline = false;

                        _activeTextEditBox.Font = fi.font();
                        _activeTextEditBox.Text = fi.text;
                        _activeTextEditBox.Select(_activeTextEditBox.TextLength, 0);
                        _activeTextEditBox.Location = new Point(//Checkbox locations are a bit special to adjust for the checkbox being bigger than the text.
                                                            (int)((fi.x + _xOffset + fi.width) * _zoomLevel) + 2
                                                            , (int)((fi.y + _yOffset + (((fi.height - fi.font().Size) / 2)) - 4) * _zoomLevel)
                                                            );
                        
                        _activeTextEditBox.Size = new Size((int)(fi.exWidth * _zoomLevel) + 1, (int)(fi.exHeight * _zoomLevel) + 1);

                        _activeTextEditBox.Show();
                        _activeTextEditBox.Focus();

                        break;

                    }
                }
            }

            designPanel.Invalidate();
        }

        private void activeTextEditBox_ContentsResized(object sender, ContentsResizedEventArgs e) {
            _activeTextEditBox.Height = e.NewRectangle.Height + 5;
            _activeTextEditBox.Width = e.NewRectangle.Width + 5;

        }

        public void stopEditing() {
            if (_activeEditField.type == Type.Label || _activeEditField.type == Type.Checkbox) {
                _activeEditField.text = _activeTextEditBox.Text;
            }

                if (_activeEditField.type == Type.RichLabel) {
                RichTextBox tempBox = new RichTextBox();

                tempBox.Rtf = _activeTextEditBox.Rtf;
                tempBox.Location = _activeTextEditBox.Location;
                tempBox.Size = _activeTextEditBox.Size;

                _activeEditField.richBox = tempBox;
            
            }

            _activeTextEditBox.Hide();
            refreshProperties(_activeEditField);//Refreshing the properties to reflect the changes to the field's text.
            _activeEditField = null;

            needToSave();
            _shouldZoom = true;//Toggling zoom back on
            _shouldDelete = true;
            _shouldMove = true;
            designPanel.Focus();    //Focusing back onto the designLabel, evidently the click that got us here did not do that already.

            _globalMode.setMode(MouseMode.Selected);
            
            calculateLabelSizes();
        }
        #endregion Editing Labels

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

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            needToSave();
            refreshHierarchyView();
            calculateLabelSizes();
            designPanel.Invalidate();
        }

        private void btnGenerateTemplates_Click(object sender, EventArgs e) {

            using (var form = new generateTemplates(_currentForm.generatingTemplates)) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    List<string> val = form.templates;
                    _currentForm.generatingTemplates = val;
                }
            }

            //generateTemplates testg = new generateTemplates();
            //testg.ShowDialog();
        }
    }

        public class Mode {
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

}