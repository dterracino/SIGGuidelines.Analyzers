using Microsoft.CodeAnalysis.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SIGGuidelines.Utilities.Sanitizing;
using SIGGuidelines.Utilities;

namespace SIGGuidelines.Analyzers.DiagnosticAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ModuleSizeAnalyzer : DiagnosticAnalyzer
    {
        internal const int MaxModuleSize = 400;
        internal const string DiagnosticId = "SIG5";
        internal const string Description = "SIG5 - Separate concerns to avoid building large classes";
        internal const string MessageFormat = "SIG5 - Module has a size of {0} LOC where {1} LOC is allowed";
        internal const string Category = "SIG";
        internal const bool IsActivatedByDefault = true;

        internal static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(DiagnosticId, Description, MessageFormat, Category,
                DiagnosticSeverity.Warning, IsActivatedByDefault);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxTreeAction(AnalyzeSymbol);

        private static void AnalyzeSymbol(SyntaxTreeAnalysisContext context)
        {
            var classes = context.Tree.GetRoot().DescendantNodes().OfType<BaseTypeDeclarationSyntax>();

            foreach (var @class in classes)
            {
                string text = @class.GetText().ToString();
                string sanitizedText = new Sanitizer().Sanitize(text);
                long moduleSize = new LineCounter(sanitizedText).NumberOfLines;

                if (moduleSize > MaxModuleSize)
                {
                    var identifier = @class.Identifier;
                    context.ReportDiagnostic(Diagnostic.Create(Rule, identifier.GetLocation(), moduleSize, MaxModuleSize));
                }
            }
        }
    }

}
