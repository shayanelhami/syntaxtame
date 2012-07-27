using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var highlighted = Highlighter.Code.GetHighlighted(@"
// your code goes here
");

            File.WriteAllText(".\\index.html", highlighted);
            Console.WriteLine("Done, open 'index.html' in borwser");
        }
    }
}
