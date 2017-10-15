using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAsCommandLine
{
    /// <summary>
    /// Static methods to get started quickly.
    /// </summary>
    public class CodeConvert
    {
        public static CommandRunnerBuilder For<T>()
        {
            return new CommandRunnerBuilder().ForType<T>();
        }
    }
}