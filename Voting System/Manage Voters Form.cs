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
    public partial class Manage_Voters_Form : Form
    {
        DB dB = new DB();


        public Manage_Voters_Form()
        {
            InitializeComponent();

        }

        private void Manage_Voters_Form_Load(object sender, EventArgs e)
        {
            LoadVoters();

        }
        private void LoadVoters(string search = "")
        {
         DataTable dtVoters = dB.GetVoters(search);
         dataGridView1.DataSource = dtVoters;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            LoadVoters(search);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Registration addVoterForm = new Registration("Admin");
            addVoterForm.IsUpdate = false; 
            addVoterForm.ShowDialog();
            LoadVoters();
            
        }



        private void button3_Click(object sender, EventArgs e)
        {


            if (dataGridView1.SelectedRows.Count > 0)
            {
                int votersId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["VoterID"].Value);
                Registration updateF = new Registration("Admin");
                updateF.voterID = votersId; 
                updateF.IsUpdate = true; 
                updateF.ShowDialog(); 
                LoadVoters(); 
            }
            else
            {
                MessageBox.Show("Please select an voter to update.");
            }
        }

          

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                  int voterID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["VoterID"].Value);

                    // Call the DeleteElection method
                    dB.DeleteVoter(voterID);

                    MessageBox.Show("Voter Deleted Successfully");
                    LoadVoters();

                }
                else
                {
                    MessageBox.Show("Please select an election to delete.");
                }


            }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin_Dashboard ad = new Admin_Dashboard();
            ad.Show();
            this.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Manage_Elections_Form ee = new Manage_Elections_Form();
            ee.Show();
            this.Close();
        }

        private void dgvVoters_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
               
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadVoters();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Admin_Dashboard ad = new Admin_Dashboard();
            ad.Show();
            this.Close();
        }
    }
}

