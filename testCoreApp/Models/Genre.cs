using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testCoreApp.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        [Required]
        public string Name { get; set; }

        private ICollection<BookGenre> BookGenre { get; } = new List<BookGenre>();
        [NotMapped]
        public IEnumerable<Book> Books => BookGenre.Select(rel => rel.Book);
    }
}
