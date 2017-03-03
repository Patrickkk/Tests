using System.Collections.ObjectModel;
using FileEtl.Console;
using FileEtl.Console.Transformers;

namespace FileEtl.Designer
{
    public class MainWindowViewModel
    {
        public ObservableCollection<IEtlStep> EtlSteps { get; set; } = new ObservableCollection<IEtlStep> { new Decompression() };

        public void DeleteEtlStep()
        {
        }

        public void AppendEtlStepAbove()
        {
        }

        public void AppendEtlStepBelow(object args)
        {
        }

        public void AppendEtlStep()
        {
        }
    }
}