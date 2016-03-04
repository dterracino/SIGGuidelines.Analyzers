using System;
using System.Text.RegularExpressions;

namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    public abstract class RegexCodeSanitizerDecorator : CodeSanitizerDecoratorBase
    {
        public readonly String regex;
        public readonly Boolean isMultiline;
        public RegexCodeSanitizerDecorator(String regex, Boolean isMultiline, CodeSanitizerBase parentCodeSanitizer) : base(parentCodeSanitizer)
        {
            this.regex = regex;
            this.isMultiline = isMultiline;
        }

        public override string Sanitize(string input)
        {
            var mode = isMultiline ? RegexOptions.Multiline : RegexOptions.Singleline;
            var returnString = isMultiline ? String.Empty : Environment.NewLine;

            String output = Regex.Replace(input, regex, returnString, mode);

            return output;
        }
    }
}
