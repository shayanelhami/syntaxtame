using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.CSharp;
using System.IO;

namespace Highlighter
{
    static class SinppetParser
    {
        public static IEnumerable<AstNode> ParseSnippet(this CSharpParser parser, ref string sourceCode)
        {
            IEnumerable<AstNode> result;
            // if we are lucky it's a full source code
            if (TryParseAsFullBody(parser, out result, sourceCode))
                return result;

            // or it can be a bunch of class members
            if (TryParseAsClassBody(parser, out result, ref sourceCode))
                return result;

            // no? then try it as a method body and hope for the best
            return ParseAsMethodBody(parser, ref sourceCode);
        }

        private static bool TryParseAsFullBody(CSharpParser parser, out IEnumerable<AstNode> nodes, string sourceCode)
        {
            using (var reader = new StringReader(sourceCode))
            {
                var result = parser.Parse(reader, "");
                if (!parser.HasErrors)
                {
                    nodes = result.Children;
                    return true;
                }
            }

            nodes = null;
            return false;
        }

        private static bool TryParseAsClassBody(CSharpParser parser, out IEnumerable<AstNode> nodes, ref string sourceCode)
        {
            var code = ExtraCode.Add(
                before: "unsafe partial class MyClass { ",
                code: sourceCode,
                after: "}");

            var result = parser.Parse(new StringReader(code), "");
            if (!parser.HasErrors)
            {
                sourceCode = code;
                nodes = result.Children;
                return true;
            }

            nodes = null;
            return false;
        }

        private static IEnumerable<AstNode> ParseAsMethodBody(CSharpParser parser, ref string sourceCode)
        {
            sourceCode = ExtraCode.Add(
                before: "unsafe partial class MyClass { void M() { ",
                code: sourceCode,
                after: "}}");
            return parser.Parse(new StringReader(sourceCode), "").Children;
        }



    }
}
