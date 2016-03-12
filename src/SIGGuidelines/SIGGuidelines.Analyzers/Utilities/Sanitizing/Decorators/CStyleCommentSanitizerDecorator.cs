using System;
using System.Text.RegularExpressions;

namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    public class CStyleCommentSanitizerDecorator : CodeSanitizerDecoratorBase
    {
        public CStyleCommentSanitizerDecorator(CodeSanitizerBase parent)
            : base(parent)
        {
        }

        private const string BlockComments = @"/\*(.*?)\*/";
        private const string LineComments = @"(?<![\\])(//)(.*?)\r?\n";
        private const string StringLiterals = @"""((\\[^\n]|[^""\n])*)""";
        private const string VerbatimStrings = @"@(""[^""]*"")+";

        private static readonly Regex CommentRegex = new Regex($"{BlockComments}|{LineComments}|{StringLiterals}|{VerbatimStrings}", RegexOptions.Singleline);

        public override string Sanitize(string input)
        {
            string inputWithoutComments = CommentRegex.Replace(input, (match) => ReplaceCommentMatchWith(match.Value));
            return inputWithoutComments;
        }

        private static string ReplaceCommentMatchWith(string matchedLine)
        {
            if (IsSingleLineComment(matchedLine))
            {
                return Environment.NewLine;
            }
            else if (IsBlockComment("/*"))
            {
                return string.Empty;
            }
            else if (IsLiteral(matchedLine))
            {
                return matchedLine;
            }
            else
            {
                throw new InvalidOperationException("The matched line was not expected to match the regular expression");
            }
        }

        private static bool IsLiteral(string line) => line.StartsWith("\"") || line.StartsWith("@\"");

        private static bool IsSingleLineComment(string line) => line.StartsWith("//");

        private static bool IsBlockComment(string line) => line.StartsWith("/*");
    }
}
