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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Voting_System
{
    public partial class viewresult : Form
    {
        private string voterEmail;
        DB dB = new DB();


        public viewresult(string email)
        {
            InitializeComponent();
            voterEmail = email;
            LoadVotes();
        }
        private void LoadVotes()
        {
            try
            {
                DataTable dtVotes = dB.GetVotes();
                dataGridView1.DataSource = dtVotes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading votes: " + ex.Message);
            }
        }

    
    private void SomeMethod()
        {
            Voter_Dashboard vd = new Voter_Dashboard(voterEmail);
            vd.Show();
            this.Close();
        }
        private void viewresult_Load(object sender, EventArgs e)
        {
            LoadElectionList();
        }

        private void LoadElectionList()
        {
            try
            {
                DataTable dtElections = dB.GetElectionList(); 
                comboBox1.DataSource = dtElections;
                comboBox1.DisplayMember = "e_name";  
                comboBox1.ValueMember = "e_id";     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading elections: " + ex.Message);
            }
        }

        private void LoadResults(string electionID, string search = "")
        {
            try
            {
                DataTable dtResults = dB.GetResults(electionID, search); 
                dataGridView1.DataSource = dtResults; 
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading results: " + ex.Message);
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                string electionID = comboBox1.SelectedValue.ToString();

                LoadResults(electionID);
            }
            else
            {
                MessageBox.Show("Please select an election to view results.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Voter_Dashboard vd = new Voter_Dashboard(voterEmail);    
            vd.Show ();
            this.Close ();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            if (comboBox1.SelectedValue != null)
            {
                string electionID = comboBox1.SelectedValue.ToString();
                LoadResults(electionID, search);
            }
            else
            {
                MessageBox.Show("Please select an election before searching.");
            }
        }

            private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
