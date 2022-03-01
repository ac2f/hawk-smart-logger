using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HawkSmartAuthGG
{
    public partial class Region : Form
    {
        public string region = "";

        public Region()
        {
            InitializeComponent();
        }
        private void ExecForm(string outgoingData)
        {
            this.Hide();
            rouletteBet.Form1 mainForm = new rouletteBet.Form1();
            mainForm.Location = this.Location;
            mainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecForm("Europe");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExecForm("Asia");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExecForm("Other");
        }
    }
}
