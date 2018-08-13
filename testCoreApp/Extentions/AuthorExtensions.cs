using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testCoreApp.Models;

namespace testCoreApp.Extentions
{
    public static class AuthorExtensions
    {
        public static string GetFullName(this Author author)
        {
            return $"{author?.LastName} {author?.FirstName} {author?.MiddleName}";
        }

        public static string GetShortName(this Author author, string symbolIfEmpty = "")
        {
            if (author == null)
            {
                return symbolIfEmpty;
            }

            return $"{(!string.IsNullOrEmpty(author.LastName) ? author.LastName : "")}" +
                $"{(!string.IsNullOrEmpty(author.FirstName) ? " " + author.FirstName.Substring(0, 1) + "." : "")}" +
                $"{(!string.IsNullOrEmpty(author.MiddleName) ? " " + author.MiddleName.Substring(0, 1) + "." : "")}";
        }
    }
}
