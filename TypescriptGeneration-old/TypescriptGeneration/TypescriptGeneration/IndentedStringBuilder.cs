using System.Text;

namespace TypescriptGeneration
{
    class IndentedStringBuilder
    {
        private string indentCharacters = "    ";
        private int indent = 0;
        private StringBuilder builder = new StringBuilder();

        public IndentedStringBuilder(string indentCharacters = "    ", int startIndent = 0)
        {
            this.indentCharacters = indentCharacters;
            this.indent = startIndent;
        }

        public void WriteLine(string text, bool withIndent = false)
        {
            if (withIndent)
            {
                WriteIndentWhitespace();
            }
            builder.AppendLine(text);
        }

        public void Write(string text, bool withIndent = false)
        {
            if(withIndent)
            {
                WriteIndentWhitespace();
            }
            builder.Append(text);
        }

        public void WriteIndentWhitespace()
        {
            for (int i = 0; i < indent; i++)
            {
                builder.Append(indentCharacters);
            }
        }

        public void IncreaseIndent(int amount = 1)
        {
            indent += amount;
        }

        public void DecreaseIndent(int amount = 1)
        {
            indent -= amount;
            if(indent < 0)
            {
                indent = 0;
            }
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
