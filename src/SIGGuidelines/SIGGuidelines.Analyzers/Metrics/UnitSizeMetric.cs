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

        public long Size
        {
            get;
            private set;
        }

        private static string ExtractRawCode(SyntaxNode node)
        {
            if (HasChildOfType<BlockSyntax>(node))
            {
                return ContentsOf<BlockSyntax>(node);
            }
            else if (HasChildOfType<ArrowExpressionClauseSyntax>(node))
            {
                return ContentsOf<ArrowExpressionClauseSyntax>(node);
            }
            else if (HasChildOfType<AccessorListSyntax>(node))
            {
                return ContentsOf<AccessorListSyntax>(node);
            }

            return string.Empty;
        }

        private static bool HasChildOfType<T>(SyntaxNode node)
            where T : SyntaxNode => node.ChildNodes().OfType<T>().Any();

        private static string ContentsOf<T>(SyntaxNode node)
            where T : SyntaxNode => node.ChildNodes().OfType<T>().First().GetText().ToString();
    }
}
