using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SIGGuidelines.Metrics;
using System.Collections.Immutable;

namespace SIGGuidelines.Analyzers.DiagnosticAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UnitComplexityAnalyzer : DiagnosticAnalyzer
    {
        internal const int MaxBranchPoints = 4;
        internal const string DiagnosticId = "SIG2";
        internal const string Description = "SIG2 - Write simple units of code: limit the number of branch points per method";
        internal const string MessageFormat = "SIG2 - Unit has {0} branchpoint(s) where {1} branchpoint(s) are allowed";
        internal const string Category = "SIG";
        internal const bool IsActivatedByDefault = true;

        internal static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(DiagnosticId, Description, MessageFormat, Category,
                DiagnosticSeverity.Warning, IsActivatedByDefault);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction((c) => this.AnalyzeSymbol(c, (c.Node as MethodDeclarationSyntax).Identifier), SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction((c) => this.AnalyzeSymbol(c, (c.Node as PropertyDeclarationSyntax).Identifier), SyntaxKind.PropertyDeclaration);
            context.RegisterSyntaxNodeAction((c) => this.AnalyzeSymbol(c, (c.Node as ConstructorDeclarationSyntax).Identifier), SyntaxKind.ConstructorDeclaration);
            context.RegisterSyntaxNodeAction((c) => this.AnalyzeSymbol(c, (c.Node as IndexerDeclarationSyntax).ThisKeyword), SyntaxKind.IndexerDeclaration);
            context.RegisterSyntaxNodeAction((c) => this.AnalyzeSymbol(c, (c.Node as OperatorDeclarationSyntax).OperatorToken), SyntaxKind.OperatorDeclaration);
        }

        private void AnalyzeSymbol(SyntaxNodeAnalysisContext context, SyntaxToken identifier)
        {
            CyclomaticComplexityMetric metric = new CyclomaticComplexityMetric(context.Node);
            int branchpoints = metric.Complexity - 1;
            if (branchpoints > MaxBranchPoints)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, identifier.GetLocation(), branchpoints, MaxBranchPoints));
            }
        }
    }
}
