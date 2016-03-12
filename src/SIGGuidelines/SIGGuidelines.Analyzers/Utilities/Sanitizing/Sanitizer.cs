using SIGGuidelines.Utilities.Sanitizing.Decorators;

namespace SIGGuidelines.Utilities.Sanitizing
{
    public class Sanitizer
    {
        public string Sanitize(string rawCode)
        {
            var sanitizer = AsCSharpSanitizer(new RawCode(rawCode));
            string sanitizedCode = sanitizer.Sanitize();
            return sanitizedCode;
        }

        private static CodeSanitizerBase AsCSharpSanitizer(CodeSanitizerBase sanitizer)
        {
            return new WhiteSpaceSanitizerDecorator(new CompilerDirectiveSanitizerDecorator(new UsingSanitizerDecorator(new CStyleCommentSanitizerDecorator(sanitizer))));
        }
    }
}
