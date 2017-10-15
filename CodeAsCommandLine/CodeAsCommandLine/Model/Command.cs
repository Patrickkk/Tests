using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeAsCommandLine.Model
{
    public class Command
    {
        public string ClassPrefix { get; set; }

        /// <summary>
        /// the command an all aliases that can be used.
        /// </summary>
        public string CommandName { get; set; } = "";

        public IEnumerable<CommandParameter> CommandParameters { get; set; }

        public MethodInfo Method { get; set; }

        public string Short { get; internal set; }
    }
}