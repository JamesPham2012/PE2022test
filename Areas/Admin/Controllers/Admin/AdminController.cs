using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using Xunit;

namespace PE2022test.Areas.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {
            objfine.auto_update();
        }

        Models.AuthorDataAccessLayer objauthor = new Models.AuthorDataAccessLayer();
        Models.BookDataAccessLayer objbook = new Models.BookDataAccessLayer();
        Models.Book_AuthorDataAccessLayer objbauthor = new Models.Book_AuthorDataAccessLayer();
        Models.CategoryDataAccessLayer objcategory = new Models.CategoryDataAccessLayer();
        Models.LeaseDataAccessLayer objlease = new Models.LeaseDataAccessLayer();
        Models.PaymentDataAccessLayer objpayment = new Models.PaymentDataAccessLayer();
        Models.UserDataAccessLayer objuser = new Models.UserDataAccessLayer();
        Models.FineDataAccessLayer objfine = new Models.FineDataAccessLayer();
        Models.ReviewDataAccessLayer objreview = new Models.ReviewDataAccessLayer();

        [HttpPost]
        /*public IActionResult Search(String search) // USER
        {
            List<Models.Book> lstBook,lstBookSearch = new List<Models.Book>();
            lstBook = objbook.GetAllBooks().ToList();

            if (!String.IsNullOrEmpty(search))
            {
                foreach (var book in lstBook)
                {
                    if (book.Title.Contains(search,StringComparison.OrdinalIgnoreCase))
                    {
                        lstBookSearch.Add(book);
                    }
                }
            }
            return View(lstBookSearch);
        }*/
        public IActionResult Search(String search)
        {
            List<Models.Book> lstBook = new List<Models.Book>();
            lstBook = objbook.GetAllBooks().ToList();

            List<Models.ViewBook> lstViewBook = new List<Models.ViewBook>();
            if (!String.IsNullOrEmpty(search))
            {
                foreach (var book in lstBook)
                {
                    if (book.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        Models.ViewBook bookView = new Models.ViewBook();
                        bookView.Initiate(book);
                        lstViewBook.Add(bookView);
                    }
                }
            }
            return View(lstViewBook);
        }

        // AUTHOR

        public IActionResult AuthorIndex() //User
        {
            List<Models.Author> lstAuthor = new List<Models.Author>();
            lstAuthor = objauthor.GetAllAuthors().ToList();
            
            return View(lstAuthor);
        }

        [HttpGet]
        public IActionResult AuthorCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AuthorCreate([Bind] Models.Author author)
        {
            if (ModelState.IsValid)
            {
                objauthor.AddAuthor(author);
                return RedirectToAction("AuthorIndex");
            }
            return View(author);
        }

        [HttpGet]
        public IActionResult AuthorEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Author author = objauthor.GetAuthorData(id);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AuthorEdit(int id, [Bind] Models.Author author)
        {
            if (id != author.Author_ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objauthor.UpdateAuthor(author);
                return RedirectToAction("AuthorIndex");
            }
            return View(author);
        }

        [HttpGet]
        public IActionResult AuthorDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Author author = objauthor.GetAuthorData(id);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpGet]
        public IActionResult AuthorDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Author author = objauthor.GetAuthorData(id);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost, ActionName("AuthorDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult AuthorDeleteConfirmed(int? id)
        {
            objauthor.DeleteAuthor(id);
            return RedirectToAction("AuthorIndex");
        }

        //***********************************************-~0~-***********************************************
        //BOOK
        
        public IActionResult BookIndex() //User
        {
            objfine.auto_update();

            List<Models.Book> lstBook = new List<Models.Book>();
            lstBook = objbook.GetAllBooks().ToList();

            return View(lstBook);
        }

        [HttpGet]
        public IActionResult BookCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookCreate([Bind] Models.Book book)
        {
            if (ModelState.IsValid)
            {
                objbook.AddBook(book);
                return RedirectToAction("BookIndex");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult BookEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Book book = objbook.GetBookData(id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookEdit(int id, [Bind] Models.Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objbook.UpdateBook(book);
                return RedirectToAction("BookIndex");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult BookDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Book book = objbook.GetBookData(id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult BookDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Book book = objbook.GetBookData(id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("BookDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult BookDeleteConfirmed(int? id)
        {
            objbook.DeleteBook(id);
            return RedirectToAction("BookIndex");
        }

        //***********************************************-~0~-***********************************************
        //BOOK_AUTHOR
        
        public IActionResult Book_AuthorIndex() //User
        {
            List<Models.Book_Author> lstBAuthor = new List<Models.Book_Author>();
            lstBAuthor = objbauthor.GetAllBook_Authors().ToList();

            List<Models.ViewBook_Author> lstvBAuthor = new List<Models.ViewBook_Author>();
            foreach (Models.Book_Author book in lstBAuthor)
            {
                Models.ViewBook_Author viewBook_Author = new Models.ViewBook_Author();
                viewBook_Author.Book_Authors = book;
                viewBook_Author.Book_Title = objbook.GetBookData(book.BookID).Title;
                viewBook_Author.Author_Name = objauthor.GetAuthorData(book.AuthorID).Author_fname;
                lstvBAuthor.Add(viewBook_Author);
            }

            return View(lstvBAuthor);
        }

        [HttpGet]
        public IActionResult Book_AuthorCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book_AuthorCreate([Bind] Models.Book_Author bauthor)
        {
            if (ModelState.IsValid)
            {
                objbauthor.AddBook_Author(bauthor);
                return RedirectToAction("Book_AuthorIndex");
            }
            return View(bauthor);
        }

        [HttpGet]
        public IActionResult Book_AuthorDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Models.Book_Author> lstBAuthor = new List<Models.Book_Author>();
            lstBAuthor = objbauthor.GetBook_AuthorData(id).ToList();

            if (lstBAuthor == null)
            {
                return NotFound();
            }

            Models.ViewBook lstvBAuthor = new Models.ViewBook();
            lstvBAuthor.Book = objbook.GetBookData(id);
            lstvBAuthor.Authors = new List<Models.Author>();
            foreach (Models.Book_Author book in lstBAuthor)
            {
                lstvBAuthor.Authors?.Add(objauthor.GetAuthorData(book.AuthorID));
            }
            return View(lstvBAuthor);
        }

        [HttpGet]
        public IActionResult Book_AuthorDelete(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }
            Models.ViewBook_Author bauthor = new Models.ViewBook_Author();
            bauthor = objbauthor.GetBook_1AuthorData(id, id2);

            if (bauthor == null)
            {
                return NotFound();
            }
            return View(bauthor);
        }

        [HttpPost, ActionName("Book_AuthorDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult Book_AuthorDeleteConfirmed(int? id, int? id2)
        {
            int a = (int)id;
            int b = (int)id2;
            objbauthor.DeleteBook_Author(a, b);
            return RedirectToAction("Book_AuthorIndex");
        }

        //***********************************************-~0~-***********************************************
        //CATEGORY
        
        public IActionResult CategoryIndex() //User
        {
            List<Models.Category> lstCategory = new List<Models.Category>();
            lstCategory = objcategory.GetAllCategories().ToList();

            return View(lstCategory);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoryCreate([Bind] Models.Category category)
        {
            if (ModelState.IsValid)
            {
                objcategory.AddCategory(category);
                return RedirectToAction("CategoryIndex");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Category_BookDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Models.Book> lstBook = new List<Models.Book>();
            lstBook = objcategory.GetBooks_Category(id).ToList();

            if (lstBook == null)
            {
                return NotFound();
            }
            return View(lstBook);
        }

        //***********************************************-~0~-***********************************************
        //LEASE
        
        public IActionResult LeaseIndex() //ADMIN
        {
            List<Models.Lease> lstlease = new List<Models.Lease>();
            lstlease = objlease.GetAllLeases().ToList();

            return View(lstlease);
        }

        [HttpGet]
        public IActionResult LeaseCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LeaseCreate([Bind] Models.Lease lease)
        {
            var interval_date_to_lease = new TimeSpan(7, 0, 0, 0);
            lease.Expiry_date = lease.Lease_date.Add(interval_date_to_lease);

            if (lease.isValid())
            {
                objlease.AddLease(lease);
                return RedirectToAction("LeaseIndex");//page book? or lease details
            }
            return View(lease);
        }

        [HttpGet]
        public IActionResult LeaseDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Lease lease = objlease.GetLeaseData(id);

            if (lease == null)
            {
                return NotFound();
            }
            return View(lease);
        }

        [HttpGet]
        public IActionResult LeaseEdit(int? id) //ADMIN
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Lease lease = objlease.GetLeaseData(id);

            if (lease == null)
            {
                return NotFound();
            }
            return View(lease);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LeaseEdit(int id, [Bind] Models.Lease lease) //ADMIN
        {
            if (id != lease.SessionID)
            {
                return NotFound();
            }
            if (lease.isValid())
            {
                objlease.UpdateLease(lease);
                return RedirectToAction("LeaseDetails", new {id});
            }
            return View();
        }

        [HttpGet]
        public IActionResult LeaseDelete(int? id)//ADMIN
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Lease lease = objlease.GetLeaseData(id);

            if (lease == null)
            {
                return NotFound();
            }
            return View(lease);
        }

        [HttpPost, ActionName("LeaseDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult LeaseDeleteConfirmed(int? id)
        {
            objlease.DeleteLease(id);
            return RedirectToAction("LeaseIndex");
        }
        //***********************************************-~0~-***********************************************
        //USER
        

        public IActionResult UserIndex() //User
        {
            List<Models.User> lstUser = new List<Models.User>();
            lstUser = objuser.GetAllUsers().ToList();

            return View(lstUser);
        }

        [HttpGet]
        public IActionResult UserCreate() //User
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserCreate([Bind] Models.User user) //User
        {
            if (ModelState.IsValid)
            {
                objuser.AddUser(user);
                return RedirectToAction("UserIndex");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult UserEdit(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.User user = objuser.GetUserData(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserEdit(int id, [Bind] Models.User user) //User
        {
            if (id != user.ISSN)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objuser.UpdateUser(user);
                return RedirectToAction("UserDetails",new {id});
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult UserDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.User user = objuser.GetUserData(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult UserDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.User user = objuser.GetUserData(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult UserDeleteConfirmed(int? id)
        {
            objuser.DeleteUser(id);
            return RedirectToAction("UserIndex");
        }
        //***********************************************-~0~-***********************************************
        //PAYMENT
        

        public IActionResult PaymentIndex() //Admin
        {
            objfine.auto_update();
            List<Models.Payment> lstPayment = new List<Models.Payment>();
            lstPayment = objpayment.GetAllPayments().ToList();

            return View(lstPayment);
        }

        [HttpGet]
        public IActionResult PaymentCreate() //Admin
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentCreate([Bind] Models.Payment payment) //Admin
        {
            objfine.auto_update();
            if (ModelState.IsValid)
            {
                objpayment.AddPayment(payment);
                return RedirectToAction("PaymentIndex");
            }
            return View(payment);
        }

        [HttpGet]
        public IActionResult PaymentEdit(int? id) //ADMIN
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Payment payment = objpayment.GetPaymentData(id);

            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentEdit(int id, [Bind] Models.Payment payment) //admin
        {
            if (id != payment.Payment_ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objpayment.UpdatePayment(payment);
                return RedirectToAction("PaymentDetails", new { id });
            }
            return View(payment);
        }

        [HttpGet]
        public IActionResult PaymentDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Payment payment = objpayment.GetPaymentData(id);

            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpGet]
        public IActionResult PaymentDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Payment payment = objpayment.GetPaymentData(id);

            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost, ActionName("PaymentDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentDeleteConfirmed(int? id)
        {
            objpayment.DeletePayment(id);
            return RedirectToAction("PaymentIndex");
        }

        //***********************************************-~0~-***********************************************
        //Fine
        

        public IActionResult FineIndex() //Admin
        {
            List<Models.Fine> lstFine = new List<Models.Fine>();
            lstFine = objfine.GetAllFines().ToList();

            return View(lstFine);
        }

        [HttpGet]
        public IActionResult FineCreate() //Admin
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FineCreate([Bind] Models.Fine fine) //Admin
        {
            if (fine.Fine_amount == 0)
                fine.ini(fine.SessionID, fine.Fine_days);

            if (ModelState.IsValid)
            {
                objfine.AddFine(fine);
                return RedirectToAction("FineIndex");
            }
            return View(fine);
        }

        [HttpGet]
        public IActionResult FineEdit(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Fine fine = objfine.GetFineData(id);

            if (fine == null)
            {
                return NotFound();
            }
            return View(fine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FineEdit(int id, [Bind] Models.Fine fine) //admin
        {
            if (id != fine.SessionID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objfine.UpdateFine(fine);
                return RedirectToAction("FineDetails", new { id });
            }
            return View(fine);
        }

        [HttpGet]
        public IActionResult FineDetails(int? id) //User
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Fine fine = objfine.GetFineData(id);

            if (fine == null)
            {
                return NotFound();
            }
            return View(fine);
        }

        [HttpGet]
        public IActionResult FineDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Fine fine = objfine.GetFineData(id);

            if (fine == null)
            {
                return NotFound();
            }
            return View(fine);
        }

        [HttpPost, ActionName("FineDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult FineDeleteConfirmed(int? id)
        {
            objfine.DeleteFine(id);
            return RedirectToAction("FineIndex");
        }

        //***********************************************-~0~-***********************************************
        //Review

        public IActionResult ReviewIndex() //USER
        {
            List<Models.Review> lstReview = new List<Models.Review>();
            lstReview = objreview.GetAllReviews().ToList();

            return View(lstReview);
        }

        [HttpGet]
        public IActionResult ReviewCreate() //USER
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReviewCreate([Bind] Models.Review review) //USER
        {
            if (ModelState.IsValid)
            {
                objreview.AddReview(review);
                return RedirectToAction("ReviewIndex");
            }
            return View(review);
        }

        [HttpGet]
        public IActionResult ReviewEdit(int? id, int? id2) //ADMIN
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }
            Models.Review review = objreview.GetReviewData(id,id2);

            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReviewEdit([Bind] Models.Review review) //admin
        {
            if (ModelState.IsValid)
            {
                objreview.UpdateReview(review);
                return RedirectToAction("ReviewDetails", new { id=review.ISSNum, id2=review.IDBook });
            }
            return View(review);
        }

        [HttpGet]
        public IActionResult ReviewDetails(int? id, int? id2) //User
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }
            Models.Review review = objreview.GetReviewData(id,id2);

            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpGet]
        public IActionResult ReviewDelete(int? id, int? id2) // ADMIN
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }
            Models.Review review = objreview.GetReviewData(id, id2);

            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost, ActionName("ReviewDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult ReviewDeleteConfirmed(int? id, int? id2)
        {
            objreview.DeleteReview(id,id2);
            return RedirectToAction("ReviewIndex");
        }
    }
}
