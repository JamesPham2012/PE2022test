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
    /*
    public class BookDataAccessLayer
    {
        string connectionString = "server=localhost;user=root;database=pe2022;port=3306;password=Kaka12345";

        public IEnumerable<Book> GetAllBooks() //view all book details 
        {
            List<Book> lstbook = new List<Book>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("GetAllBooks", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Book book = new Book();

                    book.ID = Convert.ToInt32(rdr["Book_ID"]);
                    book.Title = rdr["Title"].ToString();
                    book.Category_ID = Convert.ToInt32(rdr["Category_ID"]);
                    book.Publication_Year = Convert.ToInt32(rdr["Publication_year"]);
                    book.Quantity = Convert.ToInt32(rdr["Quantity"]);

                    lstbook.Add(book);
                }

                con.Close();
            }
            return lstbook;
        }

          
        public void AddBook(Book book)// ,Author author to add new record book(ADMIN) , can be upgrade by link with author and book_author
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmdbook = new MySqlCommand("ISUDBook", con);
                cmdbook.CommandType = CommandType.StoredProcedure;

                //MySqlCommand cmdbookauthor = new MySqlCommand("IUDBook_Author", con);
                //cmdbookauthor.CommandType = CommandType.StoredProcedure;

                //MySqlCommand cmdauthor = new MySqlCommand("IUDAuthor", con);
                //cmdauthor.CommandType = CommandType.StoredProcedure;

                cmdbook.Parameters.AddWithValue("@BookID", book.ID);
                cmdbook.Parameters.AddWithValue("@Title", book.Title);
                cmdbook.Parameters.AddWithValue("@Category_ID", book.Category_ID);
                cmdbook.Parameters.AddWithValue("@Publication_year", book.Publication_Year);
                cmdbook.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmdbook.Parameters.AddWithValue("@StatementType", "INSERT");

                //cmdbookauthor.Parameters.AddWithValue("@Book_ID", book.ID);
                //cmdbookauthor.Parameters.AddWithValue("@Author_ID", author.Author_ID);
                //cmdbookauthor.Parameters.AddWithValue("@StatementType", "INSERT");

                //cmdauthor.Parameters.AddWithValue("@AuthorID", author.Author_ID);
                //cmdauthor.Parameters.AddWithValue("@Authorfname", author.Author_fname);
                //cmdauthor.Parameters.AddWithValue("@AuthorID", author.Author_lname);
                //cmdauthor.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmdbook.ExecuteNonQuery();
                //cmdbookauthor.ExecuteNonQuery();
                //cmdauthor.ExecuteNonQuery();
                con.Close();
            }
        }
        
        public void UpdateBook(Book book)//To Update the records of a particular book(Admin), can be upgrade by link with author and book_author
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmdbook = new MySqlCommand("ISUDBook", con);
                cmdbook.CommandType = CommandType.StoredProcedure;

                cmdbook.Parameters.AddWithValue("@BookID", book.ID);
                cmdbook.Parameters.AddWithValue("@Title", book.Title);
                cmdbook.Parameters.AddWithValue("@Category_ID", book.Category_ID);
                cmdbook.Parameters.AddWithValue("@Publication_year", book.Publication_Year);
                cmdbook.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmdbook.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmdbook.ExecuteNonQuery();
                con.Close();
            }
        }

        public Book GetBookData(int? id)// To get data the records of a particular book
        {
            Book book = new Book();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM book WHERE Book_ID= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    book.ID = Convert.ToInt32(rdr["Book_ID"]);
                    book.Title = rdr["Title"].ToString();
                    book.Category_ID = Convert.ToInt32(rdr["Category_ID"]);
                    book.Publication_Year = Convert.ToInt32(rdr["Publication_year"]);
                    book.Quantity = Convert.ToInt32(rdr["Quantity"]);
                }
                con.Close();
            }
            return book;
        }

        
        public void DeleteBook(int? id)//To Delete the record on a particular book(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmdbook = new MySqlCommand("ISUDBook", con);
                cmdbook.CommandType = CommandType.StoredProcedure;

                cmdbook.Parameters.AddWithValue("@BookID", id);
                cmdbook.Parameters.AddWithValue("@StatementType", "DELETE");
                cmdbook.Parameters.AddWithValue("@Title", "");
                cmdbook.Parameters.AddWithValue("@Category_ID", 0);
                cmdbook.Parameters.AddWithValue("@Publication_year", 0);
                cmdbook.Parameters.AddWithValue("@Quantity", 0);


                con.Open();
                cmdbook.ExecuteNonQuery();
                con.Close();
            }
        }
    }*/
}
