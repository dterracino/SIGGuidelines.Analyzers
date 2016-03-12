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

        private static int CalculateComplexity(SyntaxNode unitNode)
        {
            var visitor = new CyclomaticComplexityVisitor();
            visitor.Visit(unitNode);
            return visitor.Complexity;
        }

        public int Complexity
        {
            get;
            private set;
        }

        private class CyclomaticComplexityVisitor : CSharpSyntaxWalker
        {
            private int complexity = 1;

            public int Complexity
            {
                get
                {
                    return complexity;
                }
            }

            private void IncrementComplexity()
            {
                complexity++;
            }

            private void IncrementComplexity(Action visit)
            {
                IncrementComplexity();
                visit();
            }

            public override void VisitIfStatement(IfStatementSyntax node) => IncrementComplexity(() => base.VisitIfStatement(node));

            public override void VisitForEachStatement(ForEachStatementSyntax node) => IncrementComplexity(() => base.VisitForEachStatement(node));

            public override void VisitCatchClause(CatchClauseSyntax node) => IncrementComplexity(() => base.VisitCatchClause(node));

            public override void VisitForStatement(ForStatementSyntax node) => IncrementComplexity(() => base.VisitForStatement(node));

            public override void VisitConditionalExpression(ConditionalExpressionSyntax node) => IncrementComplexity(() => base.VisitConditionalExpression(node));

            public override void VisitWhileStatement(WhileStatementSyntax node) => IncrementComplexity(() => base.VisitWhileStatement(node));

            public override void VisitDoStatement(DoStatementSyntax node) => IncrementComplexity(() => base.VisitDoStatement(node));

            public override void VisitGotoStatement(GotoStatementSyntax node) => IncrementComplexity(() => base.VisitGotoStatement(node));

            public override void VisitSwitchSection(SwitchSectionSyntax node)
            {
                if (IsSwitchCaseStatement(node))
                {
                    IncrementComplexity();
                }

                base.VisitSwitchSection(node);
            }

            public override void VisitBinaryExpression(BinaryExpressionSyntax node)
            {
                if (IsComplexityIncreasingBinaryExpression(node))
                {
                    IncrementComplexity();
                }

                base.VisitBinaryExpression(node);
            }

            private static bool IsSwitchCaseStatement(SwitchSectionSyntax node) => !node.Labels.Any(l => l.Kind() == SyntaxKind.DefaultSwitchLabel);

            private static bool IsComplexityIncreasingBinaryExpression(SyntaxNode node) => ComplexityIncreasingBinaryExpressionKinds.Contains(node.Kind());

            private static readonly SyntaxKind[] ComplexityIncreasingBinaryExpressionKinds =
            {
                SyntaxKind.LogicalAndExpression,
                SyntaxKind.LogicalOrExpression,
                SyntaxKind.CoalesceExpression
            };
        }
    }
}
