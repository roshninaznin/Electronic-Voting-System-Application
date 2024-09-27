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
    public partial class View_Candidates_Form : Form
    {
        private string voterEmail;
        DB dB = new DB();
        int electionId;
        int voterId;
        public View_Candidates_Form(int electionId, int voterId, string voterEmail)
        {
            InitializeComponent();
            this.electionId = electionId;
            this.voterId = voterId;
            this.voterEmail = voterEmail;
        }

        private void View_Candidates_Form_Load(object sender, EventArgs e)
        {
            LoadCandidates();

        }
        private void LoadCandidates(string search = "")
        {
            DataTable dtCandidates = dB.GetCandidatesByElection(electionId, search);
            if (dtCandidates.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtCandidates; 
            }
            else
            {
                MessageBox.Show("No candidates available for this election.");
                button1.Enabled = false; 
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int candidateId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CandidateID"].Value);
                Cast_Vote_Form castVoteForm = new Cast_Vote_Form(voterId, candidateId, electionId);
                castVoteForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a candidate.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Voter_Dashboard vd = new Voter_Dashboard(voterEmail);
            vd.ShowDialog();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            LoadCandidates(search);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadCandidates();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

