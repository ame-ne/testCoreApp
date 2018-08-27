using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using testCoreApp.Infrastructure;
using testCoreApp.Models;
using testCoreApp.Models.ViewModels;

namespace testCoreApp.Controllers
{
    //[Timer]
    //[CustomException]
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 2;
        private ILogger<BookController> logger;

        [ControllerContext]
        public ControllerContext cc { get; set; }

        public BookController(IBookRepository repo, ILogger<BookController> log)
        {
            repository = repo;
            logger = log;
        }

        public ViewResult List(string searchQuery, string genre, int page = 1)
        {
            var selectedBooks = repository.Books
#warning
                .ToList()
                .Where(x => genre == null || x.Genres.Any(g => g.GenreRouteId == genre));

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                var bookProps = typeof(Book).GetProperties().Where(x => x.GetCustomAttributes(typeof(DisableSearchAttribute), false).Count() == 0);
                selectedBooks = selectedBooks
                    .Where(book =>
                        bookProps.Any(property =>
                            (property.PropertyType.IsGenericType
                                ? (property.GetValue(book, null) as IEnumerable<object>)?.Any(value => value.ToString().ToLower().Contains(searchQuery))
                                : (property.GetValue(book, null)?.ToString().ToLower().Contains(searchQuery)))
                            ?? false));
                TempData["searchQuery"] = searchQuery;
            }

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

        //[HttpGet]
        //public ViewResult Search(string searchQuery, int page = 1)
        //{
        //    var books = repository.Books;
        //    var model = new BookListViewModel
        //    {
        //        Books = books.OrderBy(x => x.Title)
        //        .Skip((page - 1) * PageSize)
        //        .Take(PageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = PageSize,
        //            TotalItems = books.Count()
        //        }
        //    };
        //    return View(nameof(List), model);
        //}

        //[HttpsOnly]
        public IActionResult Index([FromServices]TestDependencyEntity ent, [FromHeader]string accept)
        {
            logger.LogDebug("111111111111111111111");
            ViewBag.Msg = "hi from dark side";            
            ViewBag.testEntGuid = ent.EntityGuid;
            ViewBag.testControllerContext = ControllerContext?.HttpContext?.Session?.Id == cc?.HttpContext?.Session?.Id;
            ViewBag.accept = accept;
            //return Json(new[] { "a", "b", "c"});
            int i = 0;
            //ViewBag.errorAction = 5 / i;
            ViewBag.currUser = HttpContext.User.Identity.Name;
            return View();            
        }
    }
}