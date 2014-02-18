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
    public partial class whiteList : Form {
        Settings _settings = Settings.instance;

        public whiteList() {
            InitializeComponent();
        }

        private DataSet _dataSet;
        private DataTable _table;

        private void whiteList_Load(object sender, EventArgs e) {
            buildWhitelistDataTable();
            //LoadGrid(_table);

            try {
                SqlConnection myConnection = new SqlConnection(_settings.dbConnectionString);
                myConnection.Open();

                try {
                    SqlCommand createTable = new SqlCommand("SELECT template_name, template_display_name, template_id " +
                                                            "FROM templates",
                                                             myConnection);
                    SqlDataReader myReader = createTable.ExecuteReader();
					
                    while (myReader.Read()) {
                        //string nameToAdd = myReader["table_name"].ToString();
                        //lstWhiteList.Items.Add(nameToAdd);
                        string nameToAdd = myReader["template_name"].ToString();
                        string displayNameToAdd = myReader["template_display_name"].ToString();
                        int intToAdd = int.Parse(myReader["template_id"].ToString());

                        if (displayNameToAdd != "") {
                            _table.Rows.Add(displayNameToAdd, nameToAdd, intToAdd);
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

        public void buildWhitelistDataTable() {
            _dataSet = new DataSet("Set");
            _table = _dataSet.Tables.Add("Table");

            _table.Columns.Add("Display", typeof(string));
            _table.Columns.Add("Name", typeof(string));
            _table.Columns.Add("id", typeof(int));

            _table.Columns["id"].Unique = true;
            _table.PrimaryKey = new DataColumn[] { _table.Columns["id"] };

            LoadGrid();

            dgBlackList.Columns[0].Width = 100;
            dgBlackList.Columns[1].Width = 65;
            dgBlackList.Columns[2].Width = 40;
        }

        public void LoadGrid() {
            dgBlackList.DataSource = _table;
        }


        private void addToWhiteList() {
            lstWhiteList.Items.Add(dgBlackList.SelectedRows[0].Cells[1].Value.ToString());
        }

        private void removeFromWhiteList() {
            
        }

        private void hide() {
            
        }

        private void show() {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addToWhiteList();
        }

        private void btnSaveList_Click(object sender, EventArgs e)
        {

        }
    }
}
