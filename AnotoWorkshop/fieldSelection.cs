﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class fieldSelection : Form {
        public string name {get;set;} 
        //public List<string> names {get;set;} - TODO - FUTURE - Later on for multi name returns
        Settings _settings = Settings.instance;
        StringBuilder templateString = new StringBuilder();
        string typeString;

        public fieldSelection(List<int> templates, string type ) {//FT1C


            typeString = "'" + type + "'";

            templateString.Append("(template_id = '");

            for (int i = 0; i < templates.Count; i++) {
                if(i < templates.Count - 1) {
                    templateString.Append(templates[i] + "' OR template_id = '");
                } else {
                    templateString.Append(templates[i] + "')");
                }
            }

            InitializeComponent();



        }


        private void btnSelectName_Click(object sender, EventArgs e) {
            name = lstNames.SelectedItem.ToString();
            name = name.Replace(" -:- " ,".");

            Close();
        }

        private void fieldSelection_Load(object sender, EventArgs e) {
            try {
                SqlConnection myConnection = new SqlConnection(_settings.dbConnectionString);

                myConnection.Open();

                try {
                    SqlCommand createTable = new SqlCommand("SELECT * FROM template_fields " +
                                                            "WHERE " + templateString + " " +
                                                            "AND field_type = " + typeString + " ",//TODO - Field Type
                                                             myConnection);
                    SqlDataReader myReader = createTable.ExecuteReader();
					
                    while (myReader.Read()) {
                        string nameToAdd = myReader["table_name"].ToString() +
                            " -:- " + myReader["field_name"].ToString();
                        if(!lstNames.Items.Contains(nameToAdd)) {
                            lstNames.Items.Add(nameToAdd);
                        }
                    }
                }

                catch (Exception s) {
                    
                }

                myConnection.Close();

            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}