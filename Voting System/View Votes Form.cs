using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voting_System
{
    public partial class View_Votes_Form : Form
    {  DB dB = new DB();
        public View_Votes_Form()
        {
            InitializeComponent();
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

        private void View_Votes_Form_Load(object sender, EventArgs e)
        {
            LoadElectionList();
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

            Admin_Dashboard ad = new Admin_Dashboard();
            ad.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
