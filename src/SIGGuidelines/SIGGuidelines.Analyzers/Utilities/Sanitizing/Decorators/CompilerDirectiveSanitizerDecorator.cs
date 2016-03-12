namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    internal class CompilerDirectiveSanitizerDecorator : RegexCodeSanitizerDecorator
    {
        public CompilerDirectiveSanitizerDecorator(CodeSanitizerBase parent)
            : base(@"^\s*\#(.*?)$[\r\n]*", true, parent)
        {
        }
    }
}
