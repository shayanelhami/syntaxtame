syntaxtame
==========

Provides server-side C# syntax highlighting.

-------------------------
Whenever you want to insert a piece of code in your web site you wish there was a decent C# syntax highlighter.

Due to the complexity of C# language client-side syntax highlighters (based on Javascript) cannot provide a fast
and slick job, at best there is always a flicker after loading the page when javascript tries to replace the code
there with a colorful one. 

"Syntax tame" is a tool which tames this complexity and allows you to do it fast and flicker free on the server side. 

All you need to say ia:

    var html = new Highlighter.Code(code).Highlighted;
    
and receive your syntax-highlighted "code" in "html" format.

There is a css file provided (default-syntax-highlighting.css) which makes your code look like Visual Studio but 
obviously you can change it and have your own colour set.