using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rouletteBet
{
    public partial class uyariEkrani : Form
    {
        public uyariEkrani()
        {
            InitializeComponent();
        }

        private void uyariEkrani_Load(object sender, EventArgs e)
        {

        }

        private void uyariEkrani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://click.casoo.partners/afs/come.php?cid=7135&ctgid=100&atype=1&brandid=3");
        }
    }
}
