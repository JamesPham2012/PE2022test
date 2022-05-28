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
    public class LeaseDataAccessLayer : BaseDALConnection
    {   
        public IEnumerable<Lease> GetAllLeases() //view all leases performance(Admin)
        {
            List<Lease> lstlease = new List<Lease>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDLease", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Session_ID", null);
                cmd.Parameters.AddWithValue("@BookID", null);
                cmd.Parameters.AddWithValue("@ISSNum", null);
                cmd.Parameters.AddWithValue("@Leasedate", null);
                cmd.Parameters.AddWithValue("@Expirydate", null);
                cmd.Parameters.AddWithValue("@report", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Lease lease = new Lease();

                    lease.SessionID = Convert.ToInt32(rdr["SessionID"]);
                    lease.Book_ID = Convert.ToInt32(rdr["Book_ID"]);
                    lease.ISSN = Convert.ToInt32(rdr["ISSN"]);
                    lease.Lease_date = Convert.ToDateTime(rdr["Lease_date"]);
                    lease.Expiry_date = Convert.ToDateTime(rdr["Expiry_date"]);
                    lease.Status = rdr["Status"].ToString();

                    lstlease.Add(lease);
                }

                con.Close();
            }
            return lstlease;
        }
        
        public void AddLease(Lease lease)// To add new record lease
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDLease", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Session_ID", lease.SessionID);
                cmd.Parameters.AddWithValue("@BookID", lease.Book_ID);
                cmd.Parameters.AddWithValue("@ISSNum", lease.ISSN);
                cmd.Parameters.AddWithValue("@Leasedate", lease.Lease_date);
                cmd.Parameters.AddWithValue("@Expirydate", lease.Expiry_date);
                cmd.Parameters.AddWithValue("@report", null);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateLease(Lease lease)//To Update the records of a particular lease(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDLease", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Session_ID", lease.SessionID);
                cmd.Parameters.AddWithValue("@BookID", lease.Book_ID);
                cmd.Parameters.AddWithValue("@ISSNum", lease.ISSN);
                cmd.Parameters.AddWithValue("@Leasedate", lease.Lease_date);
                cmd.Parameters.AddWithValue("@Expirydate", lease.Expiry_date);
                cmd.Parameters.AddWithValue("@report", lease.Status);
                cmd.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Lease GetLeaseData(int? id)
        {
            Lease lease = new Lease();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM lease WHERE SessionID= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    lease.SessionID = Convert.ToInt32(rdr["SessionID"]);
                    lease.Book_ID = Convert.ToInt32(rdr["Book_ID"]);
                    lease.ISSN = Convert.ToInt32(rdr["ISSN"]);
                    lease.Lease_date = Convert.ToDateTime(rdr["Lease_date"]);
                    lease.Expiry_date = Convert.ToDateTime(rdr["Expiry_date"]);
                    lease.Status = rdr["Status"].ToString();
                }
                con.Close();
            }
            return lease;
        }

        public void DeleteLease(int? id)//To Delete the record on a particular lease(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDLease", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@Session_ID", id);
                cmd.Parameters.AddWithValue("@BookID", null);
                cmd.Parameters.AddWithValue("@ISSNum", null);
                cmd.Parameters.AddWithValue("@Leasedate", null);
                cmd.Parameters.AddWithValue("@Expirydate", null);
                cmd.Parameters.AddWithValue("@report", null);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}


        