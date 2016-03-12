using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SIGGuidelines.Metrics;

namespace SIGGuidelines.Analyzers.DiagnosticAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UnitSizeAnalyzer : DiagnosticAnalyzer
    {
        internal const int MaxUnitSize = 15;
        internal const string DiagnosticId = "SIG1";
        internal const string Description = "SIG1 - Write short units of code: limit the length of methods and constructors";
        internal const string MessageFormat = "SIG1 - Unit has a length of {0} LOC where {1} LOC is allowed";
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
            UnitSizeMetric metric = new UnitSizeMetric(context.Node);

            if (metric.Size > MaxUnitSize)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, identifier.GetLocation(), metric.Size, MaxUnitSize));
            }
        }
    }

}
