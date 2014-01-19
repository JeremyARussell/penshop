using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class fieldSelection : Form {
        public string name {get;set;} 
        //public Liststring> names {get;set;} - TODO - FUTURE - Later on for multi name returns
        
        
        public fieldSelection() {
            InitializeComponent();
        }

        private void btnQueryDB_Click(object sender, EventArgs e) {
            try {
                SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                            "password=password17;server=server\\pentesting;" + "Trusted_Connection=no;" +
                                            "database=pentestingdata; " + "connection timeout=10");//TODO - Put in the Settings.cs class
                                                                                                   //_settings.dbConnectionString

                myConnection.Open();
                //SqlCommand myCommand = new SqlCommand("INSERT INTO table (Column1, Column2) " +
                //                        "Values ('string', 1)", myConnection);
                // - DELETE - OLD CRUD

                string nameToAdd = "testing yet";

                lstNames.Items.Add(nameToAdd);


                myConnection.Close();
            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private void btnSelectName_Click(object sender, EventArgs e) {

            //name = lstNames.SelectedItem.ToString();
            name = "testing";
            Close();
        }
    }
}