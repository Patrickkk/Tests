using System;

namespace CodeAsCommandLine.Model
{
    public class CommandParameter
    {
        public string Name { get; set; } = "";

        public int Position { get; set; } = -1;

        public Type Type { get; set; }

        public string Short { get; set; } = "";

        public string HelpText { get; set; } = "";
    }
}