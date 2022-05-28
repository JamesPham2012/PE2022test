using System;
using System.Collections.Generic;
using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
// should be 1book - many categories
{
    public class CategoryDataAccessLayer : BaseDALConnection
    {
        public IEnumerable<Category> GetAllCategories() //view all categories details
        {
            List<Category> lstcategories = new List<Category>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryID", null);
                cmd.Parameters.AddWithValue("@Category_name", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Category category = new Category();

                    category.Category_ID = Convert.ToInt32(rdr["Category_ID"]);
                    category.Category_name = rdr["Category_name"].ToString();

                    lstcategories.Add(category);
                }

                con.Close();
            }
            return lstcategories;
        }

        public void AddCategory(Category category)// To add new record category(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryID", category.Category_ID);
                cmd.Parameters.AddWithValue("@Category_name", category.Category_name);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Category GetBook_CategoryData(int? id)
        {
            Category category = new Category();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT category.Category_ID,category.Category_name FROM category JOIN book" +
                                  " WHERE book.Book_ID = " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    category.Category_ID = Convert.ToInt32(rdr["Category_ID"]);
                    category.Category_name = rdr["Category_name"].ToString();
                }

                con.Close();
            }
            return category;
        }

        public IEnumerable<Book> GetBooks_Category(int? idcate)
        {
            List<Book> books = new List<Book>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM book" +
                                  "WHERE Category_ID = " + idcate;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Book book = new Models.Book();
                    book.ID = Convert.ToInt32(rdr["Book_ID"]);
                    book.Title = rdr["Title"].ToString();
                    book.Category_ID = Convert.ToInt32(rdr["Category_ID"]);
                    book.Publication_Year = Convert.ToInt32(rdr["Publication_year"]);
                    book.Quantity = Convert.ToInt32(rdr["Quantity"]);

                    books.Add(book);
                }

                con.Close();
            }
            return books;
        }
    }
}


        