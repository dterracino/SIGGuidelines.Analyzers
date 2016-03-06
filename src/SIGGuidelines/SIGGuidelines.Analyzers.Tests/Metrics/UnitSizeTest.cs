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
    public class UnitSizeTest
    {
        [TestMethod]
        public void SizeOfUnitWithCode()
        {
            SizeOf(MethodWithBody("int a = 1;")).Should().Be(1);

            SizeOf(
                MethodWithBody
                (
                    @"int a = 1;
                      int b = 2;"
                )
            ).Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithWhitespace()
        {
            SizeOf(MethodWithBody("")).Should().Be(0);
            SizeOf(MethodWithBody(" ")).Should().Be(0);
            SizeOf(MethodWithBody("\n")).Should().Be(0);
            SizeOf(MethodWithBody("\t")).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        "
                )
            ).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        int a = 1;

                        int b = 2;
                    "
                )
            ).Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithLineComments()
        {
            SizeOf(MethodWithBody("//comment")).Should().Be(0);
            SizeOf(MethodWithBody("int a = 1;//comment")).Should().Be(1);
            SizeOf(MethodWithBody("//int a = 1;")).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        //comment
                        "
                )
            ).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        int a = 1;
                        //comment
                        int b = 2;
                    "
                )
            ).Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithBlockComments()
        {
            SizeOf(MethodWithBody("/*comment*/")).Should().Be(0);
            SizeOf(MethodWithBody("int a = 1;/*comment*/")).Should().Be(1);
            SizeOf(MethodWithBody("/*comment*/int a = 1;")).Should().Be(1);
            SizeOf(MethodWithBody("/*int a = 1;*/")).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        /* comment */
                        "
                )
            ).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        int a = 1;
                        /* comment */
                        int b = 2;
                    "
                )
            ).Should().Be(2);

            SizeOf(
                MethodWithBody
                (
                @"

                        /* 
                        comment 
                        */

                    "
                )
            ).Should().Be(0);
        

            SizeOf(
                MethodWithBody
                (
                    @"
                        int a = 1;
                        /* 
                        comment 
                        */
                        int b = 2;
                    "
                )
            ).Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithRegions()
        {
            SizeOf(MethodWithBody("#region a")).Should().Be(0);
            SizeOf(MethodWithBody("#endregion")).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        #region a

                        #endregion
                        "
                )
            ).Should().Be(0);

            SizeOf(
                MethodWithBody
                (
                    @"
                        #region a
                        int a = 1;
                        //comment
                        int b = 2;
                        #endregion
                    "
                )
            ).Should().Be(2);
        }

        private static long SizeOf(SyntaxNode method) => new UnitSizeMetric(method).Size - 2; //- 2 for padding of method body block ({ }) size

        private static SyntaxNode MethodWithBody(String body)
        {
            var tree = CSharpSyntaxTree.ParseText(String.Concat("class t{ void m(){",Environment.NewLine, body,Environment.NewLine, "} }"), new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.None, SourceCodeKind.Regular));
            var method = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            return method;
        }
    }
}
