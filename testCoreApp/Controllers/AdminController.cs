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
        private RoleManager<IdentityRole> roleManager;
        private IPasswordHasher<AppUser> passwordHasher;

        public AdminController(IBookRepository repo, UserManager<AppUser> uManager, RoleManager<IdentityRole> rManager, IPasswordHasher<AppUser> hasher)
        {
            repository = repo;
            userManager = uManager;
            roleManager = rManager;
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
        public async Task<IActionResult> EditUser(AppUser user, string[] role)
        {
            var userFromDB = await userManager.FindByIdAsync(user.Id);
            IdentityResult result = null;
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Id) || userFromDB == null)
                {
                    result = await userManager.CreateAsync(user, user.TempPassword);
                    if (result.Succeeded)
                    {
                        if (role != null && role.Length > 0)
                        {
                            var addedUser = await userManager.FindByNameAsync(user.UserName);
                            await ManageUserRoles(addedUser, role);
                            if (ModelState.IsValid)
                            {
                                TempData["message"] = $"Пользователь {user.UserName} создан";
                                return RedirectToAction("UsersList");
                            }
                        }
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(user.TempPassword))
                    {
                        userFromDB.PasswordHash = passwordHasher.HashPassword(user, user.TempPassword);
                        user.TempPassword = string.Empty;
                    }
                    userFromDB.SecurityStamp = Guid.NewGuid().ToString();
                    userFromDB.FirstName = user.FirstName;
                    userFromDB.MiddleName = user.MiddleName;
                    userFromDB.LastName = user.LastName;
                    userFromDB.UserName = user.UserName;
                    userFromDB.Email = user.Email;
                    result = await userManager.UpdateAsync(userFromDB);
                    if (result.Succeeded)
                    {
                        if (role != null && role.Length > 0)
                        {
                            var addedUser = await userManager.FindByNameAsync(user.UserName);
                            await ManageUserRoles(addedUser, role);
                            if (ModelState.IsValid)
                            {
                                TempData["message"] = $"Пользователь {user.UserName} сохранён";
                                return RedirectToAction("UsersList");
                            }
                        }
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            return View(user);
        }

        private async Task ManageUserRoles(AppUser user, string[] roles)
        {
            IdentityResult result = null;
            var userRoles = await userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) != null)
                {
                    if (!await userManager.IsInRoleAsync(user, role))
                    {
                        result = await userManager.AddToRoleAsync(user, role);
                    }
                    if (result != null && !result.Succeeded)
                    {
                        AddErrors(result);
                    }
                }
            }

            foreach(var userRole in userRoles)
            {
                if (!roles.Contains(userRole))
                {
                    result = await userManager.RemoveFromRoleAsync(user, userRole);
                }
                if (result != null && !result.Succeeded)
                {
                    AddErrors(result);
                }
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public ViewResult CreateUser()
        {
            return View("EditUser", new AppUser(""));
        }
    }
}