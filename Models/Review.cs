using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Review
    {
        public int ISSNum { get; set; }
        [Required]
        public int IDBook { get; set; }
        public DateTime Review_date { get; set; } = DateTime.Now;
        public string Review_context { get; set; }
        public int Review_star { get; set; }

    }
}
