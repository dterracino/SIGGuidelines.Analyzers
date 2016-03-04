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
            Complexity = calculateComplexity(node);
        }

        private static readonly SyntaxKind[] ComplexityIncreasingBinaryExpressionKinds =
        {
            SyntaxKind.LogicalAndExpression,
            SyntaxKind.LogicalOrExpression,
            SyntaxKind.CoalesceExpression
        };

        private static int calculateComplexity(SyntaxNode MethodDeclaration)
        {
            var complexityNodes = MethodDeclaration.DescendantNodes().Where(node => IsComplexityIncreasingNode(node));
            int complexity = 1 + complexityNodes.Count();
            return complexity;
        }


        public int Complexity
        {
            get;
            private set;
        }

        private static Boolean IsComplexityIncreasingNode(SyntaxNode node) =>
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
            
        private static Boolean IsSwitchCaseStatement(SyntaxNode node) => 
            (node is SwitchSectionSyntax && !(node as SwitchSectionSyntax).Labels.Any(l => l.Kind() == SyntaxKind.DefaultSwitchLabel));

        private static bool IsBinaryExpression(SyntaxNode node) => ComplexityIncreasingBinaryExpressionKinds.Contains(node.Kind());

    }
}
