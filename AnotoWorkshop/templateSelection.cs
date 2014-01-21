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
        public List<string> templates {get;set;}

        List<string> _workingTemplates = new List<string>();

        public templateSelection(List<string> workingTemplates) {
            _workingTemplates = workingTemplates;
            InitializeComponent();
        }

        private void btnOkay_Click(object sender, EventArgs e) {
            _workingTemplates.Clear();

            foreach(var test in chklstTemplates.CheckedItems) {
                _workingTemplates.Add(test.ToString());

            }

            templates = _workingTemplates;

            Close();
        }

        private void templateSelection_Load(object sender, EventArgs e) {
            try {
                SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                            "password=password17;server=server\\pentesting;" + "Trusted_Connection=no;" +
                                            "database=pentestingdata; " + "connection timeout=10");
                myConnection.Open();

                try {
                    SqlCommand createTable = new SqlCommand("SELECT DISTINCT table_name FROM template_fields",
                                                             myConnection);
                    SqlDataReader myReader = createTable.ExecuteReader();
					
                    while (myReader.Read()) {
                        string nameToAdd = myReader["table_name"].ToString();
                        if(!chklstTemplates.Items.Contains(nameToAdd)) {
                            if(_workingTemplates.Contains(nameToAdd)) {
                                chklstTemplates.Items.Add(nameToAdd, true);
                            } else {
                                chklstTemplates.Items.Add(nameToAdd, false);
                            }
                        }
                    }
                }

                catch (Exception s) {
                    Console.WriteLine(s.ToString());
                }

                myConnection.Close();

            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
