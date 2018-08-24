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