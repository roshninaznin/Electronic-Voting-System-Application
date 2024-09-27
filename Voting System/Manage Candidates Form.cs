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
    public partial class Manage_Candidates_Form : Form
    {
        DB dB = new DB();

        public Manage_Candidates_Form()
        {
            InitializeComponent();
        }

        private void Manage_Candidates_Form_Load(object sender, EventArgs e)
        {
            LoadCandidates();
        }
        public void LoadCandidates(string search = "")
        {
            DataTable dtCandidates = dB.GetCandidates(search); 
            dataGridView1.DataSource = dtCandidates; 
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Update_Candidate add_Update_Candidate = new Add_Update_Candidate();
            add_Update_Candidate.IsUpdate=false;
            add_Update_Candidate.ShowDialog();
            LoadCandidates();



        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int candidateId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CandidateID"].Value);
                Add_Update_Candidate updateForm = new Add_Update_Candidate();
                updateForm.candidate_id = candidateId;
                updateForm.IsUpdate = true;
                updateForm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please select a candidate to update.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int candidateId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CandidateID"].Value);

                dB.DeleteCandidate(candidateId); 
                MessageBox.Show("Candidate Deleted Successfully");
                LoadCandidates(); 
            }
            else
            {
                MessageBox.Show("Please select a candidate to delete.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string search = textBox4.Text;
            LoadCandidates(search); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadCandidates();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Admin_Dashboard ad = new Admin_Dashboard();
            ad.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Manage_Elections_Form mcan = new Manage_Elections_Form();
            mcan.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           View_Votes_Form view_Votes_Form = new View_Votes_Form();
            view_Votes_Form.Show();
            this.Close();
        }
    }
}
