using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testCoreApp.Models;
using testCoreApp.Models.ViewModels;

namespace testCoreApp.Controllers
{
    public class AuthorController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 4;

        public AuthorController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int page = 1) =>
            View(new AuthorListViewModel
            {
                Authors = repository.Authors
                    .OrderBy(x => x.LastName)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Authors.Count()
                }
            });

        public IActionResult Index()
        {
            return View();
        }
    }
}