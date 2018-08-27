using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testCoreApp.Models;

namespace testCoreApp.Extentions
{
    public static class StringExtensions
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

        public static string GetFullName(this AppUser user)
        {
            return $"{user?.LastName} {user?.FirstName} {user?.MiddleName}";
        }

        public static string GetShortName(this AppUser user, string symbolIfEmpty = "")
        {
            if (user == null)
            {
                return symbolIfEmpty;
            }

            return $"{(!string.IsNullOrEmpty(user.LastName) ? user.LastName : "")}" +
                $"{(!string.IsNullOrEmpty(user.FirstName) ? " " + user.FirstName.Substring(0, 1) + "." : "")}" +
                $"{(!string.IsNullOrEmpty(user.MiddleName) ? " " + user.MiddleName.Substring(0, 1) + "." : "")}";
        }

        private static Dictionary<char, string> data = new Dictionary<char, string>
        {
            { 'а', "a"},
            { 'б', "b"},
            { 'в', "v"},
            { 'г', "g"},
            { 'д', "d"},
            { 'е', "e"},
            { 'ё', "yo"},
            { 'ж', "zh"},
            { 'з', "z"},
            { 'и', "i"},
            { 'й', "i"},
            { 'к', "k"},
            { 'л', "l"},
            { 'м', "m"},
            { 'н', "n"},
            { 'о', "o"},
            { 'п', "p"},
            { 'р', "r"},
            { 'с', "s"},
            { 'т', "t"},
            { 'у', "u"},
            { 'ф', "f"},
            { 'х', "h"},
            { 'ц', "c"},
            { 'ч', "ch"},
            { 'ш', "sh"},
            { 'щ', "shch"},
            { 'ь', ""},
            { 'ы', "yi"},
            { 'ъ', ""},
            { 'э', "eh"},
            { 'ю', "yu"},
            { 'я', "ya"},
            { ' ', "_"},
        };

        public static string GetTranslit(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            input = input.ToLower();
            var chars = input.Select(x => x).Distinct();
            foreach(var c in chars)
            {
                input = data.ContainsKey(c) ? input.Replace(c.ToString(), data[c]) : input.Replace(c.ToString(), "");
            }
            return input;
        }
    }
}
