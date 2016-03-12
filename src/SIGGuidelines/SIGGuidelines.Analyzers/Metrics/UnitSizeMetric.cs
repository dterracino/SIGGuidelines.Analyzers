using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using SIGGuidelines.Utilities;
using SIGGuidelines.Utilities.Sanitizing;

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
            string rawCode = null;

            // long if-chain due to poor abstraction of Body and ExpressionBody properties in the Roslyn public API
            if (node is MethodDeclarationSyntax)
            {
                rawCode = (node as MethodDeclarationSyntax).Body?.GetText().ToString() ?? (node as MethodDeclarationSyntax).ExpressionBody?.GetText().ToString();
            }
            else if (node is BasePropertyDeclarationSyntax)
            {
                rawCode = (node as BasePropertyDeclarationSyntax).AccessorList?.GetText().ToString();
            }
            else if (node is ConstructorDeclarationSyntax)
            {
                rawCode = (node as ConstructorDeclarationSyntax).Body?.GetText().ToString();
            }
            else if (node is OperatorDeclarationSyntax)
            {
                rawCode = (node as OperatorDeclarationSyntax).Body?.GetText().ToString() ?? (node as OperatorDeclarationSyntax).ExpressionBody?.GetText().ToString();
            }
            else
            {
                throw new NotSupportedException(string.Format("Unit size could not be determined of node with kind '{0}'", node.RawKind));
            }

            if (string.IsNullOrEmpty(rawCode))
            {
                return string.Empty;
            }

            return rawCode;
        }

        public long Size
        {
            get;
            private set;
        }
    }
}
