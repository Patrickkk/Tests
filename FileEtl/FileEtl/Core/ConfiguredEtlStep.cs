using System;
using SoftFluent.Windows;

namespace FileEtl.Core
{
    public class ConfiguredEtlStep
    {
        public Type StepType { get; set; }

        [PropertyGridOptions(EditorDataTemplateResourceKey = "ObjectEditor", ForcePropertyChanged = true)]
        public object Config { get; set; }
    }
}