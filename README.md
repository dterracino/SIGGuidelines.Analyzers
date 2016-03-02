# WritingMaintainableSoftware-Analyzers
This repository contains an example implementation of diagnostic rules for Visual Studio 2015 ([Roslyn analyzers](https://msdn.microsoft.com/en-us/magazine/dn879356.aspx)) inspired by the book [Writing Maintainable Software](http://shop.oreilly.com/product/0636920049159.do) by Joost Visser et al.

The following guidelines from the book are implemented as diagnostic rules:

- Write short units of code: limit the length of methods and constructors.
- Write simple units of code: limit the number of branch points per method.
- Keep unit interfaces small by extracting parameters into objects.
- Separate concerns to avoid building large classes.
