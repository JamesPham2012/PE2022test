using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Book_Author
    {
        public int BookID { get; set; }
        [Required]
        public int AuthorID { get; set; }
    }
}
