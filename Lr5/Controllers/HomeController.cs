using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lr5.Models;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Lr5.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db;
        public HomeController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var modelBooks = db.Books.Include(p => p.Author).OrderBy(p => p.BookId);

            return View(modelBooks);
        }

        //[HttpPost]
        //public IActionResult Delete(int? id)
        //{
        //    Book book = db.Books.Single(p => p.BookId == id);
        //    if (book)
        //    {

        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var item = db.Books.FindAsync(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    db.Books.Remove(item.Result);
        //    db.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult addAuthor(Author author)
        {
            int Max = db.Authors.Max(p => p.AuthorId);
            author.AuthorId = Max+1;
            db.Authors.Add(author);
            db.SaveChanges();
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult addAuthor()
        {
            return View(db.Authors);
        }

        [HttpPost]
        public IActionResult DeleteAuthor(int id)
        {
            var item = db.Authors.Find(id);
            db.Authors.Remove(item);
            db.SaveChanges();
            return Redirect("addBook");
        }

        //[HttpGet]
        //public IActionResult IndexSortedId() // разобраться с сортировкой
        //{
        //    var modelBooks = db.Books.Include(p => p.Author).OrderBy(p => p.Title);

        //    return View(modelBooks);
        //}

        public IActionResult IndexSorted(string sortOrder)
        {
            ViewData["idSortParam"] = sortOrder == "id" ? "id_desc" : "id";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["TitleSortParam"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["DescriptionSortParam"] = sortOrder == "Description" ? "description_desc" : "Description";
            ViewData["AuthorSortParam"] = sortOrder == "Author" ? "author_desc" : "Author";
            var books = from b in db.Books.Include(p => p.Author)
                select b;
            switch (sortOrder)
            {
                case "id":
                    books = books.OrderByDescending(b => b.BookId);
                    break;
                case "id_desc":
                    books = books.OrderBy(b => b.BookId);
                    break;
                case "Title":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "Title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Date":
                    books = books.OrderBy(b => b.PublishedOn);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(b => b.PublishedOn);
                    break;
                case "Description":
                    books = books.OrderBy(b => b.PublishedOn);
                    break;
                case "description_desc":
                    books = books.OrderByDescending(b => b.PublishedOn);
                    break;
                case "Author":
                    books = books.OrderBy(b => b.Author.Name);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                default:
                    break;
            }
            return View("Index", books);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = db.Books.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            db.Books.Remove(item);
            db.SaveChanges();
            return Redirect("Index");
        }


        [HttpGet]
        public IActionResult addBook()
        {
            var modelBooks = db.Authors;
            return View(modelBooks);
        }

        [HttpPost]
        public IActionResult addBook([FromForm] int AuthorId, Book book)
        {
           
            book.Author = db.Authors.Single(p => p.AuthorId == AuthorId);
            int maxId = db.Books.Max(p => p.BookId);
            book.BookId = maxId + 1;
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult change(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.BookID = id;
            var modelBooks = db.Books.Include(p => p.Author).Where(p => p.BookId == id).Single();
            var modelAuthors = db.Authors;
            var viewModel = new MyViewModel
            {
                Authors = db.Authors,
                Book = modelBooks
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult changeAuthor(int? id)
        {
            Author tempAuthor = db.Authors.Single(p => p.AuthorId == id);
            return View(tempAuthor);
        }

        [HttpPost]
        public IActionResult changeAuthor(int id,Author author)
        {
            var originalData = db.Authors.Find(id);

            if (originalData == null)
            {
                return NotFound();
            }

            originalData.Name = author.Name;
            originalData.WebUrl = author.WebUrl;

            db.SaveChanges();
            return RedirectToAction("addBook");
        }


        [HttpPost]
        public IActionResult change([FromForm] int AuthorId, int id, Book book)
        {
            var originalData = db.Books.Find(id);

            if (AuthorId != null)
            {
                originalData.Author = db.Authors.SingleOrDefault(p => p.AuthorId == AuthorId);
            }

            if (originalData == null)
            {
                return NotFound();
            }

            originalData.Title = book.Title;
            originalData.Description = book.Description;
            originalData.PublishedOn = book.PublishedOn;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        //[HttpGet]
        //public IActionResult change(string? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //    }
        //}
    }
}