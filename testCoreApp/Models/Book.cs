using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testCoreApp.Models
{
    public class Book
    {
        public Guid BookId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; }
        public string ShelfIndex { get; set; }

        private ICollection<BookAuthor> BookAuthor { get; } = new List<BookAuthor>();
        [NotMapped]
        public IEnumerable<Author> Authors => BookAuthor.Select(rel => rel.Author);

        private ICollection<BookGenre> BookGenre { get; } = new List<BookGenre>();
        [NotMapped]
        public IEnumerable<Genre> Genres => BookGenre.Select(rel => rel.Genre);
    }
}
