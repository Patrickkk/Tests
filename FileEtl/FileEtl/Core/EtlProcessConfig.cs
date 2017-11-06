using System.Collections.ObjectModel;

namespace FileEtl.Core
{
    public class EtlProcessConfig
    {
        private ObservableCollection<ConfiguredEtlStep> EtlSteps { get; set; } = new ObservableCollection<ConfiguredEtlStep>();
    }
}