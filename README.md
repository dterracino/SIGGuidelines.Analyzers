# SIGGuidelines.Analyzers
## A set of diagnostic analyzers inspired by the book: Building Maintainable Software
This repository contains an example implementation of diagnostic analyzers for Visual Studio 2015 ([Roslyn analyzers](https://msdn.microsoft.com/en-us/magazine/dn879356.aspx)) inspired by the book [Building Maintainable Software](http://shop.oreilly.com/product/0636920049159.do) by Joost Visser and his colleagues from the [Software Improvement Group](https://www.sig.eu).

The following guidelines from the book are implemented as diagnostic analyzers:

- Write short units of code: limit the length of methods and constructors. (<= 15 LOC)
- Write simple units of code: limit the number of branch points per method. (<= 4 branch points)
- Keep unit interfaces small by extracting parameters into objects. (<= 4 parameters)
- Separate concerns to avoid building large classes. (<= 400 LOC)

This set of analyzers allows you to control compliance of C# code to these guidelines in the IDE, on the build server or other Code Quality Management systems that support diagnostic analyzers.

The following screenshots display the use of these analyzers in Visual Studio 2015:
![Visual Studio 2015](https://github.com/p3pijn/WritingMaintainableSoftware-Analyzers/raw/master/Screenshot1.PNG "Visual Studio 2015")

The diagnostic analyzers can be maintained as rulesets. Violations may be configured to lead to compiler warnings, errors or informationals.
![Visual Studio 2015](https://github.com/p3pijn/WritingMaintainableSoftware-Analyzers/raw/master/Screenshot2.PNG "Visual Studio 2015")

## NuGet Install
To install SIGGuidelines.Analyzers, run the following command in the Package Manager Console:

`Install-Package SIGGuidelines.Analyzers`

