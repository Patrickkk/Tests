using System;

namespace FileEtl.Core
{
    public class EtlStepconfiguration
    {
        public Type StepType { get; set; }

        public object Config { get; set; }
    }
}