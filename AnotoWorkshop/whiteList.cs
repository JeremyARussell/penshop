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
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer3.FixedPanel = FixedPanel.Panel1;


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
                        string nameToAdd = myReader["template_name"].ToString();
                        string displayNameToAdd = myReader["template_display_name"].ToString();
                        int intToAdd = int.Parse(myReader["template_id"].ToString());

                        if(_settings.whiteList.Contains(intToAdd)) {
                            ListViewItem item = new ListViewItem();
                            if (displayNameToAdd != "") {
                                item.Text = displayNameToAdd;
                            } else {
                                item.Text = nameToAdd;
                            }
                            item.Tag = intToAdd;
                            lstWhiteList.Items.Add(item);
                        }

                        if (!_settings.whiteList.Contains(intToAdd)) {
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

            dgBlackList.DataSource = _table;
            dgBlackList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgBlackList.Columns[0].FillWeight = 40;
            dgBlackList.Columns[1].FillWeight = 45;
            dgBlackList.Columns[2].FillWeight = 15;
        }

        private void addToWhiteList() {

            foreach (DataGridViewRow row in dgBlackList.SelectedRows) {
                ListViewItem item = new ListViewItem();
                if (row.Cells[0].Value.ToString() != "") {
                    item.Text = row.Cells[0].Value.ToString();
                } else {
                    item.Text = row.Cells[1].Value.ToString();
                }
                item.Tag = row.Cells[2].Value;
                lstWhiteList.Items.Add(item);
            }

            _needToSave = true;
            
        }

        private void removeFromWhiteList() {

            foreach (ListViewItem row in lstWhiteList.SelectedItems) {
                //ListViewItem item = new ListViewItem();
                //item.Text = row.Cells[0].Value.ToString();
                //item.Tag = row.Cells[2].Value;
                _settings.removeFromWhiteList((int)row.Tag);
                lstWhiteList.Items.Remove(row);
            }

            _needToSave = true;

        }

        private void hide() {
            
        }

        private void show() {
            
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            addToWhiteList();
        }

        bool _needToSave;

        private void whiteList_FormClosing(object sender, FormClosingEventArgs e) {
            foreach (ListViewItem item in lstWhiteList.Items) {
                _settings.addToWhiteList((int)item.Tag);
            }
            if (_needToSave) {
                _settings.saveToFile(); 
            }
        }

        private void dgBlackList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            addToWhiteList();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            removeFromWhiteList();
        }

        private void lstWhiteList_DoubleClick(object sender, EventArgs e)
        {
            removeFromWhiteList();
        }

        private void txtQuickSearch_TextChanged(object sender, EventArgs e)
        {
            _table.DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", txtQuickSearch.Text);
        }
    }
}
