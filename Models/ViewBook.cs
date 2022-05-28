using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PE2022test.Models
{
    public class ViewBook
    {
        Models.AuthorDataAccessLayer objauthor = new Models.AuthorDataAccessLayer();
        Models.BookDataAccessLayer objbook = new Models.BookDataAccessLayer();
        Models.Book_AuthorDataAccessLayer objbauthor = new Models.Book_AuthorDataAccessLayer();
        Models.CategoryDataAccessLayer objcategory = new Models.CategoryDataAccessLayer();
        Models.ReviewDataAccessLayer objreview = new Models.ReviewDataAccessLayer();
        Models.Take_add_img_Book taib = new Models.Take_add_img_Book();
        Models.Take_Description_Book tdb = new Models.Take_Description_Book();

        public Book? Book { get; set; }
        public List<Author>? Authors{ get; set; }
        public Category? Category { get; set; } = null;
        public List<Review>? Review { get; set; } = null;
        public int avgStar = 0;
        public string add_img_Book = string.Empty;
        public string Description = string.Empty;
        public void Initiate(Book book)
        {
            Book = book;
            Authors = new List<Author>();
            Category = new Category();

            List<Models.Book_Author> lstBAuthor = new List<Models.Book_Author>();
            lstBAuthor = objbauthor.GetBook_AuthorData(book.ID).ToList();
            foreach (Models.Book_Author bauthor in lstBAuthor)
            {
                this.Authors?.Add(objauthor.GetAuthorData(bauthor.AuthorID));
            }

            this.Category = objcategory.GetBook_CategoryData(book.Category_ID);

            this.Review = objreview.GetReviewDataForBook(book.ID).ToList();

            int Star = 0;
            foreach(Models.Review review in Review)
            {
                Star += review.Review_star;
            }
            if(this.Review.Count > 0) { this.avgStar = Star / this.Review.Count; }
            else { this.avgStar = 0; }

            this.add_img_Book = taib.get_add(book.ID);

            this.Description = tdb.get_add(book.ID);
        }
    }
}
