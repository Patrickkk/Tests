using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAsCommandLine.Model
{
    public class CommandParameter
    {
        public string Name { get; set; } = "";

        public int Position { get; set; }

        public Type Type { get; set; }

        public object Short { get; internal set; }
    }
}