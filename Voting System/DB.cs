using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

public class DB
{
    private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\OneDrive\Documents\vote.mdf;Integrated Security = True; Connect Timeout = 30";

    // Method to open a connection
    private SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    // Admin Login
    /* public void InsertAdmin(int admin_id, string username, string password)
     {

         using (SqlConnection con = new SqlConnection(connectionString))
         {
             string query = "INSERT INTO Admins (AdminID, Username, Password) VALUES (@AdminID, @Username, @Password)";

             SqlCommand cmd = new SqlCommand(query, con);
             cmd.Parameters.AddWithValue("@AdminID", admin_id);
             cmd.Parameters.AddWithValue("@Username", username);
             cmd.Parameters.AddWithValue("@Password", password);

             con.Open();
             cmd.ExecuteNonQuery();  // Execute the insert command
             con.Close();

             MessageBox.Show("Admin Inserted Successfully");
         }

     }
      */



    public bool ValidateAdminLogin(int admin_id, string username, string password)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string query = "SELECT COUNT(1) FROM Admins WHERE AdminID = @AdminID AND Username = @Username AND Password = @Password";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AdminID", admin_id);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count == 1;


            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error validating admin login: {ex.Message}");
            return false;
        }
    }



    //Voter Login
    public bool ValidateVoterLogin(string email, string password)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT COUNT(1) FROM Voters WHERE Email=@Email AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count == 1;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error validating voter login: {ex.Message}");
            return false;
        }
    }


      // Get Voters
    public DataTable GetVoters(string search = "")
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                string query;

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT * FROM Voters";
                }
                else
                {
                    query = "SELECT * FROM Voters WHERE VName LIKE @Search OR VoterID LIKE @Search";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                if (!string.IsNullOrEmpty(search))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving voters: {ex.Message}");
            return null;
        }
    }



    // Add Voter
    public void AddVoter(int voterID, string name, string email, int age, string password)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "INSERT INTO Voters (VoterID,VName, Email, Age, Password, RegistrationDate) VALUES (@VoterID, @VName, @Email, @Age, @Password, @RegistrationDate)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@VoterID", voterID);
                cmd.Parameters.AddWithValue("@VName", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding voter: {ex.Message}");
        }
    }




    // Get Voter by ID
    public DataTable GetVoterByID(int voterID)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT * FROM Voters WHERE VoterID = @VoterID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@VoterID", voterID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving voter: {ex.Message}");
            return null;
        }
    }




  


    //Update Voter
    public void UpdateVoter(int voterId, string name, string email, int age, string password)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Voters SET VName = @VName, Email = @Email, Age = @Age, Password = @Password, RegistrationDate = @RegistrationDate WHERE VoterID = @VoterID", con);
                cmd.Parameters.AddWithValue("@VoterID", voterId);
                cmd.Parameters.AddWithValue("@VName", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating voter: {ex.Message}");
        }
    }



    // Delete Voter
    public void DeleteVoter(int voterId)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();

                SqlCommand deleteVotesCmd = new SqlCommand("DELETE FROM Votes WHERE VoterID = @VoterID", con);
                deleteVotesCmd.Parameters.AddWithValue("@VoterID", voterId);
                deleteVotesCmd.ExecuteNonQuery();

                SqlCommand deleteVoterCmd = new SqlCommand("DELETE FROM Voters WHERE VoterID = @VoterID", con);
                deleteVoterCmd.Parameters.AddWithValue("@VoterID", voterId);
                deleteVoterCmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error deleting voter: {ex.Message}");
        }
    }



    // Add election 
    public void AddElection(int electionId, string electionName, string description, DateTime startDate, DateTime endDate)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO elections (e_id, e_name, e_Description, e_StartDate, e_EndDate) VALUES (@e_id, @e_name, @e_Description, @e_StartDate, @e_EndDate)", con);
                cmd.Parameters.AddWithValue("@e_id", electionId);
                cmd.Parameters.AddWithValue("@e_name", electionName);
                cmd.Parameters.AddWithValue("@e_Description", description);
                cmd.Parameters.AddWithValue("@e_StartDate", startDate);
                cmd.Parameters.AddWithValue("@e_EndDate", endDate);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Election added successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding election: {ex.Message}");
        }
    }



    // Retrieve election 
    public DataTable GetElectionByID(int electionID)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT * FROM elections WHERE e_id = @e_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@e_id", electionID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt); 
            }
            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving election: {ex.Message}");
            return null;
        }
    }



    // Get elections 
    public DataTable GetElections(string search = "")
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                string query;

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT * FROM elections"; 
                }
                else
                {
                    query = "SELECT * FROM elections WHERE e_name LIKE @Search OR e_id LIKE @Search";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                if (!string.IsNullOrEmpty(search))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + search + "%"); 
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt); 
            }
            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving elections: {ex.Message}");
            return null;
        }
    }



    // Update election
    public void UpdateElection(int electionId, string electionName, string description, DateTime startDate, DateTime endDate)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE elections SET e_name = @e_name, e_Description = @e_Description, e_StartDate = @e_StartDate, e_EndDate = @e_EndDate WHERE e_id = @e_id", conn);
                cmd.Parameters.AddWithValue("@e_id", electionId);
                cmd.Parameters.AddWithValue("@e_name", electionName);
                cmd.Parameters.AddWithValue("@e_Description", description);
                cmd.Parameters.AddWithValue("@e_StartDate", startDate);
                cmd.Parameters.AddWithValue("@e_EndDate", endDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Election updated successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating election: {ex.Message}");
        }
    }



    // Delete election
    public void DeleteElection(int electionId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand deleteVotesCmd = new SqlCommand("DELETE FROM Votes WHERE e_id = @e_id", conn);
                deleteVotesCmd.Parameters.AddWithValue("@e_id", electionId);
                deleteVotesCmd.ExecuteNonQuery();

                SqlCommand deleteCandidatesCmd = new SqlCommand("DELETE FROM Candidates WHERE e_id = @e_id", conn);
                deleteCandidatesCmd.Parameters.AddWithValue("@e_id", electionId);
                deleteCandidatesCmd.ExecuteNonQuery();

                SqlCommand deleteElectionCmd = new SqlCommand("DELETE FROM elections WHERE e_id = @e_id", conn);
                deleteElectionCmd.Parameters.AddWithValue("@e_id", electionId);
                deleteElectionCmd.ExecuteNonQuery();

                MessageBox.Show("Election deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error deleting election: {ex.Message}");
        }
    }



    // Add candidate 
    public void AddCandidate(int candidate_id, string candidateName, string party, int electionId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Candidates (CandidateID, Name, Party, e_id) VALUES (@CandidateID, @Name, @Party, @e_id)", conn);
                cmd.Parameters.AddWithValue("@CandidateID", candidate_id);
                cmd.Parameters.AddWithValue("@Name", candidateName);
                cmd.Parameters.AddWithValue("@Party", party);
                cmd.Parameters.AddWithValue("@e_id", electionId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Candidate added successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding candidate: {ex.Message}");
        }
    }



    // Load elections 
    public void LoadElectionsIntoComboBox(ComboBox comboBox)
    {
        try
        {
            DataTable dtElections = GetElections(); 

            if (dtElections.Rows.Count > 0)
            {
                comboBox.DataSource = dtElections;
                comboBox.DisplayMember = "e_name"; 
                comboBox.ValueMember = "e_id";     
            }
            else
            {
                MessageBox.Show("No elections found.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading elections: {ex.Message}");
        }
    }



    // Retrieve candidate 
    public DataTable GetCandidateByID(int candidate_id)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT * FROM Candidates WHERE CandidateID = @CandidateID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CandidateID", candidate_id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt); 
            }
            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving candidate: {ex.Message}");
            return null;
        }
    }



    // Get candidates 
    public DataTable GetCandidates(string search = "")
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                string query;

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT * FROM Candidates"; 
                }
                else
                {
                    query = "SELECT * FROM Candidates WHERE Name LIKE @Search OR CandidateID LIKE @Search";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                if (!string.IsNullOrEmpty(search))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + search + "%"); 
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt); 
            }

            return dt; 
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving candidates: {ex.Message}");
            return null;
        }
    }



    // Update candidate 
    public void UpdateCandidate(int candidateId, string candidateName, string party, int electionId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Candidates SET Name = @CandidateName, Party = @Party, e_id = @ElectionID WHERE CandidateID = @CandidateID", conn);
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                cmd.Parameters.AddWithValue("@CandidateName", candidateName);
                cmd.Parameters.AddWithValue("@Party", party);
                cmd.Parameters.AddWithValue("@ElectionID", electionId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Candidate updated successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating candidate: {ex.Message}");
        }
    }



    // Delete candidate
    public void DeleteCandidate(int candidateId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
                SqlCommand deleteVotesCmd = new SqlCommand("DELETE FROM Votes WHERE CandidateID = @CandidateID", conn);
                deleteVotesCmd.Parameters.AddWithValue("@CandidateID", candidateId);
                deleteVotesCmd.ExecuteNonQuery();

                
                SqlCommand deleteCandidateCmd = new SqlCommand("DELETE FROM Candidates WHERE CandidateID = @CandidateID", conn);
                deleteCandidateCmd.Parameters.AddWithValue("@CandidateID", candidateId);
                deleteCandidateCmd.ExecuteNonQuery();
                MessageBox.Show("Candidate deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error deleting candidate: {ex.Message}");
        }
    }



    // Get Voter 
    public int GetVoterIdByEmail(string email)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT VoterID FROM Voters WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving voter ID: {ex.Message}");
            return -1; 
        }
    }



    // Get Elections
    public DataTable GetOpenElections()
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT e_id, e_name FROM elections"; 
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtElections = new DataTable();
                adapter.Fill(dtElections);
                return dtElections;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving open elections: {ex.Message}");
            return null;
        }
    }



    // Get Candidates
    public DataTable GetCandidatesByElection(int electionId, string search = "")
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = @"
            SELECT 

                c.CandidateID, 
                c.Name, 
                c.Party, 
                e.e_name AS ElectionName
            FROM 
                Candidates c
            JOIN 
                elections e ON c.e_id = e.e_id
            WHERE 
                c.e_id = @e_id
                AND (C.CandidateID LIKE @search OR c.Name LIKE @search OR c.Party LIKE @search)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@e_id", electionId);
                cmd.Parameters.AddWithValue("@search", "%" + search + "%"); 

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtCandidates = new DataTable();
                adapter.Fill(dtCandidates);
                return dtCandidates;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving candidates by election: {ex.Message}");
            return null;
        }
    }



    // Get CandidateName
    public string GetCandidateName(int candidateId)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT Name FROM Candidates WHERE CandidateID=@CandidateID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);

                con.Open();
                return cmd.ExecuteScalar()?.ToString() ?? throw new Exception("Candidate not found.");
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }
    }



    // Cast Vote
    public bool CastVote(int voterId, int candidateId, int electionId)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(); 

                try
                {
                    string checkQuery = "SELECT COUNT(1) FROM Votes WHERE VoterID = @VoterID AND e_id = @e_id";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con, transaction); 
                    checkCmd.Parameters.AddWithValue("@VoterID", voterId);
                    checkCmd.Parameters.AddWithValue("@e_id", electionId);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        return false; 
                    }

                    
                    string insertQuery = "INSERT INTO Votes (VoterID, CandidateID, e_id, VoteTime) VALUES (@VoterID, @CandidateID, @e_id, @VoteTime)";
                    SqlCommand cmd = new SqlCommand(insertQuery, con, transaction); 
                    cmd.Parameters.AddWithValue("@VoterID", voterId);
                    cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    cmd.Parameters.AddWithValue("@e_id", electionId);
                    cmd.Parameters.AddWithValue("@VoteTime", DateTime.Now);

                    cmd.ExecuteNonQuery();

                    transaction.Commit(); 
                    return true; 
                }
                catch
                {
                    transaction.Rollback(); 
                    throw; 
                }
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }
    }

    //has already vote?
    public bool HasVoterAlreadyVoted(int voterId, int electionId)
    {
        bool hasVoted = false;

        string query = "SELECT COUNT(*) FROM Votes WHERE VoterID = @VoterID AND e_id = @e_id";

        using (SqlConnection conn = GetConnection()) 
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@VoterID", voterId);
            cmd.Parameters.AddWithValue("@e_id", electionId);

            conn.Open();
            int voteCount = (int)cmd.ExecuteScalar(); 

            if (voteCount > 0)
            {
                hasVoted = true; 
            }

            conn.Close();
        }

        return hasVoted;
    }


    // Get ElectionsResults
    public DataTable GetElectionResults(int electionId)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = @"
            SELECT 
                c.Name AS CandidateName, 
                c.Party, 
                COUNT(v.VoteID) AS VoteCount
            FROM 
                Votes v
            INNER JOIN 
                Candidates c ON v.CandidateID = c.CandidateID
            WHERE 
                v.e_id = @e_id
            GROUP BY 
                c.Name, c.Party";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@e_id", electionId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtResults = new DataTable();
                adapter.Fill(dtResults);
                return dtResults;
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }
    }
    //Get Votes
    public DataTable GetVotes()
    {
        try
        {
            DataTable dtVotes = new DataTable();
            using (SqlConnection con = GetConnection()) 
            {
                string query = @"SELECT v.VoteID, vt.VName AS VoterName, c.Name AS CandidateName, COUNT(v.VoterID) AS VoteCount, e.e_id, e.e_name AS ElectionName, v.VoteTime
                FROM Votes v
                JOIN elections e ON v.e_id = e.e_id
                JOIN Voters vt ON v.VoterID = vt.VoterID
                JOIN Candidates c ON v.CandidateID = c.CandidateID
                GROUP BY v.VoteID, vt.VName, c.Name, e.e_id, e.e_name, v.VoteTime";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtVotes); 
            }
            return dtVotes;
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }
    }



    //Get votes with status
    public DataTable GetVotesWithStatus()
    {
        try
        {
            DataTable dtVotes = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT * FROM Votes"; 
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtVotes); 
            }
            return dtVotes;
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }
    }



    //Election list
    public DataTable GetElectionList()
    {
        DataTable dtElections = new DataTable();

        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT e_id, e_name FROM elections";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dtElections);
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }

        return dtElections;
    }



    //update vote status
    public void UpdateVoteStatus(int voteID, string status)
    {
        try
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "UPDATE Votes SET status = @status WHERE VoteID = @VoteID"; 
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@VoteID", voteID);
                con.Open();
                cmd.ExecuteNonQuery(); 
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }
    }



    //Get Result
    public DataTable GetResults(string electionID, string search = "")
    {
        try
        {
            DataTable dtResults = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                string query = @"
            SELECT C.CandidateID, C.Name AS CandidateName, COUNT(V.VoterID) AS VoteCount
            FROM Candidates C
            LEFT JOIN Votes V ON C.CandidateID = V.CandidateID
            WHERE C.e_id = @e_id AND (C.CandidateID LIKE @search OR C.Name LIKE @search) GROUP BY C.CandidateID, C.Name";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@e_id", electionID);
                cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtResults);
            }
            return dtResults; 
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("Database error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred: " + ex.Message);
        }
    }
}
