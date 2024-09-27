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
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manage_Voters_Form manageVotersForm = new Manage_Voters_Form();
            manageVotersForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manage_Elections_Form manageElectionsForm = new Manage_Elections_Form();
            manageElectionsForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Manage_Candidates_Form manageCandidatesForm = new Manage_Candidates_Form();
            manageCandidatesForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            View_Votes_Form viewVotesForm = new View_Votes_Form();
            viewVotesForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Check_Votes_Status_Form checkVotesStatusForm = new Check_Votes_Status_Form();
            checkVotesStatusForm.Show();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Opencs open = new Opencs();
            open.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
