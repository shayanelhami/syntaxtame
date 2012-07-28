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
            var code = @"// my comment
    int a =1;
    float y = 2.5;
    string x1 = String.Empty;
    Int32 u = 3;
    var inside = Outside;        
    X X = new X(""string<b>"", 12);
    Outside = 13;
";

            var htmlTemplate = @"
<html>
<style>
 pre { background: #ddd; padding: 2px; }
 code .keyword { color: darkBlue; }
 code .type { color: #2B91AF; }
 code .comment { color: #008000; }
 code .string { color: #A31515; }
</style>
<body>
This is the code:<br />
<pre><code>[#CODE_GOES_HERE#]</code></pre>
</body>
</html>
";
            var highlighted = new Highlighter.Code(code).Highlighted;
            var html = htmlTemplate.Replace("[#CODE_GOES_HERE#]", highlighted);

            File.WriteAllText(@"C:\Test\index.html", html);
            Console.WriteLine("Done, open 'index.html' in borwser");
        }
    }
}
