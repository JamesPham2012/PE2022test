using System;
using System.Collections.Generic;
using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
// upgrade auto-create payment through lease and fine when click the button
{
    public class PaymentDataAccessLayer : BaseDALConnection
    {   
        public IEnumerable<Payment> GetAllPayments() //view all payments performance(Admin)
        {
            List<Payment> lstpayment = new List<Payment>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaymentID", null);
                cmd.Parameters.AddWithValue("@CustomerID", null);
                cmd.Parameters.AddWithValue("@Leasedate", null);
                cmd.Parameters.AddWithValue("@Paymentdate", null);
                cmd.Parameters.AddWithValue("@Paymentamount", null);
                cmd.Parameters.AddWithValue("@StatementType", "SELECT");

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Payment payment = new Payment();

                    payment.Payment_ID = Convert.ToInt32(rdr["Payment_ID"]);
                    payment.Customer_ID = Convert.ToInt32(rdr["Customer_ID"]);
                    payment.Lease_date = Convert.ToDateTime(rdr["Lease_date"]);
                    payment.Payment_date = Convert.ToDateTime(rdr["Payment_date"]);
                    payment.Payment_amount = Convert.ToInt32(rdr["Payment_amount"]);

                    lstpayment.Add(payment);
                }

                con.Close();
            }
            return lstpayment;
        }
        
        public void AddPayment(Payment payment)// To add new record lease
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                FineDataAccessLayer fine = new FineDataAccessLayer();
                LeaseDataAccessLayer lease = new LeaseDataAccessLayer();
                payment.Lease_date = lease.GetLeaseData(payment.Payment_ID).Lease_date;
                payment.Payment_amount = 7000 + fine.GetFineData(payment.Payment_ID).Fine_amount;

                MySqlCommand cmd = new MySqlCommand("ISUDPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaymentID", payment.Payment_ID);
                cmd.Parameters.AddWithValue("@CustomerID", payment.Customer_ID);
                cmd.Parameters.AddWithValue("@Leasedate", payment.Lease_date);
                cmd.Parameters.AddWithValue("@Paymentdate", payment.Payment_date);
                cmd.Parameters.AddWithValue("@Paymentamount", payment.Payment_amount);
                cmd.Parameters.AddWithValue("@StatementType", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdatePayment(Payment payment)//To Update the records of a particular payment(ADMIN)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaymentID", payment.Payment_ID);
                cmd.Parameters.AddWithValue("@CustomerID", payment.Customer_ID);
                cmd.Parameters.AddWithValue("@Leasedate", payment.Lease_date);
                cmd.Parameters.AddWithValue("@Paymentdate", payment.Payment_date);
                cmd.Parameters.AddWithValue("@Paymentamount", payment.Payment_amount);
                cmd.Parameters.AddWithValue("@StatementType", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Payment GetPaymentData(int? id)
        {
            Payment payment = new Payment();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM payment WHERE Payment_ID= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    payment.Payment_ID = Convert.ToInt32(rdr["Payment_ID"]);
                    payment.Customer_ID = Convert.ToInt32(rdr["Customer_ID"]);
                    payment.Lease_date = Convert.ToDateTime(rdr["Lease_date"]);
                    payment.Payment_date = Convert.ToDateTime(rdr["Payment_date"]);
                    payment.Payment_amount = Convert.ToInt32(rdr["Payment_amount"]);
                }
                con.Close();
            }
            return payment;
        }

        public void DeletePayment(int? id)//To Delete the record on a particular payment(Admin)
        {

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("ISUDPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "DELETE");
                cmd.Parameters.AddWithValue("@PaymentID", id);
                cmd.Parameters.AddWithValue("@CustomerID", null);
                cmd.Parameters.AddWithValue("@Leasedate", null);
                cmd.Parameters.AddWithValue("@Paymentdate", null);
                cmd.Parameters.AddWithValue("@Paymentamount", null);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}


        