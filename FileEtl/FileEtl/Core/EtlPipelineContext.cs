using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FileEtl.Core
{
    public class EtlPipelineContext
    {
        public IEtlStep LastRunStep { get; set; }

        public IReadOnlyList<IEtlStep> EtlSteps { get; private set; }

        public ImmutableDictionary<Type, object> CurrentData { get; private set; }

        public EtlPipelineContext(IReadOnlyList<IEtlStep> etlSteps, ImmutableDictionary<Type, object> currentData, IEtlStep lastRunStep)
        {
            this.EtlSteps = etlSteps;
            this.CurrentData = currentData;
            this.LastRunStep = lastRunStep;
        }
    }
}