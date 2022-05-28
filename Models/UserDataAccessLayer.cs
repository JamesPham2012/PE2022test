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
    public class UserDataAccessLayer : BaseDALConnection
    {
        public IEnumerable<User> GetAllUsers() //view all user details (Admin)
        {
            List<User> lstuser = new List<User>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("GetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();

                    user.ISSN = Convert.ToInt32(rdr["ISSN"]);
                    user.Name = rdr["Name"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Address = rdr["Address"].ToString();
                    user.Phone = rdr["Phone"].ToString();

                    lstuser.Add(user);
                }

                con.Close();
            }
            return lstuser;
        }

        public void AddUser(User user)// To add new record book(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ISSN_cus", user.ISSN);
                cmd.Parameters.AddWithValue("@Name_cus", user.Name);
                cmd.Parameters.AddWithValue("@Email_cus", user.Email);
                cmd.Parameters.AddWithValue("@Address_cus", user.Address);
                cmd.Parameters.AddWithValue("@Phone_cus", user.Phone);
                cmd.Parameters.AddWithValue("@PASS", user.Pass);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateUser(User user)//To Update the records of a particular user
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ISSN_cus", user.ISSN);
                cmd.Parameters.AddWithValue("@Name_cus", user.Name);
                cmd.Parameters.AddWithValue("@Email_cus", user.Email);
                cmd.Parameters.AddWithValue("@Address_cus", user.Address);
                cmd.Parameters.AddWithValue("@Phone_cus", user.Phone);
                cmd.Parameters.AddWithValue("@PASS", user.Pass);
                cmd.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public User GetUserData(int? id)
        {
            User user = new User();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM user WHERE ISSN= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.ISSN = Convert.ToInt32(rdr["ISSN"]);
                    user.Name = rdr["Name"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Address = rdr["Address"].ToString();
                    user.Phone = rdr["Phone"].ToString();
                }
                con.Close();
            }
            return user;
        }

        public void DeleteUser(int? id)//To Delete the record on a particular user(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ISSN_cus", id);
                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@Name_cus", "");
                cmd.Parameters.AddWithValue("@Email_cus", "");
                cmd.Parameters.AddWithValue("@Phone_cus", "");
                cmd.Parameters.AddWithValue("@Address_cus", "");
                cmd.Parameters.AddWithValue("@PASS", "");



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}


        