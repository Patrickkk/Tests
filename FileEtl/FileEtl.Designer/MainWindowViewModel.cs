using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FileEtl.Core;
using PropertyChanged;

namespace FileEtl.Designer
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel //: INotifyPropertyChanged
    {
        public string StepsFilter { get; set; } = "";

        public ObservableCollection<IEtlStep> EtlSteps { get; set; } = new ObservableCollection<IEtlStep>();

        public EtlStepSignature SelectedAvailableSteps { get; set; }

        [DependsOn(nameof(StepsFilter), nameof(EtlSteps))]
        public IEnumerable<string> AvailableSteps { get { return AllEtlSteps.Where(x => x.Type.FullName.ToLowerInvariant().Contains(StepsFilter.ToLowerInvariant())).Select(x => x.ToString()); } }

        public ObservableCollection<EtlStepSignature> AllEtlSteps { get; set; } = new ObservableCollection<EtlStepSignature> { };

        public MainWindowViewModel()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).EtlSteps().ToArray();
            this.AllEtlSteps.AddRange(types.SelectEtlStepSignature());
        }

        public void AddStep()
        {
            var a = SelectedAvailableSteps;
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