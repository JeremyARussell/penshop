using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace PenShop
{
    public partial class generateTemplates : Form {
        public generateTemplates(List<string> formtemplates) {
            InitializeComponent();
            templates = new List<string>();

            foreach (string template in formtemplates) {
                ListViewItem item = new ListViewItem();
                item.Text = template;
                templates.Add(template);
                lstvGenerateTemplates.Items.Add(item);
            }

        }

        public List<string> templates {get;set;} 

        private void btnAdd_Click(object sender, EventArgs e) {

            ListViewItem item = new ListViewItem();
            item.Text = Interaction.InputBox("Please type in the name of the template you wish to generate","Template","");

            templates.Add(item.Text);
            lstvGenerateTemplates.Items.Add(item);

        }

        private void btnRemove_Click(object sender, EventArgs e) {
            foreach (ListViewItem row in lstvGenerateTemplates.SelectedItems) {
                lstvGenerateTemplates.Items.Remove(row);
                templates.Remove(row.Text);
            }
        }

        private void btnOkay_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
