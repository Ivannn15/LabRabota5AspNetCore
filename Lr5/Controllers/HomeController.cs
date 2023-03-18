using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lr5.Models;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;

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
            var modelBooks = db.Books.Include(p => p.Author);

            return View(modelBooks);
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