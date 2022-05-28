using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Fine
    {
        public int SessionID { get; set; }
        [Required]
        public int Fine_days { get; set; }
        public int Fine_amount { get; set; } = 0;
        public void ini(int ID, int days) { this.SessionID = ID; this.Fine_days = days; this.Fine_amount = Fine_days * 1000; }
    }
}
