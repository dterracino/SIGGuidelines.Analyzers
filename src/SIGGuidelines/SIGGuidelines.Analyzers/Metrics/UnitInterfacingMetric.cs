using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SIGGuidelines.Metrics
{
    public class UnitInterfacingMetric
    {
        public UnitInterfacingMetric(SyntaxNode unitNode)
        {
            this.Parameters = CalculateParameters(unitNode);
        }

        private static int CalculateParameters(SyntaxNode unitNode)
        {
            if (unitNode is BaseMethodDeclarationSyntax)
            {
                return (unitNode as BaseMethodDeclarationSyntax).ParameterList.Parameters.Count;
            }
            else if (unitNode is IndexerDeclarationSyntax)
            {
                return (unitNode as IndexerDeclarationSyntax).ParameterList.Parameters.Count;
            }
            else
            {
                return 0;
            }
        }

        public int Parameters
        {
            get;
            private set;
        }
    }
}
