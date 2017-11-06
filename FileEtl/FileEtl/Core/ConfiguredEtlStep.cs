using System;

namespace FileEtl.Core
{
    public class ConfiguredEtlStep
    {
        public Type StepType { get; set; }

        public object Config { get; set; }
    }
}