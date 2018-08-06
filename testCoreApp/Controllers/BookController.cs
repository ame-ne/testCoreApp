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

        public BookController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Books);

        public IActionResult Index()
        {
            return View();
        }
    }
}