using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testCoreApp.Extentions;
using Newtonsoft.Json;

namespace testCoreApp.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public string GenreRouteId
        {
            get
            {
                var strId = GenreId.ToString();
                var idPart = strId.Substring(0, 8);
                return $"{Name.GetTranslit()}_{idPart}";
            }
        }

        private ICollection<BookGenre> BookGenre { get; } = new List<BookGenre>();
        [NotMapped]
        [JsonIgnore]
        public IEnumerable<Book> Books => BookGenre.Select(rel => rel.Book);
    }
}
