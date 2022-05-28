using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Category_ID { get; set; }
        [Required]
        public int Publication_Year { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = "IT, ngành của muôn nghề <3";

    }
}
