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
    public partial class Cast_Vote_Form : Form
    {
        DB dB= new DB();
        int voterId;
        int candidateId;
        int electionId;
        public Cast_Vote_Form(int voterId, int candidateId, int electionId)
        {
            InitializeComponent();
            this.voterId = voterId;
            this.candidateId = candidateId;
            this.electionId = electionId;
        }

        private void Cast_Vote_Form_Load(object sender, EventArgs e)
        {
            label1.Text = dB.GetCandidateName(candidateId);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dB.HasVoterAlreadyVoted(voterId, electionId))
            {
                MessageBox.Show("You have already cast your vote for this election.");
                this.Close();
                return;
            }

            bool success = dB.CastVote(voterId, candidateId, electionId);

            if (success)
            {
                MessageBox.Show("Vote Cast Successfully!");
                this.Close(); 
            }
            else
            {
                MessageBox.Show("An error occurred while casting your vote. Please try again.");
            }
        }
    }
    }
