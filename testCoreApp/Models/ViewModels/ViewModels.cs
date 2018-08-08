using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Models.ViewModels
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentGenre { get; set; }
    }

    public class AuthorListViewModel
    {
        public IEnumerable<Author> Authors { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
