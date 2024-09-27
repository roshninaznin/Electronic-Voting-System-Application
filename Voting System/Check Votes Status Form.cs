using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voting_System
{
    public partial class Check_Votes_Status_Form : Form
    {
        DB dB = new DB();
        public Check_Votes_Status_Form()
        {
            InitializeComponent();
        }
        private void LoadVotesWithStatus()
        {
            try
            {
                DataTable dtVotes = dB.GetVotesWithStatus(); 
                dataGridView1.DataSource = dtVotes; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading votes with status: " + ex.Message); 
            }
        }
        private void Check_Votes_Status_Form_Load(object sender, EventArgs e)
        {
            LoadVotesWithStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MarkVoteStatus("Tampered");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MarkVoteStatus("Untampered");
        }
        private void MarkVoteStatus(string status)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int voteID = Convert.ToInt32(row.Cells["VoteID"].Value); 
                        dB.UpdateVoteStatus(voteID, status); 
                    }
                    LoadVotesWithStatus(); 
                    MessageBox.Show($"Vote status updated to {status} successfully."); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating vote status: " + ex.Message); 
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Admin_Dashboard ad = new Admin_Dashboard(); 
            ad.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Admin_Dashboard ad = new Admin_Dashboard();
            ad.ShowDialog();
            this.Close();
        }
    }
}
