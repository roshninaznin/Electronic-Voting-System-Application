using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Voting_System
{
    public partial class Voter_Login_Form : Form
    {
        DB db = new DB();

        public Voter_Login_Form()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;

        }

        private void Voter_Login_Form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (IsValidInput(email, password)) 
            {
                try
                {
                    bool isValid = db.ValidateVoterLogin(email, password);

                    if (isValid)
                    {
                        MessageBox.Show("Voter Login Successful");
                        Voter_Dashboard voterDashboard = new Voter_Dashboard(email); 
                        voterDashboard.Show();
                        this.Hide(); 
                    }
                    else
                    {
                        MessageBox.Show("Invalid Email or Password.\nPlease complete registration.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during login: " + ex.Message); 
                }
            }
        }
            private bool IsValidInput(string email, string password)
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Email and Password are required.");
                    return false;
                }

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address.");
                    return false;
                }

                return true;
            }


            private bool IsValidEmail(string email)
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, emailPattern);
            }



            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration registrationForm = new Registration("Voter");
            registrationForm.ShowDialog();
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Opencs opencs = new Opencs();
            opencs.ShowDialog();    
            this.Hide();
        }
    }
}
