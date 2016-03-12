namespace SIGGuidelines.Utilities.Sanitizing
{
    public abstract class CodeSanitizerDecoratorBase : CodeSanitizerBase
    {
        public CodeSanitizerDecoratorBase(CodeSanitizerBase parentSanitizer)
        {
            this.parentSanitizer = parentSanitizer;
        }

        private readonly CodeSanitizerBase parentSanitizer;

        public override string Sanitize()
        {
            string code = this.parentSanitizer.Sanitize();
            string sanitizedCode = this.Sanitize(code);
            return sanitizedCode;
        }

        public abstract string Sanitize(string input);
    }
}
