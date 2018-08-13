using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Models
{
    public class FakeBookRepository //: IBookRepository
    {
        public IQueryable<Book> Books => new List<Book>{
            //new Book()
            //{
            //    BookId = new Guid(),
            //    Title ="T1",
            //    Description = "t1D",
            //    PageSize = 10,
            //    Authors = new List<Author>
            //    {
            //        new Author()
            //        {
            //            AuthorId = new Guid(),
            //            FirstName = "A1F",
            //            LastName = "A1L",
            //            MiddleName = "A1M"
            //        }
            //    },
            //    Genres = new List<Genre>
            //    {
            //        new Genre()
            //        {
            //            GenreId = new Guid(),
            //            Name = "G1"
            //        }
            //    }
            //},
            //new Book()
            //{
            //    BookId = new Guid(),
            //    Title ="T2",
            //    Description = "t2D",
            //    PageSize = 20,
            //    Authors = new List<Author>
            //    {
            //        new Author()
            //        {
            //            AuthorId = new Guid(),
            //            FirstName = "A2F",
            //            LastName = "A2L",
            //            MiddleName = "A2M"
            //        }
            //    },
            //    Genres = new List<Genre>
            //    {
            //        new Genre()
            //        {
            //            GenreId = new Guid(),
            //            Name = "G2"
            //        }
            //    }
            //}
        }.AsQueryable<Book>();

        public IQueryable<Author> Authors => new List<Author>().AsQueryable();
    }
}
