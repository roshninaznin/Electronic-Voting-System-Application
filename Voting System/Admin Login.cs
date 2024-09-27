using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Voting_System
{
    public partial class Admin_Login : Form
    {
        DB dB = new DB();

        public Admin_Login()
        {
            InitializeComponent();
            textBox3.UseSystemPasswordChar = true;

        }

        private void Admin_Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int adminId;
                if (!int.TryParse(textBox1.Text, out adminId))
                {
                    MessageBox.Show("Invalid Admin ID. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = textBox2.Text;
                string password = textBox3.Text;

                bool isValidLogin = dB.ValidateAdminLogin(adminId, username, password);

                if (isValidLogin)
                {
                    MessageBox.Show("Admin Login Successful", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Admin_Dashboard adminDashboard = new Admin_Dashboard();
                    adminDashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Opencs opencs = new Opencs();
            opencs.Show();
            this.Close();
        }
    }
}
