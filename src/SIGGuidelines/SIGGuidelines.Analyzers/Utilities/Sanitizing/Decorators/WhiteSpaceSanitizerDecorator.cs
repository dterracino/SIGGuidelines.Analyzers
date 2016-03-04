namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    public class WhiteSpaceSanitizerDecorator : RegexCodeSanitizerDecorator
    {
        public WhiteSpaceSanitizerDecorator(CodeSanitizerBase parent) : base(@"^\s+$[\r\n]*", true,parent) { }

    }
}
