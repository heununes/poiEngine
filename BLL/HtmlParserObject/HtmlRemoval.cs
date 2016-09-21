using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace poiEngine.BLL.HtmlParserObject
{
    /// <summary>
    /// Methods to remove HTML from strings.
    /// </summary>
    public static class HtmlRemoval
    {
        /// <summary>
        /// Compiled regular expression for performance.
        /// Param value ->  new Regex("<.*?>", RegexOptions.Compiled)
        /// </summary>
        static Regex _htmlRegex;

        public static Regex HtmlRegex
        {
            get
            {
                return _htmlRegex;
            }

            set
            {
                _htmlRegex = value;
            }
        }

        /// <summary>
        /// Remove HTML from string with Regex (string).
        /// </summary>
        public static string StripTagsRegex(string source, string regex)
        {
            return Regex.Replace(source, regex, string.Empty);
        }

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return HtmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }

}
