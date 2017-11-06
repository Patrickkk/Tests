using System.Windows;
using ReactiveUI;
using System.Reactive.Linq;

namespace FileEtl.ReactiveUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainViewModel>
    {
        public MainWindow()
        {
            ViewModel = new MainViewModel();
            InitializeComponent();
            DataContext = ViewModel;

            //this.BindCommand(this.ViewModel, x => x.AddEtlStepCommand, view => view.AddEtlStepButton);
            //this.BindCommand(this.ViewModel, x => x.AddEtlStepCommand, view => view.AddEtlStepButton);
            //this.BindCommand(this.ViewModel, x => x.AddEtlStepCommand, view => view.AddEtlStepButton);

            //this.WhenAnyValue(x => x.SelectedEtlStepJson)
            //    .InvokeCommand(ViewModel.UpdateConfigFromJsonCommand);

            this.WhenAnyValue(x => x.ConfiguredEtlSteps.SelectedItem)
                .Where(x => x != null)
                .InvokeCommand(ViewModel.SelectedConfiguredEtlStepChanged);
        }

        public MainViewModel ViewModel { get; set; }

        MainViewModel IViewFor<MainViewModel>.ViewModel { get { return ViewModel; } set { ViewModel = (MainViewModel)value; } }

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = (MainViewModel)value; } }
    }
}