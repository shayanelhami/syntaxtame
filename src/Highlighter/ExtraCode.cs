namespace Highlighter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class ExtraCode
    {
        public const string BeginTag = "/*[[*/";
        public const string EndTag = "/*]]*/";

        public static string Add(string before, string code, string after)
        {
            var sb = new StringBuilder();

            sb.Append(BeginTag);
            sb.Append(before);
            sb.Append(EndTag);
            sb.Append(code);
            sb.Append(BeginTag);
            sb.Append(after);
            sb.Append(EndTag);

            return sb.ToString();
        }

        public static string Remove(string code)
        {
            var spannedBeginTag = String.Format("<span class='{0}'>{1}</span>", Phrase.Comment.ToCss(), BeginTag);
            var spannedEndTag = String.Format("<span class='{0}'>{1}</span>", Phrase.Comment.ToCss(), EndTag);

            // for removing successfully recognised and marked as comments
            code = Remove(code, spannedBeginTag, spannedEndTag);

            // in case there were errors and highlighter was unable to recognise as comment:
            code = Remove(code, BeginTag, EndTag);

            return code;
        }

        private static string Remove(string code, string beginTag, string endTag)
        {
            int startIndex;
            while ((startIndex = code.IndexOf(beginTag)) != -1)
            {
                var endIndex = code.IndexOf(endTag, startIndex) + endTag.Length;
                code = code.Remove(startIndex, endIndex - startIndex);
            }

            return code;
        }
    }
}
