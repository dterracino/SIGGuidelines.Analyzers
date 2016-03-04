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
            String rawCode = extractRawCode(unitNode);
            var sanitizedCode = new Sanitizer().Sanitize(rawCode);

            LineCounter counter = new LineCounter(sanitizedCode);
            Size = counter.NumberOfLines;
        }

        private static String extractRawCode(SyntaxNode Node)
        {
            String rawCode = null;

            //long if-chain due to poor abstraction of Body and ExpressionBody properties in the Roslyn public API
            if (Node is MethodDeclarationSyntax)
            {
                rawCode = (Node as MethodDeclarationSyntax).Body?.GetText().ToString() ?? (Node as MethodDeclarationSyntax).ExpressionBody?.GetText().ToString();
            }
            else if (Node is BasePropertyDeclarationSyntax)
            {
                rawCode = (Node as BasePropertyDeclarationSyntax).AccessorList?.GetText().ToString();
            }
            else if (Node is ConstructorDeclarationSyntax)
            {
                rawCode = (Node as ConstructorDeclarationSyntax).Body?.GetText().ToString();
            }
            else if (Node is OperatorDeclarationSyntax)
            {
                rawCode = (Node as OperatorDeclarationSyntax).Body?.GetText().ToString() ?? (Node as OperatorDeclarationSyntax).ExpressionBody?.GetText().ToString();
            }
            else
            {
                throw new NotSupportedException(String.Format("Unit size could not be determined of node with kind '{0}'", Node.RawKind));
            }

            if (String.IsNullOrEmpty(rawCode))
                return string.Empty;

            return rawCode;
        }

        public long Size
        {
            get;
            private set;
        }
    }
}
