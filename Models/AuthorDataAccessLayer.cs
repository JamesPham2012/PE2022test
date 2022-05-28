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
    public class AuthorDataAccessLayer : BaseDALConnection
    {
        //string connectionString = "server=localhost;user=root;database=pe2022;port=3306;password=Kaka12345";

        public IEnumerable<Author> GetAllAuthors() //view all user details (Admin)
        {
            List<Author> lstauthor = new List<Author>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDAuthor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AuthorID", null);
                cmd.Parameters.AddWithValue("@Authorfname", null);
                cmd.Parameters.AddWithValue("@Authorlname", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Author author = new Author();

                    author.Author_ID = Convert.ToInt32(rdr["Author_ID"]);
                    author.Author_fname = rdr["Author_fname"].ToString();
                    author.Author_lname = rdr["Author_lname"].ToString();

                    lstauthor.Add(author);
                }

                con.Close();
            }
            return lstauthor;
        }

        public void AddAuthor(Author author)// To add new record book(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDAuthor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AuthorID", author.Author_ID);
                cmd.Parameters.AddWithValue("@Authorfname", author.Author_fname);
                cmd.Parameters.AddWithValue("@Authorlname", author.Author_lname);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateAuthor(Author author)//To Update the records of a particular user
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDAuthor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AuthorID", author.Author_ID);
                cmd.Parameters.AddWithValue("@Authorfname", author.Author_fname);
                cmd.Parameters.AddWithValue("@Authorlname", author.Author_lname);
                cmd.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Author GetAuthorData(int? id)
        {
            Author author = new Author();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM author WHERE Author_ID= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    author.Author_ID = Convert.ToInt32(rdr["Author_ID"]);
                    author.Author_fname = rdr["Author_fname"].ToString();
                    author.Author_lname = rdr["Author_lname"].ToString();
                }
                con.Close();
            }
            return author;
        }

        public void DeleteAuthor(int? id)//To Delete the record on a particular user(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDAuthor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@AuthorID", id);
                cmd.Parameters.AddWithValue("@Authorfname", "");
                cmd.Parameters.AddWithValue("@Authorlname", "");


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}


        