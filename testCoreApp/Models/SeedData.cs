using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new Author
                    {
                        AuthorId = Guid.NewGuid(),
                        LastName = "Толстой",
                        FirstName = "Лев",
                        MiddleName = "Николаевич"
                    },
                    new Author
                    {
                        AuthorId = Guid.NewGuid(),
                        LastName = "Сартр",
                        FirstName = "Жан-Поль"
                    },
                    new Author
                    {
                        AuthorId = Guid.NewGuid(),
                        LastName = "Гейман",
                        FirstName = "Нил"
                    },
                    new Author
                    {
                        AuthorId = Guid.NewGuid(),
                        LastName = "Стругацкий",
                        FirstName = "Аркадий",
                        MiddleName = "Натанович"
                    },
                    new Author
                    {
                        AuthorId = Guid.NewGuid(),
                        LastName = "Стругацкий",
                        FirstName = "Борис",
                        MiddleName = "Натанович"
                    }
                );
            }
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(
                    new Genre()
                    {
                        GenreId = Guid.NewGuid(),
                        Name = "Роман"
                    },
                    new Genre()
                    {
                        GenreId = Guid.NewGuid(),
                        Name = "Сказки"
                    },
                    new Genre()
                    {
                        GenreId = Guid.NewGuid(),
                        Name = "Фантастическая повесть"
                    }
                );
            }
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book()
                    {
                        BookId = Guid.NewGuid(),
                        Title = "Война и мир",
                        Description = "Большая книжка, угу",
                        PageSize = 100500,
                        ShelfIndex = "T001"                        
                    },
                    new Book()
                    {
                        BookId = Guid.NewGuid(),
                        Title = "Тошнота",
                        Description = "Депрессивная книжка",
                        PageSize = 100,
                        ShelfIndex = "С001"
                    }, 
                    new Book()
                    {
                        BookId = Guid.NewGuid(),
                        Title = "Американские боги",
                        Description = "Надеюсь, когда-нибудь прочитаю",
                        PageSize = 452,
                        ShelfIndex = "Н001"
                    },
                    new Book()
                    {
                        BookId = Guid.NewGuid(),
                        Title = "Пикник на обочине",
                        Description = "Грустная книжка",
                        PageSize = 202,
                        ShelfIndex = "С002"
                    }
                );


                context.SaveChanges();
                //TODO: это делается красивее?
                //ElementAt от IQueryable not supported..
                context.AddRange(
                    new BookAuthor { Book = context.Books.FirstOrDefault(x => x.Title == "Война и мир"), Author = context.Authors.FirstOrDefault(x => x.LastName == "Толстой") },
                    new BookGenre { Book = context.Books.FirstOrDefault(x => x.Title == "Война и мир"), Genre = context.Genres.FirstOrDefault(x => x.Name == "Роман") },

                    new BookAuthor { Book = context.Books.FirstOrDefault(x => x.Title == "Тошнота"), Author = context.Authors.FirstOrDefault(x => x.LastName == "Сартр") },
                    new BookGenre { Book = context.Books.FirstOrDefault(x => x.Title == "Тошнота"), Genre = context.Genres.FirstOrDefault(x => x.Name == "Роман") },

                    new BookAuthor { Book = context.Books.FirstOrDefault(x => x.Title == "Американские боги"), Author = context.Authors.FirstOrDefault(x => x.LastName == "Гейман") },
                    new BookGenre { Book = context.Books.FirstOrDefault(x => x.Title == "Американские боги"), Genre = context.Genres.FirstOrDefault(x => x.Name == "Роман") },
                    new BookGenre { Book = context.Books.FirstOrDefault(x => x.Title == "Американские боги"), Genre = context.Genres.FirstOrDefault(x => x.Name == "Сказки") },

                    new BookAuthor { Book = context.Books.FirstOrDefault(x => x.Title == "Пикник на обочине"), Author = context.Authors.FirstOrDefault(x => x.LastName == "Стругацкий" && x.FirstName == "Борис") },
                    new BookAuthor { Book = context.Books.FirstOrDefault(x => x.Title == "Пикник на обочине"), Author = context.Authors.FirstOrDefault(x => x.LastName == "Стругацкий" && x.FirstName == "Аркадий") },
                    new BookGenre { Book = context.Books.FirstOrDefault(x => x.Title == "Пикник на обочине"), Genre = context.Genres.FirstOrDefault(x => x.Name == "Фантастическая повесть") }
                );
            }
            context.SaveChanges();
        }
    }
}
