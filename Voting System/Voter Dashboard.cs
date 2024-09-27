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
    
    public partial class Voter_Dashboard : Form
    {
        DB dB = new DB();
        string voterEmail;
        int voterId;

        public Voter_Dashboard(string email)
        {
            InitializeComponent();
            voterEmail = email;
            voterId = dB.GetVoterIdByEmail(voterEmail);
        }

        private void Voter_Dashboard_Load(object sender, EventArgs e)
        {
            LoadElections();

        }
        private void LoadElections()
        {
            DataTable dtElections = dB.GetOpenElections();
            if (dtElections.Rows.Count > 0)
            {
                comboBox1.DataSource = dtElections;
                comboBox1.DisplayMember = "e_name";
                comboBox1.ValueMember = "e_id";
            }
            else
            {
                MessageBox.Show("No open elections available at the moment.");
                comboBox1.DataSource = null; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox1.SelectedValue.ToString(), out int electionId))
            {
                View_Candidates_Form viewCandidatesForm = new View_Candidates_Form(electionId, voterId, voterEmail);
                viewCandidatesForm.ShowDialog(); 
            }
            else
            {
                MessageBox.Show("Invalid election selected. Please try again.");
            }
        }
    

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                viewresult viewResultForm = new viewresult(voterEmail); 
                viewResultForm.Show();
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Please select an election to view results.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Opencs open = new Opencs();
            open.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
