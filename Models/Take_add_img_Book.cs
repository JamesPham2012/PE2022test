using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Take_add_img_Book
    {
        public string address = string.Empty;
        private string[,] data = new string[,]
        {
            {"1","/css/img/book-images/1.jpg"},
            {"2","/css/img/book-images/2.jpg"},
            {"3","/css/img/book-images/3.jpg"},
            {"4","/css/img/book-images/4.jpg"},
            {"5","/css/img/book-images/5.jpg"},
            {"6","/css/img/book-images/6.jpg"},
            {"7","/css/img/book-images/7.jpg"},
            {"8","/css/img/book-images/8.jpg"},
        };

        public string get_add(int id)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (Convert.ToInt32(this.data[i, 0]) == id)
                {
                    this.address = this.data[i, 1];
                }
            }
            return this.address;
        }
    }
}
