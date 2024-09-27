using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Voting_System
{
    public partial class Add_Update_Candidate : Form
    {
        DB dB = new DB();
        public int candidate_id { get; set; }
        public bool IsUpdate { get; set; } = false; 
        public Add_Update_Candidate()
        {
            InitializeComponent();
        }

        private void Add_Update_Candidate_Load(object sender, EventArgs e)
        {
            dB.LoadElectionsIntoComboBox(comboBox1);

            if (IsUpdate)
            {
                
                DataTable candidateData = dB.GetElectionByID(candidate_id);
               
                    if (candidateData.Rows.Count > 0)
                    {

                        textBox1.Text = candidateData.Rows[0]["CandidateID"].ToString();
                        textBox2.Text = candidateData.Rows[0]["Name"].ToString();
                        textBox3.Text = candidateData.Rows[0]["Party"].ToString();

                        comboBox1.SelectedItem = candidateData.Rows[0]["ElectionName"].ToString();
                    }
            }
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            int candidate_id = int.Parse(textBox1.Text);
            string candidateName = textBox2.Text;
            string party = textBox3.Text;
            int electionId = (int)comboBox1.SelectedValue;

            if (IsUpdate)
            {
                dB.UpdateCandidate(candidate_id, candidateName, party, electionId);
                MessageBox.Show("Candidate Updated Successfully");
            }
            else
            {
                dB.AddCandidate(candidate_id, candidateName, party, electionId);
                MessageBox.Show("Candidate Added Successfully");
            }

            this.Close(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manage_Candidates_Form mcan = new Manage_Candidates_Form();
            mcan.ShowDialog();
            this.Close();
        }
    }
    }

    

