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
        public static string GetHighlighted(string sourceCode) {
            var parser = new CSharpParser();
            var insertions = new List<Insertion>();
            var doc = new ReadOnlyDocument(sourceCode);
            var compilationUnit = parser.Parse(doc, "");

            foreach (var element in compilationUnit.Children) {
                Walk(element, doc, insertions);
            }

            return Merge(doc, insertions);
        }

        #region Keywords
        static string[] Keywords = new string[] {
		    "abstract","event","new","struct","as","explicit","null","switch","base","extern","object","this","bool",
            "false","operator","throw","break","finally","out","true","byte","fixed","override","try","case","float",
            "params","typeof","catch","for","private","uint","char","foreach","protected","ulong","checked","goto",
            "public","unchecked","class","if","readonly","unsafe","const","implicit","ref","ushort","continue","in",
            "return","using","decimal","int","sbyte","virtual","default","interface","sealed","volatile","delegate",
            "internal","short","void","do","is","sizeof","while","double","lock","stackalloc"," ","else","long",
            "static"," ","enum","namespace","string"
		};
        #endregion

        private static void Walk(AstNode node, IDocument doc, List<Insertion> insertions) {
            string phrase = null;

            if (node.Role == Roles.Comment) {
                phrase = "comment";
            } else if (node.Role == Roles.Type) {
                phrase = "type";

                var type = (node as PrimitiveType);
                if (type != null && type.Keyword != null)
                    phrase = "keyword";
            } else if (node is PrimitiveExpression) {
                if (node.GetText().StartsWith("\"") ||
                    node.GetText().StartsWith("'")) {
                    phrase = "string";
                }
            } else if (node is CSharpTokenNode) {
                if (Keywords.Contains(node.GetText()))
                    phrase = "keyword";
            }

            if (phrase != null) {
                insertions.Add(new Insertion {
                    Offset = doc.GetOffset(node.StartLocation),
                    Phrase = String.Format("<span class='{0}'>", phrase)
                });

                insertions.Add(new Insertion {
                    Offset = doc.GetOffset(node.EndLocation),
                    Phrase = "</span>"
                });

            }

            foreach (var child in node.Children) {
                Walk(child, doc, insertions);
            }
        }

        private static string Merge(ReadOnlyDocument doc, List<Insertion> insertions) {
            var sb = new StringBuilder();

            for (int index = 0; index < doc.TextLength; index++) {
                var insertion = insertions.FirstOrDefault(ins => ins.Offset == index);
                if (insertion != null) {
                    sb.Append(insertion.Phrase);
                }

                var ch = doc.GetCharAt(index);
                if (ch == '<') {
                    sb.Append("&lt;");
                } else if (ch == '>') {
                    sb.Append("&gt;");
                } else {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }
    }
}



