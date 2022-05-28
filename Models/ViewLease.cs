using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PE2022test.Models;
namespace PE2022test.Models
{
    public class ViewLease
    {
        Models.BookDataAccessLayer objbook = new Models.BookDataAccessLayer();
        public Lease? Lease { get; set; }
        public Book? Book{ get; set; }
        ViewLease(Lease lease){
            Lease = lease;
            Book = objbook.GetBookData(lease.Book_ID);
        }
    }
}
