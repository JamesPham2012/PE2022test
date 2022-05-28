using Microsoft.AspNetCore.Mvc;
using PE2022test.Models;
using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace PE2022test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Models.AuthorDataAccessLayer objauthor = new Models.AuthorDataAccessLayer();
        Models.BookDataAccessLayer objbook = new Models.BookDataAccessLayer();
        Models.Book_AuthorDataAccessLayer objbauthor = new Models.Book_AuthorDataAccessLayer();
        Models.CategoryDataAccessLayer objcategory = new Models.CategoryDataAccessLayer();
        Models.LeaseDataAccessLayer objlease = new Models.LeaseDataAccessLayer();
        Models.PaymentDataAccessLayer objpayment = new Models.PaymentDataAccessLayer();
        Models.UserDataAccessLayer objuser = new Models.UserDataAccessLayer();
        Models.FineDataAccessLayer objfine = new Models.FineDataAccessLayer();
        Models.ReviewDataAccessLayer objreview = new Models.ReviewDataAccessLayer();

        //public HomeController(ILogger<HomeController> logger)
        //{
        //   _logger = logger;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Searchtest(String search) 
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

        //Author
        public IActionResult AuthorIndex() 
        {
            List<Models.Author> lstAuthor = new List<Models.Author>();
            lstAuthor = objauthor.GetAllAuthors().ToList();

            return View(lstAuthor);
        }


        //Book
        public IActionResult BookIndex() //User
        {

            List<Models.Book> lstBook = new List<Models.Book>();
            lstBook = objbook.GetAllBooks().ToList();

            List<Models.ViewBook> lstViewBook = new List<Models.ViewBook>();
            foreach(var book in lstBook)
            {
                Models.ViewBook bookView = new Models.ViewBook();
                bookView.Initiate(book);
                lstViewBook.Add(bookView);
            }

            return View(lstViewBook);
        }

        public IActionResult BookIndex1()
        {
            List<Models.Book> lstBook = new List<Models.Book>();
            lstBook = objbook.GetAllBooks().ToList();

            List<Models.ViewBook> lstViewBook = new List<Models.ViewBook>();
            foreach (var book in lstBook)
            {
                Models.ViewBook bookView = new Models.ViewBook();
                bookView.Initiate(book);
                lstViewBook.Add(bookView);
            }

            return View(lstViewBook);
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

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                List<User> lstuser = objuser.GetAllUsers().ToList();
                var check = lstuser.Where(s => s.ISSN.Equals(_user.ISSN));
                if (check == null)
                {
                    //_user.pass = GetMD5(_user.pass);
                    objuser.AddUser(_user);

                    //add session
                    HttpContext.Session.SetInt32("ISSN", _user.ISSN);

                    //test
                    int? id = HttpContext.Session.GetInt32("ISSN");
                    ViewBag.Message = "ID : " + id;

                    return RedirectToAction("BookIndex1"); //"User",_user
                }
                else
                {
                    ViewBag.Message = "This ISSN already exists";
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                // password = GetMD5(password);
                List<User> lstuser = objuser.GetAllUsers().ToList();
                var data = lstuser.Where(s => s.Email.Equals(email) && s.Pass.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    HttpContext.Session.SetInt32("ISSN", data.FirstOrDefault().ISSN);

                    //test
                    int? id = HttpContext.Session.GetInt32("ISSN");
                    ViewBag.Message = "ID : " + id;
                    //return RedirectToAction("BookIndex");//"User",_user
                }
                else
                {
                    ViewBag.Message = "Login failed";
                    //return RedirectToAction("BookIndex");//"Login"
                }
            }
            return View();
        }
    }
}