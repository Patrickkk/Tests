using System;
using System.Collections.Generic;
using FileEtl.Core;

namespace FileEtl.Preview
{
    public class RunEtlStep
    {
        public IEtlStep EtlStep { get; set; }

        public Dictionary<Type, object> ResultingValues { get; set; } = new Dictionary<Type, object>();
    }
}