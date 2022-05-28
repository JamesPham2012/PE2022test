using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class ViewLeases_User
    {
        public User? User { get; set; }
        public List<Lease>? Leases{ get; set; }//ViewLease?
    }
}
