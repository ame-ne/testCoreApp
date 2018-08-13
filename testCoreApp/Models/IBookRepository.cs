using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Models
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }
        IQueryable<Author> Authors { get; }
        IQueryable<Genre> Genres { get; }
    }
}
