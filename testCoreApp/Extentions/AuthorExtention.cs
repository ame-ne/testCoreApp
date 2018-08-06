using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testCoreApp.Models;

namespace testCoreApp.Extentions
{
    public static class AuthorExtention
    {
        public static string GetFullName(this Author author)
        {
            return $"{author?.LastName} {author?.FirstName} {author?.MiddleName}";
        }

        public static string GetShortName(this Author author)
        {
            return author.GetFullName();
        }
    }
}
