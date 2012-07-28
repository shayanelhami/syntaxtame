using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Highlighter
{
    static class LanguageExtensions
    {
        #region Keywords
        static string[] Keywords = new string[] {
		    "abstract","event","new","struct","as","explicit","null","switch","base","extern","object","this","bool",
            "false","operator","throw","break","finally","out","true","byte","fixed","override","try","case","float",
            "params","typeof","catch","for","private","uint","char","foreach","protected","ulong","checked","goto",
            "public","unchecked","class","if","readonly","unsafe","const","implicit","ref","ushort","continue","in",
            "return","using","decimal","int","sbyte","virtual","default","interface","sealed","volatile","delegate",
            "internal","short","void","do","is","sizeof","while","double","lock","stackalloc","else","long",
            "static","enum","namespace","string", "var", "partial"
		};
        #endregion

        public static bool IsString(this string text)
        {
            return text.StartsWith("\"") || text.StartsWith("'");
        }

        public static bool IsKeyword(this string text)
        {
            return Keywords.Contains(text);
        }
    }
}
