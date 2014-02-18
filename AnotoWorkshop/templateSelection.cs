using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class templateSelection : Form {
        public List<int> templates {get;set;}
        Settings _settings = Settings.instance;

        List<int> _workingTemplates = new List<int>();

        private DataSet _dataSet;
        private DataTable _table;


        public templateSelection(List<int> workingTemplates) {
            _workingTemplates = workingTemplates;
            InitializeComponent();
        }

        private void templateSelection_Load(object sender, EventArgs e) {
            buildTemplateDataTable();

            try {
                SqlConnection myConnection = new SqlConnection(_settings.dbConnectionString);
                myConnection.Open();

                try {
                    SqlCommand createTable = new SqlCommand("SELECT template_name, template_display_name, template_id " +
                                                            "FROM templates",
                                                             myConnection);
                    SqlDataReader myReader = createTable.ExecuteReader();
					
                    while (myReader.Read()) {
                        string nameToAdd = myReader["template_name"].ToString();
                        string displayNameToAdd = myReader["template_display_name"].ToString();
                        int intToAdd = int.Parse(myReader["template_id"].ToString());

                        if (displayNameToAdd != "") {//OR isn't in the white list, for later. - FT1A
                            if (_workingTemplates.Contains(intToAdd)) {
                                _table.Rows.Add(true, displayNameToAdd, nameToAdd, intToAdd);//For the Grid
                            } else {
                                _table.Rows.Add(false, displayNameToAdd, nameToAdd, intToAdd);
                            }
                        }
                    }
                }

                catch (Exception s) {
                    Console.WriteLine(s.Source.ToString());
                }

                myConnection.Close();

            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        
        public void buildTemplateDataTable() {
            _dataSet = new DataSet("Set");
            _table = _dataSet.Tables.Add("Table");

            _table.Columns.Add("Selected", typeof(bool));
            _table.Columns.Add("Display", typeof(string));
            _table.Columns.Add("Name", typeof(string));
            _table.Columns.Add("id", typeof(int));

            _table.Columns["id"].Unique = true;
            _table.PrimaryKey = new DataColumn[] { _table.Columns["id"] };

            dgTemplates.DataSource = _table;

            dgTemplates.Columns[1].ReadOnly = true;
            dgTemplates.Columns[2].ReadOnly = true;
            dgTemplates.Columns[3].ReadOnly = true;

            dgTemplates.Columns[0].Width = 25;
            dgTemplates.Columns[1].Width = 100;
            dgTemplates.Columns[2].Width = 100;
            dgTemplates.Columns[3].Width = 50;
        }

        private void btnOkay_Click(object sender, EventArgs e) {
            _workingTemplates.Clear();

            foreach(DataRow templateRow in _table.Rows) {
                if (templateRow.Field<bool>(0)) {
                    _workingTemplates.Add(templateRow.Field<int>(3));
                }
            }            
            
            templates = _workingTemplates;
            Close();
        }
    }
}
