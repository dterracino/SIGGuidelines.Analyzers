# SIGGuidelines.Analyzers

NEWS Oct 1. 2016: This project will soon be transfered to a [BetterCodeHub.com](https://bettercodehub.com) Community repository. Pull Requests are welcome.

## CI

[![Build status](https://ci.appveyor.com/api/projects/status/f260wpgmyiva86it?svg=true)](https://ci.appveyor.com/project/p3pijn/sigguidelines-analyzers)
[![BCH compliancy](http://bettercodehub.com/edge/badge/p3pijn/SIGGuidelines.Analyzers)](https://bettercodehub.com)


## A set of diagnostic analyzers inspired by the book: Building Maintainable Software
`SIGGuidelines.Analyzers` is a Visual Studio 2015 extension that provides on-the-fly feedback to developers on violations in `C#` code to a subset of the guidelines presented in the book [Building Maintainable Software](http://shop.oreilly.com/product/0636920049159.do) by Joost Visser and other consultants from the [Software Improvement Group](https://www.sig.eu). 

## Installation
To install SIGGuidelines.Analyzers package to your project, run the following command in the Package Manager Console:

`Install-Package SIGGuidelines.Analyzers`

##Prerequisites
- For on-the-fly feedback in the IDE: Visual Studio 2015+.
- For use in quality checks during builds: MSBuild 14+.
- For controlling compliance in SonarQube: SonarQube 5.3+.

## Implemented guidelines

The following guidelines from the book are implemented as diagnostic analyzers:

- Write short units of code: limit the length of methods and constructors. (<= 15 LOC)
- Write simple units of code: limit the number of branch points per method. (<= 4 branch points)
- Keep unit interfaces small by extracting parameters into objects. (<= 4 parameters)
- Separate concerns to avoid building large classes. (<= 400 LOC)

An overview of all 10 guidelines described in the book can be found at https://www.sig.eu/nl/building-maintainable-software.
Please refer to the book for an extensive explanation of each guideline.

## Preview
This set of analyzers allows you to control compliance of C# code to the above guidelines in the IDE, on the build server or with Code Quality Management systems that support diagnostic analyzers (like for example [SonarQube](http://www.sonarqube.org/)).

The following screenshots display the use of these analyzers in Visual Studio 2015:
![Visual Studio 2015](https://github.com/p3pijn/SIGGuidelines.Analyzers/raw/master/IDE.png "Visual Studio 2015")

The compiler generated warnings when the guidelines are violated:
![Visual Studio 2015](https://github.com/p3pijn/SIGGuidelines.Analyzers/raw/master/Screenshot1.PNG "Visual Studio 2015")

The severity of the warnings generated by diagnostic analyzers can be configured in a ruleset file:
![Visual Studio 2015](https://github.com/p3pijn/SIGGuidelines.Analyzers/raw/master/Screenshot2.PNG "Visual Studio 2015")

## About this project
I feel that the book Building Maintainable Software provides a clear and well-argued set of guidelines on how to write highly maintainable code. Especially valuable is the fact that the presented guidelines are so clear and simple they can be understood by developers of various experience levels and even by less-technical stakeholders. 

After reading the book Building Maintainable Software, I decided to investigate support for the presented guidelines in currently available free code quality tools. For the `Java` language, existing free tools (for example [CheckStyle](http://checkstyle.sourceforge.net/checks.html)) provide good support for the unit guidelines presented in the book. For the `C#` language however, I felt that the currently available free tools provide little support for the presented unit guidelines or provide poor feedback. Hence, I decided to create an example implementation of 3 unit guidelines and 1 module guideline from the book as diagnostic analyzers. 

I felt the other guidelines are either well supported by other tools, or less suitable to implement as diagnostic analyzer (mostly because their abstraction level is at the component level or the system level). The diagnostic analyzers presented in this repository can (and should) be used complementary to other code quality tools to aid developers in writing maintainable code. 

I can recommend [SonarLint for Visual Studio](http://www.sonarlint.org/visualstudio/) for a set of analyzers that find many code smells in C# code.

## Disclaimer
The diagnostic analyzers in this repository are just my personal interpretation of the guidelines presented in the book. Feel free to use and adjust these analyzers or source code as you like. Feedback is of course appreciated, you can reach me at [@p3pijn](https://twitter.com/p3pijn).



