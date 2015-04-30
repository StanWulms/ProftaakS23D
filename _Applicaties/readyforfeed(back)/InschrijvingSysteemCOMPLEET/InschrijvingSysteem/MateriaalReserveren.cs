using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InschrijvingSysteem
{
    public partial class txtbox_rfid : Form
    {
        public txtbox_rfid()
        {
            InitializeComponent();
        }

        private void btn_reserveer_Click(object sender, EventArgs e)
        {

        }

        private void lb_voorwerplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbox_voorwerp.Text = lb_voorwerplist.SelectedItem.ToString();
        }
    }
}
