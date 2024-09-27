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
    public partial class Add_Update_Election : Form
    { DB dB = new DB();
        public int ElectionID { get; set; }
        public bool IsUpdate { get; set; } = false;
        public Add_Update_Election()
        {
            InitializeComponent();
        }
        private void Add_Update_Election_Load(object sender, EventArgs e)
        {
            if(IsUpdate){
               
                DataTable electionData = dB.GetElectionByID(ElectionID);
                if (electionData.Rows.Count > 0)
                {
                    textBox1.Text = electionData.Rows[0]["e_id"].ToString();
                    textBox2.Text = electionData.Rows[0]["e_name"].ToString();
                    textBox3.Text = electionData.Rows[0]["e_Description"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(electionData.Rows[0]["e_StartDate"]);
                    dateTimePicker2.Value = Convert.ToDateTime(electionData.Rows[0]["e_EndDate"]);
                }
            }
        }
        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            int electionID = int.Parse(textBox1.Text);
            string electionName = textBox2.Text;
            string description = textBox3.Text;
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;



            if (IsUpdate)
            {
               
                dB.UpdateElection(electionID, electionName, description, startDate, endDate);
                MessageBox.Show("Election Updated Successfully");
            }
            else
            {
                
                dB.AddElection(electionID, electionName, description, startDate, endDate);
                MessageBox.Show("Election Added Successfully");
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manage_Elections_Form me = new Manage_Elections_Form();
            me.ShowDialog();
            this.Close();
        }
    }

    
}

    

