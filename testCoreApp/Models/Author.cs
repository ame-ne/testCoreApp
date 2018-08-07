using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testCoreApp.Models
{
    public class Author
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        private ICollection<BookAuthor> BookAuthor { get; } = new List<BookAuthor>();
        [NotMapped]
        public IEnumerable<Book> Books => BookAuthor.Select(rel => rel.Book);
    }
}
