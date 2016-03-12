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
            InnerSizeOf(MethodWithBody("int a = 1;")).Should().Be(1);

            InnerSizeOf(
                MethodWithBody(
                    @"int a = 1;
                      int b = 2;"))
                .Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithWhitespace()
        {
            InnerSizeOf(MethodWithBody(string.Empty)).Should().Be(0);
            InnerSizeOf(MethodWithBody(" ")).Should().Be(0);
            InnerSizeOf(MethodWithBody("\n")).Should().Be(0);
            InnerSizeOf(MethodWithBody("\t")).Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        "))
                .Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        int a = 1;

                        int b = 2;
                    "))
                .Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithLineComments()
        {
            InnerSizeOf(MethodWithBody("//comment")).Should().Be(0);
            InnerSizeOf(MethodWithBody("int a = 1;//comment")).Should().Be(1);
            InnerSizeOf(MethodWithBody("//int a = 1;")).Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        //comment
                        "))
                .Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        int a = 1;
                        //comment
                        int b = 2;
                    "))
                .Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithBlockComments()
        {
            InnerSizeOf(MethodWithBody("/*comment*/")).Should().Be(0);
            InnerSizeOf(MethodWithBody("int a = 1;/*comment*/")).Should().Be(1);
            InnerSizeOf(MethodWithBody("/*comment*/int a = 1;")).Should().Be(1);
            InnerSizeOf(MethodWithBody("/*int a = 1;*/")).Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        /* comment */
                        "))
                .Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        int a = 1;
                        /* comment */
                        int b = 2;
                    "))
                .Should().Be(2);

            InnerSizeOf(
                MethodWithBody(
                    @"

                        /* 
                        comment 
                        */

                    "))
                .Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        int a = 1;
                        /* 
                        comment 
                        */
                        int b = 2;
                    "))
                .Should().Be(2);
        }

        [TestMethod]
        public void SizeOfUnitWithRegions()
        {
            InnerSizeOf(MethodWithBody("#region a")).Should().Be(0);
            InnerSizeOf(MethodWithBody("#endregion")).Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        #region a

                        #endregion
                        "))
                .Should().Be(0);

            InnerSizeOf(
                MethodWithBody(
                    @"
                        #region a
                        int a = 1;
                        //comment
                        int b = 2;
                        #endregion
                    "))
                .Should().Be(2);
        }

        [TestMethod]
        public void SizeOfProperty()
        {
            SizeOf(Member(@"
                int m
                {
                    get{ }
                    set{ }
                }
                ")).Should().Be(4);
        }

        [TestMethod]
        public void SizeOfIndexer()
        {
            SizeOf(Member(@"
                int this[int m]
                {
                    get{ }
                    set{ }
                }
                ")).Should().Be(4);
        }

        [TestMethod]
        public void SizeOfOperatorDeclaration()
        {
            SizeOf(Member(@"
                public static int operator z(int a, int b)
                {
                }
                ")).Should().Be(2);
        }

        [TestMethod]
        public void SizeOfAbstractMethod()
        {
            SizeOf(Member("public abstract int m();")).Should().Be(1);
        }

        [TestMethod]
        public void SizeOfDestructor()
        {
            SizeOf(Member(@"
                public ~t()
                {
                }
                ")).Should().Be(2);
        }

        [TestMethod]
        public void SizeOfConstructor()
        {
            SizeOf(Member(@"
                public t()
                {
                }
                ")).Should().Be(2);
        }

        private static long InnerSizeOf(SyntaxNode method) => SizeOf(method) - 2; // -2 for added opening and closing brackets ({ and }).

        private static long SizeOf(SyntaxNode method) => new UnitSizeMetric(method).Size; // -2 for added opening and closing brackets ({ and }).

        private static SyntaxNode MethodWithBody(string body)
        {
            var tree = CSharpSyntaxTree.ParseText(string.Concat("class t{ void m(){", Environment.NewLine, body, Environment.NewLine, "} }"), new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.None, SourceCodeKind.Regular));
            var method = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            return method;
        }

        private static SyntaxNode Member(string body)
        {
            var tree = CSharpSyntaxTree.ParseText(string.Concat("class t{ ", Environment.NewLine, body, Environment.NewLine, " }"), new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.None, SourceCodeKind.Regular));
            var method = tree.GetRoot().DescendantNodes().OfType<MemberDeclarationSyntax>().Skip(1).First();
            return method;
        }
    }
}
