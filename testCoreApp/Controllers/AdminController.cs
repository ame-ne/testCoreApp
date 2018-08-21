using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using testCoreApp.Models;

namespace testCoreApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IBookRepository repository;

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Books);
        }

        public ViewResult Edit(string bookId)
        {
            return View(repository.Books.FirstOrDefault(b => b.BookRouteId == bookId));
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBook(book);
                TempData["message"] = $"{book.Title} сохранена";
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }

        public ViewResult Create() => View("Edit", new Book());

        [HttpPost]
        public IActionResult Delete(Guid bookId)
        {
            Book book = repository.DeleteBook(bookId);
            if (book != null)
            {
                TempData["message"] = $"{book.Title} удалена";
            }
            return RedirectToAction("Index");
        }
    }
}