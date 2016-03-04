using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGGuidelines.Utilities.Sanitizing
{
    public abstract class CodeSanitizerDecoratorBase : CodeSanitizerBase
    {
        public CodeSanitizerDecoratorBase(CodeSanitizerBase parentSanitizer)
        {
            this.parentSanitizer = parentSanitizer;
        }

        private CodeSanitizerBase parentSanitizer;

        public override string getRawCode()
        {
            return parentSanitizer.getRawCode();
        }

        public override string getSanitizedCode()
        {
            String code = parentSanitizer.getSanitizedCode();
            String sanitizedCode =  Sanitize(code);
            return sanitizedCode;
        }

        public abstract String Sanitize(String input);

    }
}
