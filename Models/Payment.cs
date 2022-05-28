using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Payment
    {
        public int Payment_ID { get; set; }
        [Required]
        public int Customer_ID { get; set; }
        public DateTime Lease_date { get; set; } = DateTime.Now;
        public DateTime Payment_date { get; set; } = DateTime.Now;
        public int Payment_amount { get; set; } = 7000;
    }
}
