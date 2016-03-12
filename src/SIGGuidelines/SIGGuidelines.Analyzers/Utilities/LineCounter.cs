namespace SIGGuidelines.Utilities
{
    public class LineCounter
    {
        public LineCounter(string text)
        {
            this.NumberOfLines = CountLines(text);
        }

        public long NumberOfLines { get; private set; }

        private static long CountLines(string code)
        {
            code = code.Trim();
            long count = 1;
            int position = -1;

            while ((position = code.IndexOf('\n', position + 1)) != -1)
            {
                count++;
            }

            return count;
        }
    }
}
