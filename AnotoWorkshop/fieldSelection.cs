using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AnotoWorkshop {

    public partial class fieldSelection : Form {

        public fieldSelection() {
            InitializeComponent();
        }

        private void btnQueryDB_Click(object sender, EventArgs e) {
            try {
                SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                            "password=***REMOVED***;server=***REMOVED***\\pentesting;" + "Trusted_Connection=yes;" +
                                            "database=pentesting; " + "connection timeout=30");

                myConnection.Open();

                SqlCommand myCommand = new SqlCommand("INSERT INTO table (Column1, Column2) " +
                                        "Values ('string', 1)", myConnection);
                myConnection.Close();
            } catch (SqlException ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}