using System;
using SIGGuidelines.Utilities.Sanitizing.Decorators;

namespace SIGGuidelines.Utilities.Sanitizing
{
    public class Sanitizer
    {

        public String Sanitize(String rawCode)
        {
            CodeSanitizerBase code = new RawCode(rawCode);

            code = new CStyleCommentSanitizerDecorator(code);
            code = new UsingSanitizerDecorator(code);
            code = new CompilerDirectiveSanitizerDecorator(code);
            code = new WhiteSpaceSanitizerDecorator(code);
            String sanitizedCode = code.getSanitizedCode();

            return sanitizedCode;
        }
    }
}
