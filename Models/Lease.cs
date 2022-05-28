using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Lease
    {
        public int SessionID { get; set; }
        public int Book_ID { get; set; }
        public int ISSN { get; set; }
        public DateTime Lease_date { get; set; }
        public DateTime Expiry_date { get; set; }
        public string Status { get; set; } = "active";

        public bool Notify()
        {
            if (this.Expiry_date < DateTime.Now)
            {
                return true;
            }
            else { return false;}
        } 

        public bool isValid()
        {
            try
            {
                if (SessionID.Equals(null) || Book_ID.Equals(null) || ISSN.Equals(null) || Lease_date.Equals(null) || Expiry_date.Equals(null))
                {
                    return false;
                }
                else { return true; };
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
