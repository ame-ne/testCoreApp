using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testCoreApp.Extentions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace testCoreApp.Models
{
    public class Book
    {
        //[BindNever]
        public Guid BookId { get; set; }
        [Required(ErrorMessage = "Обязательно")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Кол-во страниц должно быть больше 0")]
        public int PageSize { get; set; }
        public string ShelfIndex { get; set; }
        [NotMapped]
        public string BookRouteId
        {
            get
            {
                var strId = BookId.ToString();
                var idPart = strId.Substring(0, 8);
                return $"{Title.GetTranslit()}_{idPart}";
            }
        }

        private ICollection<BookAuthor> BookAuthor { get; } = new List<BookAuthor>();
        [NotMapped]
        [JsonIgnore]
        public IEnumerable<Author> Authors => BookAuthor.Select(rel => rel.Author);

        private ICollection<BookGenre> BookGenre { get; } = new List<BookGenre>();
        [NotMapped]
        [JsonIgnore]
        public IEnumerable<Genre> Genres => BookGenre.Select(rel => rel.Genre);
    }
}
