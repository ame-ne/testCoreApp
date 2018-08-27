using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testCoreApp.Extentions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using testCoreApp.Infrastructure;

namespace testCoreApp.Models
{
    public class Book
    {
        //[BindNever]
        [DisableSearch]
        public Guid BookId { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательно")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Кол-во страниц")]
        [UIHint("number")]
        [Range(1, int.MaxValue, ErrorMessage = "Кол-во страниц должно быть больше 0")]
        [DisableSearch]
        public int PageSize { get; set; }

        [Display(Name = "Полочный индекс")]
        public string ShelfIndex { get; set; }

        [Display(Name = "Доступно шт.")]
        [UIHint("number")]
        [Range(0, 100, ErrorMessage = "Кол-во должно быть неотрицательным")]
        [DisableSearch]
        public int Available { get; set; }

        [NotMapped]
        [DisableSearch]
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
        [Display(Name = "Авторы")]
        public IEnumerable<Author> Authors => BookAuthor.Select(rel => rel.Author);

        private ICollection<BookGenre> BookGenre { get; } = new List<BookGenre>();

        [NotMapped]
        [JsonIgnore]
        [Display(Name = "Жанры")]
        public IEnumerable<Genre> Genres => BookGenre.Select(rel => rel.Genre);
    }
}
