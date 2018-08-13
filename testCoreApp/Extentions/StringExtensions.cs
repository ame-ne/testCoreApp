using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCoreApp.Extentions
{
    public static class StringExtensions
    {
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
