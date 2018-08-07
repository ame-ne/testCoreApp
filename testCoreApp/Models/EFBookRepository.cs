using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Models
{
    public class EFBookRepository : IBookRepository
    {
        private ApplicationDbContext context;

        public EFBookRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Book> Books => context.Books.Include("BookAuthor.Author").Include("BookGenre.Genre");
        public IQueryable<Author> Authors => context.Authors.Include("BookAuthor.Book");
        public IQueryable<Genre> Genres => context.Genres.Include("BookGenre.Book");
    }
}
