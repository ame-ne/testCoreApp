using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testCoreApp.Models;
using testCoreApp.Models.ViewModels;

namespace testCoreApp.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 2;

        public BookController(IBookRepository repo)
        {
            repository = repo;
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
            return View();
        }
    }
}