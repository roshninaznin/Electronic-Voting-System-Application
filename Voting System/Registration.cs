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
using System.Xml.Linq;

namespace Voting_System
{
    public partial class Registration : Form
    {
        DB dB = new DB();
        public int voterID { get; set; }
        public bool IsUpdate { get; set; } = false; 

        private string userType;
        public Registration(string userType)
        {
            InitializeComponent();
            this.userType = userType;
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            
              if (IsUpdate)
            {
               
                DataTable voterData = dB.GetVoterByID(voterID);
               
                    if (voterData.Rows.Count > 0)
                    {
                        textBox1.Text = voterData.Rows[0]["VoterID"].ToString();
                        textBox2.Text = voterData.Rows[0]["VName"].ToString();
                        textBox3.Text = voterData.Rows[0]["Email"].ToString();
                        textBox4.Text = voterData.Rows[0]["Age"].ToString();
                        textBox5.Text = voterData.Rows[0]["Password"].ToString();


                    }
            }
             
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                if (!IsValidInput())
                {
                    return;
                }

                int voterID = int.Parse(textBox1.Text.Trim());
                string name = textBox2.Text.Trim();
                string email = textBox3.Text.Trim();
                int age = int.Parse(textBox4.Text.Trim());
                string password = textBox5.Text.Trim();

                if (IsUpdate)
                {
                    dB.UpdateVoter(voterID, name, email, age, password);
                    MessageBox.Show("Voter Updated Successfully");
                }
                else
                {
                    dB.AddVoter(voterID, name, email, age, password);
                    MessageBox.Show("Voter Added Successfully");
                }

                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private bool IsValidInput()
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim()) ||
                string.IsNullOrEmpty(textBox3.Text.Trim()) || string.IsNullOrEmpty(textBox4.Text.Trim()) ||
                string.IsNullOrEmpty(textBox5.Text.Trim()))
            {
                MessageBox.Show("All fields are required.");
                return false;
            }

            if (!IsValidEmail(textBox3.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid email address.");
                return false;
            }

            if (!int.TryParse(textBox4.Text.Trim(), out int age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid age.");
                return false;
            }

            return true; // All inputs are valid
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                textBox1.Text = " ";
                textBox2.Text = " ";
                textBox3.Text = " ";
                textBox4.Text = " ";
                textBox5.Text = " ";

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (userType == "Admin")
            {
                
                Manage_Voters_Form managePage = new Manage_Voters_Form();
                managePage.Show();
                this.Hide(); 
            }
            else if (userType == "Voter")
            {
                Voter_Login_Form voterlogin = new Voter_Login_Form();
                voterlogin.Show();
                this.Hide(); 
            }
        }
    }
    }
    


   