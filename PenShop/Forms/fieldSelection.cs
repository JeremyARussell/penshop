using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace PenShop {
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
            name = dgFields.SelectedRows[0].Cells[0].Value.ToString();
            name = name.Replace(" -:- " ,".");

            Close();
        }

        private DataSet _dataSet = new DataSet("Set");
        private DataTable _table;



        private void fieldSelection_Load(object sender, EventArgs e) {

            _table = _dataSet.Tables.Add("Table");
            _table.Columns.Add("Name", typeof(string));
            _table.PrimaryKey = new DataColumn[] { _table.Columns["Name"] };
            dgFields.DataSource = _table;
            dgFields.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgFields.Columns[0].FillWeight = 100;
            



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
                        if(!_table.Rows.Contains(nameToAdd)) {
                            _table.Rows.Add(nameToAdd);
                        }
                    }
                }

                catch (Exception s) {
                    Console.Write(s);
                }

                myConnection.Close();

            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private void txtQuickSearch_TextChanged(object sender, EventArgs e)
        {
            _table.DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", txtQuickSearch.Text);
        }
    }
}