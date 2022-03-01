using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auth.GG_Winform_Example
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register f2 = new Register();
            f2.Show();
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            if(API.Login(username.Text,password.Text))
            {
                MessageBox.Show("Successfully Logged in!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                API.Log(User.Username, "Login success!");
                btnGirisYap.Enabled = false;
                this.Hide();
                rouletteBet.Form1 mainForm = new rouletteBet.Form1();
                mainForm.Location = this.Location;
                mainForm.Show();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void siticoneImageButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
