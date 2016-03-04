namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    class CompilerDirectiveSanitizerDecorator : RegexCodeSanitizerDecorator
    {
        public CompilerDirectiveSanitizerDecorator(CodeSanitizerBase parent) : base(@"^\s*\#(.*?)$[\r\n]*", true, parent) { }
    }
}
