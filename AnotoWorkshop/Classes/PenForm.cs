﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ImageMagick;

namespace AnotoWorkshop {
    public class PenForm {

        #region Variables
        //Public Variables
        public string FormName;
        public string ThisFormsPath; //Todo - Internal Filepath for forms, needed during import really
        public List<int> FormTemplates = new List<int>();
       
        public List<string> generatingTemplates = new List<string>();

        private List<FormPage> _formPages = new List<FormPage>();
        private int _formVersion;
        private int _totalPages;

        private readonly XmlDocument _dom = new XmlDocument();
        private int _counter;

        private Settings _settings = Settings.instance;

        #endregion Variables

        #region Initializers

        //public PenForm(string file, Dictionary<int, FormatSet> workingFormatSets) {
        //    _importFormatSetTicker = workingFormatSets.Count;
        //    try {
        //       _dom.Load(file);
        //        importForm(_dom.DocumentElement/*, workingFormatSets*/);
        //    } catch (XmlException xmlEx) {
        //        MessageBox.Show(xmlEx.Message);
        //    } catch (Exception ex) {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        public PenForm(string file, /*Dictionary<int, FormatSet> workingFormatSets,*/ string newName) {
            //_importFormatSetTicker = workingFormatSets.Count;
            try {
                _dom.Load(file);
                importForm(_dom.DocumentElement/*, workingFormatSets*/);
                FormName = newName;
            } catch (XmlException xmlEx) {
                MessageBox.Show(xmlEx.Message);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public PenForm(string file) {
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
            addNewBlankPage();
        }

        public List<FormPage> formPages {
            get { return _formPages; }
            set { _formPages = value; }
        }

        #endregion Initializers

        #region Form Saving - Almost Done

        public void saveForm() {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
            Directory.CreateDirectory(_settings.formsFolderLocation);
            using (XmlWriter writer = XmlWriter.Create(_settings.formsFolderLocation + @"\" + FormName + ".penform", settings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("PenForm");
                writer.WriteAttributeString("name", FormName);
                writer.WriteAttributeString("version", _formVersion.ToString());

                writer.WriteStartElement("Templates");//Saving the template association list
                foreach(int template in FormTemplates) {
                    writer.WriteStartElement("Template");
                    writer.WriteString(template.ToString());
                    writer.WriteEndElement();//Template                   
                }
                writer.WriteEndElement();//Templates

                writer.WriteStartElement("GeneratingTemplates");//Saving the template association list
                foreach (string template in generatingTemplates) {
                    writer.WriteStartElement("GeneratingTemplate");
                    writer.WriteString(template);
                    writer.WriteEndElement();//GeneratingTemplate                  
                }
                writer.WriteEndElement();//GeneratingTemplates


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
                        writer.WriteAttributeString("fontFamily", fi.fontTypeface);
                        writer.WriteAttributeString("fontSize", fi.fontSize.ToString());
                        writer.WriteAttributeString("fontStyle", fi.fontStyle.ToString());

                        writer.WriteAttributeString("hidden", fi.hidden.ToString());
                        writer.WriteAttributeString("readOnly", fi.readOnly.ToString());
                        //writer.WriteAttributeString("name", fi.group);
                        //writer.WriteAttributeString("name", fi.textTypes);
                        if(fi.type == Type.RichLabel && fi.richBox != null) writer.WriteAttributeString("RTC", fi.richBox.Rtf);//For Fancy Labels
                        if(fi.type == Type.OptionGroup) {//Options group saving stuff
                            writer.WriteStartElement("SubItems");
                            writer.WriteAttributeString("columns", fi.columns.ToString());
                            foreach (var si in fi.items) {
                                writer.WriteStartElement("item");
                                writer.WriteAttributeString("number", si.Key.ToString());
                                writer.WriteAttributeString("value", si.Value.value);
                                writer.WriteAttributeString("label_text", si.Value.text);
                                writer.WriteEndElement();//item                  
                            }
                            writer.WriteEndElement();//SubItems
                        }

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
            foreach (XmlNode xn1 in form) {//Extract templates and pages
                if (xn1.Name == @"Templates") {//For the templates
                    processTemplatesList(xn1);
                }
                if (xn1.Name == @"GeneratingTemplates") {//For the templates
                    processGeneratingTemplatesList(xn1);
                }
                if (xn1.Name == @"Page") {//For the pages
                    pages[_counter] = xn1;
                    _counter++;
                }
            }

            for (int i = 0; i < _counter; i++) {//Process the pages
                XmlNodeList selectedPage = pages[i].ChildNodes;
                FormPage workingPage = new FormPage(i);
                foreach (XmlNode fieldNode in selectedPage) {
                    workingPage.Fields.Add(loadNode(fieldNode));
                }
                addPage(workingPage);
            }
        }

        private void processTemplatesList(XmlNode node) {
            foreach(XmlNode nd in node) {
                if(nd.Name == "Template") {
                    FormTemplates.Add(Convert.ToInt32(nd.InnerText));
                }
            }
        }

        private void processGeneratingTemplatesList(XmlNode node) {
            foreach(XmlNode nd in node) {
                if(nd.Name == "GeneratingTemplate") {
                    generatingTemplates.Add(nd.InnerText);
                }
            }
        }

        

        /// <summary>
        /// Helper method to work with Properties as out parameters.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static Type parseType(string text) {
            Type tmp;
            Enum.TryParse(text, out tmp);
            return tmp;
        }

        /// <summary>
        /// Helper method to work with Properties as out parameters.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static bool parseBool(string text) {
            bool tmp;
            bool.TryParse(text, out tmp);
            return tmp;
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

                        workingField.type = parseType(reader["type"]);

                        workingField.text = reader["text"];
                        workingField.name = reader["name"];

                        workingField.fontTypeface = reader["fontFamily"];
                        int.TryParse(reader["fontSize"], out workingField.fontSize);
                        Enum.TryParse(reader["fontStyle"], out workingField.fontStyle);



                        workingField.hidden   = parseBool(reader["hidden"]);
                        workingField.readOnly = parseBool(reader["readOnly"]);

                        if (workingField.type == Type.RichLabel) {//Fancy Label code
                            workingField.rtc = reader["RTC"];
                        }
                        
                    }

                    if (reader.Name == "SubItems") {
                        int.TryParse(reader["columns"], out workingField.columns);

                        XmlReader ogReader = XmlReader.Create(new StringReader(reader.ReadOuterXml()));

                        while (ogReader.Read()) {
                            if (ogReader.Name == "item") {
                                SubItem subToAdd = new SubItem();
                                subToAdd.value = ogReader["value"];
                                subToAdd.text  = ogReader["label_text"];

                                workingField.items.Add(Convert.ToInt32(ogReader["number"]), subToAdd);
                            }
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

        private void importForm(XmlNode form/*, Dictionary<int, FormatSet> workingFormatSets*/)//Franklin
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
                    workingPage.Fields.Add(processNodeForImporting(fieldNode));
                }

                addPage(workingPage);
            }
        }

