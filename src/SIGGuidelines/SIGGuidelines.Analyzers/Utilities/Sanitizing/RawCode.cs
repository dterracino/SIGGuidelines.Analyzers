namespace SIGGuidelines.Utilities.Sanitizing
{
    public class RawCode : CodeSanitizerBase
    {
        private readonly string code;

        public RawCode(string code)
        {
            this.code = code;
        }

        public override string Sanitize()
        {
            return this.code;
        }
    }
}
