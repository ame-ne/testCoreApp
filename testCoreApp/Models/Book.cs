﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testCoreApp.Extentions;
using Newtonsoft.Json;

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
