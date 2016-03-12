using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using SIGGuidelines.Utilities;
using SIGGuidelines.Utilities.Sanitizing;
using System.Linq;

namespace SIGGuidelines.Metrics
{
    public class UnitSizeMetric
    {
        public UnitSizeMetric(SyntaxNode unitNode)
        {
            string rawCode = ExtractRawCode(unitNode);
            var sanitizedCode = new Sanitizer().Sanitize(rawCode);

            LineCounter counter = new LineCounter(sanitizedCode);
            this.Size = counter.NumberOfLines;
        }

        private static string ExtractRawCode(SyntaxNode node)
        {
            if (HasBody(node))
            {
                return ContentsOf<BlockSyntax>(node);
            }
            else if (HasExpressionBody(node))
            {
                return ContentsOf<ArrowExpressionClauseSyntax>(node);
            }
            else if (HasAccessorList(node))
            {
                return ContentsOf<AccessorListSyntax>(node);
            }

            return string.Empty;
        }

        private static bool HasBody(SyntaxNode node) => node.ChildNodes().OfType<BlockSyntax>().Any();

        private static bool HasExpressionBody(SyntaxNode node) => node.ChildNodes().OfType<ArrowExpressionClauseSyntax>().Any();

        private static bool HasAccessorList(SyntaxNode node) => node.ChildNodes().OfType<AccessorListSyntax>().Any();

        private static string ContentsOf<T>(SyntaxNode node)
            where T : SyntaxNode => node.ChildNodes().OfType<T>().First().GetText().ToString();

        public long Size
        {
            get;
            private set;
        }
    }
}
