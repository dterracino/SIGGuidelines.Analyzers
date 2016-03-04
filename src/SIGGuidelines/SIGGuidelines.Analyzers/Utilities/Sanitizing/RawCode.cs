using System;

namespace SIGGuidelines.Utilities.Sanitizing
{
    public class RawCode : CodeSanitizerBase
    {
        public String code;
        public RawCode(String code)
        {
            this.code = code;
        }

        public override string getRawCode()
        {
            return code;
        }

        public override string getSanitizedCode()
        {
            return getRawCode();
        }
    }
}