        private Field processNodeForImporting(XmlNode field) {
            try {
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
                            workingField.x = convertUM(reader["x"]);
                            workingField.y = convertUM(reader["y"]);
                        } else {
                            workingField.hidden = true;
                        }

                        if (reader["access"] != null) {//Figuring out access level, basically if this textField is for a variable or written input(handriwiting recognition)
                            workingField.readOnly = reader["access"] == "readOnly";
                        }

                        if (reader["w"] == null) {
                            workingField.width = convertUM(reader["minW"]);
                            workingField.height = convertUM(reader["minH"]);
                        } else {
                            workingField.width = convertUM(reader["w"]);//Width
                            workingField.height = convertUM(reader["h"]);//Height
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
                        workingField.type = Type.RichLabel;
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

                    if (reader.Name == "font") {//TODO - NEW FONT IMPORTING TECHNIQUE NEEDED STAT
                        if (reader.IsStartElement()) {
                            //if (reader["size"] != null) {
                            //    workingField.fontSizeString = reader["size"];
                            //}
                            //if (workingField.fontSize == 10) break;
                            //workingField.formatSet.fontTypeface = reader["typeface"];
                            //if (reader["weight"] != null && reader["weight"] != "") {
                            //    workingField.formatSet.fontWeight = reader["weight"];
                            //} else {
                            //    workingField.formatSet.fontWeight = "normal";
                            //}
                        }
                    }
                }//End loop

                //workingFormatSet = workingField.formatSet;

                //if (!checkForDuplicateInDictionary(workingFormatSets, workingFormatSet)) {
                //    workingFormatSet = workingField.formatSet;
                //    workingFormatSets.Add(_importFormatSetTicker, workingFormatSet);
                //    _importFormatSetTicker++;
                //}

                return workingField;
            } catch (Exception e) {
                throw e;
            }
        }

        //private string fontSizeString {TODO - Might not need this, here for just in cases
            //get { return _fontSize.ToString() + "pt"; }
            //set { _fontSize = Convert.ToInt32(value.Substring(0, value.IndexOf("pt"))); }
        //}


        /*private bool checkForDuplicateInDictionary(Dictionary<int, FormatSet> toCheckAgainst, FormatSet setToCheck) {
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
        }*/

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
            FormPage workingPage = new FormPage(_totalPages);
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

        public void exportXDP() {//.xdp export //TODO - FINISH ALREADY
            string versionString = _formVersion.ToString("D7");

            const string adobeXdpNs = @"http://ns.adobe.com/xdp/";//Xdp
            const string adobeXdpPrefix = @"xdp";
            const string adobeXfaNs = @"http://www.xfa.org/schema/xfa-template/2.6/";//Xfa
            //const string adobeXfaPrefix = @"Xfa";
            const string adobeXfalNs = @"http://www.xfa.org/schema/xfa-locale-set/2.6/";
            //prefix xfal nogo
            const string adobeXNs = @"adobe:ns:meta/";
            const string adobeXPrefix = @"x"; 
            const string adobeRdfNs = @"http://www.w3.org/1999/02/22-rdf-syntax-ns#";
            const string adobeRdfPrefix = @"rdf";
            const string adobeXmpNs = "http://ns.adobe.com/xap/1.0/";
            const string adobeXmpPrefix = @"xmp";
            const string adobePdfPrefix = @"pdf";
            const string adobexmpMMPrefix = "xmpMM";
            const string adobeDcPrefix = "dc";
            const string adobeDescPrefix = "desc";
            //const string adobeParsePrefix = "xfa";
            //const string adobeXNs = "adobe:ns:meta/";
            //const string adobeXPrefix = "x";
                                                                                                                            ///UTF is to remove the BOM, which messes with the parsing done by the Anoto software
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, IndentChars = "   " , Encoding = new System.Text.UTF8Encoding(false)};//&#32; - Whitespace?

