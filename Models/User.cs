using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace PE2022test.Models
{
    public class User
    {
        public int ISSN { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Pass { get; set; }
        public int ACCESS_CONTROL { get; set; }
        //encrypt and decrypt the password
        public static string EncodeServerName(string pass)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(pass));
        }

        public static string DecodeServerName(string pass)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(pass));
        }
    }
}
