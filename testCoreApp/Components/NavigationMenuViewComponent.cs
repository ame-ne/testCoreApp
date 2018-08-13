using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testCoreApp.Models;

namespace testCoreApp.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IBookRepository repository;


        public NavigationMenuViewComponent(IBookRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenre = RouteData?.Values["genre"];
            return View(repository.Genres.OrderBy(x => x.Name));
        }
    }
}
