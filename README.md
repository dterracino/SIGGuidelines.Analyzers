# SIGGuidelines.Analyzers
## A set of diagnostic analyzers inspired by the book: Building Maintainable Software
`SIGGuidelines.Analyzers` is a Visual Studio 2015 extension that provides on-the-fly feedback to developers on violations in `C#` code to a subset of the guidelines presented in the book [Building Maintainable Software](http://shop.oreilly.com/product/0636920049159.do) by Joost Visser and other consultants from the [Software Improvement Group](https://www.sig.eu). 


## Installation
To install SIGGuidelines.Analyzers package to your project, run the following command in the Package Manager Console:

`Install-Package SIGGuidelines.Analyzers`

## Implemented guidelines

The following guidelines from the book are implemented as diagnostic analyzers:

- Write short units of code: limit the length of methods and constructors. (<= 15 LOC)
- Write simple units of code: limit the number of branch points per method. (<= 4 branch points)
- Keep unit interfaces small by extracting parameters into objects. (<= 4 parameters)
- Separate concerns to avoid building large classes. (<= 400 LOC)

An overview of all 10 guidelines described in the book can be found at https://www.sig.eu/nl/building-maintainable-software.
Please refer to the book for an extensive explanation of each guideline.

## About this project
After reading the book Building Maintainable Software, I decided to investigate support for the presented guidelines in currently available free code quality tools. For the `Java` language, existing free tools (like f.e. [CheckStyle](http://checkstyle.sourceforge.net/checks.html)) provide good support for the unit guidelines presented in the book. For the `C#` language however, I felt that the currently available free tooling provides little support for the presented unit guidelines. Hence, I decided to create an example implementation of 3 unit guidelines and 1 module guideline from the book. I felt the other guidelines are either well supported by other tools, or less suitable to implement as diagnostic analyzer (mostly because their abstraction level is at the component level or the system level). The diagnostic analyzers presented in this repository can (and should) be used complementairy to other code quality tools to aid developers in writing maintainable code.

##Prerequisites
You build your C# code with MSBuild 14 (or higher) with for example Visual Studio 2015 or the SonarQube Scanner.

## Preview

This set of analyzers allows you to control compliance of C# code to these guidelines in the IDE, on the build server or with Code Quality Management systems that support diagnostic analyzers (like f.e. [SonarQube](http://www.sonarqube.org/)).

The following screenshots display the use of these analyzers in Visual Studio 2015:
![Visual Studio 2015](https://github.com/p3pijn/SIGGuidelines.Analyzers/raw/master/Screenshot1.PNG "Visual Studio 2015")

The diagnostic analyzers can be maintained as rulesets. Violations may be configured to lead to compiler warnings, errors or informationals.
![Visual Studio 2015](https://github.com/p3pijn/SIGGuidelines.Analyzers/raw/master/Screenshot2.PNG "Visual Studio 2015")



