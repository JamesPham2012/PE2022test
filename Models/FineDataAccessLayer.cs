using System;
using System.Collections.Generic;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class FineDataAccessLayer : BaseDALConnection
    {
        public IEnumerable<Fine> GetAllFines() //view all fines performance(Admin)
        {
            List<Fine> lstfine = new List<Fine>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDFine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Session_ID", null);
                cmd.Parameters.AddWithValue("@Finedays", null);
                cmd.Parameters.AddWithValue("@Fineamount", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");
                //upgrade here to update the fine_days on time
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Fine fine = new Fine();

                    fine.SessionID = Convert.ToInt32(rdr["SessionID"]);
                    fine.Fine_days = Convert.ToInt32(rdr["Fine_days"]);
                    fine.Fine_amount = Convert.ToInt32(rdr["Fine_amount"]);
                    

                    lstfine.Add(fine);
                }

                con.Close();
            }
            return lstfine;
        }
        
        public void AddFine(Fine fine)// To add new record fine
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDFine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Session_ID", fine.SessionID);
                cmd.Parameters.AddWithValue("@Finedays", fine.Fine_days);
                cmd.Parameters.AddWithValue("@Fineamount", fine.Fine_amount);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateFine(Fine fine)//To Update the records of a particular fine(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDFine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Session_ID", fine.SessionID);
                cmd.Parameters.AddWithValue("@Finedays", fine.Fine_days);
                cmd.Parameters.AddWithValue("@Fineamount", fine.Fine_amount);
                cmd.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Fine GetFineData(int? id)
        {
            Fine fine = new Fine();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM fine WHERE SessionID= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    fine.SessionID = Convert.ToInt32(rdr["SessionID"]);
                    fine.Fine_days = Convert.ToInt32(rdr["Fine_days"]);
                    fine.Fine_amount = Convert.ToInt32(rdr["Fine_amount"]);
                }
                con.Close();
            }
            return fine;
        }

        public void DeleteFine(int? id)//To Delete the record on a particular fine(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDFine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@Session_ID", id);
                cmd.Parameters.AddWithValue("@Finedays", null);
                cmd.Parameters.AddWithValue("@Fineamount", null);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void auto_update()//auto update fine(AUTO)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDFine", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Session_ID", null);
                cmd.Parameters.AddWithValue("@Finedays", null);
                cmd.Parameters.AddWithValue("@Fineamount", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                

                while (rdr.Read())
                {
                    Fine fine = new Fine();
                    LeaseDataAccessLayer lease = new LeaseDataAccessLayer();
                    int sessID = Convert.ToInt32(rdr["SessionID"]);

                    fine.ini(sessID,(DateTime.Now - lease.GetLeaseData(sessID).Expiry_date).Days);
                    if (fine.Fine_amount < 0)
                        fine.Fine_amount = 0;

                    UpdateFine(fine);
                }
                con.Close();
            }
        }
    }
}


        