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
    public class Book_AuthorDataAccessLayer : BaseDALConnection
    {
        public IEnumerable<Book_Author> GetAllBook_Authors() //view all user details (Admin)
        {
            List<Book_Author> lstbauthor = new List<Book_Author>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDBook_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Book_ID", null);
                cmd.Parameters.AddWithValue("@Author_ID", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Book_Author bauthor = new Book_Author();

                    bauthor.BookID = Convert.ToInt32(rdr["BookID"]);
                    bauthor.AuthorID = Convert.ToInt32(rdr["AuthorID"]);

                    lstbauthor.Add(bauthor);
                }

                con.Close();
            }
            return lstbauthor;
        }

        public void AddBook_Author(Book_Author bauthor)// To add new record book(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDBook_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Book_ID", bauthor.BookID);
                cmd.Parameters.AddWithValue("@Author_ID", bauthor.AuthorID);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateBook_Author(Book_Author bauthor)//To Update the records of a particular user
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDBook_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Book_ID", bauthor.BookID);
                cmd.Parameters.AddWithValue("@Author_ID", bauthor.AuthorID);
                cmd.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public ViewBook_Author GetBook_1AuthorData(int? id,int? id2)
        {
            ViewBook_Author vbauthor = new ViewBook_Author();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDBook_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Book_ID", id);
                cmd.Parameters.AddWithValue("@Author_ID", id2);
                cmd.Parameters.AddWithValue("@StatementType", "SELECTBA");

                vbauthor.Book_Authors = new Book_Author();
                vbauthor.Book_Authors.AuthorID = (int)id2;
                vbauthor.Book_Authors.BookID = (int)id;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("AuthorID")))
                    {
                        Models.BookDataAccessLayer objbook = new Models.BookDataAccessLayer();
                        Models.AuthorDataAccessLayer objau = new Models.AuthorDataAccessLayer();
                        vbauthor.Book_Title = objbook.GetBookData(id).Title;
                        vbauthor.Author_Name = objau.GetAuthorData(id2).Author_fname;
                        break;
                    }
                }
                con.Close();
            }
            return vbauthor;
        }

        public IEnumerable<Book_Author> GetBook_AuthorData(int? id)
        {
            List<Book_Author> lstbauthor = new List<Book_Author>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM book_author WHERE BookID= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Book_Author bauthor = new Book_Author();

                    bauthor.BookID = Convert.ToInt32(rdr["BookID"]);
                    bauthor.AuthorID = Convert.ToInt32(rdr["AuthorID"]);

                    lstbauthor.Add(bauthor);
                }

                con.Close();
            }
            return lstbauthor;
        }

        public void DeleteBook_Author(int? id, int? id2)//To Delete the record on a particular user(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDBook_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@Author_ID", id2);
                cmd.Parameters.AddWithValue("@Book_ID", id);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteBook_AllAuthors(int? id)//To Delete the record on a particular user(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDBook_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@Book_ID", id);
                cmd.Parameters.AddWithValue("@Author_ID", 0);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}


        