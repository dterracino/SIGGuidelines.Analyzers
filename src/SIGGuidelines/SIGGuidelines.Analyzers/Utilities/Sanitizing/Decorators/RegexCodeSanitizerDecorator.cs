using System;
using System.Text.RegularExpressions;

namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    public abstract class RegexCodeSanitizerDecorator : CodeSanitizerDecoratorBase
    {
        private readonly Regex expression;
        private readonly bool isMultiline;

        public RegexCodeSanitizerDecorator(string pattern, bool isMultiline, CodeSanitizerBase parentCodeSanitizer)
            : base(parentCodeSanitizer)
        {
            var mode = isMultiline ? RegexOptions.Multiline : RegexOptions.Singleline;
            this.expression = new Regex(pattern, mode);
            this.isMultiline = isMultiline;
        }

        public override string Sanitize(string input)
        {
            var replaceValue = this.isMultiline ? string.Empty : Environment.NewLine;
            string output = this.expression.Replace(input, replaceValue);
            return output;
        }
    }
}
