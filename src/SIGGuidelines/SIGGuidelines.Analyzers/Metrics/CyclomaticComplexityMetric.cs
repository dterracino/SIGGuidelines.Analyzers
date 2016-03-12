using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace SIGGuidelines.Metrics
{
    public class CyclomaticComplexityMetric
    {
        public CyclomaticComplexityMetric(SyntaxNode node)
        {
            this.Complexity = CalculateComplexity(node);
        }

        private static readonly SyntaxKind[] ComplexityIncreasingBinaryExpressionKinds =
        {
            SyntaxKind.LogicalAndExpression,
            SyntaxKind.LogicalOrExpression,
            SyntaxKind.CoalesceExpression
        };

        private static int CalculateComplexity(SyntaxNode methodDeclaration)
        {
            var complexityNodes = methodDeclaration.DescendantNodes().Where(node => IsComplexityIncreasingNode(node));
            int complexity = 1 + complexityNodes.Count();
            return complexity;
        }

        public int Complexity
        {
            get;
            private set;
        }

        private static bool IsComplexityIncreasingNode(SyntaxNode node) =>
            node is IfStatementSyntax ||
            node is ForEachStatementSyntax ||
            node is CatchClauseSyntax ||
            node is ForStatementSyntax ||
            node is ConditionalExpressionSyntax ||
            node is WhileStatementSyntax ||
            node is DoStatementSyntax ||
            node is GotoStatementSyntax ||
            IsSwitchCaseStatement(node) ||
            IsBinaryExpression(node);

        private static bool IsSwitchCaseStatement(SyntaxNode node) =>
            node is SwitchSectionSyntax && !(node as SwitchSectionSyntax).Labels.Any(l => l.Kind() == SyntaxKind.DefaultSwitchLabel);

        private static bool IsBinaryExpression(SyntaxNode node) => ComplexityIncreasingBinaryExpressionKinds.Contains(node.Kind());
    }
}
