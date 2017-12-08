using System.Collections.Generic;

namespace CodeAsCommandLine.Model
{
    public class CommandClass
    {
        public string ClassName { get; set; } = "";

        public string ClassNameShort { get; set; } = "";

        public List<Command> Commands { get; set; } = new List<Command>();
    }
}