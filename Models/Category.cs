using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Category
    {
        public int Category_ID { get; set; }
        [Required]
        public string Category_name { get; set; }
    }
}
