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
        public static CommandRunnerBuilder ForType<T>()
        {
            return new CommandRunnerBuilder().ForType<T>();
        }
    }
}