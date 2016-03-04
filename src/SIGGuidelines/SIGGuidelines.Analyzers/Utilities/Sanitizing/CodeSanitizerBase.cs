namespace SIGGuidelines.Utilities.Sanitizing
{
    public abstract class CodeSanitizerBase
    {

        public abstract string getRawCode();

        public abstract string getSanitizedCode();
    }
}