            //Create the export directory if it doesn't exist already. 
            Directory.CreateDirectory(_settings.exportFolder);
            //Write out the xml document and save it.
            using (XmlWriter writer = XmlWriter.Create(_settings.exportFolder + @"\" + FormName + "." + versionString + ".xdp", settings)) {
                writer.WriteStartDocument();

                writer.WriteProcessingInstruction("xfa", "generator=\"AdobeLiveCycleDesignerES_V9.0.0.2.20120627.2.874785\" APIVersion=\"3.1.20001.0\"");

                writer.WriteStartElement(adobeXdpPrefix, "xdp", adobeXdpNs);
                //Add attribute for timestamp
                //Figure how how the uuid setup works, do they change each time? etc.


                writer.WriteStartElement("template", adobeXfaNs);
                writer.WriteProcessingInstruction("formServer", "defaultPDFRenderFormat acrobat8.1static");
                writer.WriteProcessingInstruction("formServer", "allowRenderCaching 0");
                writer.WriteProcessingInstruction("formServer", "formModel both");


                writer.WriteStartElement("subform");
                writer.WriteAttributeString("name", FormName);
                writer.WriteAttributeString("layout", "tb");
                writer.WriteAttributeString("locale", "en_US");
                writer.WriteAttributeString("restoreState", "auto");

                writer.WriteStartElement("pageSet");
                    writer.WriteStartElement("pageArea");
                    writer.WriteAttributeString("name", "Page1");
                    writer.WriteAttributeString("id", "Page1");
                        writer.WriteStartElement("contentArea");
                        writer.WriteAttributeString("w", "612pt");
                        writer.WriteAttributeString("h", "792pt");
                        writer.WriteAttributeString("id", "contentArea_ID");
                        writer.WriteEndElement();//contentArea

                        writer.WriteStartElement("medium");
                        writer.WriteAttributeString("stock", "default");
                        writer.WriteAttributeString("short", "612pt");
                        writer.WriteAttributeString("long", "792pt");
                        writer.WriteEndElement();//medium
                    
                    writer.WriteProcessingInstruction("templateDesigner", "expand 0");
                    writer.WriteEndElement();//pageArea
                writer.WriteProcessingInstruction("templateDesigner", "expand 1");
                writer.WriteEndElement();//pageSet

                int pageCount = 0;

                foreach (FormPage page in formPages) {
                    pageCount++;
                    writer.WriteStartElement("subform");
                    writer.WriteAttributeString("w", "612pt");
                    writer.WriteAttributeString("h", "792pt");

                    if (pageCount == 1) {
                        writer.WriteStartElement("field");
                        writer.WriteAttributeString("name", "script");
                        writer.WriteAttributeString("w", "0.353mm");
                        writer.WriteAttributeString("h", "0.352mm");
                        writer.WriteAttributeString("access", "readOnly");
                        writer.WriteAttributeString("presence", "invisible");
                        writer.WriteAttributeString("x", "0.353mm");
                        writer.WriteAttributeString("y", "2.117mm");
                        writer.WriteStartElement("ui");
                        writer.WriteStartElement("textEdit");
                        writer.WriteStartElement("border");
                        writer.WriteAttributeString("presence", "hidden");
                        writer.WriteProcessingInstruction("templateDesigner", "StyleID aped0"); //DONE
                        writer.WriteEndElement(); //border                                
                        writer.WriteStartElement("margin");
                        writer.WriteEndElement(); //margin
                        writer.WriteEndElement(); //textEdit
                        writer.WriteEndElement(); //ui

                        writer.WriteStartElement("font");
                        writer.WriteAttributeString("typeface", "Arial");
                        writer.WriteStartElement("fill");
                        writer.WriteStartElement("color");
                        writer.WriteAttributeString("value", "51,102,255");
                        writer.WriteEndElement(); //color
                        writer.WriteEndElement(); //fill
                        writer.WriteEndElement(); //font

                        writer.WriteStartElement("margin");
                        writer.WriteAttributeString("topInset", "0mm");
                        writer.WriteAttributeString("bottomInset", "0mm");
                        writer.WriteAttributeString("leftInset", "0mm");
                        writer.WriteAttributeString("rightInset", "0mm");
                        writer.WriteEndElement(); //margin

                        writer.WriteStartElement("para");
                        writer.WriteAttributeString("vAlign", "middle");
                        writer.WriteEndElement(); //para

                        writer.WriteStartElement("calculate");
                        writer.WriteStartElement("script");
                        writer.WriteAttributeString("contentType", @"application/x-javascript");
                        writer.WriteAttributeString("runAt", "server");
                        foreach (string template in generatingTemplates) {
                            writer.WriteString(@"NextPen.Data.GenerateHistory(" + template + ");" + "\n");
                        }

                            //TODO - Get this going for my correctly generating whichever template needed.
                        writer.WriteEndElement(); //script
                        writer.WriteEndElement(); //calculate

                        writer.WriteEndElement(); //field
                    }


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

                                writer.WriteStartElement("font");//TODO - TOTALLY NEED NEW FONT EXPORTING CODE AS WELL
                                //if (fi.formatSet.fontTypeface != null || fi.formatSet.fontTypeface != "") {
                                //    writer.WriteAttributeString("typeface", fi.formatSet.fontTypeface);
                                //}
                                //if (fi.formatSet.fontSize > 0) {
                                //    writer.WriteAttributeString("size", fi.formatSet.fontSizeString);
                                //}
                                //if (fi.formatSet.fontWeight != "normal" || fi.formatSet.fontWeight != null) {
                                //    writer.WriteAttributeString("weight", fi.formatSet.fontWeight);
                                //}
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



                                writer.WriteStartElement("draw");
                                writer.WriteAttributeString("name", fi.name);
                                writer.WriteAttributeString("x", convertToMm(fi.x + fi.width + 3));
                                writer.WriteAttributeString("y", convertToMm((fi.y) + (fi.exHeight / 2)));
                                writer.WriteAttributeString("w", convertToMm(fi.exWidth));
                                writer.WriteAttributeString("h", convertToMm(fi.exHeight));
                                writer.WriteStartElement("ui");
                                writer.WriteStartElement("textEdit");
                                writer.WriteEndElement();//textEdit
                                writer.WriteEndElement();//ui

                                writer.WriteStartElement("value");
                                writer.WriteStartElement("text");
                                if (fi.text != null) { writer.WriteString(fi.text.ToString()); }
                                writer.WriteEndElement();//text
                                writer.WriteEndElement();//value

                                writer.WriteStartElement("font");//TODO - NEW FONT CODE HERE AS WELL
                                if (fi.fontTypeface != null) {
                                    writer.WriteAttributeString("typeface", fi.fontTypeface);
                                }
                                writer.WriteAttributeString("baselineShift", "0pt");

                                if (fi.fontSize > 0) {
                                    writer.WriteAttributeString("size", fi.fontSize.ToString() + "pt");
                                }
                                //Font
                                if (fi.fontStyle.HasFlag(FontStyle.Bold)) {
                                    writer.WriteAttributeString("weight", "bold");
                                }
                                if (fi.fontStyle.HasFlag(FontStyle.Italic)) {
                                    writer.WriteAttributeString("posture", "italic");
                                }
                                if (fi.fontStyle.HasFlag(FontStyle.Underline)) {
                                    writer.WriteAttributeString("underline", "1");
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

                            #endregion Checkbox - Mostly Done, double check before ".xdp export" is complete

                            #region Options Group - Not Started

                            case Type.OptionGroup:

                                //Exclusion group
                                writer.WriteStartElement("exclGroup");
                                writer.WriteAttributeString("name", fi.name);
                                writer.WriteAttributeString("x", convertToMm(fi.x));//exclGroups in designer only use the position, then ...
                                writer.WriteAttributeString("y", convertToMm(fi.y));//...recalculate the controls border size using the individual items

                                    //Fields for each subtiem
                                    for(int i = 1; i <= fi.items.Count; i++) {
                                        writer.WriteStartElement("field");
                                    //...name is value without spaces, width and height same square for each one) position is the subitem x and y (calculated before hand for the group)
                                        writer.WriteAttributeString("w", "3.4999mm");
                                        writer.WriteAttributeString("h", "3.4999mm");
                                        string nameSansSpaces = fi.items[i].value.Replace(" ", "");
                                        writer.WriteAttributeString("name", nameSansSpaces);
                                        writer.WriteAttributeString("x", convertToMm(fi.items[i].x));
                                        writer.WriteAttributeString("y", convertToMm(fi.items[i].y));
                                        //All the same standard ui elements and values
                                            writer.WriteStartElement("ui");
                                                writer.WriteStartElement("checkButton");
                                                writer.WriteAttributeString("size", "3.175mm");
                                                    writer.WriteStartElement("border");
                                                        writer.WriteStartElement("fill");
                                                            writer.WriteStartElement("color");
                                                            writer.WriteAttributeString("value", "255, 255, 255");
                                                            writer.WriteEndElement();//color
                                                        writer.WriteEndElement();//fill
                                                        writer.WriteStartElement("edge");
                                                        writer.WriteAttributeString("thickness", "0.2501mm");
                                                            writer.WriteStartElement("color");
                                                            writer.WriteAttributeString("value", "51,102,255");
                                                            writer.WriteEndElement();//color
                                                        writer.WriteEndElement();//edge
                                                        writer.WriteStartElement("corner");
                                                        writer.WriteAttributeString("thickness", "0.25mm");
                                                            writer.WriteStartElement("color");
                                                            writer.WriteAttributeString("value", "51,102,255");
                                                            writer.WriteEndElement();//color
                                                        writer.WriteEndElement();//corner
                                                    writer.WriteEndElement();//border
                                                    writer.WriteStartElement("margin");
                                                    writer.WriteEndElement();//margin
                                                writer.WriteEndElement();//checkButton
                                            writer.WriteEndElement();//ui
                                            writer.WriteStartElement("font");
                                            writer.WriteAttributeString("typeface", "Arial");//TODO - fonts are not handled at all for OG's, need to do this
                                            writer.WriteEndElement();//font
                                            writer.WriteStartElement("margin");
                                            writer.WriteAttributeString("leftInset", "0mm");
                                            writer.WriteAttributeString("rightInset", "0mm");
                                            writer.WriteEndElement();//margin
                                            writer.WriteStartElement("para");
                                            writer.WriteAttributeString("vAlign", "middle");
                                            writer.WriteEndElement();//para
                                            writer.WriteStartElement("border");
                                                writer.WriteStartElement("edge");
                                                writer.WriteAttributeString("thickness", "0.25mm");
                                                writer.WriteAttributeString("presense", "hidden");
                                                    writer.WriteStartElement("color");
                                                    writer.WriteAttributeString("value", "51,102,255");
                                                    writer.WriteEndElement();//color
                                                writer.WriteEndElement();//edge
                                                writer.WriteStartElement("corner");
                                                writer.WriteAttributeString("thickness", "0.25mm");
                                                writer.WriteAttributeString("presense", "hidden");
                                                    writer.WriteStartElement("color");
                                                    writer.WriteAttributeString("value", "51,102,255");
                                                    writer.WriteEndElement();//color
                                                writer.WriteEndElement();//corner
                                            writer.WriteEndElement();//border
                                        //The items has the char for char value of the subitem value
                                            writer.WriteStartElement("items");
                                                writer.WriteStartElement("text");
                                                writer.WriteString(fi.items[i].value);
                                                writer.WriteEndElement();//text
                                            writer.WriteEndElement();//items    

                                        writer.WriteEndElement();//field
                                    }
                                writer.WriteEndElement();//exclGroup

                                //Last is for each item make a label using their label text.
                                for(int i = 1; i <= fi.items.Count; i++) {
                                    writer.WriteStartElement("draw");
                                    writer.WriteAttributeString("name", fi.name);
                                    writer.WriteAttributeString("x", convertToMm(fi.x + fi.items[i].x + 14));
                                    writer.WriteAttributeString("y", convertToMm(fi.y + fi.items[i].y));
                                    writer.WriteAttributeString("w", convertToMm(fi.items[i].width + 10));//Abuffer needed for the actual PenDesktop program
                                    writer.WriteAttributeString("h", convertToMm(fi.items[i].height));
                                        writer.WriteStartElement("ui");
                                            writer.WriteStartElement("textEdit");
                                            writer.WriteEndElement();//textEdit
                                        writer.WriteEndElement();//ui

                                        writer.WriteStartElement("value");
                                            writer.WriteStartElement("text");
                                            writer.WriteString(fi.items[i].text);
                                            writer.WriteEndElement();//text
                                        writer.WriteEndElement();//value

                                        writer.WriteStartElement("font");
                                        writer.WriteAttributeString("typeface", "Arial");
                                        writer.WriteAttributeString("baselineShift", "0pt");
                                        writer.WriteAttributeString("size", "12pt");
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
                                    writer.WriteEndElement();//draw
                                }

                                break;

                            #endregion Options Group - Not Started

                            #region Fancy Label - Not Started

                            case Type.RichLabel:
                                //writer.WriteStartElement("field"); TODO - Saving the new RichLabels
                                //writer.WriteEndElement();//end field
                                break;

                            #endregion Fancy Label - Not Started

                            #region Label - Mostly Done, double check before ".xdp export" is complete

                            case Type.Label:
                                writer.WriteStartElement("draw");
                                writer.WriteAttributeString("name", fi.name);
                                writer.WriteAttributeString("x", convertToMm(fi.x));
                                writer.WriteAttributeString("y", convertToMm(fi.y));
                                writer.WriteAttributeString("w", convertToMm(fi.width + 10));//Abuffer needed for the actual PenDesktop program
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

                                    writer.WriteStartElement("font");//TODO - NEW FONT CODE HERE AS WELL
                                    if (fi.fontTypeface != null) {
                                        writer.WriteAttributeString("typeface", fi.fontTypeface);
                                    }
                                    writer.WriteAttributeString("baselineShift", "0pt");

                                    if (fi.fontSize > 0) {
                                        writer.WriteAttributeString("size", fi.fontSize.ToString() + "pt");
                                    }
                                    //Font
                                    if (fi.fontStyle.HasFlag(FontStyle.Bold)) {
                                        writer.WriteAttributeString("weight", "bold");
                                    }
                                    if (fi.fontStyle.HasFlag(FontStyle.Italic)) {
                                        writer.WriteAttributeString("posture", "italic");
                                    }
                                    if (fi.fontStyle.HasFlag(FontStyle.Underline)) {
                                        writer.WriteAttributeString("underline", "1");
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
                                writer.WriteEndElement();//draw
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
                    writer.WriteProcessingInstruction("templateDesigner", "Zoom 130");//TODO - What's this mean?
                    writer.WriteProcessingInstruction("templateDesigner", "FormTargetVersion 26");//DONE
                    writer.WriteProcessingInstruction("templateDesigner", "Rulers horizontal:1, vertical:1, guidelines:1, crosshairs:0");//DONE

                writer.WriteEndElement();//template

                writer.WriteStartElement("config", "http://www.xfa.org/schema/xci/2.6/");    
                    writer.WriteStartElement("agent");
                        writer.WriteAttributeString("name", "designer");
                            writer.WriteStartElement("destination");
                            writer.WriteString("pdf");
                            writer.WriteEndElement();//destination
                            writer.WriteStartElement("pdf");
                                writer.WriteStartElement("fontInfo");
                                writer.WriteEndElement();//fontInfo
                        writer.WriteEndElement();//pdf
                    writer.WriteEndElement();//agent

                    writer.WriteStartElement("present");
                        writer.WriteStartElement("destination");
                        writer.WriteString("pdf");
                        writer.WriteEndElement();//destination
                        writer.WriteStartElement("pdf");
                            writer.WriteStartElement("fontInfo");
                                    writer.WriteStartElement("embed", adobeXfaNs);
                                    writer.WriteString("0");
                                    writer.WriteEndElement();//embed
                            writer.WriteEndElement();//fontInfo
                            writer.WriteStartElement("tagged", adobeXfaNs);
                            writer.WriteString("0");
                            writer.WriteEndElement();//tagged
                            writer.WriteStartElement("version", adobeXfaNs);
                            writer.WriteString("1.7");
                            writer.WriteEndElement();//version
                            writer.WriteStartElement("adobeExtensionLevel", adobeXfaNs);
                            writer.WriteString("1");
                            writer.WriteEndElement();//adobeExtensionLevel
                        writer.WriteEndElement();//pdf
                        writer.WriteStartElement("cache", adobeXfaNs);
                            writer.WriteStartElement("macroCache");
                            writer.WriteEndElement();//macroCache
                        writer.WriteEndElement();//cache
                        writer.WriteStartElement("xdp", adobeXfaNs);
                            writer.WriteStartElement("packets");
                            writer.WriteString("*");
                            writer.WriteEndElement();//packets
                        writer.WriteEndElement();//xdp
                    writer.WriteEndElement();//present

                    writer.WriteStartElement("psMap", adobeXfaNs);
                        writer.WriteStartElement("font");
                        writer.WriteAttributeString("typeface", "Calibri"); 
                        writer.WriteAttributeString("psName","Calibri");
                        writer.WriteAttributeString("weight","normal");
                        writer.WriteAttributeString("posture","normal");
                        writer.WriteEndElement();//font
                        writer.WriteStartElement("font");
                        writer.WriteAttributeString("typeface","MS Reference Sans Serif");
                        writer.WriteAttributeString("psName","MSReferenceSansSerif");
                        writer.WriteAttributeString("weight","normal");
                        writer.WriteAttributeString("posture","normal");
                        writer.WriteEndElement();//font
                    writer.WriteEndElement();//psMap
                    
                writer.WriteEndElement();//config


                
                writer.WriteStartElement("localeSet", adobeXfalNs );    
                    writer.WriteStartElement("locale");
                    writer.WriteAttributeString("name","en_US");
                    writer.WriteAttributeString("desc","English (United States)");
                        writer.WriteStartElement("calendarSymbols");
                    writer.WriteAttributeString("name","gregorian");
                        writer.WriteStartElement("monthNames");
                        writer.WriteStartElement("month");
                            writer.WriteString("January");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("February");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("March");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("April");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("May");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("June");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("July");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("August");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("September");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("October");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("November");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("December");
                        writer.WriteEndElement();//month
                    writer.WriteEndElement();//monthNames

                    writer.WriteStartElement("MonthNames");
                    writer.WriteAttributeString("abbr","1");
                        writer.WriteStartElement("month");
                            writer.WriteString("Jan");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Feb");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Mar");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Apr");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("May");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Jun");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Jul");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Aug");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Sep");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Oct");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Nov");
                        writer.WriteEndElement();//month
                        writer.WriteStartElement("month");
                            writer.WriteString("Dec");
                        writer.WriteEndElement();//month
                    writer.WriteEndElement();//monthNames
                
                    writer.WriteStartElement("dayNames");
                        writer.WriteStartElement("day");
                            writer.WriteString("Sunday");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Monday");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Tuesday");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Wednesday");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Thursday");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Friday");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Saturday");
                        writer.WriteEndElement();//day
                    writer.WriteEndElement();//dayNames
                    
                    writer.WriteStartElement("dayNames");
                        writer.WriteAttributeString("abbr","1");
                        writer.WriteStartElement("day"); 
                            writer.WriteString("Sun");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Mon");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Tue");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Wed");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Thur");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Fri");
                        writer.WriteEndElement();//day
                        writer.WriteStartElement("day");
                            writer.WriteString("Sat");
                        writer.WriteEndElement();//day
                    writer.WriteEndElement();//dayNames

                    writer.WriteStartElement("meridiemNames");
                        writer.WriteStartElement("meridiem");
                            writer.WriteString("AM");
                        writer.WriteEndElement();//meridiem
                    writer.WriteStartElement("meridiem");
                            writer.WriteString("PM");
                        writer.WriteEndElement();//meridiem
                    writer.WriteEndElement();//meridiemNames

                    writer.WriteStartElement("eraNames");
                        writer.WriteStartElement("era");
                            writer.WriteString("BC");
                        writer.WriteEndElement();//era
                    writer.WriteStartElement("era");
                            writer.WriteString("AD");
                        writer.WriteEndElement();//era
                    writer.WriteEndElement();//eraNames
                        writer.WriteEndElement();//calendarSymbols
                    
                    writer.WriteStartElement("datePatterns");
                        writer.WriteStartElement("datePattern");
                        writer.WriteAttributeString("name","full");
                        writer.WriteString("EEEE, MMMM D, YYYY");
                        writer.WriteEndElement();//datePattern
                        writer.WriteStartElement("datePattern");
                        writer.WriteAttributeString("name","long");
                        writer.WriteString("MMMM D, YYYY");
                        writer.WriteEndElement();//datePattern
                        writer.WriteStartElement("datePattern");
                        writer.WriteAttributeString("name","medium");
                        writer.WriteString("MMM D, YYYY");
                        writer.WriteEndElement();//datePattern
                        writer.WriteStartElement("datePattern");
                        writer.WriteAttributeString("name","short");
                        writer.WriteString("M/D/YY");
                        writer.WriteEndElement();//datePattern
                    writer.WriteEndElement();//dataPatterns

                    writer.WriteStartElement("timePatterns");
                        writer.WriteStartElement("timePattern");
                        writer.WriteAttributeString("name","full");
                        writer.WriteString("h:MM:SS A Z");
                        writer.WriteEndElement();//timePattern
                        writer.WriteStartElement("timePattern");
                        writer.WriteAttributeString("name","long");
                        writer.WriteString("h:MM:SS A Z");
                        writer.WriteEndElement();//timePattern
                        writer.WriteStartElement("timePattern");
                        writer.WriteAttributeString("name","medium");
                        writer.WriteString("h:MM:SS A");
                        writer.WriteEndElement();//timePattern
                        writer.WriteStartElement("timePattern");
                        writer.WriteAttributeString("name","short");
                        writer.WriteString("h:MM A");
                        writer.WriteEndElement();//timepattern
                    writer.WriteEndElement();//timePatterns

                    writer.WriteStartElement("dateTimeSymbols");
                        writer.WriteString("GyMdkHmsSEDFwWahKzZ");
                    writer.WriteEndElement();//dateTimeSymbols

                    writer.WriteStartElement("numberPatterns");
                        writer.WriteStartElement("numberPattern");
                        writer.WriteAttributeString("name","numeric");
                        writer.WriteString("z,zz9.zzz");
                        writer.WriteEndElement();//numberPattern
                        writer.WriteStartElement("numberPattern");
                        writer.WriteAttributeString("name","currency");
                        writer.WriteString("$z,zz9.99|($z,zz9.99)");
                        writer.WriteEndElement();//numberPattern
                        writer.WriteStartElement("numberPattern");
                        writer.WriteAttributeString("name","percent");
                        writer.WriteString("z,zz9%");
                        writer.WriteEndElement();//numberPattern
                    writer.WriteEndElement();//numberPatterns
                    
                    writer.WriteStartElement("numberSymbols");
                        writer.WriteStartElement("numberSymbol");
                        writer.WriteAttributeString("name","decimal");
                        writer.WriteString(".");
                        writer.WriteEndElement();//numberSymbol
                        writer.WriteStartElement("numberSymbol");
                        writer.WriteAttributeString("name","grouping");
                        writer.WriteString(",");
                        writer.WriteEndElement();//numberSymbol
                        writer.WriteStartElement("numberSymbol");
                        writer.WriteAttributeString("name","percent");
                        writer.WriteString("%");
                        writer.WriteEndElement();//numberSymbol
                        writer.WriteStartElement("numberSymbol");
                        writer.WriteAttributeString("name","minus");
                        writer.WriteString("-");
                        writer.WriteEndElement();//numberSymbol
                        writer.WriteStartElement("numberSymbol");
                        writer.WriteAttributeString("name","zero");
                        writer.WriteString("0");
                        writer.WriteEndElement();//numberSymbol
                    writer.WriteEndElement();//numberSymbols

                    writer.WriteStartElement("currencySymbols");
                        writer.WriteStartElement("currencySymbol");
                        writer.WriteAttributeString("name","symbol");
                        writer.WriteString("$");
                        writer.WriteEndElement();//currencySymbol
                        writer.WriteStartElement("currencySymbol");
                        writer.WriteAttributeString("name","isoname");
                        writer.WriteString("USD");
                        writer.WriteEndElement();//currencySymbol
                        writer.WriteStartElement("currencySymbol");
                        writer.WriteAttributeString("name","decimal");
                        writer.WriteString(".");
                        writer.WriteEndElement();//currencySymbol
                    writer.WriteEndElement();//currencySymbols



                    writer.WriteStartElement("typefaces");
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Myriad Pro");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Minion Pro");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Courier Std");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Adobe Pi Std");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Adobe Hebrew");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Adobe Arabic");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Adobe Thai");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Kozuka Gothic Pro-VI M");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Kozuka Mincho Pro-VI R");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Adobe Ming Std L");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Adobe Song Std L");
                        writer.WriteEndElement();//typeface
                        writer.WriteStartElement("typeface");
                            writer.WriteAttributeString("name","Adobe Myungjo Std M");
                        writer.WriteEndElement();//typeface
                    writer.WriteEndElement();//typefaces
                
                writer.WriteEndElement();//locale
                writer.WriteEndElement();//localeSet
                



                writer.WriteStartElement(adobeXPrefix, "xmpmeta", adobeXNs);
                writer.WriteAttributeString("xmlns", "x", null, adobeXNs);
                writer.WriteAttributeString(adobeXPrefix, "xmptk", null, "Adobe XMP Core 4.2.1-c041 52.337767, 2008/04/13-15:41:00        ");

                    writer.WriteStartElement( adobeRdfPrefix, "RDF" , adobeRdfNs);

                        writer.WriteStartElement( adobeRdfPrefix, "Description", null);
                        writer.WriteAttributeString("xmlns", adobeXmpPrefix, null, adobeXmpNs);
                        writer.WriteAttributeString(adobeRdfPrefix, "about", null, "");

                            writer.WriteStartElement( adobeXmpPrefix, "MetadataDate", null);
                            writer.WriteString(System.DateTime.Today.Date.Year.ToString()
                                               + "-"
                                               + System.DateTime.Today.Date.Month.ToString()
                                               + "-"
                                               + System.DateTime.Today.Date.Day.ToString()
                                               + "T" 
                                               + System.DateTime.Now.TimeOfDay.Hours.ToString()
                                               + ":"
                                               + System.DateTime.Now.TimeOfDay.Minutes.ToString()
                                               + ":"
                                               + System.DateTime.Now.TimeOfDay.Seconds.ToString()
                                               );
                //TODO "2013-07-24T19:12:33Z"- FIX THIS -> (System.DateTime.Today.ToString() + "T" + System.DateTime.Now.ToString());
                            writer.WriteEndElement();//MetadataDate
                            
                            writer.WriteStartElement(adobeXmpPrefix,"CreatorTool",null);
                            writer.WriteString("Adobe LiveCycle Designer ES 9.0");
                            writer.WriteEndElement();//CreatorTool
                        writer.WriteEndElement();//Description

                        writer.WriteStartElement( adobeRdfPrefix, "Description", null);
                        writer.WriteAttributeString("xmlns", adobePdfPrefix, null, "http://ns.adobe.com/pdf/1.3/");
                        writer.WriteAttributeString(adobeRdfPrefix, "about", null, "");

                            writer.WriteStartElement( adobePdfPrefix, "Producer", null );
                            writer.WriteString("Adobe LiveCycle Designer ES 9.0");
                            writer.WriteEndElement();//Producer
                        writer.WriteEndElement();//Description

                        writer.WriteStartElement( adobeRdfPrefix, "Description", null);
                        writer.WriteAttributeString("xmlns", adobexmpMMPrefix, null, "http://ns.adobe.com/xap/1.0/mm/");
                        writer.WriteAttributeString(adobeRdfPrefix, "about", null, "");

                            writer.WriteStartElement( adobexmpMMPrefix, "DocumentID", null );
                            writer.WriteString("uuid:6f5407a5-74a5-403e-8859-47473cec5c21");
                            writer.WriteEndElement();//Producer
                        writer.WriteEndElement();//Description

                /*
                        writer.WriteStartElement( adobeRdfPrefix, "Description", null);
                        writer.WriteAttributeString("xmlns", adobeDcPrefix, null, "http://purl.org/dc/elements/1.1/");
                        writer.WriteAttributeString(adobeRdfPrefix, "about", null, "");
                        //TODO - FIX THIS ->   <rdf:li xml:lang="x-default">Test Form</rdf:li>

                            writer.WriteStartElement( adobeDcPrefix, "title", null );
                                writer.WriteStartElement(adobeRdfPrefix, "Alt", null);
                                    writer.WriteStartElement(adobeRdfPrefix, "li", null);
                            
                                    writer.WriteEndElement();//title        
                                writer.WriteEndElement();//Alt    
                            writer.WriteEndElement();//li
                        writer.WriteEndElement();//Description


                        writer.WriteStartElement( adobeRdfPrefix, "Description", null);
                        writer.WriteAttributeString("xmlns", adobeDescPrefix, null, "http://ns.adobe.com/xfa/promoted-desc/");
                        writer.WriteAttributeString(adobeRdfPrefix, "about", null, "");

                            writer.WriteStartElement( adobeDescPrefix, "version", null );
                            writer.WriteAttributeString(adobeRdfPrefix, "parseType", null, "Resource");
               
                                writer.WriteStartElement(adobeRdfPrefix, "value", null);
                                writer.WriteString("8.2.1.4029.1.523496.503679");
                                writer.WriteEndElement();//value

                                writer.WriteStartElement(adobeDescPrefix,"ref", null);
                                writer.WriteString("/template/subform[1]");
                                writer.WriteEndElement();//ref

                        writer.WriteEndElement();//value

                            
                        
                        
                        writer.WriteEndElement();//Description

                */
                writer.WriteEndElement();//xmpmeta


                  /*  writer.WriteStartElement(adobeRdfPrefix,"RDF",adobeRdfNs);
                        writer.WriteStartElement(adobeRdfPrefix, "Description", adobeXmpNs);

                        writer.WriteAttributeString(adobeXmpPrefix , "about", adobeXmpNs, @"");

                  */



                writer.WriteEndDocument();
            }

            updateFusionForms();
            updateFormsPad();

        }

        private void updateFusionForms() {
            string versionString = _formVersion.ToString("D7");
            string lastVersionString = (_formVersion - 1).ToString("D7");
            XmlDocument fusionFormDom = new XmlDocument();

            try {
                fusionFormDom.Load(_settings.exportFolder + @"\" + @"FusionPrintForms.xml");
            }
            catch(Exception) {
                throw;
            }
          
            try {
                //This loop goes through and removes the last added management node from the
                //FusionPrintForms.xml file. This file controls the list of forms you can 
                //print inside the fusion print program.
                XmlNodeList fusionNodeList = fusionFormDom.SelectNodes("FusionPrintForms/Forms");
                if (fusionNodeList != null)
                    foreach( XmlNode xNode in fusionNodeList) {
                        if (xNode.SelectSingleNode("Form").Attributes["xdpfilename"].Value ==
                            FormName + "." + lastVersionString + ".xdp") {
                            xNode.ParentNode.RemoveChild(xNode);
                        }

                    }
            }
            catch(Exception) {
                throw;
            }
          
            try {
                //Main node that will hold the sub node and be added to the root
                XmlNode formsNode = fusionFormDom.CreateNode(XmlNodeType.Element, "Forms", null);

                XmlNode formNode = fusionFormDom.CreateElement("Form");
                //Name of the form attribute
                XmlAttribute nameAttr = fusionFormDom.CreateAttribute("name");
                nameAttr.Value = FormName;
                //Filepath to the updated version attribute
                XmlAttribute filepathAttr = fusionFormDom.CreateAttribute("xdpfilename");
                filepathAttr.Value = FormName + "." + versionString + ".xdp";
                //Add up the attributes
                formNode.Attributes.Append(nameAttr);
                formNode.Attributes.Append(filepathAttr);

                formsNode.AppendChild(formNode);

                fusionFormDom.DocumentElement.AppendChild(formsNode);

                fusionFormDom.Save(_settings.exportFolder + @"\" + @"FusionPrintForms.xml");
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        private void updateFormsPad() { 
            string versionString = _formVersion.ToString("D7");
            XmlDocument formsPadDom = new XmlDocument();

            try {
                formsPadDom.Load(_settings.exportFolder + @"\" + @"forms.pad");

            }
            catch(Exception) {
                throw;
            }
          
            try {

                XmlNode podInfoNode = formsPadDom.CreateNode(XmlNodeType.Element, "PODInfo", null);
                //name = filename w/ version padding
                XmlAttribute nameAttr = formsPadDom.CreateAttribute("name");
                nameAttr.Value = FormName + "." + versionString + ".xdp";
                //multipage = page count
                XmlAttribute pageCountAttr = formsPadDom.CreateAttribute("multipage");
                pageCountAttr.Value = totalPages().ToString();
                podInfoNode.Attributes.Append(nameAttr);
                podInfoNode.Attributes.Append(pageCountAttr);


                XmlNode podNode = formsPadDom.CreateElement("application_info");
                //file = filepath like earlier(not sure why they do it twice.)
                XmlAttribute filePathAttr = formsPadDom.CreateAttribute("file");
                filePathAttr.Value = FormName + "." + versionString + ".xdp";
                //x_offset = 0 - default coords
                XmlAttribute x_offsetAttr = formsPadDom.CreateAttribute("x_offset");
                x_offsetAttr.Value = "0";
                //y_offset = -4 - default coords
                XmlAttribute y_offsetAttr = formsPadDom.CreateAttribute("y_offset");
                y_offsetAttr.Value = "-4" ;
                //title = FormName by itself
                XmlAttribute titleAttr = formsPadDom.CreateAttribute("title");
                titleAttr.Value = FormName;
                //instanceID = %%last_name%% - Used internally by the anoto pen, default
                XmlAttribute instanceIDAttr = formsPadDom.CreateAttribute("instanceID");
                instanceIDAttr.Value = @"%%last_name%%";
                //target = xml - forms stlye? Not sure exactly, it's default as well
                XmlAttribute targetAttr = formsPadDom.CreateAttribute("target");
                targetAttr.Value = "xml";
                //AnotoCoordinateVal = 300 - Internally used default
                XmlAttribute anotoXAttr = formsPadDom.CreateAttribute("AnotoCoordinateVal");
                anotoXAttr.Value = "300";
                //AnotoCoordinateVal_Y = 300 - Internally used default
                XmlAttribute anotoYAttr = formsPadDom.CreateAttribute("AnotoCoordinateVal_Y");
                anotoYAttr.Value = "300";

                //String together the attributes and add them to our working node
                podNode.Attributes.Append(filePathAttr);
                podNode.Attributes.Append(x_offsetAttr);
                podNode.Attributes.Append(y_offsetAttr);
                podNode.Attributes.Append(titleAttr);
                podNode.Attributes.Append(instanceIDAttr);
                podNode.Attributes.Append(targetAttr);
                podNode.Attributes.Append(anotoXAttr);
                podNode.Attributes.Append(anotoYAttr);
                //Add the working podNode to the main node we're adding.
                podInfoNode.AppendChild(podNode);
                //Appending our main node to a specific
                formsPadDom.SelectSingleNode("pad/POD").AppendChild(podInfoNode);

                formsPadDom.Save(_settings.exportFolder + @"\" + @"forms.pad");
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion XDP Exporting

        #region EPS Exporting

        public void exportEPS() {//TODO - Finish this soon

            string versionString = _formVersion.ToString("D7");

            foreach(FormPage page in _formPages) {
                Bitmap bitTest = new Bitmap(612, 792);
                bitTest.SetResolution(72.0f, 72.0f);

                Rectangle pageRectangle = new Rectangle();
                pageRectangle.X = 0;
                pageRectangle.Y = 0;
                pageRectangle.Height = 792;
                pageRectangle.Width = 612;

                Graphics e = Graphics.FromImage(bitTest);

                e.FillRectangle(new SolidBrush(Color.White), pageRectangle);

                foreach (Field fi in page.Fields)
                {
                    if (!fi.hidden)
                    {
                        switch (fi.type)
                        {
                            case Type.TextField:
                                Pen p1 = new Pen(Color.FromArgb(255, 51, 102, 255));
                                e.DrawRectangle(p1, new Rectangle((new Point(fi.x, fi.y)), (new Size(fi.width, fi.height))));
                                p1.Color = Color.FromArgb(255, 51, 102, 255);
                                e.DrawLine(p1, fi.x + 1, fi.y - 1 + fi.height, //Creates the shadow line for the text box.
                                                        fi.x - 1 + fi.width, fi.y - 1 + fi.height);
                                break;

                            case Type.Label:
                                Pen p2 = new Pen(Color.FromArgb(255, 51, 102, 255));
                                //e.Graphics.DrawRectangle(p2, new Rectangle((new Point(fi.x, fi.y)), fi.rect().Size));

                                //p2.Color = Color.FromArgb(255, 51, 102, 255);
                                e.DrawString(fi.text, fi.font(), p2.Brush, new Point(fi.x, fi.y));
                                break;

                            case Type.RichLabel:
                                //Pen flPen = new Pen(Color.FromArgb(255, 51, 102, 255));
                                //e.Graphics.DrawRectangle(flPen, new Rectangle((new Point(fi.x, fi.y)), fi.rect().Size));

                                //e.DrawRtfText(fi.richBox.Rtf, fi.rect(), new Point(0, 0));

                                //flPen.Color = Color.Black;
                                //e.DrawString(fi.text, fi.formatSet.font(), flPen.Brush, new Point(fi.x, fi.y));
                                break;

                            case Type.RectangleDraw:
                                Pen recPen = new Pen(Color.FromArgb(255, 51, 102, 255));
                                e.DrawRectangle(recPen, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                                break;

                            case Type.LineDraw:
                                Pen linePen = new Pen(Color.FromArgb(255, 51, 102, 255));
                                e.DrawRectangle(linePen, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                                break;

                            case Type.Checkbox:
                                Pen p3 = new Pen(Color.FromArgb(255, 51, 102, 255));
                                e.DrawRectangle(p3, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                                //p3.Color = Color.Black;
                                e.DrawString(fi.text, fi.font(), p3.Brush, new Point(fi.x + fi.width, (int)(fi.y + (((fi.height - fi.font().Size) / 2)) - 4)));
                                break;

                            case Type.OptionGroup:
                                Pen p4 = new Pen(Color.FromArgb(255, 51, 102, 255)) {Width = 2};
                                //e.DrawRectangle(p4, new Rectangle((new Point(fi.x, fi.y)),  (new Size(fi.width, fi.height))));
                                foreach (var item in fi.items) {
                                    e.DrawRectangle(p4, new Rectangle(item.Value.x + fi.x + 2, item.Value.y +  fi.y + 2, 12, 12));
                                    
                                    e.DrawString(item.Value.text, new Font(new FontFamily("Arial"), 12 ), p4.Brush, item.Value.x + fi.x + 14, item.Value.y +  fi.y);
                                }
                                break;
                        }
                    }
                }

                using (MagickImage image = new MagickImage(bitTest)) {

                    var ime = image.Density;
                    Console.WriteLine(ime.Width + " - " + ime.Height);
                
                    image.Format = MagickFormat.Eps;
                    image.Write(_settings.exportFolder + @"\" + FormName + "." + versionString + "_" + (page.PageNumber + 1) +  ".eps");
                }
            }
        }

        #endregion EPS Exporting

        #region PNG Exporting

        public void exportPNG() {

            string versionString = _formVersion.ToString("D7");
            SolidBrush pngFiller = new SolidBrush(Color.FromArgb(255, 222, 229, 255));
            Pen pngBlue = new Pen(Color.FromArgb(255, 51, 102, 255));
            double pngMulti = 2.08333;

            foreach(FormPage page in _formPages) {
                Bitmap bitTest = new Bitmap(1275, 1650);
                bitTest.SetResolution(150.0f, 150.0f);

                Rectangle pageRectangle = new Rectangle();
                pageRectangle.X = 0;
                pageRectangle.Y = 0;
                pageRectangle.Height = 1650;
                pageRectangle.Width = 1275;

                Graphics e = Graphics.FromImage(bitTest);

                e.FillRectangle(new SolidBrush(Color.White), pageRectangle);



                //(int)(() * pngMulti)

                foreach (Field fi in page.Fields) {
                    if (!fi.hidden) {
                        switch (fi.type) {
                            case Type.TextField:
                                if (fi.readOnly)
                                    e.DrawLine(pngBlue, (int)((fi.x + 1) * pngMulti), (int)((fi.y - 1 + fi.height) * pngMulti),
                                                        (int)((fi.x - 1 + fi.width) * pngMulti), (int)((fi.y - 1 + fi.height) * pngMulti));
                                else
                                    e.FillRectangle(pngFiller, new Rectangle((new Point((int)((fi.x) * pngMulti), (int)((fi.y) * pngMulti)))
                                        , (new Size((int)((fi.width) * pngMulti), (int)((fi.height) * pngMulti)))));
                                break;

                            case Type.Label:
                                e.DrawString(fi.text, fi.pFont(), pngBlue.Brush, new Point((int)((fi.x) * pngMulti), (int)((fi.y) * pngMulti)));
                                break;

                            case Type.RichLabel:
                                break;

                            case Type.RectangleDraw:
                                e.DrawRectangle(pngBlue, new Rectangle((new Point((int)((fi.x) * pngMulti), (int)((fi.y) * pngMulti))), 
                                    (new Size((int)((fi.width) * pngMulti), (int)((fi.height) * pngMulti)))));
                                break;

                            case Type.LineDraw:
                                e.DrawRectangle(pngBlue, new Rectangle((new Point((int)((fi.x) * pngMulti), (int)((fi.y) * pngMulti))), 
                                    (new Size((int)((fi.width) * pngMulti), (int)((fi.height) * pngMulti)))));
                                break;

                            case Type.Checkbox:
                                e.DrawRectangle(pngBlue, new Rectangle((new Point((int)((fi.x - 1) * pngMulti), (int)((fi.y - 1) * pngMulti))),  
                                    (new Size((int)((fi.width + 1) * pngMulti), (int)((fi.height + 1) * pngMulti)))));
                                
                                e.FillRectangle(pngFiller, new Rectangle((new Point((int)((fi.x) * pngMulti), (int)((fi.y) * pngMulti))), 
                                    (new Size((int)((fi.width) * pngMulti), (int)((fi.height) * pngMulti)))));
                               
                                e.DrawString(fi.text, fi.pFont(), pngBlue.Brush, new Point((int)((fi.x + fi.width) * pngMulti), 
                                                                                           (int)(((int)(fi.y + (((fi.height - fi.pFont().Size) / 2)) - 4)) * pngMulti)));
                                break;

                            case Type.OptionGroup:
                                Pen p4 = new Pen(Color.FromArgb(255, 51, 102, 255)) {Width = 2};
                                foreach (var item in fi.items) {
                                    e.DrawRectangle(p4, new Rectangle((int)((item.Value.x + fi.x + 2) * pngMulti), (int)((item.Value.y +  fi.y + 2) * pngMulti),
                                        (int)((12) * pngMulti), (int)((12) * pngMulti)));
                                    
                                    e.FillRectangle(pngFiller, new Rectangle((new Point((int)((item.Value.x + fi.x + 2) * pngMulti), (int)((item.Value.y +  fi.y + 2) * pngMulti))), 
                                        (new Size((int)((12) * pngMulti), (int)((12) * pngMulti)))));
                                    
                                    e.DrawString(item.Value.text, new Font(new FontFamily("Arial"), 12), p4.Brush, 
                                        (int)((item.Value.x + fi.x + 14) * pngMulti), (int)((item.Value.y +  fi.y) * pngMulti));
                                }
                                break;
                        }
                    }
                }

                using (MagickImage image = new MagickImage(bitTest)) {                
                    image.Format = MagickFormat.Png;
                    image.Write(_settings.exportFolder + @"\" + FormName + "." + versionString + "_" + page.PageNumber +  ".png");
                }
            }
        }

        #endregion PNG Exporting

        #region Properties

        public int versionNumber {
            get { return _formVersion ; }
            set { _formVersion = value ; }
        }

        #endregion Properties

    }
}