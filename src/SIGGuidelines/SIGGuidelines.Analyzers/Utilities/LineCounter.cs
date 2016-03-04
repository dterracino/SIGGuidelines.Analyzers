using System;

namespace SIGGuidelines.Utilities
{
    public class LineCounter
    {
        public LineCounter(String Text)
        {
            NumberOfLines = countLines(Text);
        }

        public long NumberOfLines { get; private set; }

        private static long countLines(String Code)
        {
            Code = Code.Trim();
            long count = 1;
            int position = -1;
            while ((position = Code.IndexOf('\n', position + 1)) != -1)
            {
                count++;
            }
            return count;
        }
    }
}
