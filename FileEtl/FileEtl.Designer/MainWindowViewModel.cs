using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
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

        public ICommand Command { get { return new XCommand(); } }
    }

    internal class XCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}