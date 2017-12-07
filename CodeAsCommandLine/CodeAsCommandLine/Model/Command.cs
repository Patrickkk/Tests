using System.Collections.Generic;
using System.Reflection;

namespace CodeAsCommandLine.Model
{
    public class Command
    {
        public string ClassPrefix { get; set; }
        public string ClassShortPrefix { get; set; } = "";

        /// <summary>
        /// the command an all aliases that can be used.
        /// </summary>
        public string CommandName { get; set; } = "";

        public List<CommandParameter> CommandParameters { get; set; } = new List<CommandParameter>();

        public MethodInfo Method { get; set; }

        public string Short { get; set; } = "";

        public string HelpText { get; set; } = "";
    }
}