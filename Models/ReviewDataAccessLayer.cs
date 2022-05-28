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
    public class ReviewDataAccessLayer : BaseDALConnection
    {   
        public IEnumerable<Review> GetAllReviews() //view all payments performance(Admin)
        {
            List<Review> lstreview = new List<Review>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDReview", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ISSNumb", null);
                cmd.Parameters.AddWithValue("@ID_Book", null);
                cmd.Parameters.AddWithValue("@Reviewdate", null);
                cmd.Parameters.AddWithValue("@Reviewcontext", null);
                cmd.Parameters.AddWithValue("@Reviewstar", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Review review = new Review();

                    review.ISSNum = Convert.ToInt32(rdr["ISSNum"]);
                    review.IDBook = Convert.ToInt32(rdr["IDBook"]);
                    review.Review_date = Convert.ToDateTime(rdr["Review_date"]);
                    review.Review_context = rdr["Review_context"].ToString();
                    review.Review_star = Convert.ToInt32(rdr["Review_star"]);

                    lstreview.Add(review);
                }

                con.Close();
            }
            return lstreview;
        }
        
        public void AddReview(Review review)// To add new record review
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDReview", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ISSNumb", review.ISSNum);
                cmd.Parameters.AddWithValue("@ID_Book", review.IDBook);
                cmd.Parameters.AddWithValue("@Reviewdate", review.Review_date);
                cmd.Parameters.AddWithValue("@Reviewcontext", review.Review_context);
                cmd.Parameters.AddWithValue("@Reviewstar", review.Review_star);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateReview(Review review)//To Update the records of a particular payment(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDReview", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ISSNumb", review.ISSNum);
                cmd.Parameters.AddWithValue("@ID_Book", review.IDBook);
                cmd.Parameters.AddWithValue("@Reviewdate", review.Review_date);
                cmd.Parameters.AddWithValue("@Reviewcontext", review.Review_context);
                cmd.Parameters.AddWithValue("@Reviewstar", review.Review_star);
                cmd.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Review GetReviewData(int? id, int? id2)
        {
            Review review = new Review();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM review WHERE ISSNum= " + id + " AND IDBook = " +id2;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    review.ISSNum = Convert.ToInt32(rdr["ISSNum"]);
                    review.IDBook = Convert.ToInt32(rdr["IDBook"]);
                    review.Review_date = Convert.ToDateTime(rdr["Review_date"]);
                    review.Review_context = rdr["Review_context"].ToString();
                    review.Review_star = Convert.ToInt32(rdr["Review_star"]);
                }
                con.Close();
            }
            return review;
        }

        public IEnumerable<Review> GetReviewDataForBook(int? id)
        {
            List<Review> lstreview = new List<Review>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM review WHERE IDBook = " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Review review = new Review();

                    review.ISSNum = Convert.ToInt32(rdr["ISSNum"]);
                    review.IDBook = Convert.ToInt32(rdr["IDBook"]);
                    review.Review_date = Convert.ToDateTime(rdr["Review_date"]);
                    review.Review_context = rdr["Review_context"].ToString();
                    review.Review_star = Convert.ToInt32(rdr["Review_star"]);

                    lstreview.Add(review);
                }
                con.Close();
            }
            return lstreview;
        }

        public void DeleteReview(int? id, int? id2)//To Delete the record on a particular payment(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDReview", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@ISSNumb", id);
                cmd.Parameters.AddWithValue("@ID_Book", id2);
                cmd.Parameters.AddWithValue("@Reviewdate", null);
                cmd.Parameters.AddWithValue("@Reviewcontext", null);
                cmd.Parameters.AddWithValue("@Reviewstar", null);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}


        