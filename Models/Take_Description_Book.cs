using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class Take_Description_Book
    {
        public string script = string.Empty;
        private string[,] data = new string[,]
        {
            {"1","IT, ngành của muôn nghề <3" },
            {"2","Ngồi trên nhìn xuống bờ ao, thấy em lội nước mặc áo màu hồng"},
            {"3","Theo em chú cáo lời đồn, quả nhiên em xinh hút hồn anh đây"},
            {"4","Áo em là áo hai dây, pha thêm chút màu lá cây là tuyệt"},
            {"5","Như một cuốn sách đã duyệt, có bìa là phải có phần nội dung"},
            {"6","Nhìn em là đàn ông sung, tiếp nhiều đến nỗi như sung rơi rụng"},
            {"7","Sống chưa mười tám mùa phượng, thế là nhiều anh lại tưởng đi tù"},
            {"8","Nói chứ đó chỉ là hù, chén em như cái bánh trưng này đi"},
        };

        public string get_add(int id)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (Convert.ToInt32(this.data[i, 0]) == id)
                {
                    this.script = this.data[i, 1];
                }
            }
            return this.script;
        }
    }
}
