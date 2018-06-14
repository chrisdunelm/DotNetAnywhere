# mini-demo-dna

mini-demo-dna is the smallest possible C code that is able to execute the simplest
possible .NET executable.

In less than 250 lines of C it is able to load an executable built using the
standard Microsoft C# compiler. It is only able to execute an empty Main() method,
but demonstrates that creating your own .NET runtime may not be as complex as you
imagine! There probably isn't any situation where you would actually *want* to
create your own  .NET runtime, but it's fun to experiment :)

This code was live-coded from (almost) nothing at [NDC-Oslo](https://ndcoslo.com/)
2018 in exactly one hour at the talk:
[So you want to create your own .NET runtime](https://ndcoslo.com/talk/so-you-want-to-create-your-own-net-runtime/).

The code for the .NET runtime itself is all in
[mini-demo-dna/main.c](https://github.com/chrisdunelm/DotNetAnywhere/blob/master/mini-demo-dna/mini-demo-dna/main.c)

The video will be available shortly...

All information required to understand/extend/improve this code is in the .NET
specification which is freely available:
[EMCA-335](https://www.ecma-international.org/publications/standards/Ecma-335.htm)

This code is purely for demonstration, and is entirely unsuitable for any real use.

Enjoy!
