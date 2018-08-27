using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using testCoreApp.Models;

namespace testCoreApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IBookRepository repository;
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;

        public AdminController(IBookRepository repo, UserManager<AppUser> manager, IPasswordHasher<AppUser> hasher)
        {
            repository = repo;
            userManager = manager;
            passwordHasher = hasher;
        }

        public ViewResult Index()
        {
            return View(repository.Books);
        }

        public ViewResult Edit(string bookRouteId)
        {
            return View(repository.Books.FirstOrDefault(b => b.BookRouteId == bookRouteId));
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var modState = ModelState.GetValidationState(nameof(Book.Title));
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

        public ViewResult UsersList()
        {
            return View(userManager.Users.ToList());
        }

        public async Task<IActionResult> EditUser(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(user.TempPassword))
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, user.TempPassword);
                    user.TempPassword = string.Empty;
                }
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Пользователь {user.UserName} сохранён";
                    return RedirectToAction("UsersList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(user);
        }

        public ViewResult CreateUser()
        {
            return View("EditUser", new AppUser(""));
        }
    }
}