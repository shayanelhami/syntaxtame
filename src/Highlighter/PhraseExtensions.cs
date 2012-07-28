using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Highlighter
{
    static class PhraseExtensions
    {
        public static string ToCss(this Phrase phrase)
        {
            return phrase.ToString().ToLower();
        }
    }
}
