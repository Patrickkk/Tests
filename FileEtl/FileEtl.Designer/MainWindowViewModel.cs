using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using FileEtl.Core;
using Newtonsoft.Json;
using PropertyChanged;

namespace FileEtl.Designer
{
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

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).EtlSteps().ToArray();
            this.AllEtlSteps.AddRange(types.SelectEtlStepSignature());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EtlStepSignature> AllEtlSteps { get; set; } = new ObservableCollection<EtlStepSignature> { };

        [DependsOn(nameof(StepsFilter), nameof(EtlSteps))]
        public IEnumerable<string> AvailableSteps { get { return AllEtlSteps.Where(x => x.Type.FullName.ToLowerInvariant().Contains(StepsFilter.ToLowerInvariant())).Select(x => x.Type.FullName); } }

        public ICommand DeleteCommand { get { return new DeleteCommand(); } }
        public ObservableCollection<ConfiguredEtlStep> EtlSteps { get; set; } = new ObservableCollection<ConfiguredEtlStep>();
        public System.Collections.Immutable.ImmutableDictionary<Type, object> LastValue { get; private set; }
        public ObservableCollection<EtlPipelineContext> RunEtlStepResults { get; set; } = new ObservableCollection<EtlPipelineContext>();
        public string SelectedAvailableStep { get; set; }
        public ConfiguredEtlStep SelectedEtlStep { get; set; } = null;

        [DependsOn(nameof(SelectedEtlStep))]
        public string SelectedEtlStepJson
        {
            get
            {
                if (SelectedEtlStep != null)
                {
                    try
                    {
                        return JsonConvert.SerializeObject(SelectedEtlStep.Config, Formatting.Indented);
                    }
                    catch (Exception)
                    {
                    }
                }
                return "";
            }
            set
            {
                if (SelectedEtlStep != null)
                {
                    try
                    {
                        SelectedEtlStep.Config = JsonConvert.DeserializeObject(value, SelectedEtlStep.StepType.GetIConfigurableConfigurationType());
                    }
                    catch (Exception e)
                    {
                        var a = e;
                    }
                }
            }
        }

        public ConfiguredEtlStep SelectedRunEtlStepResult { get; set; } = null;

        [DependsOn(nameof(SelectedRunEtlStepResult))]
        public string SelectedRunEtlStepResultJson
        {
            get
            {
                if (SelectedEtlStep != null)
                {
                    try
                    {
                        return JsonConvert.SerializeObject(SelectedRunEtlStepResult, Formatting.Indented);
                    }
                    catch (Exception)
                    {
                    }
                }
                return "";
            }
            set
            {
            }
        }

        public string StepsFilter { get; set; } = "";

        public void AddStep()
        {
            var etlStepType = AllEtlSteps.Single(x => x.Type.FullName == SelectedAvailableStep).Type;
            var newStep = new ConfiguredEtlStep { StepType = etlStepType, Config = etlStepType.GetNewIConfigurableConfigurationObject() };
            EtlSteps.Add(newStep);
        }

        public void AppendEtlStep()
        {
        }

        public void AppendEtlStepAbove()
        {
        }

        public void AppendEtlStepBelow(object args)
        {
        }

        public void DeleteEtlStep()
        {
        }

        public void Preview()
        {
            RunEtlStepResults.Clear();

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).EtlSteps().ToList();
            var container = new SimpleInjector.Container();
            types.ForEach(x => container.Register(x));
            var pipeline = EtlProcessFactory.CreateEtlPipeline(container, this.EtlSteps.ToList());
            PipelineExecutor.RunPipeline(pipeline, context => { }, context => { RunEtlStepResults.Add(context); });
        }
    }

    internal class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object sender)
        {
            throw new NotImplementedException();
        }
    }
}