using System.Collections.ObjectModel;
using System.Windows.Input;
using Caliburn.PresentationFramework.Commands;
using FileEtl.Console;

namespace FileEtl.Designer
{
    public class MainWindowViewModel
    {
        public ObservableCollection<IEtlStep> EtlSteps { get; set; }

        public void AppendEtlStep()
        {
        }
    }
}