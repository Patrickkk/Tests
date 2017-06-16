using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FileEtl.Core;

namespace FileEtl.Designer
{
    public class MainWindowViewModel
    {
        public ObservableCollection<IEtlStep> EtlSteps { get; set; } = new ObservableCollection<IEtlStep>();

        public ObservableCollection<EtlStepSignature> AvailableSteps { get; set; } = new ObservableCollection<EtlStepSignature> { };

        public MainWindowViewModel()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).EtlSteps().ToArray();
            this.AvailableSteps.AddRange(types.SelectEtlStepSignature());
        }

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

    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> x, IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                x.Add(item);
            }
        }
    }
}