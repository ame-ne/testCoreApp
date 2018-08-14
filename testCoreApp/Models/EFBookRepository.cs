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

        public Book DeleteBook(Guid bookId)
        {
            Book dbEntry = context.Books.FirstOrDefault(b => b.BookId == bookId);
            if (dbEntry != null)
            {
                context.Books.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveBook(Book book)
        {
            if (book.BookId == Guid.Empty)
            {
                book.BookId = Guid.NewGuid();
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.FirstOrDefault(b => b.BookId == book.BookId);
                if (dbEntry != null)
                {
                    dbEntry.Title = book.Title;
                    dbEntry.PageSize = book.PageSize;
                    dbEntry.ShelfIndex = book.ShelfIndex;
                    dbEntry.Description = book.Description;
                }
            }
            context.SaveChanges();
        }
    }
}
