using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGGuidelines.Metrics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SIGGuidelines.Analyzers.Tests.Metrics
{
    [TestClass]
    public class CyclomaticComplexityTest
    {
        [TestMethod]
        public void IfStatementTest()
        {
            ComplexityOf(MethodWithBody("if(true){}")).Should().Be(2);
        }

        [TestMethod]
        public void IfElseStatementTest()
        {
            ComplexityOf(MethodWithBody("if(true){}else{}")).Should().Be(2);
            ComplexityOf(MethodWithBody("if(true && false){}else{}")).Should().Be(3);
            ComplexityOf(MethodWithBody("if(true){}else if(true){}else{}")).Should().Be(3);
        }

        [TestMethod]
        public void InfixOrStatementTest()
        {
            ComplexityOf(MethodWithBody("var a = true || false;")).Should().Be(2);
            ComplexityOf(MethodWithBody("return true || false;")).Should().Be(2);
        }

        [TestMethod]
        public void InfixAndStatementTest()
        {
            ComplexityOf(MethodWithBody("var a = true && true;")).Should().Be(2);
            ComplexityOf(MethodWithBody("return true && true;")).Should().Be(2);
        }

        [TestMethod]
        public void ForStatementTest()
        {
            ComplexityOf(MethodWithBody("for(;;){}")).Should().Be(2);
        }

        [TestMethod]
        public void ForeachStatementTest()
        {
            ComplexityOf(MethodWithBody("foreach(var a in b){}")).Should().Be(2);
        }

        [TestMethod]
        public void GotoStatementTest()
        {
            ComplexityOf(MethodWithBody("goto a; a: return;")).Should().Be(2);
        }

        [TestMethod]
        public void ConditionalStatementTest()
        {
            ComplexityOf(MethodWithBody("var a = true ? 1 : 2;")).Should().Be(2);
        }

        [TestMethod]
        public void NullCoalescingStatementTest()
        {
            ComplexityOf(MethodWithBody("var a = b ?? c;")).Should().Be(2);
        }

        [TestMethod]
        public void WhileStatementTest()
        {
            ComplexityOf(MethodWithBody("while(true){}")).Should().Be(2);
        }

        [TestMethod]
        public void DoStatementTest()
        {
            ComplexityOf(MethodWithBody("do{}while(true);")).Should().Be(2);
        }

        [TestMethod]
        public void CatchStatementTest()
        {
            ComplexityOf(MethodWithBody("try{}catch{}")).Should().Be(2);
        }


        [TestMethod]
        public void SwitchStatementTest()
        {
            ComplexityOf(MethodWithBody("switch(1){case 1: break;}")).Should().Be(2);
            ComplexityOf(MethodWithBody("switch(1){case 1: case 2: break;}")).Should().Be(2);
            ComplexityOf(MethodWithBody("switch(1){case 1: break; case 2: break;}")).Should().Be(3);
            ComplexityOf(MethodWithBody("switch(1){case 1: break; case 2: break; default: break;}")).Should().Be(3);
        }

        private static int ComplexityOf(SyntaxNode method) => new CyclomaticComplexityMetric(method).Complexity;
        
        private static SyntaxNode MethodWithBody(String source)
        {
            var tree = CSharpSyntaxTree.ParseText(String.Concat("class t{ void m(){ " ,source, "} }"), new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.None, SourceCodeKind.Regular));
            var method = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            return method;
        }
    }
}
