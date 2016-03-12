using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SIGGuidelines.Metrics;
using FluentAssertions;

namespace SIGGuidelines.Analyzers.Tests.Metrics
{
    [TestClass]
    public class UnitInterfacingTest
    {
        [TestMethod]
        public void ParameterTest()
        {
            ParameterValueOf(MethodWithSignature("void m()")).Should().Be(0);
            ParameterValueOf(MethodWithSignature("Dictionairy<int,int> m()")).Should().Be(0);
            ParameterValueOf(MethodWithSignature("void m(int a)")).Should().Be(1);
            ParameterValueOf(MethodWithSignature("void m(int a, boolean b)")).Should().Be(2);
            ParameterValueOf(MethodWithSignature("void m(int a, boolean b = false)")).Should().Be(2);
            ParameterValueOf(MethodWithSignature("void m<T>()")).Should().Be(0);
            ParameterValueOf(MethodWithSignature("void m([NotNull] String a)")).Should().Be(1);
            ParameterValueOf(MethodWithSignature("void m(params int[] a)")).Should().Be(1);
        }

        private static int ParameterValueOf(SyntaxNode method) => new UnitInterfacingMetric(method).Parameters;

        private static SyntaxNode MethodWithSignature(string signature)
        {
            var tree = CSharpSyntaxTree.ParseText(string.Concat("class t{ ", signature, "{} }"), new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.None, SourceCodeKind.Regular));
            var method = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            return method;
        }
    }
}
