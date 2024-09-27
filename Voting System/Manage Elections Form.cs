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
    public partial class Manage_Elections_Form : Form
    {
        DB dB = new DB();

        public Manage_Elections_Form()
        {
            InitializeComponent();
        }

        private void Manage_Elections_Form_Load(object sender, EventArgs e)
        {
            LoadElections();
        }
        private void LoadElections(string search = "")
        {
            DataTable dtElections = dB.GetElections(search);
            dataGridView1.DataSource = dtElections;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            LoadElections(search);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_Update_Election addForm = new Add_Update_Election();
            addForm.IsUpdate = false; 
            addForm.ShowDialog(); 
            LoadElections(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int electionId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["e_id"].Value);
                Add_Update_Election updateForm = new Add_Update_Election();
                updateForm.ElectionID = electionId; 
                updateForm.IsUpdate = true; 
                updateForm.ShowDialog(); 
                LoadElections(); 
            }
            else
            {
                MessageBox.Show("Please select an election to update.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int electionId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["e_id"].Value);

                
                dB.DeleteElection(electionId);

                MessageBox.Show("Election Deleted Successfully");
                LoadElections(); 
            }
            else
            {
                MessageBox.Show("Please select an election to delete.");
            }
        }

         

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin_Dashboard ad = new Admin_Dashboard();
            ad.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Manage_Candidates_Form cc = new Manage_Candidates_Form();
            cc.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadElections();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Admin_Dashboard ad = new Admin_Dashboard();
            ad.Show();
            this.Close();
        }
    }
}
