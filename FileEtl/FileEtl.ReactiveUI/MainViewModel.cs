using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Controls;
using FileEtl.Core;
using ReactiveUI;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PropertyChanged;
using System.IO;

namespace FileEtl.ReactiveUI
{
    [AddINotifyPropertyChangedInterface]
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

    [AddINotifyPropertyChangedInterface]
    public class MainViewModel : ReactiveObject
    {
        private string _etlStepsFilterText;

        private ObservableAsPropertyHelper<List<EtlStepSignature>> _SearchResults;

        private string _selectedEtlStepJson;

        private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            //TypeNameHandling = TypeNameHandling,
            Formatting = Formatting.Indented,
        };

        public MainViewModel()
        {
            this.UpdateConfigFromJsonCommand = ReactiveCommand.Create<string>(UpdateConfigurationFromJson);

            this.WhenAnyValue(x => x.SelectedEtlStepJson)
                .InvokeCommand(UpdateConfigFromJsonCommand);

            this.SelectedConfiguredEtlStepChanged = ReactiveCommand.Create<ConfiguredEtlStep>(ConfiguredEtlStepChangedEvent);

            this.AddEtlStepCommand = ReactiveCommand.Create(AddSelectedEtlStep);
            this.SaveCommand = ReactiveCommand.Create(SaveConfig);
            this.LoadCommand = ReactiveCommand.Create(LoadConfig);

            this.FilterAvailableEtlStepsCommand = ReactiveCommand.CreateFromTask<string, List<EtlStepSignature>>(filterText => GetAvailableEtlSteps(filterText));

            this.WhenAnyValue(x => x.EtlStepsFilterText)
                .Throttle(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler)
                .Select(x => x?.Trim())
                .DistinctUntilChanged()
                .InvokeCommand(FilterAvailableEtlStepsCommand);

            _SearchResults = FilterAvailableEtlStepsCommand.ToProperty(this, x => x.FilteredEtlSteps, GetAllEtlStepSignatures());
        }

        public ReactiveCommand AddEtlStepCommand { get; set; }

        public ObservableCollection<ConfiguredEtlStep> ConfiguredEtlSteps { get; set; } = new ObservableCollection<ConfiguredEtlStep>();

        public string EtlStepsFilterText
        {
            get { return _etlStepsFilterText; }
            set { this.RaiseAndSetIfChanged(ref _etlStepsFilterText, value); }
        }

        public IObserver<EventPattern<SelectionChangedEventArgs>> EtlStepsSlectionChanged { get; internal set; }

        public ReactiveCommand<string, List<EtlStepSignature>> FilterAvailableEtlStepsCommand { get; }

        public List<EtlStepSignature> FilteredEtlSteps => _SearchResults.Value;

        public ReactiveCommand LoadCommand { get; set; }

        public ReactiveCommand SaveCommand { get; set; }

        public EtlStepSignature SelectedAvailableEtlStep { get; set; }

        public ConfiguredEtlStep SelectedConfiguredEtlStep { get; set; }

        public ReactiveCommand<ConfiguredEtlStep, Unit> SelectedConfiguredEtlStepChanged { get; set; }

        public string SelectedEtlStepJson
        {
            get { return _selectedEtlStepJson; }
            set { this.RaiseAndSetIfChanged(ref _selectedEtlStepJson, value); }
        }

        public ReactiveCommand<string, Unit> UpdateConfigFromJsonCommand { get; }

        private static List<EtlStepSignature> GetAllEtlStepSignatures()
        {
            return AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .SelectMany(x => x.GetTypes())
                                 .EtlSteps()
                                 .ToList()
                                 .SelectEtlStepSignature()
                                 .ToList();
        }

        private void AddSelectedEtlStep()
        {
            if (SelectedAvailableEtlStep != null)
            {
                var etlStepType = SelectedAvailableEtlStep.Type;
                var newStep = new ConfiguredEtlStep { StepType = etlStepType, Config = etlStepType.GetNewIConfigurableConfigurationObject() };
                ConfiguredEtlSteps.Add(newStep);
            }
        }

        private void ConfiguredEtlStepChangedEvent(ConfiguredEtlStep arg)
        {
            this.SelectedEtlStepJson = JsonConvert.SerializeObject(arg.Config, jsonSerializerSettings);
        }

        private Task<List<EtlStepSignature>> GetAvailableEtlSteps(string filterText)
        {
            return Task.Run(() =>
            {
                return GetAllEtlStepSignatures()
                    .Where(x => filterText == null || x.TypeName.ToLower().Contains(filterText.ToLower()))
                    .ToList();
            });
        }

        private void LoadConfig()
        {
            if (File.Exists("Etl.json"))
            {
                var data = File.ReadAllText("Etl.json");
                var etlSteps = JsonConvert.DeserializeObject<List<ConfiguredEtlStep>>(data, jsonSerializerSettings);
            }
        }

        private void SaveConfig()
        {
            if (File.Exists("Etl.json"))
            {
                File.Delete("Etl.json");
            }

            File.WriteAllText("Etl.json", JsonConvert.SerializeObject(ConfiguredEtlSteps.ToList(), jsonSerializerSettings));
        }

        private void UpdateConfigurationFromJson(string json)
        {
            try
            {
                this.SelectedConfiguredEtlStep.Config = JsonConvert.DeserializeObject(json, this.SelectedConfiguredEtlStep.StepType.GetIConfigurableConfigurationType(), jsonSerializerSettings);
            }
            catch (Exception e)
            {
                var a = e;
            }
        }
    }
}