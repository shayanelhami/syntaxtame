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
            var code = @"
    using System.IO;

class _____ {

    private string Merge(ReadOnlyDocument doc, List<Insertion> insertions) 
    {
        // my comment
        X X = new X(""string"", 12);
        Outside = 13;
        var inside = Outside;

            File.WriteAllText(@""C:\Test\a.html"", html.Replace(""[#HERE#]"", merge));

    }
}
";

            var htmlTemplate = @"
<html>
<style>
 code .keyword { color: darkBlue; }
 code .type { color: #2B91AF; }
 code .comment { color: #008000; }
 code .string { color: #A31515; }
</style>
<body>
<pre>
<code>
[#CODE_GOES_HERE#]
</code>
</pre>
</body>
</html>
";
            var highlighted = Highlighter.Code.GetHighlighted(code);
            var html = htmlTemplate.Replace("[#CODE_GOES_HERE#]", highlighted);

            File.WriteAllText(@"C:\Test\index.html", html);
            Console.WriteLine("Done, open 'index.html' in borwser");
        }
    }
}
