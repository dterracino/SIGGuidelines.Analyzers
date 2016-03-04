namespace SIGGuidelines.Utilities.Sanitizing.Decorators
{
    public class UsingSanitizerDecorator : RegexCodeSanitizerDecorator
    {
        public UsingSanitizerDecorator(CodeSanitizerBase parent) : base(@"^(using) [A-Za-z\.]*;[\r\n]*", true,parent) { }
    }
}
