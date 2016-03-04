using System;
using System.Text.RegularExpressions;

namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    public class CStyleCommentSanitizerDecorator : CodeSanitizerDecoratorBase
    {

        public CStyleCommentSanitizerDecorator(CodeSanitizerBase parent) : base(parent)
        {

        }

        public override string Sanitize(string input)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"(?<![\\])(//)(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";

            string noComments = Regex.Replace(input,
                blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                me =>
                {
                    if (me.Value.StartsWith("//"))
                        return Environment.NewLine;
                    else if (me.Value.StartsWith("/*"))
                        return String.Empty;
                    else
                        return me.Value;

                },
                RegexOptions.Singleline);

            return noComments;
        }
    }
}
