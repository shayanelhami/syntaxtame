using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory;

namespace Highlighter
{
    public class Code
    {
        string SourceCode;
        ReadOnlyDocument Document;
        List<Insertion> Insertions;

        public Code(string sourceCode)
        {
            SourceCode = sourceCode;
            Insertions = new List<Insertion>();
        }

        private string _Highlighted;
        public string Highlighted
        {
            get
            {
                return _Highlighted ?? (_Highlighted = GetHighlighted());
            }
        }

        private string GetHighlighted()
        {
            var parser = new CSharpParser();

            var nodes = parser.ParseSnippet(ref SourceCode);
            Document = new ReadOnlyDocument(SourceCode);

            foreach (var element in nodes)
            {
                Walk(element);
            }

            var merged = Merge();
            var cleaned = ExtraCode.Remove(merged);

            return cleaned;
        }

        private void Walk(AstNode node)
        {
            var phrase = FindPhrase(node);
            if (phrase != Phrase.Unknwon)
            {
                AddInsertion(node, phrase);
            }

            foreach (var child in node.Children)
            {
                Walk(child);
            }
        }

        private static Phrase FindPhrase(AstNode node)
        {
            var text = node.GetText();

            if (node.Role == Roles.Comment)
                return Phrase.Comment;

            if (node.Role == Roles.Type)
            {
                var type = (node as PrimitiveType);
                if (type != null)
                    return (type.Keyword != null) ? Phrase.Keyword : Phrase.Type;

                // some keywords can be type like "var" which our parser does not recognise
                return text.IsKeyword() ? Phrase.Keyword : Phrase.Type;
            }

            if (node is PrimitiveExpression)
            {
                if (text.IsString())
                    return Phrase.String;
            }

            if (node is CSharpTokenNode)
            {
                if (text.IsKeyword())
                    return Phrase.Keyword;
            }

            return Phrase.Unknwon;
        }

        private void AddInsertion(AstNode node, Phrase phrase)
        {
            // start tag
            Insertions.Add(new Insertion
            {
                Offset = Document.GetOffset(node.StartLocation),
                Phrase = String.Format("<span class='{0}'>", phrase.ToCss())
            });

            // end tag
            Insertions.Add(new Insertion
            {
                Offset = Document.GetOffset(node.EndLocation),
                Phrase = "</span>"
            });
        }

        private string Merge()
        {
            var sb = new StringBuilder();

            for (int index = 0; index < Document.TextLength; index++)
            {
                var insertions = Insertions.Where(ins => ins.Offset == index);
                foreach (var ins in insertions)
                {
                    sb.Append(ins.Phrase);
                }

                AppendHtmlSafeCharacter(sb, index);
            }

            var appends = Insertions.Where(ins => ins.Offset >= Document.TextLength).OrderBy(ins => ins.Offset);
            foreach (var ins in appends)
            {
                sb.Append(ins.Phrase);
            }

            return sb.ToString();
        }

        private void AppendHtmlSafeCharacter(StringBuilder sb, int index)
        {
            var ch = Document.GetCharAt(index);

            if (ch == '<')
            {
                sb.Append("&lt;");
            } else if (ch == '>')
            {
                sb.Append("&gt;");
            } else
            {
                sb.Append(ch);
            }
        }

    }
}



