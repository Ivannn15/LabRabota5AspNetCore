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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = db.Books.FindAsync(id);
            if (item != null)
            {
                return NotFound();
            }
            db.Books.Remove(item.Result);
            db.SaveChangesAsync();

            return RedirectToAction("Index");
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
            return View(modelBooks);
        }

        [HttpPost]
        public IActionResult change(int id, Book book)
        {
            var originalData = db.Books.Find(id);

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