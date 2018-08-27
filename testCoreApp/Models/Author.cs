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
    public class Author
    {
        public Guid AuthorId { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [NotMapped]
        public string AuthorRouteId
        {
            get
            {
                var strId = AuthorId.ToString();
                var idPart = strId.Substring(0, 8);
                return $"{this.GetShortName().GetTranslit()}_{idPart}";
            }
        }

        private ICollection<BookAuthor> BookAuthor { get; } = new List<BookAuthor>();

        [NotMapped]
        [JsonIgnore]
        [Display(Name = "Книги")]
        public IEnumerable<Book> Books => BookAuthor.Select(rel => rel.Book);

        public override string ToString()
        {
            return this.GetFullName();
        }
    }
}
