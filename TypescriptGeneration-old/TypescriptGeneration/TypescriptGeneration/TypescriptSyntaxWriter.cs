namespace TypescriptGeneration
{
    class TypescriptSyntaxWriter
    {
        private IndentedStringBuilder builder = new IndentedStringBuilder();
        private bool IsFirstWriteOnThisLine = true;

        public void IncreaseIndent(int amount = 1)
        {
            builder.IncreaseIndent(amount);
        }

        public void DecreaseIndent(int amount = 1)
        {
            builder.DecreaseIndent();
        }

        public void WriteOpeningBracket()
        {
            WriteLine("{");
            IncreaseIndent();
        }

        public void WriteTypeAnnotation(string type)
        {
            Write(": ");
            Write(type);
        }

        internal void WriteSpace()
        {
            Write(" ");
        }

        internal void WriteLine(string code)
        {
            var indentRequired = this.IsFirstWriteOnThisLine && string.IsNullOrEmpty(code) == false;
            builder.WriteLine(code, indentRequired);
            IsFirstWriteOnThisLine = true;
        }

        internal void WriteLine()
        {
            WriteLine("");
        }

        internal void Write(string code)
        {
            builder.Write(code, this.IsFirstWriteOnThisLine);
            this.IsFirstWriteOnThisLine = false;
        }

        internal void WriteClosingBracket()
        {
            DecreaseIndent();
            WriteLine("}");
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
