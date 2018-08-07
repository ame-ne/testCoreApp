using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testCoreApp.Models;

namespace testCoreApp.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 1;

        public BookController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int bookPage = 1) => View(
            repository.Books
            .OrderBy(x=>x.Title)
            .Skip((bookPage- 1) * PageSize)
            .Take(PageSize));

        public IActionResult Index()
        {
            return View();
        }
    }
}