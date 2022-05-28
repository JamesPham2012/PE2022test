using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class ViewBook_Author
    {
        public Book_Author? Book_Authors { get; set; }
        public string? Book_Title { get; set; }
        public string? Author_Name { get; set; }
    }
}
