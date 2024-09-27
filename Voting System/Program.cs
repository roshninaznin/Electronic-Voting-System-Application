using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Voting_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Start with the LoginForm
                string testEmail = "test@example.com";  // Mock email for Voter_Dashboard
                int demoElectionId = 1;  // Mock election ID for View_Candidates_Form and Cast_Vote_Form
                int sampleVoterId = 1;     // Mock voter ID for View_Candidates_Form and Cast_Vote_Form
                int exampleCandidateId = 1; // Mock candidate ID for Cast_Vote_Form

                //Admin Module
                Application.Run(new Opencs());
                Application.Run(new Admin_Login());
                Application.Run(new Admin_Dashboard());
                Application.Run(new Manage_Voters_Form());
                Application.Run(new Registration("Admin"));
                Application.Run(new Manage_Elections_Form());
                Application.Run(new Manage_Candidates_Form());
                Application.Run(new View_Votes_Form());
                Application.Run(new Check_Votes_Status_Form());
                //Voter Module
                //Application.Run(new Voter_Login_Form()); 
                Application.Run(new Registration("Voter"));
                Application.Run(new Voter_Dashboard(testEmail));  // Pass the email argument to Voter_Dashboard
                Application.Run(new View_Candidates_Form(demoElectionId, sampleVoterId, testEmail)); // Pass electionId and voterId
                Application.Run(new Cast_Vote_Form(sampleVoterId, exampleCandidateId, demoElectionId)); // Pass voterId, candidateId, and electionId
                Application.Run(new viewresult(testEmail));
            }

            catch (SqlException sqlEx)
            {
                // Handle SQL specific exceptions
                MessageBox.Show("A database error occurred: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
