using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnotoWorkshop {
    public partial class labelCreator : Form {
        public string text {get;set;}
        public string type {get;set;}

        Settings _settings = Settings.instance;

        public labelCreator() {
            InitializeComponent();
        }

        private void labelCreator_Load(object sender, EventArgs e) {

            foreach (var fSet in _settings.globalFormatSet) {
                cmbFormatSetNames.Items.Add(fSet.Key);
            }

            AcceptButton = btnDone;

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            text = txtText.Text;
            type = cmbFormatSetNames.Text;

            Close();
        }
    }
}
