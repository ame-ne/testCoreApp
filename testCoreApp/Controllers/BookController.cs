using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using testCoreApp.Models;
using testCoreApp.Models.ViewModels;

namespace testCoreApp.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 2;
        private ILogger<BookController> logger;

        public BookController(IBookRepository repo, ILogger<BookController> log)
        {
            repository = repo;
            logger = log;
        }

        public ViewResult List(string genre, int page = 1) {
            var selectedBooks = repository.Books
#warning
                .ToList()
                .Where(x => genre == null || x.Genres.Any(g => g.GenreRouteId == genre));

            return View(new BookListViewModel
            {
                Books = selectedBooks
                    .OrderBy(x => x.Title)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = selectedBooks.Count()
                },
                CurrentGenre = genre
            });
        }

        public IActionResult Index()
        {
            logger.LogDebug("111111111111111111111");
            ViewBag.Msg = "hi from dark side";
            return View();
        }
    }
}