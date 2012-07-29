syntaxtame
==========

Provides server-side C# syntax highlighting.

-------------------------
Whenever you want to insert a piece of code in your web site you wish there was a decent C# syntax highlighter.

Due to the complexity of C# language client-side syntax highlighters (based on Javascript) cannot provide a fast
and slick job, at best there is always a flicker after loading the page when javascript tries to replace the code
there with a colorful one. 

"Syntax tame" is a tool which tames this complexity and allows you to do it fast and flicker free on the server side. 

All you need to say is:

```c#
    var html = new Highlighter.Code(code).Highlighted;
```

and receive your syntax-highlighted "code" in "html" format. Or if you prefer verbosity:

```c#
    var text = "using System; /* .... */";
    var myCode = new Highlighter.Code(text);
    var html = myCode.Highlighted;
```

There is a css file provided (default-syntax-highlighting.css) which makes your code look like Visual Studio but 
obviously you can change it and have your own colour set.

## How does it work?

Internally syntaxtame uses [NRefactory](http://wiki.sharpdevelop.net/NRefactory.ashx) to parse C# code. It means 
it is one step further than a simple lexer that only recognises keywords and string literals.

## Limitations

* Only C# language is supported (sorry VB fellows)
* Your code should be _correct_ as much as possible (Not compilable necessarily). Code snippets are acceptable.
* Calling static methods on types (like File.Exists() method) does not recognise the class name as "type" automatically. In practice something like that needs introduction of references and providing all "using" lines. Something that you don't want to do in a simple code snippet (Come on! even Visual Studio doesn't recognise them as types until you add references and usings)

