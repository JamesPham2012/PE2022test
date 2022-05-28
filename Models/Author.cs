using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Author
    {
        public int Author_ID { get; set; }
        [Required]
        public string Author_fname { get; set; }
        public string Author_lname { get; set; }
    }
}
