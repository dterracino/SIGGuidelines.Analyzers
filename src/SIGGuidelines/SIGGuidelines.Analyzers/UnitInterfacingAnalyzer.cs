using Microsoft.CodeAnalysis.Diagnostics;
using System;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SIGGuidelines.Metrics;

namespace SIGGuidelines.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UnitInterfacingAnalyzer : DiagnosticAnalyzer
    {
        internal const int MaxParameters = 4;
        internal const string DiagnosticId = "SIG4";
        internal const string Description = "SIG4 - Keep unit interfaces small by extracting parameters into objects";
        internal const string MessageFormat = "SIG4 - Unit has an interface of {0} parameters where {1} parameters are allowed";
        internal const string Category = "SIG";
        internal const bool IsActivatedByDefault = true;

        internal static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(DiagnosticId, Description, MessageFormat, Category,
                DiagnosticSeverity.Warning, IsActivatedByDefault);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction((c) => AnalyzeSymbol(c, (c.Node as MethodDeclarationSyntax).ParameterList), SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction((c) => AnalyzeSymbol(c, (c.Node as IndexerDeclarationSyntax).ParameterList), SyntaxKind.IndexerDeclaration);
            context.RegisterSyntaxNodeAction((c) => AnalyzeSymbol(c, (c.Node as ConstructorDeclarationSyntax).ParameterList), SyntaxKind.ConstructorDeclaration);
        }

        private void AnalyzeSymbol(SyntaxNodeAnalysisContext context, SyntaxNode parametersyntax)
        {
            UnitInterfacingMetric metric = new UnitInterfacingMetric(context.Node);

            if (metric.Parameters > MaxParameters)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, parametersyntax.GetLocation(), metric.Parameters, MaxParameters));
            }
        }
    }
}