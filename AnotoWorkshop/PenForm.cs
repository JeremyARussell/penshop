using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AnotoWorkshop {
    public class PenForm {

        #region Variables
        //Public Variables
        public string FormName;
        public string thisFormsPath; //Todo - Internal Filepath for forms

        private List<FormPage> _formPages = new List<FormPage>();//TODO - Undid my privitizing of FormPages - it made it to where I couldn't access the functions in the class. Need to create public interface to this section when I get home.
        private int _formVersion;
        private int _totalPages;

        private readonly XmlDocument _dom = new XmlDocument();
        private int _counter;
        private int _importFormatSetTicker;

        private Settings _settings;

        #endregion Variables

        #region Initializers

        public PenForm(string file, Dictionary<int, FormatSet> workingFormatSets) {
            _importFormatSetTicker = workingFormatSets.Count;
            try {
                _dom.Load(file);
                importForm(_dom.DocumentElement, workingFormatSets);
            } catch (XmlException xmlEx) {
                MessageBox.Show(xmlEx.Message);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public PenForm(string file, Dictionary<int, FormatSet> workingFormatSets, string newName) {
            if (_settings == null) {
                _settings = Settings.instance;
            }
            _importFormatSetTicker = workingFormatSets.Count;
            try {
                _dom.Load(file);
                importForm(_dom.DocumentElement, workingFormatSets);
                FormName = newName;
            } catch (XmlException xmlEx) {
                MessageBox.Show(xmlEx.Message);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public PenForm(string file) {
            if (_settings == null) {
                _settings = Settings.instance;
            }
            try {
                _dom.Load(file);
                loadForm(_dom.DocumentElement);
            } catch (XmlException xmlEx) {
                MessageBox.Show(xmlEx.Message);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public PenForm() {
            //For making new blank form later.
        }

        public List<FormPage> formPages {
            get { return _formPages; }
            set { _formPages = value; }
        }

        #endregion Initializers

        #region Form Saving - Almost Done

        public void saveForm() {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
            using (XmlWriter writer = XmlWriter.Create(_settings.formsFolderLocation + FormName + ".penform", settings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("PenForm");
                writer.WriteAttributeString("name", FormName);
                writer.WriteAttributeString("version", _formVersion.ToString());

                foreach (FormPage page in formPages) {
                    writer.WriteStartElement("Page");
                    writer.WriteAttributeString("pageNumber", page.PageNumber.ToString());

                    foreach (Field fi in page.Fields) {
                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("name", fi.name);
                        writer.WriteAttributeString("x", fi.x.ToString());
                        writer.WriteAttributeString("y", fi.y.ToString());
                        writer.WriteAttributeString("width", fi.width.ToString());
                        writer.WriteAttributeString("height", fi.height.ToString());
                        writer.WriteAttributeString("type", fi.type.ToString());
                        writer.WriteAttributeString("text", fi.text);
                        writer.WriteAttributeString("formatSetName", fi.formatSet.name);
                        //writer.WriteAttributeString("hidden", fi.hidden);
                        //writer.WriteAttributeString("name", fi.group);
                        //writer.WriteAttributeString("name", fi.readOnly);
                        //writer.WriteAttributeString("name", fi.textTypes);
                        //writer.WriteAttributeString("name", fi.texts);//For Fancy Labels

                        writer.WriteEndElement();//Field
                    }

                    writer.WriteEndElement();//Page
                }

                writer.WriteEndElement();//PenForm
                writer.WriteEndDocument();
            }
        }

        #endregion Form Saving - Almost Done

        #region Form Loading - Kinda Done

        private void loadForm(XmlNode form) {
            XmlNode[] pages = new XmlNode[16];//Spot for 16 pages
            if (form.Attributes != null) {
                FormName = form.Attributes["name"].Value;
                _formVersion = Convert.ToInt32(form.Attributes["version"].Value);
            }
            foreach (XmlNode xn1 in form) {//Extract pages
                if (xn1.Name == @"Page") {
                    pages[_counter] = xn1;
                    _counter++;
                }
            }

            for (int i = 0; i < _counter; i++) {//Process the pages
                XmlNodeList selectedPage = pages[i].ChildNodes;
                FormPage workingPage = new FormPage(i);
                foreach (XmlNode fieldNode in selectedPage) {
                    workingPage.addField(loadNode(fieldNode));
                }
                addPage(workingPage);
            }
        }

        private Field loadNode(XmlNode field) {
            try {
                Field workingField = new Field();

                XmlReader reader = XmlReader.Create(new StringReader(field.OuterXml));

                while (reader.Read()) {
                    if (reader.Name == "Field") {
                        if (!reader.IsStartElement()) break;
                        workingField.name = reader["name"];
                        workingField.x = Convert.ToInt32(reader["x"]);
                        workingField.y = Convert.ToInt32(reader["y"]);
                        workingField.width = Convert.ToInt32(reader["width"]);
                        workingField.height = Convert.ToInt32(reader["height"]);

                        Enum.TryParse(reader["type"], out workingField.type);

                        workingField.text = reader["text"];
                        workingField.name = reader["name"];
                        workingField.formatSetName = reader["formatSetName"];

                        try {
                            workingField.formatSet =
                                _settings.getFormatSetByName(workingField.formatSetName);
                        } catch (Exception) {
                        }
                    }
                }//End loop

                return workingField;
            } catch (Exception e) {
                throw e;
            }
        }

        #endregion Form Loading - Kinda Done

        #region Form Importing

        private void importForm(XmlNode form, Dictionary<int, FormatSet> workingFormatSets)//Franklin
        {
            XmlNode[] pages = new XmlNode[16];//Spot for 16 pages

            foreach (XmlNode xn1 in form) {//Extract pages
                if (xn1.Name == @"template") {
                    XmlNode templateNode = xn1;
                    foreach (XmlNode xn2 in templateNode) {
                        if (xn2.Name == @"subform") {
                            if (xn2.Attributes != null && xn2.Attributes["name"] != null) {
                                FormName = xn2.Attributes["name"].InnerText;
                            }
                            XmlNode primarySubformNode = xn2;
                            foreach (XmlNode xn3 in primarySubformNode) {
                                if (xn3.Name == @"subform") {
                                    pages[_counter] = xn3;
                                    _counter++;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < _counter; i++) {//Process the pages
                XmlNodeList selectedPage = pages[i].ChildNodes;
                FormPage workingPage = new FormPage(i);
                foreach (XmlNode fieldNode in selectedPage) {
                    workingPage.addField(processNode(fieldNode, workingFormatSets));
                }

                addPage(workingPage);
            }
        }

        private Field processNode(XmlNode field, Dictionary<int, FormatSet> workingFormatSets) {
            try {
                FormatSet workingFormatSet = new FormatSet();
                Field workingField = new Field();
                int fieldFlag = 0;

                XmlReader reader = XmlReader.Create(new StringReader(field.OuterXml));

                while (reader.Read()) {
                    if (reader.Name == "templateDesigner") break;

                    if (reader.Name == "field" || reader.Name == "draw") {
                        if (reader.Name == "field") fieldFlag = 1;
                        if (reader.Name == "draw") fieldFlag = 2;

                        if (!reader.IsStartElement()) break;

                        workingField.name = reader["name"];

                        if (reader["presence"] != "invisible") {
                            workingField.zx = convertUM(reader["x"]);
                            workingField.zy = convertUM(reader["y"]);
                        } else {
                            workingField.hidden = true;
                        }

                        if (reader["access"] != null) {//Figuring out access level, basically if this textField is for a variable or written input(handriwiting recognition)
                            workingField.readOnly = reader["access"] == "readOnly";
                        }

                        if (reader["w"] == null) {
                            workingField.zwidth = convertUM(reader["minW"]);
                            workingField.zheight = convertUM(reader["minH"]);
                        } else {
                            workingField.zwidth = convertUM(reader["w"]);//Width
                            workingField.zheight = convertUM(reader["h"]);//Height
                        }
                    }

                    if (reader.Name == "ui") {
                        workingField.type = processUiNode(reader.ReadOuterXml(), fieldFlag);
                    }

                    if (reader.Name == "text") {
                        if (workingField.type == Type.Label) {
                            workingField.text = reader.ReadInnerXml();
                        }
                    }

                    if (reader.Name == "exData") {
                        workingField.type = Type.FancyLabel;
                        workingField.text = "Fancy Field Testing Text";//TODO - Finish this loading junk, text coupled with formatting.
                    }

                    if (reader.Name == "rectangle") {
                        workingField.type = Type.RectangleDraw;
                    }

                    if (reader.Name == "line") {
                        workingField.type = Type.LineDraw;
                        if (workingField.height == 0) workingField.height = 2;
                        if (workingField.width == 0) workingField.width = 2;
                    }

                    if (reader.Name == "font") {//pulling font information into field - TODO - imp this sucka
                        if (reader.IsStartElement()) {
                            if (reader["size"] != null) {
                                workingField.formatSet.fontSizeString = reader["size"];//TODO - imp
                            }
                            if (workingField.formatSet.fontSize == 10) break;
                            workingField.formatSet.fontTypeface = reader["typeface"];//TODO - Build a texttype for importing
                            if (reader["weight"] != null && reader["weight"] != "") {
                                workingField.formatSet.fontWeight = reader["weight"];//TODO - imp
                            } else {
                                workingField.formatSet.fontWeight = "normal";//TODO - imp
                            }
                        }
                    }
                }//End loop

                workingFormatSet = workingField.formatSet;

                if (!checkForDuplicateInDictionary(workingFormatSets, workingFormatSet)) {
                    workingFormatSet = workingField.formatSet;
                    workingFormatSets.Add(_importFormatSetTicker, workingFormatSet);
                    _importFormatSetTicker++;
                }

                return workingField;
            } catch (Exception e) {
                throw e;
            }
        }

        private bool checkForDuplicateInDictionary(Dictionary<int, FormatSet> toCheckAgainst, FormatSet setToCheck) {
            bool isaDup = false;

            for (int i = 0; i < toCheckAgainst.Count; i++) {
                if (setToCheck.fontSize == toCheckAgainst[i].fontSize &&
                    setToCheck.fontTypeface == toCheckAgainst[i].fontTypeface &&
                    setToCheck.fontWeight == toCheckAgainst[i].fontWeight// &&
                    //setToCheck.fontSize == toCheckAgainst[i].fontSize &&
                    //setToCheck.fontSize == toCheckAgainst[i].fontSize &&
                    //setToCheck.fontSize == toCheckAgainst[i].fontSize &&
                    //setToCheck.fontSize == toCheckAgainst[i].fontSize
                    ) {
                    setToCheck.name = toCheckAgainst[i].name;
                    isaDup = true;
                    return isaDup;
                }
            }
            return isaDup;
        }

        //private  List<List<string>, List<TextType>> processExDataNode(string ui)

        private static int convertUM(string measurementString) {
            try {
                double conversionUnit = 0;
                string placeholder = measurementString;

                if (measurementString.IndexOf("in") > 0) {
                    conversionUnit = 71.628;
                    placeholder = placeholder.Substring(0, placeholder.IndexOf("in"));
                }
                if (measurementString.IndexOf("mm") > 0) {
                    conversionUnit = 2.82;
                    placeholder = placeholder.Substring(0, placeholder.IndexOf("mm"));
                }

                int retInt = Convert.ToInt32(Convert.ToDouble(placeholder) * conversionUnit);// * 1.25);//TODO - 1.25 needs to be DPI

                return retInt;
            } catch (Exception e) {
                throw e;
            }
        }

        private static Type processUiNode(string ui, int flag) {
            XmlReader reader = XmlReader.Create(new StringReader(ui));

            while (reader.Read()) {
                Type fieldType;
                if (flag == 1) {
                    if (reader.Name == "textEdit") {
                        fieldType = Type.TextField;
                        return fieldType;
                    }
                    if (reader.Name == "checkButton") {
                        fieldType = Type.Checkbox;
                        return fieldType;
                    }
                }
                if (flag == 2) {
                    if (reader.Name == "textEdit") {
                        fieldType = Type.Label;
                        return fieldType;
                    }
                }
            }
            return Type.None;
        }

        #endregion Form Importing

        #region Class Functions

        public void addPage(FormPage page) {
            formPages.Add(page);
            _totalPages++;
        }

        //Todo - remove page
        //Will need to go through the higher numbered  pages and lower their pagenumber by one.
        //foreach formspages, if PageNumber > then removed page.pagenumber.

        public FormPage page(int pagenum) {
            return formPages[pagenum];
        }

        public void addNewBlankPage() {
            FormPage workingPage = new FormPage(_totalPages + 1);
            addPage(workingPage);
        }

        public int totalPages() {
            return _totalPages;
        }

        #endregion Class Functions

        #region XDP Exporting

        private string convertToMm(int number) {
            string retStr;
            string tempString = (number / 2.82).ToString();
            if (tempString.Length > 3) {
                retStr = tempString.Substring(0, 6) + "mm";
            } else {
                retStr = tempString + "mm";
            }

            return retStr;
        }

        public void exportXDP() {//.xdp export
            _formVersion++;

            saveForm();

            string versionString = _formVersion.ToString("D7");

            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };

            using (XmlWriter writer = XmlWriter.Create(FormName + "." + versionString + ".xdp", settings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("xdp");

                writer.WriteStartElement("template");

                writer.WriteStartElement("subform");
                writer.WriteAttributeString("name", "TESTING");//TODO - Use formname

                foreach (FormPage page in formPages) {
                    writer.WriteStartElement("subform");

                    foreach (Field fi in page.Fields) {
                        switch (fi.type) {

                            #region TextField - Mostly Done, double check before ".xdp export" is complete

                            case Type.TextField:
                                writer.WriteStartElement("field");
                                writer.WriteAttributeString("name", fi.name);
                                if (fi.readOnly) writer.WriteAttributeString("access", "readOnly");
                                writer.WriteAttributeString("x", convertToMm(fi.x));
                                writer.WriteAttributeString("y", convertToMm(fi.y));
                                writer.WriteAttributeString("w", convertToMm(fi.width));
                                writer.WriteAttributeString("h", convertToMm(fi.height));
                                writer.WriteStartElement("ui");
                                writer.WriteStartElement("textEdit");
                                writer.WriteStartElement("border");
                                if (fi.hidden || fi.readOnly) {//TODO - Later, sometimes when fi is readonly we still have some drawn border,
                                    //Todo - like an underline, need to fix this up at some point. Edge settings will need to be in the TextType class.
                                    //Todo - There is a difference between a hidden border, and an invisible field, I have them combined right now (they need to not be).
                                    writer.WriteAttributeString("presence", "hidden");
                                    writer.WriteEndElement();//border
                                } else {
                                    writer.WriteStartElement("edge");
                                    writer.WriteAttributeString("presence", "hidden");
                                    writer.WriteEndElement();//edge
                                    writer.WriteStartElement("edge");
                                    writer.WriteAttributeString("presence", "hidden");
                                    writer.WriteEndElement();//edge
                                    writer.WriteStartElement("edge");
                                    writer.WriteStartElement("color");
                                    writer.WriteAttributeString("value", "51,102,255");
                                    writer.WriteEndElement();//color
                                    writer.WriteEndElement();//edge
                                    writer.WriteStartElement("edge");
                                    writer.WriteAttributeString("presence", "hidden");
                                    writer.WriteEndElement();//edge
                                    writer.WriteStartElement("corner");
                                    writer.WriteEndElement();//corner
                                    writer.WriteStartElement("corner");
                                    writer.WriteEndElement();//corner
                                    writer.WriteStartElement("corner");
                                    writer.WriteStartElement("color");
                                    writer.WriteAttributeString("value", "51,102,255");
                                    writer.WriteEndElement();//color
                                    writer.WriteEndElement();//corner
                                    writer.WriteStartElement("corner");
                                    writer.WriteEndElement();//corner
                                    writer.WriteEndElement();//border
                                }
                                writer.WriteStartElement("margin");
                                writer.WriteEndElement();//margin
                                writer.WriteEndElement();//textEdit
                                writer.WriteEndElement();//ui

                                writer.WriteStartElement("font");
                                if (fi.formatSet.fontTypeface != null || fi.formatSet.fontTypeface != "") {
                                    writer.WriteAttributeString("typeface", fi.formatSet.fontTypeface);
                                }
                                if (fi.formatSet.fontSize > 0) {
                                    writer.WriteAttributeString("size", fi.formatSet.fontSizeString);
                                }
                                if (fi.formatSet.fontWeight != "normal" || fi.formatSet.fontWeight != null) {
                                    writer.WriteAttributeString("weight", fi.formatSet.fontWeight);
                                }
                                //fill color goes here, we aren't doing that yet though.
                                writer.WriteEndElement();//font

                                writer.WriteStartElement("margin");
                                writer.WriteAttributeString("bottomInset", "0mm");
                                writer.WriteAttributeString("leftInset", "0mm");
                                writer.WriteAttributeString("rightInset", "0mm");
                                writer.WriteAttributeString("topInset", "0mm");
                                writer.WriteEndElement();//margin

                                if (fi.readOnly) {
                                    writer.WriteStartElement("para");
                                    writer.WriteAttributeString("vAlign", "middle");
                                    writer.WriteEndElement();//para

                                    writer.WriteStartElement("calculate");
                                    writer.WriteAttributeString("override", "error");
                                    writer.WriteEndElement();//calculate
                                }
                                writer.WriteEndElement();//field
                                break;

                            #endregion TextField - Mostly Done, double check before ".xdp export" is complete

                            #region Checkbox - Mostly Done, double check before ".xdp export" is complete

                            case Type.Checkbox:
                                writer.WriteStartElement("field");
                                writer.WriteAttributeString("name", fi.name);
                                writer.WriteAttributeString("x", convertToMm(fi.x));
                                writer.WriteAttributeString("y", convertToMm(fi.y));
                                writer.WriteAttributeString("w", convertToMm(fi.width));
                                writer.WriteAttributeString("h", convertToMm(fi.height));
                                writer.WriteStartElement("ui");
                                writer.WriteStartElement("checkButton");
                                writer.WriteStartElement("border");
                                writer.WriteStartElement("edge");
                                writer.WriteStartElement("color");
                                writer.WriteAttributeString("value", "51,102,255");
                                writer.WriteEndElement();//color
                                writer.WriteEndElement();//edge
                                writer.WriteStartElement("corner");
                                writer.WriteStartElement("color");
                                writer.WriteAttributeString("value", "51,102,255");
                                writer.WriteEndElement();//color
                                writer.WriteEndElement();//corner
                                writer.WriteEndElement();//border
                                writer.WriteEndElement();//checkButton
                                writer.WriteEndElement();//ui
                                //Fonts maybe later
                                //Margin later
                                //Para later
                                writer.WriteStartElement("value");
                                writer.WriteStartElement("integer");
                                writer.WriteString("0");
                                writer.WriteEndElement();//integer
                                writer.WriteEndElement();//value
                                //Caption later
                                writer.WriteStartElement("items");
                                writer.WriteStartElement("integer");
                                writer.WriteString("1");
                                writer.WriteEndElement();//integer
                                writer.WriteStartElement("integer");
                                writer.WriteString("0");
                                writer.WriteEndElement();//integer
                                writer.WriteStartElement("integer");
                                writer.WriteString("2");
                                writer.WriteEndElement();//integer
                                writer.WriteEndElement();//items
                                writer.WriteEndElement();//end field
                                break;

                            #endregion Checkbox - Mostly Done, double check before ".xdp export" is complete

                            #region Options Group - Not Started

                            case Type.OptionsGroup:
                                //writer.WriteStartElement("field");
                                //writer.WriteEndElement();//end field
                                break;

                            #endregion Options Group - Not Started

                            #region Fancy Label - Not Started

                            case Type.FancyLabel:
                                //writer.WriteStartElement("field");
                                //writer.WriteEndElement();//end field
                                break;

                            #endregion Fancy Label - Not Started

                            #region Label - Mostly Done, double check before ".xdp export" is complete

                            case Type.Label:
                                writer.WriteStartElement("draw");
                                writer.WriteAttributeString("name", fi.name);
                                writer.WriteAttributeString("x", convertToMm(fi.x));
                                writer.WriteAttributeString("y", convertToMm(fi.y));
                                writer.WriteAttributeString("w", convertToMm(fi.width));
                                writer.WriteAttributeString("h", convertToMm(fi.height));
                                writer.WriteStartElement("ui");
                                writer.WriteStartElement("textEdit");
                                writer.WriteEndElement();//textEdit
                                writer.WriteEndElement();//ui

                                writer.WriteStartElement("value");
                                writer.WriteStartElement("text");
                                if (fi.text != null) { writer.WriteString(fi.text.ToString()); }
                                writer.WriteEndElement();//text
                                writer.WriteEndElement();//value

                                writer.WriteStartElement("font");
                                if (fi.formatSet.fontTypeface != null) {
                                    writer.WriteAttributeString("typeface", fi.formatSet.fontTypeface);
                                }
                                writer.WriteAttributeString("baselineShift", "0pt");

                                if (fi.formatSet.fontSize > 0) {
                                    writer.WriteAttributeString("size", fi.formatSet.fontSizeString);
                                }
                                if (fi.formatSet.fontWeight != "normal" || fi.formatSet.fontWeight != null) {
                                    writer.WriteAttributeString("weight", fi.formatSet.fontWeight);
                                }
                                writer.WriteStartElement("fill");
                                writer.WriteStartElement("color");
                                writer.WriteAttributeString("value", "51,102,255");
                                writer.WriteEndElement();//color                                    writer.WriteEndElement();//font
                                writer.WriteEndElement();//fill                                    writer.WriteEndElement();//font
                                writer.WriteEndElement();//font                                    writer.WriteEndElement();//font

                                writer.WriteStartElement("margin");
                                writer.WriteAttributeString("bottomInset", "0mm");
                                writer.WriteAttributeString("leftInset", "0mm");
                                writer.WriteAttributeString("rightInset", "0mm");
                                writer.WriteAttributeString("topInset", "0mm");
                                writer.WriteEndElement();//margin
                                writer.WriteEndElement();//field
                                break;

                            #endregion Label - Mostly Done, double check before ".xdp export" is complete

                            #region Line Draw - Finished, dc like the others still

                            case Type.LineDraw:
                                writer.WriteStartElement("draw");
                                writer.WriteAttributeString("name", fi.name);
                                writer.WriteAttributeString("x", convertToMm(fi.x));
                                writer.WriteAttributeString("y", convertToMm(fi.y));
                                if (fi.height < 5) {
                                    writer.WriteAttributeString("h", "0in");
                                    writer.WriteAttributeString("w", convertToMm(fi.width));
                                }
                                if (fi.width < 5) {
                                    writer.WriteAttributeString("w", "0in");
                                    writer.WriteAttributeString("h", convertToMm(fi.height));
                                }
                                writer.WriteStartElement("value");
                                writer.WriteStartElement("line");
                                writer.WriteStartElement("edge");
                                writer.WriteStartElement("color");
                                writer.WriteAttributeString("value", "51,102,255");
                                writer.WriteEndElement();//end color
                                writer.WriteEndElement();//end edge
                                writer.WriteEndElement();//end line
                                writer.WriteEndElement();//end value
                                writer.WriteEndElement();//end draw
                                break;

                            #endregion Line Draw - Finished, dc like the others still

                            #region Rectangle Draw - Should be all set - dc before feature completion

                            case Type.RectangleDraw:
                                writer.WriteStartElement("draw");
                                writer.WriteAttributeString("name", fi.name);
                                writer.WriteAttributeString("x", convertToMm(fi.x));
                                writer.WriteAttributeString("y", convertToMm(fi.y));
                                writer.WriteAttributeString("w", convertToMm(fi.width));
                                writer.WriteAttributeString("h", convertToMm(fi.height));
                                writer.WriteStartElement("value");
                                writer.WriteStartElement("rectangle");
                                writer.WriteStartElement("edge");
                                writer.WriteStartElement("color");
                                writer.WriteAttributeString("value", "51,102,255");
                                writer.WriteEndElement();//end color
                                writer.WriteEndElement();//end edge
                                writer.WriteStartElement("corner");
                                writer.WriteAttributeString("radius", "5.08mm");
                                writer.WriteStartElement("color");
                                writer.WriteAttributeString("value", "51,102,255");
                                writer.WriteEndElement();//end color
                                writer.WriteEndElement();//end corner
                                writer.WriteEndElement();//end rectangle
                                writer.WriteEndElement();//end value
                                writer.WriteEndElement();//end draw
                                break;

                            #endregion Rectangle Draw - Should be all set - dc before feature completion
                        }
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();//subform
                writer.WriteEndElement();//template
                writer.WriteEndDocument();
            }
        }

        #endregion XDP Exporting

        #region Properties

        public int versionNumber {
            get { return _formVersion ; }
        }

        #endregion Properties

    }
}