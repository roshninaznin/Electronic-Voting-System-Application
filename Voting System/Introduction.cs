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
    public partial class Introduction : Form
    {
        public Introduction()
        {
            InitializeComponent();
        }

        private void Introduction_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Opencs op = new Opencs();
                op.ShowDialog(); // Use ShowDialog if you want the Opencs form to be modal
                this.Close();    // Close the Introduction form after Opencs is closed
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the main menu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
